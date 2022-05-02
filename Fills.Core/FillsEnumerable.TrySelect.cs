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

    public static IEnumerable<TResult> TrySelect<TArg, TElement, TResult>(
        this IEnumerable<TElement> source,
        TArg arg,
        TrySelector<TArg, TElement, TResult> trySelector
    )
    {
        foreach (var item in source)
        {
            if (trySelector(arg, item, out var result))
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

    public static IEnumerable<TResult> TrySelect<TArg, TElement, TResult>(
        this IEnumerable<TElement> source,
        TArg arg,
        TrySelector<TArg, TElement, TResult> trySelector,
        Hint<TResult> resultHint
    )
    {
        foreach (var item in source)
        {
            if (trySelector(arg, item, out var result))
            {
                yield return result;
            }
        }
    }
}
