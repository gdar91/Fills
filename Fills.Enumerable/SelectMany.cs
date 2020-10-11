using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Fills
{
    public static partial class ExtensionsForEnumerable
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TElement> SelectMany<TElement>(
            this IEnumerable<IEnumerable<TElement>> source
        )
        {
            return source.SelectMany(collection => collection);
        }
    }
}
