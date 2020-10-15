using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;

namespace Fills
{
    public static partial class FillsObservable
    {
        public static IObservable<TElement> FromAsyncEnumerable<TElement>(
            Func<CancellationToken, IAsyncEnumerable<TElement>> asyncEnumerableFactory
        )
        {
            return Observable.Create<TElement>(async (observer, cancellationToken) =>
            {
                var asyncEnumerable = asyncEnumerableFactory(cancellationToken);

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
