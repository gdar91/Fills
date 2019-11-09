using System.Collections.Generic;
using System.Linq;

namespace Fills.Enumerable
{
    public static partial class EnumerableExtensions
    {
        public static bool Any(this IEnumerable<bool> source)
            => source.Any(result => result);
    }   
}
