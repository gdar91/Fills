using Microsoft.FSharp.Collections;

namespace Fills;

public static class FillsList
{
    public static FSharpList<T> Return<T>(T value) => ListModule.Singleton(value);


    public static FSharpList<T> Empty<T>() => ListModule.Empty<T>();

    public static FSharpList<T> Empty<T>(Hint<T> hint) => ListModule.Empty<T>();


    public static Hint<FSharpList<T>> Hint<T>(Hint<T> hint) => default;

    public static Hint<T> UnHint<T>(Hint<FSharpList<T>> hint) => default;
}
