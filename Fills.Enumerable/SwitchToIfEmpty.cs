using System.Collections.Generic;

namespace Fills.Enumerable
{
    public static partial class EnumerableExtensions
    {
        public static IEnumerable<TElement> SwitchToIfEmpty<TElement>(
            this IEnumerable<TElement> source,
            IEnumerable<TElement> secondary
        )
        {
            var any = false;

            foreach (var item in source)
            {
                yield return item;

                any = true;
            }

            if (any)
            {
                yield break;
            }

            foreach (var item in secondary)
            {
                yield return item;
            }
        }
    }
}
