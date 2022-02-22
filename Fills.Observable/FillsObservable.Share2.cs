using System.Reactive.Concurrency;
using System.Reactive.Subjects;

namespace Fills;

public static partial class FillsObservableExtensions
{
    private static IObservable<TValue> ShareCore2<TSubjectState, TValue>(
        this IObservable<TValue> observable,
        TSubjectState subjectState,
        Func<TSubjectState, ISubject<TValue>> subjectFactory
    )
    {
        return
            new SharedObservable<TSubjectState, TValue>(
                observable,
                subjectState,
                subjectFactory,
                TimeSpan.Zero,
                Scheduler.Default
            );
    }

    private static IObservable<TValue> ShareCore2<TSubjectState, TValue>(
        this IObservable<TValue> observable,
        TSubjectState subjectState,
        Func<TSubjectState, ISubject<TValue>> subjectFactory,
        TimeSpan disconnectDelay
    )
    {
        return
            new SharedObservable<TSubjectState, TValue>(
                observable,
                subjectState,
                subjectFactory,
                disconnectDelay,
                Scheduler.Default
            );
    }

    private static IObservable<TValue> ShareCore2<TSubjectState, TValue>(
        this IObservable<TValue> observable,
        TSubjectState subjectState,
        Func<TSubjectState, ISubject<TValue>> subjectFactory,
        TimeSpan disconnectDelay,
        IScheduler disconnectScheduler
    )
    {
        return
            new SharedObservable<TSubjectState, TValue>(
                observable,
                subjectState,
                subjectFactory,
                disconnectDelay,
                disconnectScheduler
            );
    }




    public static IObservable<TValue> Share2<TValue>(this IObservable<TValue> observable)
    {
        return observable.ShareCore2(0, static _ => new Subject<TValue>());
    }

    public static IObservable<TValue> ShareKeep<TValue>(this IObservable<TValue> observable, TimeSpan disconnectDelay)
    {
        return observable.ShareCore2(0, static _ => new Subject<TValue>(), disconnectDelay);
    }

    public static IObservable<TValue> ShareKeep<TValue>(
        this IObservable<TValue> observable,
        TimeSpan disconnectDelay,
        IScheduler disconnectScheduler
    )
    {
        return observable.ShareCore2(0, static _ => new Subject<TValue>(), disconnectDelay, disconnectScheduler);
    }


    public static IObservable<TValue> Share2<TValue>(
        this IObservable<TValue> observable,
        TValue initialValue
    )
    {
        return observable.ShareCore2(initialValue, static initialValue => new BehaviorSubject<TValue>(initialValue));
    }

    public static IObservable<TValue> ShareKeep<TValue>(
        this IObservable<TValue> observable,
        TValue initialValue,
        TimeSpan disconnectDelay
    )
    {
        return
            observable.ShareCore2(
                initialValue,
                static initialValue => new BehaviorSubject<TValue>(initialValue),
                disconnectDelay
            );
    }

    public static IObservable<TValue> ShareKeep<TValue>(
        this IObservable<TValue> observable,
        TValue initialValue,
        TimeSpan disconnectDelay,
        IScheduler disconnectScheduler
    )
    {
        return
            observable.ShareCore2(
                initialValue,
                static initialValue => new BehaviorSubject<TValue>(initialValue),
                disconnectDelay,
                disconnectScheduler
            );
    }




    public static IObservable<TValue> ShareReplay2<TValue>(this IObservable<TValue> observable)
    {
        return observable.ShareCore2(0, static _ => new ReplaySubject<TValue>());
    }

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        TimeSpan disconnectDelay
    )
    {
        return observable.ShareCore2(0, static _ => new ReplaySubject<TValue>(), disconnectDelay);
    }

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        TimeSpan disconnectDelay,
        IScheduler disconnectScheduler
    )
    {
        return observable.ShareCore2(0, static _ => new ReplaySubject<TValue>(), disconnectDelay, disconnectScheduler);
    }


    public static IObservable<TValue> ShareReplay2<TValue>(
        this IObservable<TValue> observable,
        int bufferSize
    )
    {
        return observable.ShareCore2(bufferSize, static bufferSize => new ReplaySubject<TValue>(bufferSize));
    }

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        int bufferSize,
        TimeSpan disconnectDelay
    )
    {
        return
            observable.ShareCore2(
                bufferSize,
                static bufferSize => new ReplaySubject<TValue>(bufferSize),
                disconnectDelay
            );
    }

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        int bufferSize,
        TimeSpan disconnectDelay,
        IScheduler disconnectScheduler
    )
    {
        return
            observable.ShareCore2(
                bufferSize,
                static bufferSize => new ReplaySubject<TValue>(bufferSize),
                disconnectDelay,
                disconnectScheduler
            );
    }


    public static IObservable<TValue> ShareReplay2<TValue>(
        this IObservable<TValue> observable,
        IScheduler scheduler
    )
    {
        return observable.ShareCore2(scheduler, static scheduler => new ReplaySubject<TValue>(scheduler));
    }

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        IScheduler scheduler,
        TimeSpan disconnectDelay
    )
    {
        return
            observable.ShareCore2(
                scheduler,
                static scheduler => new ReplaySubject<TValue>(scheduler),
                disconnectDelay
            );
    }

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        IScheduler scheduler,
        TimeSpan disconnectDelay,
        IScheduler disconnectScheduler
    )
    {
        return
            observable.ShareCore2(
                scheduler,
                static scheduler => new ReplaySubject<TValue>(scheduler),
                disconnectDelay,
                disconnectScheduler
            );
    }


    public static IObservable<TValue> ShareReplay2<TValue>(
        this IObservable<TValue> observable,
        TimeSpan window
    )
    {
        return observable.ShareCore2(window, static window => new ReplaySubject<TValue>(window));
    }

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        TimeSpan window,
        TimeSpan disconnectDelay
    )
    {
        return observable.ShareCore2(window, static window => new ReplaySubject<TValue>(window), disconnectDelay);
    }

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        TimeSpan window,
        TimeSpan disconnectDelay,
        IScheduler disconnectScheduler
    )
    {
        return
            observable.ShareCore2(
                window,
                static window => new ReplaySubject<TValue>(window),
                disconnectDelay,
                disconnectScheduler
            );
    }


    public static IObservable<TValue> ShareReplay2<TValue>(
        this IObservable<TValue> observable,
        int bufferSize,
        IScheduler scheduler
    )
    {
        return
            observable.ShareCore2(
                (bufferSize, scheduler),
                static tuple => new ReplaySubject<TValue>(tuple.bufferSize, tuple.scheduler)
            );
    }

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        int bufferSize,
        IScheduler scheduler,
        TimeSpan disconnectDelay
    )
    {
        return
            observable.ShareCore2(
                (bufferSize, scheduler),
                static tuple => new ReplaySubject<TValue>(tuple.bufferSize, tuple.scheduler),
                disconnectDelay
            );
    }

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        int bufferSize,
        IScheduler scheduler,
        TimeSpan disconnectDelay,
        IScheduler disconnectScheduler
    )
    {
        return
            observable.ShareCore2(
                (bufferSize, scheduler),
                static tuple => new ReplaySubject<TValue>(tuple.bufferSize, tuple.scheduler),
                disconnectDelay,
                disconnectScheduler
            );
    }


    public static IObservable<TValue> ShareReplay2<TValue>(
        this IObservable<TValue> observable,
        int bufferSize,
        TimeSpan window
    )
    {
        return
            observable.ShareCore2(
                (bufferSize, window),
                static tuple => new ReplaySubject<TValue>(tuple.bufferSize, tuple.window)
            );
    }

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        int bufferSize,
        TimeSpan window,
        TimeSpan disconnectDelay
    )
    {
        return
            observable.ShareCore2(
                (bufferSize, window),
                static tuple => new ReplaySubject<TValue>(tuple.bufferSize, tuple.window),
                disconnectDelay
            );
    }

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        int bufferSize,
        TimeSpan window,
        TimeSpan disconnectDelay,
        IScheduler disconnectScheduler
    )
    {
        return
            observable.ShareCore2(
                (bufferSize, window),
                static tuple => new ReplaySubject<TValue>(tuple.bufferSize, tuple.window),
                disconnectDelay,
                disconnectScheduler
            );
    }


    public static IObservable<TValue> ShareReplay2<TValue>(
        this IObservable<TValue> observable,
        TimeSpan window,
        IScheduler scheduler
    )
    {
        return
            observable.ShareCore2(
                (window, scheduler),
                static tuple => new ReplaySubject<TValue>(tuple.window, tuple.scheduler)
            );
    }

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        TimeSpan window,
        IScheduler scheduler,
        TimeSpan disconnectDelay
    )
    {
        return
            observable.ShareCore2(
                (window, scheduler),
                static tuple => new ReplaySubject<TValue>(tuple.window, tuple.scheduler),
                disconnectDelay
            );
    }

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        TimeSpan window,
        IScheduler scheduler,
        TimeSpan disconnectDelay,
        IScheduler disconnectScheduler
    )
    {
        return
            observable.ShareCore2(
                (window, scheduler),
                static tuple => new ReplaySubject<TValue>(tuple.window, tuple.scheduler),
                disconnectDelay,
                disconnectScheduler
            );
    }


    public static IObservable<TValue> ShareReplay2<TValue>(
        this IObservable<TValue> observable,
        int bufferSize,
        TimeSpan window,
        IScheduler scheduler
    )
    {
        return
            observable.ShareCore2(
                (bufferSize, window, scheduler),
                static tuple => new ReplaySubject<TValue>(tuple.bufferSize, tuple.window, tuple.scheduler)
            );
    }

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        int bufferSize,
        TimeSpan window,
        IScheduler scheduler,
        TimeSpan disconnectDelay
    )
    {
        return
            observable.ShareCore2(
                (bufferSize, window, scheduler),
                static tuple => new ReplaySubject<TValue>(tuple.bufferSize, tuple.window, tuple.scheduler),
                disconnectDelay
            );
    }

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        int bufferSize,
        TimeSpan window,
        IScheduler scheduler,
        TimeSpan disconnectDelay,
        IScheduler disconnectScheduler
    )
    {
        return
            observable.ShareCore2(
                (bufferSize, window, scheduler),
                static tuple => new ReplaySubject<TValue>(tuple.bufferSize, tuple.window, tuple.scheduler),
                disconnectDelay,
                disconnectScheduler
            );
    }
}
