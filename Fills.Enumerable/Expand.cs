using System;
using System.Collections.Generic;

namespace Fills.Enumerable
{
    public static partial class EnumerableExtensions
    {
        public static IEnumerable<TElement> Expand<TElement>(
            this IEnumerable<TElement> source,
            Func<TElement, IEnumerable<TElement>> func
        )
        {
            foreach (var item in source)
            {
                yield return item;

                var expansion = func(item).Expand(func);

                foreach (var innerItem in expansion)
                {
                    yield return innerItem;
                }
            }
        }
    }
}
