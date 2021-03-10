using System;
using System.Collections.Generic;

namespace Fills
{
    public static partial class ExtensionsForEnumerable
    {
        public static IEnumerable<TElement> Scan<TElement>(
            this IEnumerable<TElement> source,
            Func<TElement, TElement, TElement> func
        )
        {
            var enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                yield break;
            }

            var accumulate = enumerator.Current;

            yield return accumulate;

            while (enumerator.MoveNext())
            {
                accumulate = func(accumulate, enumerator.Current);

                yield return accumulate;
            }
        }


        public static IEnumerable<TAccumulate> Scan<TElement, TAccumulate>(
            this IEnumerable<TElement> source,
            TAccumulate seed,
            Func<TAccumulate, TElement, TAccumulate> func
        )
        {
            var accumulate = seed;

            yield return accumulate;

            foreach (var item in source)
            {
                accumulate = func(accumulate, item);

                yield return accumulate;
            }
        }


        public static IEnumerable<TResult> Scan<TElement, TAccumulate, TResult>(
            this IEnumerable<TElement> source,
            TAccumulate seed,
            Func<TAccumulate, TElement, TAccumulate> func,
            Func<TAccumulate, TResult> resultSelector
        )
        {
            var accumulate = seed;
            var result = resultSelector(accumulate);

            yield return result;

            foreach (var item in source)
            {
                accumulate = func(accumulate, item);
                result = resultSelector(accumulate);

                yield return result;
            }
        }
    }
}
