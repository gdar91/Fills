using System;
using System.Collections.Generic;
using System.Linq;

namespace Fills.Enumerable
{
    public static partial class EnumerableExtensions
    {
        public static TElement Aggregate<TElement>(
            this IEnumerable<TElement> source,
            Func<TElement, TElement, TElement> func,
            Func<TElement, bool> zeroValuePredicate
        )
        {
            var enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException(
                    $"{nameof(source)} contains no elements."
                );
            }

            var accumulate = enumerator.Current;

            while (!zeroValuePredicate(accumulate) && enumerator.MoveNext())
            {
                accumulate = func(accumulate, enumerator.Current);
            }

            return accumulate;
        }

        public static TAccumulate Aggregate<TElement, TAccumulate>(
            this IEnumerable<TElement> source,
            TAccumulate seed,
            Func<TAccumulate, TElement, TAccumulate> func,
            Func<TAccumulate, bool> zeroValuePredicate
        )
        {
            if (zeroValuePredicate(seed))
            {
                return seed;
            }

            var accumulate = seed;

            foreach (var item in source)
            {
                accumulate = func(accumulate, item);

                if (zeroValuePredicate(accumulate))
                {
                    break;
                }
            }
            
            return accumulate;
        }

        public static TResult Aggregate<TElement, TAccumulate, TResult>(
            this IEnumerable<TElement> source,
            TAccumulate seed,
            Func<TAccumulate, TElement, TAccumulate> func,
            Func<TAccumulate, TResult> resultSelector,
            Func<TAccumulate, bool> zeroValuePredicate
        )
        {
            return resultSelector(source.Aggregate(seed, func, zeroValuePredicate));
        }

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
