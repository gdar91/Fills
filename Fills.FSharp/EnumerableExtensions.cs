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
                    Cache<TElement, TKey, TValue>.ToFSharpMapTrySelector
                )
            );
    }

    public static FSharpMap<TKey, TValue> ToFSharpMap<TKey, TValue>(this IDictionary<TKey, TValue> dictionary) =>
        MapModule.OfSeq(dictionary.Select(Cache<TKey, TValue>.ToFSharpMapTupleOfKeyValuePair));




    private static class Cache<T1, T2>
    {
        public static readonly Func<KeyValuePair<T1, T2>, Tuple<T1, T2>> ToFSharpMapTupleOfKeyValuePair =
            static keyValuePair => Tuple.Create(keyValuePair.Key, keyValuePair.Value);
    }

    private static class Cache<T1, T2, T3>
    {
        public static readonly
            TrySelector<(Func<T1, T2>, Func<T1, T3>), T1, Tuple<T2, T3>>
            ToFSharpMapTrySelector =
                static ((Func<T1, T2> KeyOf, Func<T1, T3> ValueOf) state, T1 element, out Tuple<T2, T3> result) =>
                {
                    result = Tuple.Create(state.KeyOf(element), state.ValueOf(element));
                    return true;
                };
    }
}
