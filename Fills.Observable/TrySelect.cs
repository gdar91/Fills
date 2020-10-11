using System;
using System.Reactive.Linq;

namespace Fills
{
    public static partial class ExtensionsForObservable
    {
        public delegate bool TrySelector<TElement, TProjection>(
            TElement element,
            out TProjection projection
        );

        public static IObservable<TProjection> TrySelect<TElement, TProjection>(
            this IObservable<TElement> source,
            TrySelector<TElement, TProjection> trySelector
        )
        {
            return System.Reactive.Linq.Observable.Create<TProjection>(observer =>
                source.Subscribe(
                    element =>
                    {
                        if (trySelector(element, out var projection))
                            observer.OnNext(projection);
                    },
                    observer.OnError,
                    observer.OnCompleted
                )
            );
        }
    }
}
