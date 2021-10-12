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


    public static IObservable<TElement> Share<TElement>(this IObservable<TElement> observable)
    {
        return ShareCore(observable, () => new Subject<TElement>());
    }


    public static IObservable<TElement> ShareReplay<TElement>(this IObservable<TElement> observable)
    {
        return ShareCore(observable, () => new ReplaySubject<TElement>());
    }


    public static IObservable<TElement> ShareReplay<TElement>(this IObservable<TElement> observable, int bufferSize)
    {
        return ShareCore(observable, () => new ReplaySubject<TElement>(bufferSize));
    }


    public static IObservable<TElement> ShareReplay<TElement>(
        this IObservable<TElement> observable,
        IScheduler scheduler
    )
    {
        return ShareCore(observable, () => new ReplaySubject<TElement>(scheduler));
    }


    public static IObservable<TElement> ShareReplay<TElement>(this IObservable<TElement> observable, TimeSpan window)
    {
        return ShareCore(observable, () => new ReplaySubject<TElement>(window));
    }


    public static IObservable<TElement> ShareReplay<TElement>(
        this IObservable<TElement> observable,
        int bufferSize,
        IScheduler scheduler
    )
    {
        return ShareCore(observable, () => new ReplaySubject<TElement>(bufferSize, scheduler));
    }


    public static IObservable<TElement> ShareReplay<TElement>(
        this IObservable<TElement> observable,
        int bufferSize,
        TimeSpan window
    )
    {
        return ShareCore(observable, () => new ReplaySubject<TElement>(bufferSize, window));
    }


    public static IObservable<TElement> ShareReplay<TElement>(
        this IObservable<TElement> observable,
        TimeSpan window,
        IScheduler scheduler
    )
    {
        return ShareCore(observable, () => new ReplaySubject<TElement>(window, scheduler));
    }


    public static IObservable<TElement> ShareReplay<TElement>(
        this IObservable<TElement> observable,
        int bufferSize,
        TimeSpan window,
        IScheduler scheduler
    )
    {
        return ShareCore(observable, () => new ReplaySubject<TElement>(bufferSize, window, scheduler));
    }
}
