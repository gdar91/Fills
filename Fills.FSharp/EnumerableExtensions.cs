using Microsoft.FSharp.Collections;

namespace Fills;

public static class EnumerableExtensions
{
    public static FSharpList<T> ToFSharpList<T>(this IEnumerable<T> enumerable) => ListModule.OfSeq(enumerable);


    public static FSharpSet<T> ToFSharpSet<T>(this IEnumerable<T> enumerable) => SetModule.OfSeq(enumerable);


    public static FSharpMap<TKey, TValue> ToFSharpMap<TKey, TValue>(this IEnumerable<Tuple<TKey, TValue>> enumerable) =>
        MapModule.OfSeq(enumerable);

    public static FSharpMap<TKey, TValue> ToFSharpMap<TElement, TKey, TValue>(
        this IEnumerable<TElement> enumerable,
        Func<TElement, TKey> keySelector,
        Func<TElement, TValue> valueSelector
    )
    {
        return
            MapModule.OfSeq(
                enumerable.TrySelect(
                    (keySelector, valueSelector),
                    static (
                        (Func<TElement, TKey> KeyOf, Func<TElement, TValue> ValueOf) tuple,
                        TElement element,
                        out Tuple<TKey, TValue> result
                    ) =>
                    {
                        result = Tuple.Create(tuple.KeyOf(element), tuple.ValueOf(element));
                        return true;
                    }
                )
            );
    }

    public static FSharpMap<TKey, TValue> ToFSharpMap<TArg, TElement, TKey, TValue>(
        this IEnumerable<TElement> enumerable,
        TArg arg,
        Func<TArg, TElement, TKey> keySelector,
        Func<TArg, TElement, TValue> valueSelector
    )
    {
        return
            MapModule.OfSeq(
                enumerable.TrySelect(
                    (arg, keySelector, valueSelector),
                    static (
                        (TArg arg, Func<TArg, TElement, TKey> KeyOf, Func<TArg, TElement, TValue> ValueOf) tuple,
                        TElement element,
                        out Tuple<TKey, TValue> result
                    ) =>
                    {
                        result = Tuple.Create(tuple.KeyOf(tuple.arg, element), tuple.ValueOf(tuple.arg, element));
                        return true;
                    }
                )
            );
    }

    public static FSharpMap<TKey, TValue> ToFSharpMap<TKey, TValue>(this IDictionary<TKey, TValue> dictionary) =>
        MapModule.OfSeq(dictionary.Select(static keyValuePair => Tuple.Create(keyValuePair.Key, keyValuePair.Value)));
}
