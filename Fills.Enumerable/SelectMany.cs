using System.Collections.Generic;
using System.Linq;

namespace Fills.Enumerable
{
    public static partial class EnumerableExtensions
    {
        public static IEnumerable<TElement> SelectMany<TElement>(
            this IEnumerable<IEnumerable<TElement>> source
        )
        {
            return source.SelectMany(collection => collection);
        }
    }
}
