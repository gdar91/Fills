using System.Collections.Generic;

namespace Fills
{
    public static partial class ExtensionsForEnumerable
    {
        public static IEnumerable<TElement> EmptyCoalesce<TElement>(
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
