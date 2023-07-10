using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Fills;


internal readonly record struct StartWithAsyncArg<TArg, TElement, TAsyncValue, TResult>(
    IObservable<TElement> Source,
    TArg Arg,
    Func<TArg, CancellationToken, Task<TAsyncValue>> AsyncValueFunc,
    Func<TArg, TAsyncValue, IObservable<TElement>, IObservable<TResult>> ObservableSelector
);


public static partial class FillsObservableExtensions
{
    public static IObservable<TResult> StartWithAsync<TArg, TElement, TAsyncValue, TResult>(
        this IObservable<TElement> source,
        TArg arg,
        Func<TArg, CancellationToken, Task<TAsyncValue>> asyncValueFunc,
        Func<TArg, TAsyncValue, IObservable<TElement>, IObservable<TResult>> observableSelector
    )
    {
        return
            FillsObservable.Create<StartWithAsyncArg<TArg, TElement, TAsyncValue, TResult>, TResult>(
                new(source, arg, asyncValueFunc, observableSelector),
                StartWithAsyncSubscribe
            );
    }


    private static async Task<IDisposable> StartWithAsyncSubscribe<TArg, TElement, TAsyncValue, TResult>(
        StartWithAsyncArg<TArg, TElement, TAsyncValue, TResult> arg,
        IObserver<TResult> observer,
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
                var asyncValue = await arg.AsyncValueFunc(arg.Arg, cancellationToken).ConfigureAwait(false);

                var observable = arg.ObservableSelector(arg.Arg, asyncValue, connectableObservable);

                var subscription = observable.Subscribe(observer);

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
