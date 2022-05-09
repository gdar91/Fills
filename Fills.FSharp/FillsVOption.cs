using Microsoft.FSharp.Core;

namespace Fills;

public static class FillsVOption
{
    public static FSharpValueOption<T> Return<T>(T item) => FSharpValueOption<T>.NewValueSome(item);


    public static FSharpValueOption<T> Empty<T>() => FSharpValueOption<T>.ValueNone;

    public static FSharpValueOption<T> Empty<T>(Hint<T> hint) => FSharpValueOption<T>.ValueNone;


    public static Hint<FSharpValueOption<TElement>> Hint<TElement>(Hint<TElement> hint) => default;

    public static Hint<TElement> UnHint<TElement>(Hint<FSharpValueOption<TElement>> hint) => default;
}
