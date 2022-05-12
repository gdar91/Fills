namespace Fills;

public static class FillsEnumerable
{
    public static IEnumerable<TElement> Return<TElement>(TElement element) => new[] { element };


    public static IEnumerable<TElement> Empty<TElement>() => Enumerable.Empty<TElement>();

    public static IEnumerable<TElement> Empty<TElement>(Hint<TElement> hint) => Enumerable.Empty<TElement>();


    public static Hint<IEnumerable<TElement>> Hint<TElement>(Hint<TElement> hint) => default;

    public static Hint<TElement> UnHint<TElement>(Hint<IEnumerable<TElement>> hint) => default;
}
