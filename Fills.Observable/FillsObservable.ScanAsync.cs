using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Fills;


internal sealed record ScanAsyncArg<TArg, TElement, TAccumulate>(
    IObservable<TElement> Source,
    TArg Arg,
    Func<TArg, CancellationToken, Task<TAccumulate>> SeedTaskFunc,
    Func<TArg, TAccumulate, TElement, TAccumulate> AccumulatorFunc
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
            FillsObservable.Create(
                new ScanAsyncArg<TArg, TElement, TAccumulate>(source, arg, seedTaskFunc, accumulatorFunc),
                ScanAsyncSubscribe,
                Hint.Of<TAccumulate>()
            );
    }


    private static async Task<IDisposable> ScanAsyncSubscribe<TArg, TElement, TAccumulate>(
        ScanAsyncArg<TArg, TElement, TAccumulate> arg,
        IObserver<TAccumulate> observer,
        CancellationToken cancellationToken
    )
    {
        var subject = new Subject<long>();

        try
        {
            var connectableObservable = arg.Source.DelayElementsUntil(subject).Publish();

            var connection = connectableObservable.Connect();

            try
            {
                var seed = await arg.SeedTaskFunc(arg.Arg, cancellationToken).ConfigureAwait(false);

                var subscription =
                    connectableObservable
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
                        .Select(static arg => arg.accumulate)
                        .Subscribe(observer);

                try
                {
                    subject.OnNext(default);

                    return
                        Disposable.Create(
                            (subject, connection, subscription),
                            static arg =>
                            {
                                using var subjectResource = arg.subject;
                                using var connectionResource = arg.connection;
                                using var subscriptionResource = arg.subscription;
                            }
                        );
                }
                catch
                {
                    using var subscriptionResource = subscription;
                    throw;
                }
            }
            catch
            {
                using var connectionResource = connection;
                throw;
            }
        }
        catch
        {
            using var subjectResource = subject;
            throw;
        }
    }
}
