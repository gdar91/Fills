using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Fills
{
    public static partial class ExtensionsForObservable
    {
        public static IConnectableObservable<TElement> PublishResetting<TElement>(
            this IObservable<TElement> observable
        )
        {
            return
                observable.Multicast(
                    new ResettingSubject<TElement>(() => new Subject<TElement>())
                );
        }
    }
}
