using System;
using System.Collections.Generic;

namespace Fills
{
    public static partial class ExtensionsForEnumerable
    {
        public static TResult Let<TElement, TResult>(
            this IEnumerable<TElement> source,
            Func<IEnumerable<TElement>, TResult> func
        )
        {
            return func(source);
        }
    }
}
