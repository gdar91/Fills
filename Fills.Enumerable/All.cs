using System.Collections.Generic;
using System.Linq;

namespace Fills.Enumerable
{
    public static partial class EnumerableExtensions
    {
        public static bool All(this IEnumerable<bool> source)
            => source.All(result => result);
    }
}
