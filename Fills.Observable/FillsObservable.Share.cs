using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Fills;

public static partial class FillsObservableExtensions
{
    private static IObservable<TElement> ShareCore<TElement>(
        this IObservable<TElement> observable,
        Func<ISubject<TElement>> subjectFactory
    )
    {
        var connectableObservable =
            new ConnectableObservable<TElement, TElement>(
                observable,
                () => new ResettingSubject<TElement>(subjectFactory)
            );

        return connectableObservable
            .RefCount(1, TimeSpan.Zero, ImmediateScheduler.Instance)
            .Let(observable =>
                Observable.Create<TElement>(observer =>
                {
                    IDisposable? subscription = null;

                    subscription =
                        observable.Subscribe(
                            observer.OnNext,
                            error =>
                            {
                                try
                                {
                                    subscription?.Dispose();
                                }
                                catch
                                { }

                                observer.OnError(error);
                            },
                            () =>
                            {
                                try
                                {
                                    subscription?.Dispose();
                                }
                                catch
                                { }

                                observer.OnCompleted();
                            }
                        );

                    return subscription;
                })
            );
    }




    public static IObservable<TElement> Share<TElement>(
        this IObservable<TElement> observable
    )
    {
        return observable.ShareCore(() => new Subject<TElement>());
    }

    public static IObservable<TElement> Share<TElement>(
        this IObservable<TElement> observable,
        TElement initialValue
    )
    {
        return observable.ShareCore(() => new BehaviorSubject<TElement>(initialValue));
    }




    public static IObservable<TElement> ShareReplay<TElement>(
        this IObservable<TElement> observable
    )
    {
        return observable.ShareCore(() => new ReplaySubject<TElement>());
    }

    public static IObservable<TElement> ShareReplay<TElement>(
        this IObservable<TElement> observable,
        int bufferSize
    )
    {
        return observable.ShareCore(() => new ReplaySubject<TElement>(bufferSize));
    }

    public static IObservable<TElement> ShareReplay<TElement>(
        this IObservable<TElement> observable,
        IScheduler scheduler
    )
    {
        return observable.ShareCore(() => new ReplaySubject<TElement>(scheduler));
    }

    public static IObservable<TElement> ShareReplay<TElement>(
        this IObservable<TElement> observable,
        TimeSpan window
    )
    {
        return observable.ShareCore(() => new ReplaySubject<TElement>(window));
    }

    public static IObservable<TElement> ShareReplay<TElement>(
        this IObservable<TElement> observable,
        int bufferSize,
        IScheduler scheduler
    )
    {
        return observable.ShareCore(() => new ReplaySubject<TElement>(bufferSize, scheduler));
    }

    public static IObservable<TElement> ShareReplay<TElement>(
        this IObservable<TElement> observable,
        int bufferSize,
        TimeSpan window
    )
    {
        return observable.ShareCore(() => new ReplaySubject<TElement>(bufferSize, window));
    }

    public static IObservable<TElement> ShareReplay<TElement>(
        this IObservable<TElement> observable,
        TimeSpan window,
        IScheduler scheduler
    )
    {
        return observable.ShareCore(() => new ReplaySubject<TElement>(window, scheduler));
    }

    public static IObservable<TElement> ShareReplay<TElement>(
        this IObservable<TElement> observable,
        int bufferSize,
        TimeSpan window,
        IScheduler scheduler
    )
    {
        return observable.ShareCore(() => new ReplaySubject<TElement>(bufferSize, window, scheduler));
    }
}
