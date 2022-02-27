namespace Fills;

public static partial class FillsObservableExtensions
{
    public static IObservable<TElement> Where<TState, TElement>(
        this IObservable<TElement> source,
        TState state,
        Func<TState, TElement, bool> predicate
    )
    {
        return
            FillsObservable.Create(
                (source, state, predicate),
                Cache<TState, TElement>.WhereSubscribe,
                Hint.Of<TElement>()
            );
    }
}
