namespace Fills;

public static class FillsEnumerable
{
    public static IEnumerable<TElement> Return<TElement>(TElement element)
    {
        yield return element;
    }


    public static IEnumerable<TElement> Empty<TElement>()
    {
        yield break;
    }

    public static IEnumerable<TElement> Empty<TElement>(Hint<TElement> hint)
    {
        yield break;
    }


    public static Hint<IEnumerable<TElement>> Hint<TElement>(Hint<TElement> hint) => default;

    public static Hint<TElement> UnHint<TElement>(Hint<IEnumerable<TElement>> hint) => default;
}
