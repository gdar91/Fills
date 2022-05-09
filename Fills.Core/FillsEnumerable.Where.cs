namespace Fills;

public static partial class FillsEnumerableExtensions
{
    public static IEnumerable<TElement> Where<TArg, TElement>(
        this IEnumerable<TElement> source,
        TArg arg,
        Func<TArg, TElement, bool> predicate
    )
    {
        foreach (var item in source)
        {
            if (predicate(arg, item))
            {
                yield return item;
            }
        }
    }
}
