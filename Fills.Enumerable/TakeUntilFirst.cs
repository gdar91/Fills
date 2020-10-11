using System;
using System.Collections.Generic;

namespace Fills
{
    public static partial class ExtensionsForEnumerable
    {
        public static IEnumerable<TElement> TakeUntilFirst<TElement>(
            this IEnumerable<TElement> source,
            Func<TElement, bool> predicate
        )
        {
            foreach (var item in source)
            {
                yield return item;

                if (predicate(item))
                {
                    yield break;
                }
            }
        }
    }
}
