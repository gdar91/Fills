using System;
using System.Collections.Generic;
using System.Linq;

namespace Fills
{
    public static partial class ExtensionsForEnumerable
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
    }
}
