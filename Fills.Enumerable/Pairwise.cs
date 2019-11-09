using System.Collections.Generic;

namespace Fills.Enumerable
{
    public static partial class EnumerableExtensions
    {
        public static IEnumerable<(TElement previous, TElement current)> Pairwise<TElement>(
            this IEnumerable<TElement> source
        )
        {
            var enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                yield break;
            }

            var previous = enumerator.Current;

            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                var pair = (previous, current);

                yield return pair;
            }
        }
    }
}
