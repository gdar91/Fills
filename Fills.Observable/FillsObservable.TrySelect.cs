namespace Fills;

public static partial class FillsObservableExtensions
{
    public static IObservable<TResult> TrySelect<TElement, TResult>(
        this IObservable<TElement> source,
        TrySelector<TElement, TResult> trySelector
    )
    {
        return
            FillsObservable.Create(
                (source, trySelector),
                Cache<TElement, TResult>.TrySelectSubscribe,
                Hint.Of<TResult>()
            );
    }

    public static IObservable<TResult> TrySelect<TState, TElement, TResult>(
        this IObservable<TElement> source,
        TState state,
        TrySelector<TState, TElement, TResult> trySelector
    )
    {
        return
            FillsObservable.Create(
                (source, state, trySelector),
                Cache<TState, TElement, TResult>.TrySelectSubscribe,
                Hint.Of<TResult>()
            );
    }

    public static IObservable<TResult> TrySelect<TElement, TResult>(
        this IObservable<TElement> source,
        TrySelector<TElement, TResult> trySelector,
        Hint<TResult> resultHint
    )
    {
        return
            FillsObservable.Create(
                (source, trySelector),
                Cache<TElement, TResult>.TrySelectSubscribe,
                Hint.Of<TResult>()
            );
    }

    public static IObservable<TResult> TrySelect<TState, TElement, TResult>(
        this IObservable<TElement> source,
        TState state,
        TrySelector<TState, TElement, TResult> trySelector,
        Hint<TResult> resultHint
    )
    {
        return
            FillsObservable.Create(
                (source, state, trySelector),
                Cache<TState, TElement, TResult>.TrySelectSubscribe,
                Hint.Of<TResult>()
            );
    }
}
