using System;
using System.Reactive.Linq;

namespace Fills
{
    public static partial class ExtensionsForObservable
    {
        public static IObservable<(TElement previous, TElement current)> Pairwise<TElement>(
            this IObservable<TElement> source
        )
        {
            return source
                .Scan(
                    (default(TElement), default(TElement)),
                    (state, element) => (state.Item2, element)
                )
                .Skip(1);
        }


        public static IObservable<(TElement previous, TElement current)> Pairwise<TElement>(
            this IObservable<TElement> source,
            TElement initialElement
        )
        {
            return source
                .Scan(
                    (default(TElement), initialElement),
                    (state, element) => (state.Item2, element)
                );
        }
    }
}
