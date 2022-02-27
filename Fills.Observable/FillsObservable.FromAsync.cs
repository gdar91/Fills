namespace Fills;

public static partial class FillsObservable
{
    public static IObservable<TElement> FromAsync<TState, TElement>(
        TState state,
        Func<TState, CancellationToken, Task<TElement>> funcAsync
    )
    {
        return
            Create(
                (state, funcAsync),
                FillsObservableExtensions.Cache<TState, TElement>.FromAsyncSubscribeAsync,
                Fills.Hint.Of<TElement>()
            );
    }
}
