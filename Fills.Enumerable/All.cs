using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Fills
{
    public static partial class ExtensionsForEnumerable
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool All(this IEnumerable<bool> source)
            => source.All(result => result);
    }
}
