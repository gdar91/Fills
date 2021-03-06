using System.Collections.Generic;

namespace Fills.Enumerable
{
    public static partial class EnumerableExtensions
    {
        public delegate bool TrySelector<TElement, TProjection>(TElement element, out TProjection projection);


        public static IEnumerable<TProjection> TrySelect<TElement, TProjection>(
            this IEnumerable<TElement> source,
            TrySelector<TElement, TProjection> trySelector
        )
        {
            foreach (var item in source)
            {
                if (trySelector(item, out var projection))
                {
                    yield return projection;
                }
            }
        }
    }
}
