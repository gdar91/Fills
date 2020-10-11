using System;
using System.Collections.Generic;

namespace Fills
{
    public static partial class ExtensionsForEnumerable
    {
        public static void ForEach<TElement>(
            this IEnumerable<TElement> source,
            Action<TElement> action
        )
        {
            foreach (var item in source)
            {
                action(item);
            }
        }


        public static void ForEach<TElement>(
            this IEnumerable<TElement> source,
            Action<TElement, long> action
        )
        {
            var i = 0L;

            foreach (var item in source)
            {
                action(item, i++);
            }
        }
    }
}
