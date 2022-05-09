namespace Fills;

public static partial class FillsEnumerableExtensions
{
    public static IEnumerable<TResult> Select<TArg, TElement, TResult>(
        this IEnumerable<TElement> source,
        TArg arg,
        Func<TArg, TElement, TResult> selector
    )
    {
        foreach (var item in source)
        {
            yield return selector(arg, item);
        }
    }
}
