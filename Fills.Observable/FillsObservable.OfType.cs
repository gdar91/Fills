namespace Fills;

public static partial class FillsObservableExtensions
{
    public static IObservable<TResult> OfType<TElement, TIntermediateResult, TResult>(
        this IObservable<TElement> source,
        Func<TIntermediateResult, TResult> selector
    )
    {
        return
            source.TrySelect(
                selector,
                static (Func<TIntermediateResult, TResult> selector, TElement element, out TResult result) =>
                {
                    if (element is TIntermediateResult intermediateResult)
                    {
                        result = selector(intermediateResult);

                        return true;
                    }

                    result = default!;

                    return false;
                }
            );
    }


    public static IObservable<TResult> OfType<TElement, TIntermediateResult, TResult>(
        this IObservable<TElement> source,
        Hint<TIntermediateResult> hint,
        Func<TIntermediateResult, TResult> selector
    )
    {
        return OfType(source, selector);
    }


    public static IObservable<TResult> OfType<TState, TElement, TIntermediateResult, TResult>(
        this IObservable<TElement> source,
        TState state,
        Func<TState, TIntermediateResult, TResult> selector
    )
    {
        return
            source.TrySelect(
                (state, selector),
                static (
                    (TState state, Func<TState, TIntermediateResult, TResult> selector) tuple,
                    TElement element,
                    out TResult result
                ) =>
                {
                    if (element is TIntermediateResult intermediateResult)
                    {
                        result = tuple.selector(tuple.state, intermediateResult);

                        return true;
                    }

                    result = default!;

                    return false;
                }
            );
    }


    public static IObservable<TResult> OfType<TState, TElement, TIntermediateResult, TResult>(
        this IObservable<TElement> source,
        TState state,
        Hint<TIntermediateResult> hint,
        Func<TState, TIntermediateResult, TResult> selector
    )
    {
        return OfType(source, state, selector);
    }
}
