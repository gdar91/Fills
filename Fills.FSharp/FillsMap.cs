using Microsoft.FSharp.Collections;

namespace Fills;

public static class FillsMap
{
    public static FSharpMap<TKey, TValue> Empty<TKey, TValue>() => MapModule.Empty<TKey, TValue>();

    public static FSharpMap<TKey, TValue> Empty<TKey, TValue>(Hint<TKey> keyHint, Hint<TValue> valueHint) =>
        MapModule.Empty<TKey, TValue>();

    public static Hint<FSharpMap<TKey, TValue>> Hint<TKey, TValue>(Hint<TKey> keyHint, Hint<TValue> valueHint) =>
        default;

    public static Hint<TResult> UnHint<TKey, TValue, TResult>(
        Hint<FSharpMap<TKey, TValue>> hint,
        Func<Hint<TKey>, Hint<TValue>, Hint<TResult>> func
    )
    {
        return default;
    }
}
