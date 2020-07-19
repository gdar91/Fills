using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Fills.Enumerable
{
    public static partial class EnumerableExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool All(this IEnumerable<bool> source)
            => source.All(result => result);
    }
}
