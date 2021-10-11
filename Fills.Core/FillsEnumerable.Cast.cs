namespace Fills;

public static partial class FillsEnumerableExtensions
{
    public static IEnumerable<TResult> Cast<TElement, TResult>(
        this IEnumerable<TElement> source,
        Hint<TResult> resultHint
    )
        where TResult : TElement
    {
        foreach (var item in source)
        {
            yield return (TResult) item!;
        }
    }
}
