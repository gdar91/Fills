using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Fills;

public static class FillsObservable
{
    public static IObservable<TElement> Return<TElement>(TElement value) =>
        Observable.Return(value);

    public static IObservable<TElement> Return<TElement>(TElement value, IScheduler scheduler) =>
        Observable.Return(value, scheduler);


    public static IObservable<TElement> Empty<TElement>() =>
        Observable.Empty<TElement>();

    public static IObservable<TElement> Empty<TElement>(IScheduler scheduler) =>
        Observable.Empty<TElement>(scheduler);

    public static IObservable<TElement> Empty<TElement>(Hint<TElement> hint) =>
        Observable.Empty<TElement>();

    public static IObservable<TElement> Empty<TElement>(IScheduler scheduler, Hint<TElement> hint) =>
        Observable.Empty<TElement>(scheduler);


    public static IObservable<TElement> Never<TElement>() =>
        Observable.Never<TElement>();

    public static IObservable<TElement> Never<TElement>(Hint<TElement> hint) =>
        Observable.Never<TElement>();


    public static IObservable<TElement> FromAsyncEnumerable<TElement>(
        Func<CancellationToken, IAsyncEnumerable<TElement>> asyncEnumerableFunc
    )
    {
        return Observable.Create<TElement>(async (observer, cancellationToken) =>
        {
            var asyncEnumerable = asyncEnumerableFunc(cancellationToken);

            await foreach (var item in asyncEnumerable.WithCancellation(cancellationToken))
            {
                observer.OnNext(item);
            }

            observer.OnCompleted();

            return Disposable.Empty;
        });
    }


    public static Hint<IObservable<TElement>> Hint<TElement>(Hint<TElement> hint) => default;

    public static Hint<TElement> UnHint<TElement>(Hint<IObservable<TElement>> hint) => default;
}
