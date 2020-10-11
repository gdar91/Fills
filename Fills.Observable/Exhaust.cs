using System;
using System.Reactive.Linq;

namespace Fills
{
    public static partial class ExtensionsForObservable
    {
        public static IObservable<TElement> Exhaust<TElement>(
            this IObservable<IObservable<TElement>> source
        )
        {
            var acquired = false;
            var padlock = new object();


            return source
                .Where(Acquire)
                .Select(observable => observable.Finally(Release))
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
    }
}
