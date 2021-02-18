using Microsoft.FSharp.Collections;
using System.Collections.Generic;

namespace Fills
{
    public static class EnumerableExtensions
    {
        public static FSharpList<T> ToFSharpList<T>(this IEnumerable<T> enumerable)
        {
            return ListModule.OfSeq(enumerable);
        }


        public static FSharpSet<T> ToFSharpSet<T>(this IEnumerable<T> enumerable)
        {
            return SetModule.OfSeq(enumerable);
        }
    }
}
