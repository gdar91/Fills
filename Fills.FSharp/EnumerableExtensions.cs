using Microsoft.FSharp.Collections;

namespace Fills;

public static class EnumerableExtensions
{
    public static FSharpList<T> ToFSharpList<T>(this IEnumerable<T> enumerable) => ListModule.OfSeq(enumerable);


    public static FSharpSet<T> ToFSharpSet<T>(this IEnumerable<T> enumerable) => SetModule.OfSeq(enumerable);


    public static FSharpMap<TKey, TValue> ToFSharpMap<TKey, TValue>(this IEnumerable<Tuple<TKey, TValue>> enumerable)
    {
        return MapModule.OfSeq(enumerable);
    }

    public static FSharpMap<TKey, TValue> ToFSharpMap<TElement, TKey, TValue>(
        this IEnumerable<TElement> enumerable,
        Func<TElement, TKey> keySelector,
        Func<TElement, TValue> valueSelector
    )
    {
        return MapModule.OfSeq(
            enumerable.Select(element => Tuple.Create(keySelector(element), valueSelector(element)))
        );
    }


    public static FSharpMap<TKey, TValue> ToFSharpMap<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
    {
        return MapModule.OfSeq(
            dictionary.Select(keyValuePair => Tuple.Create(keyValuePair.Key, keyValuePair.Value))
        );
    }
}
