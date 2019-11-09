using System;
using System.Collections.Generic;

namespace Fills.Enumerable
{
    public static partial class EnumerableExtensions
    {
        public static TResult Pipe<TElement, TResult>(
            this IEnumerable<TElement> source,
            Func<IEnumerable<TElement>, TResult> func
        )
        {
            return func(source);
        }


        public static void Pipe<TElement>(
            this IEnumerable<TElement> source,
            Action<IEnumerable<TElement>> action
        )
        {
            action(source);
        }
    }
}
