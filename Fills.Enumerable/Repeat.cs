using System.Collections.Generic;

namespace Fills
{
    public static partial class ExtensionsForEnumerable
    {
        public static IEnumerable<TElement> Repeat<TElement>(
            this IEnumerable<TElement> source,
            long count
        )
        {
            for (var i = 0L; i < count; i++)
            {
                foreach (var item in source)
                {
                    yield return item;
                }
            }
        }
    }
}
