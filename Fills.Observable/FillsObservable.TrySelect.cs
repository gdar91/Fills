namespace Fills;

public static partial class FillsObservableExtensions
{
    public static IObservable<TResult> TrySelect<TElement, TResult>(
        this IObservable<TElement> source,
        TrySelector<TElement, TResult> trySelector
    )
    {
        return new TrySelectObservable<TElement, TResult>(source, trySelector);
    }

    public static IObservable<TResult> TrySelect<TState, TElement, TResult>(
        this IObservable<TElement> source,
        TState state,
        TrySelector<TState, TElement, TResult> trySelector
    )
    {
        return new TrySelect<TState, TElement, TResult>(source, state, trySelector);
    }

    public static IObservable<TResult> TrySelect<TElement, TResult>(
        this IObservable<TElement> source,
        TrySelector<TElement, TResult> trySelector,
        Hint<TResult> resultHint
    )
    {
        return new TrySelectObservable<TElement, TResult>(source, trySelector);
    }

    public static IObservable<TResult> TrySelect<TState, TElement, TResult>(
        this IObservable<TElement> source,
        TState state,
        TrySelector<TState, TElement, TResult> trySelector,
        Hint<TResult> resultHint
    )
    {
        return new TrySelect<TState, TElement, TResult>(source, state, trySelector);
    }
}
