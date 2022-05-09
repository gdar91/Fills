namespace Fills;

public readonly struct Hint<T>
{
}


public static partial class Hint
{
    public static Hint<T> Of<T>() => default;

    public static Hint<T> OfValue<T>(T value) => default;

    public static Hint<T> OfFactory<T>(Func<T> factory) => default;

    public static T Identity<T>(this Hint<T> hint, T value) => value;
}
