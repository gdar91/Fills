using System;
using System.Reactive.Linq;

namespace Fills.Observable
{
    public static class ObservableExtensions
    {
        public static IObservable<(TElement previous, TElement current)> Pairwise<TElement>(
            this IObservable<TElement> source
        )
        {
            return source
                .Scan(
                    (default(TElement)!, default(TElement)!),
                    (state, element) => (state.Item2, element)
                )
                .Skip(1);
        }


        public static IObservable<TElement> Exhaust<TElement>(
            this IObservable<IObservable<TElement>> source
        )
        {
            var acquired = false;
            var padlock = new object();


            return source
                .Where(Acquire)
                .Select(observable => observable.Do(next => { }, Release))
                .Concat();
            

            bool Acquire(IObservable<TElement> observable)
            {
                lock (padlock)
                    return acquired
                        ? false
                        : acquired = true;
            }

            void Release()
            {
                lock (padlock)
                    acquired = false;
            }
        }


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
