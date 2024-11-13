namespace Fills;

public static class FillsArray
{
    public static TElement[] Return<TElement>(TElement element) => [element];


    public static TElement[] Empty<TElement>() => [];

    public static TElement[] Empty<TElement>(Hint<TElement> hint) => [];


    public static Hint<TElement[]> Hint<TElement>(Hint<TElement> hint) => default;

    public static Hint<TElement> UnHint<TElement>(Hint<TElement[]> hint) => default;
}
