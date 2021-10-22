using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Fills;

public static partial class FillsObservableExtensions
{
    private static IObservable<TElement> ShareCore<TElement>(
        IObservable<TElement> observable,
        Func<ISubject<TElement>> subjectFactory
    )
    {
        return observable
            .Multicast(new ResettingSubject<TElement>(subjectFactory))
            .RefCount();
    }

    private static IObservable<TElement> ShareCore<TElement>(
        IObservable<TElement> observable,
        Func<ISubject<TElement>> subjectFactory,
        TimeSpan disconnectDelay
    )
    {
        return observable
            .Multicast(new ResettingSubject<TElement>(subjectFactory))
            .RefCount(disconnectDelay);
    }

    private static IObservable<TElement> ShareCore<TElement>(
        IObservable<TElement> observable,
        Func<ISubject<TElement>> subjectFactory,
        TimeSpan disconnectDelay,
        IScheduler scheduler
    )
    {
        return observable
            .Multicast(new ResettingSubject<TElement>(subjectFactory))
            .RefCount(disconnectDelay, scheduler);
    }




    public static IObservable<TElement> Share<TElement>(this IObservable<TElement> observable)
    {
        return ShareCore(observable, () => new Subject<TElement>());
    }

    public static IObservable<TElement> Share<TElement>(
        this IObservable<TElement> observable,
        TimeSpan disconnectDelay
    )
    {
        return ShareCore(observable, () => new Subject<TElement>(), disconnectDelay);
    }

    public static IObservable<TElement> Share<TElement>(
        this IObservable<TElement> observable,
        TimeSpan disconnectDelay,
        IScheduler scheduler
    )
    {
        return ShareCore(observable, () => new Subject<TElement>(), disconnectDelay, scheduler);
    }




    public static IObservable<TElement> ShareReplay<TElement>(this IObservable<TElement> observable)
    {
        return ShareCore(observable, () => new ReplaySubject<TElement>(1));
    }

    public static IObservable<TElement> ShareReplay<TElement>(
        this IObservable<TElement> observable,
        TimeSpan disconnectDelay
    )
    {
        return ShareCore(observable, () => new ReplaySubject<TElement>(1), disconnectDelay);
    }

    public static IObservable<TElement> ShareReplay<TElement>(
        this IObservable<TElement> observable,
        IScheduler scheduler
    )
    {
        return ShareCore(observable, () => new ReplaySubject<TElement>(1, scheduler));
    }

    public static IObservable<TElement> ShareReplay<TElement>(
        this IObservable<TElement> observable,
        TimeSpan disconnectDelay,
        IScheduler scheduler
    )
    {
        return ShareCore(observable, () => new ReplaySubject<TElement>(1, scheduler), disconnectDelay, scheduler);
    }
}
