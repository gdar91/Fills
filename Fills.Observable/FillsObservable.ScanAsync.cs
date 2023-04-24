using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Fills;


internal sealed record ScanAsyncArgs<TArg, TElement, TAccumulate>(
    IObservable<TElement> Source,
    TArg Arg,
    Func<TArg, CancellationToken, Task<TAccumulate>> SeedTaskFunc,
    Func<TArg, TAccumulate, TElement, TAccumulate> AccumulatorFunc,
    Subject<long> Subject
);


public static partial class FillsObservableExtensions
{
    public static IObservable<TAccumulate> ScanAsync<TArg, TElement, TAccumulate>(
        this IObservable<TElement> source,
        TArg arg,
        Func<TArg, CancellationToken, Task<TAccumulate>> seedTaskFunc,
        Func<TArg, TAccumulate, TElement, TAccumulate> accumulatorFunc
    )
    {
        return
            Observable.Using(
                static () => new Subject<long>(),
                subject =>
                    FillsObservable.Create<ScanAsyncArgs<TArg, TElement, TAccumulate>, TAccumulate>(
                        new(source, arg, seedTaskFunc, accumulatorFunc, subject),
                        ScanAsyncSubscribe
                    )
            );
    }


    private static async Task<IDisposable> ScanAsyncSubscribe<TArg, TElement, TAccumulate>(
        ScanAsyncArgs<TArg, TElement, TAccumulate> arg,
        IObserver<TAccumulate> observer,
        CancellationToken cancellationToken
    )
    {
        TAccumulate seed = default!;

        var subscription =
            arg.Source
                .DelayElementsUntil(arg.Subject)
                .Publish(elements =>
                    elements
                        .Window(elements.Take(1), _ => elements.IgnoreElements())
                        .SelectMany(window =>
                            window
                                .Scan(
                                    (arg, accumulate: seed),
                                    static (accumulate, element) =>
                                    (
                                        accumulate.arg,
                                        accumulate.arg.AccumulatorFunc(
                                            accumulate.arg.Arg,
                                            accumulate.accumulate, element
                                        )
                                    )
                                )
                                .Select(arg => arg.accumulate)
                        )
                )
                .Subscribe(observer);

        try
        {
            seed = await arg.SeedTaskFunc(arg.Arg, cancellationToken).ConfigureAwait(false);

            arg.Subject.OnNext(default);

            return subscription;
        }
        catch
        {
            using var subscriptionResource = subscription;
            throw;
        }
    }
}
