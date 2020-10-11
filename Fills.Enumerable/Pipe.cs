using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Fills
{
    public static partial class ExtensionsForEnumerable
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult Pipe<TElement, TResult>(
            this IEnumerable<TElement> source,
            Func<IEnumerable<TElement>, TResult> func
        )
        {
            return func(source);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Pipe<TElement>(
            this IEnumerable<TElement> source,
            Action<IEnumerable<TElement>> action
        )
        {
            action(source);
        }
    }
}
