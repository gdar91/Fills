namespace Fills;

public static partial class FillsObservableExtensions
{
    public static IObservable<TResult> Select<TState, TElement, TResult>(
        this IObservable<TElement> source,
        TState state,
        Func<TState, TElement, TResult> selector
    )
    {
        return
            FillsObservable.Create(
                (source, state, selector),
                Cache<TState, TElement, TResult>.SelectSubscribe,
                Hint.Of<TResult>()
            );
    }
}
