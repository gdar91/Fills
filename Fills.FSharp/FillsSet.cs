using Microsoft.FSharp.Collections;

namespace Fills
{
    public static class FillsSet
    {
        public static FSharpSet<T> Return<T>(T value) => SetModule.Singleton(value);

        public static FSharpSet<T> Empty<T>() => SetModule.Empty<T>();

        public static FSharpSet<T> Empty<T>(Hint<T> hint) => SetModule.Empty<T>();

        public static Hint<T> Hint<T>(Hint<FSharpSet<T>> hint) => default;

        public static Hint<FSharpSet<T>> Unhint<T>(Hint<T> hint) => default;
    }
}
