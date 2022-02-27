using System.Reactive.Concurrency;
using System.Reactive.Subjects;

namespace Fills;

public static partial class FillsObservableExtensions
{
    private static IObservable<TValue> ShareCore<TSubjectState, TValue>(
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

    private static IObservable<TValue> ShareCore<TSubjectState, TValue>(
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

    private static IObservable<TValue> ShareCore<TSubjectState, TValue>(
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




    public static IObservable<TValue> Share<TValue>(this IObservable<TValue> observable) =>
        observable.ShareCore(0, Cache<TValue>.Subject);

    public static IObservable<TValue> ShareKeep<TValue>(this IObservable<TValue> observable, TimeSpan disconnectDelay)
    {
        return observable.ShareCore(0, Cache<TValue>.Subject, disconnectDelay);
    }

    public static IObservable<TValue> ShareKeep<TValue>(
        this IObservable<TValue> observable,
        TimeSpan disconnectDelay,
        IScheduler disconnectScheduler
    )
    {
        return observable.ShareCore(0, Cache<TValue>.Subject, disconnectDelay, disconnectScheduler);
    }


    public static IObservable<TValue> Share<TValue>(this IObservable<TValue> observable, TValue initialValue) =>
        observable.ShareCore(initialValue, Cache<TValue>.BehaviorSubject);

    public static IObservable<TValue> ShareKeep<TValue>(
        this IObservable<TValue> observable,
        TValue initialValue,
        TimeSpan disconnectDelay
    )
    {
        return observable.ShareCore(initialValue, Cache<TValue>.BehaviorSubject, disconnectDelay);
    }

    public static IObservable<TValue> ShareKeep<TValue>(
        this IObservable<TValue> observable,
        TValue initialValue,
        TimeSpan disconnectDelay,
        IScheduler disconnectScheduler
    )
    {
        return
            observable.ShareCore(
                initialValue,
                Cache<TValue>.BehaviorSubject,
                disconnectDelay,
                disconnectScheduler
            );
    }




    public static IObservable<TValue> ShareReplay<TValue>(this IObservable<TValue> observable) =>
        observable.ShareCore(0, Cache<TValue>.ReplaySubject0);

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        TimeSpan disconnectDelay
    )
    {
        return observable.ShareCore(0, Cache<TValue>.ReplaySubject0, disconnectDelay);
    }

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        TimeSpan disconnectDelay,
        IScheduler disconnectScheduler
    )
    {
        return observable.ShareCore(0, Cache<TValue>.ReplaySubject0, disconnectDelay, disconnectScheduler);
    }


    public static IObservable<TValue> ShareReplay<TValue>(this IObservable<TValue> observable, int bufferSize) =>
        observable.ShareCore(bufferSize, Cache<TValue>.ReplaySubjectB);

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        int bufferSize,
        TimeSpan disconnectDelay
    )
    {
        return observable.ShareCore(bufferSize, Cache<TValue>.ReplaySubjectB, disconnectDelay);
    }

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        int bufferSize,
        TimeSpan disconnectDelay,
        IScheduler disconnectScheduler
    )
    {
        return observable.ShareCore(bufferSize, Cache<TValue>.ReplaySubjectB, disconnectDelay, disconnectScheduler);
    }


    public static IObservable<TValue> ShareReplay<TValue>(this IObservable<TValue> observable, TimeSpan window) =>
        observable.ShareCore(window, Cache<TValue>.ReplaySubjectW);

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        TimeSpan window,
        TimeSpan disconnectDelay
    )
    {
        return observable.ShareCore(window, Cache<TValue>.ReplaySubjectW, disconnectDelay);
    }

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        TimeSpan window,
        TimeSpan disconnectDelay,
        IScheduler disconnectScheduler
    )
    {
        return observable.ShareCore(window, Cache<TValue>.ReplaySubjectW, disconnectDelay, disconnectScheduler);
    }


    public static IObservable<TValue> ShareReplay<TValue>(this IObservable<TValue> observable, IScheduler scheduler) =>
        observable.ShareCore(scheduler, Cache<TValue>.ReplaySubjectS);

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        IScheduler scheduler,
        TimeSpan disconnectDelay
    )
    {
        return observable.ShareCore(scheduler, Cache<TValue>.ReplaySubjectS, disconnectDelay);
    }

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        IScheduler scheduler,
        TimeSpan disconnectDelay,
        IScheduler disconnectScheduler
    )
    {
        return observable.ShareCore(scheduler, Cache<TValue>.ReplaySubjectS, disconnectDelay, disconnectScheduler);
    }


    public static IObservable<TValue> ShareReplay<TValue>(
        this IObservable<TValue> observable,
        int bufferSize,
        TimeSpan window
    )
    {
        return observable.ShareCore((bufferSize, window), Cache<TValue>.ReplaySubjectBw);
    }

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        int bufferSize,
        TimeSpan window,
        TimeSpan disconnectDelay
    )
    {
        return observable.ShareCore((bufferSize, window), Cache<TValue>.ReplaySubjectBw, disconnectDelay);
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
            observable.ShareCore((bufferSize, window),
                Cache<TValue>.ReplaySubjectBw,
                disconnectDelay,
                disconnectScheduler
            );
    }


    public static IObservable<TValue> ShareReplay<TValue>(
        this IObservable<TValue> observable,
        int bufferSize,
        IScheduler scheduler
    )
    {
        return observable.ShareCore((bufferSize, scheduler), Cache<TValue>.ReplaySubjectBs);
    }

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        int bufferSize,
        IScheduler scheduler,
        TimeSpan disconnectDelay
    )
    {
        return observable.ShareCore((bufferSize, scheduler), Cache<TValue>.ReplaySubjectBs, disconnectDelay);
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
            observable.ShareCore(
                (bufferSize, scheduler),
                Cache<TValue>.ReplaySubjectBs,
                disconnectDelay,
                disconnectScheduler
            );
    }


    public static IObservable<TValue> ShareReplay<TValue>(
        this IObservable<TValue> observable,
        TimeSpan window,
        IScheduler scheduler
    )
    {
        return observable.ShareCore((window, scheduler), Cache<TValue>.ReplaySubjectWs);
    }

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        TimeSpan window,
        IScheduler scheduler,
        TimeSpan disconnectDelay
    )
    {
        return observable.ShareCore((window, scheduler), Cache<TValue>.ReplaySubjectWs, disconnectDelay);
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
            observable.ShareCore(
                (window, scheduler),
                Cache<TValue>.ReplaySubjectWs,
                disconnectDelay,
                disconnectScheduler
            );
    }


    public static IObservable<TValue> ShareReplay<TValue>(
        this IObservable<TValue> observable,
        int bufferSize,
        TimeSpan window,
        IScheduler scheduler
    )
    {
        return observable.ShareCore((bufferSize, window, scheduler), Cache<TValue>.ReplaySubject3);
    }

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        int bufferSize,
        TimeSpan window,
        IScheduler scheduler,
        TimeSpan disconnectDelay
    )
    {
        return observable.ShareCore((bufferSize, window, scheduler), Cache<TValue>.ReplaySubject3, disconnectDelay);
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
            observable.ShareCore(
                (bufferSize, window, scheduler),
                Cache<TValue>.ReplaySubject3,
                disconnectDelay,
                disconnectScheduler
            );
    }
}
