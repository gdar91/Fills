using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Fills
{
    public static partial class ExtensionsForObservable
    {
        public static IConnectableObservable<TElement> ReplayResetting<TElement>(
            this IObservable<TElement> observable,
            int bufferSize
        )
        {
            return
                observable.Multicast(
                    new ResettingSubject<TElement>(() =>
                        new ReplaySubject<TElement>(bufferSize)
                    )
                );
        }
    }
}
