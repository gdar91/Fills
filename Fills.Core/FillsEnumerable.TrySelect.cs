namespace Fills;

public static partial class FillsEnumerableExtensions
{
    public static IEnumerable<TResult> TrySelect<TElement, TResult>(
        this IEnumerable<TElement> source,
        TrySelector<TElement, TResult> trySelector
    )
    {
        foreach (var item in source)
        {
            if (trySelector(item, out var result))
            {
                yield return result;
            }
        }
    }
    
    public static IEnumerable<TResult> TrySelect<TState, TElement, TResult>(
        this IEnumerable<TElement> source,
        TState state,
        TrySelector<TState, TElement, TResult> trySelector
    )
    {
        foreach (var item in source)
        {
            if (trySelector(state, item, out var result))
            {
                yield return result;
            }
        }
    }

    public static IEnumerable<TResult> TrySelect<TElement, TResult>(
        this IEnumerable<TElement> source,
        TrySelector<TElement, TResult> trySelector,
        Hint<TResult> resultHint
    )
    {
        foreach (var item in source)
        {
            if (trySelector(item, out var result))
            {
                yield return result;
            }
        }
    }
    
    public static IEnumerable<TResult> TrySelect<TState, TElement, TResult>(
        this IEnumerable<TElement> source,
        TState state,
        TrySelector<TState, TElement, TResult> trySelector,
        Hint<TResult> resultHint
    )
    {
        foreach (var item in source)
        {
            if (trySelector(state, item, out var result))
            {
                yield return result;
            }
        }
    }
}
