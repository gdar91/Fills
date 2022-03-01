using System.Reactive.Disposables;

namespace Fills;

public static partial class FillsObservable
{
    public static IObservable<TElement> FromAsyncEnumerable<TElement>(
        Func<CancellationToken, IAsyncEnumerable<TElement>> asyncEnumerableFactory
    )
    {
        return
            Create(
                asyncEnumerableFactory,
                FromAsyncEnumerableModule<TElement>.SubscribeAsync,
                Fills.Hint.Of<TElement>()
            );
    }


    public static IObservable<TElement> FromAsyncEnumerable<TState, TElement>(
        TState state,
        Func<TState, CancellationToken, IAsyncEnumerable<TElement>> asyncEnumerableFactory
    )
    {
        return
            Create(
                (state, asyncEnumerableFactory),
                FromAsyncEnumerableModule<TState, TElement>.SubscribeAsync,
                Fills.Hint.Of<TElement>()
            );
    }


    private static class FromAsyncEnumerableModule<TElement>
    {
        public static readonly
            Func<
                Func<CancellationToken, IAsyncEnumerable<TElement>>,
                IObserver<TElement>,
                CancellationToken,
                Task<IDisposable>
            >
            SubscribeAsync =
                static async (asyncEnumerableFactory, observer, cancellationToken) =>
                {
                    var asyncEnumerable = asyncEnumerableFactory(cancellationToken);

                    await foreach (var item in asyncEnumerable.WithCancellation(cancellationToken))
                    {
                        observer.OnNext(item);
                    }

                    observer.OnCompleted();

                    return Disposable.Empty;
                };
    }

    private static class FromAsyncEnumerableModule<TState, TElement>
    {
        public static readonly
            Func<
                (TState state,  Func<TState, CancellationToken, IAsyncEnumerable<TElement>> asyncEnumerableFactory),
                IObserver<TElement>,
                CancellationToken,
                Task<IDisposable>
            >
            SubscribeAsync =
                static async (tuple, observer, cancellationToken) =>
                {
                    var asyncEnumerable = tuple.asyncEnumerableFactory(tuple.state, cancellationToken);

                    await foreach (var item in asyncEnumerable.WithCancellation(cancellationToken))
                    {
                        observer.OnNext(item);
                    }

                    observer.OnCompleted();

                    return Disposable.Empty;
                };
    }
}
