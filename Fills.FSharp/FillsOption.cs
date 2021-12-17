using Microsoft.FSharp.Core;

namespace Fills;

public static class FillsOption
{
    public static FSharpOption<T> Return<T>(T item) => new(item);


    public static FSharpOption<T> Empty<T>() => FSharpOption<T>.None;

    public static FSharpOption<T> Empty<T>(Hint<T> hint) => FSharpOption<T>.None;


    public static Hint<FSharpOption<TElement>> Hint<TElement>(Hint<TElement> hint) => default;

    public static Hint<TElement> UnHint<TElement>(Hint<FSharpOption<TElement>> hint) => default;
}
