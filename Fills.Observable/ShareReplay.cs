using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Fills
{
    public static partial class ExtensionsForObservable
    {
        public static IObservable<TElement> ShareReplay<TElement>(
            this IObservable<TElement> observable,
            int bufferSize
        )
        {
            return
                new ConnectableObservable<TElement, TElement>(
                    observable,
                    () =>
                        new ResettingSubject<TElement>(() =>
                            new ReplaySubject<TElement>(bufferSize)
                        )
                )
                    .RefCount();
        }
    }
}
