using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Fills
{
    public static partial class ExtensionsForObservable
    {
        public static IObservable<TElement> AsObservable<TElement>(
            this IAsyncEnumerable<TElement> asyncEnumerable
        )
        {
            return Observable.Create<TElement>(async (observer, cancellationToken) =>
            {
                await foreach (var item in asyncEnumerable)
                {
                    observer.OnNext(item);
                }

                observer.OnCompleted();

                return Disposable.Empty;
            });
        }
    }
}
