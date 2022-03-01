using System.Reactive.Disposables;

namespace Fills;

public static partial class FillsObservable
{
    public static IObservable<TElement> Defer<TState, TElement>(
        TState state,
        Func<TState, IObservable<TElement>> observableFactory
    )
    {
        return Create((state, observableFactory), DeferModule<TState, TElement>.Subscribe, Fills.Hint.Of<TElement>());
    }


    public static IObservable<TElement> Defer<TState, TElement>(
        TState state,
        Func<TState, CancellationToken, Task<IObservable<TElement>>> observableFactoryAsync
    )
    {
        return
            Create(
                (state, observableFactoryAsync),
                DeferModule<TState, TElement>.SubscribeAsync,
                Fills.Hint.Of<TElement>()
            );
    }


    private static class DeferModule<TState, TElement>
    {
        public static readonly
            Func<
                (TState state, Func<TState, IObservable<TElement>> observableFactory),
                IObserver<TElement>,
                IDisposable
            >
            Subscribe =
                static (tuple, observer) =>
                {
                    try
                    {
                        return tuple.observableFactory(tuple.state).Subscribe(observer);
                    }
                    catch (Exception exception)
                    {
                        observer.OnError(exception);

                        return Disposable.Empty;
                    }
                };

        public static readonly
            Func<
                (TState state, Func<TState, CancellationToken, Task<IObservable<TElement>>> observableFactoryAsync),
                IObserver<TElement>,
                CancellationToken,
                Task<IDisposable>
            >
            SubscribeAsync =
                static async (tuple, observer, cancellationToken) =>
                {
                    try
                    {
                        var observable =
                            await tuple
                                .observableFactoryAsync(tuple.state, cancellationToken)
                                .ConfigureAwait(false);

                        return observable.Subscribe(observer);
                    }
                    catch (Exception exception)
                    {
                        observer.OnError(exception);

                        return Disposable.Empty;
                    }
                };
    }
}
