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
                FillsObservableExtensions.Cache<TElement>.FromAsyncEnumerableSubscribeAsync,
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
                FillsObservableExtensions.Cache<TState, TElement>.FromAsyncEnumerableSubscribeAsync,
                Fills.Hint.Of<TElement>()
            );
    }
}
