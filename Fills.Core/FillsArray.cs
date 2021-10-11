namespace Fills;

public static class FillsArray
{
    public static TElement[] Return<TElement>(TElement element) => new[] { element };


    public static TElement[] Empty<TElement>() => Array.Empty<TElement>();

    public static TElement[] Empty<TElement>(Hint<TElement> hint) => Array.Empty<TElement>();


    public static Hint<TElement[]> Hint<TElement>(Hint<TElement> hint) => default;

    public static Hint<TElement> Unhint<TElement>(Hint<TElement[]> hint) => default;
}
