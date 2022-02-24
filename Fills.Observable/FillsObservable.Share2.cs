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




    public static IObservable<TValue> Share2<TValue>(this IObservable<TValue> observable) =>
        observable.ShareCore2(0, Lambdas<TValue>.Subject);

    public static IObservable<TValue> ShareKeep<TValue>(this IObservable<TValue> observable, TimeSpan disconnectDelay)
    {
        return observable.ShareCore2(0, Lambdas<TValue>.Subject, disconnectDelay);
    }

    public static IObservable<TValue> ShareKeep<TValue>(
        this IObservable<TValue> observable,
        TimeSpan disconnectDelay,
        IScheduler disconnectScheduler
    )
    {
        return observable.ShareCore2(0, Lambdas<TValue>.Subject, disconnectDelay, disconnectScheduler);
    }


    public static IObservable<TValue> Share2<TValue>(this IObservable<TValue> observable, TValue initialValue) =>
        observable.ShareCore2(initialValue, Lambdas<TValue>.BehaviorSubject);

    public static IObservable<TValue> ShareKeep<TValue>(
        this IObservable<TValue> observable,
        TValue initialValue,
        TimeSpan disconnectDelay
    )
    {
        return observable.ShareCore2(initialValue, Lambdas<TValue>.BehaviorSubject, disconnectDelay);
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
                Lambdas<TValue>.BehaviorSubject,
                disconnectDelay,
                disconnectScheduler
            );
    }




    public static IObservable<TValue> ShareReplay2<TValue>(this IObservable<TValue> observable) =>
        observable.ShareCore2(0, Lambdas<TValue>.ReplaySubject0);

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        TimeSpan disconnectDelay
    )
    {
        return observable.ShareCore2(0, Lambdas<TValue>.ReplaySubject0, disconnectDelay);
    }

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        TimeSpan disconnectDelay,
        IScheduler disconnectScheduler
    )
    {
        return observable.ShareCore2(0, Lambdas<TValue>.ReplaySubject0, disconnectDelay, disconnectScheduler);
    }


    public static IObservable<TValue> ShareReplay2<TValue>(this IObservable<TValue> observable, int bufferSize) =>
        observable.ShareCore2(bufferSize, Lambdas<TValue>.ReplaySubjectB);

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        int bufferSize,
        TimeSpan disconnectDelay
    )
    {
        return observable.ShareCore2(bufferSize, Lambdas<TValue>.ReplaySubjectB, disconnectDelay);
    }

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        int bufferSize,
        TimeSpan disconnectDelay,
        IScheduler disconnectScheduler
    )
    {
        return observable.ShareCore2(bufferSize, Lambdas<TValue>.ReplaySubjectB, disconnectDelay, disconnectScheduler);
    }


    public static IObservable<TValue> ShareReplay2<TValue>(this IObservable<TValue> observable, TimeSpan window) =>
        observable.ShareCore2(window, Lambdas<TValue>.ReplaySubjectW);

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        TimeSpan window,
        TimeSpan disconnectDelay
    )
    {
        return observable.ShareCore2(window, Lambdas<TValue>.ReplaySubjectW, disconnectDelay);
    }

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        TimeSpan window,
        TimeSpan disconnectDelay,
        IScheduler disconnectScheduler
    )
    {
        return observable.ShareCore2(window, Lambdas<TValue>.ReplaySubjectW, disconnectDelay, disconnectScheduler);
    }


    public static IObservable<TValue> ShareReplay2<TValue>(this IObservable<TValue> observable, IScheduler scheduler) =>
        observable.ShareCore2(scheduler, Lambdas<TValue>.ReplaySubjectS);

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        IScheduler scheduler,
        TimeSpan disconnectDelay
    )
    {
        return observable.ShareCore2(scheduler, Lambdas<TValue>.ReplaySubjectS, disconnectDelay);
    }

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        IScheduler scheduler,
        TimeSpan disconnectDelay,
        IScheduler disconnectScheduler
    )
    {
        return observable.ShareCore2(scheduler, Lambdas<TValue>.ReplaySubjectS, disconnectDelay, disconnectScheduler);
    }


    public static IObservable<TValue> ShareReplay2<TValue>(
        this IObservable<TValue> observable,
        int bufferSize,
        TimeSpan window
    )
    {
        return observable.ShareCore2((bufferSize, window), Lambdas<TValue>.ReplaySubjectBw);
    }

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        int bufferSize,
        TimeSpan window,
        TimeSpan disconnectDelay
    )
    {
        return observable.ShareCore2((bufferSize, window), Lambdas<TValue>.ReplaySubjectBw, disconnectDelay);
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
            observable.ShareCore2((bufferSize, window),
                Lambdas<TValue>.ReplaySubjectBw,
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
        return observable.ShareCore2((bufferSize, scheduler), Lambdas<TValue>.ReplaySubjectBs);
    }

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        int bufferSize,
        IScheduler scheduler,
        TimeSpan disconnectDelay
    )
    {
        return observable.ShareCore2((bufferSize, scheduler), Lambdas<TValue>.ReplaySubjectBs, disconnectDelay);
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
                Lambdas<TValue>.ReplaySubjectBs,
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
        return observable.ShareCore2((window, scheduler), Lambdas<TValue>.ReplaySubjectWs);
    }

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        TimeSpan window,
        IScheduler scheduler,
        TimeSpan disconnectDelay
    )
    {
        return observable.ShareCore2((window, scheduler), Lambdas<TValue>.ReplaySubjectWs, disconnectDelay);
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
                Lambdas<TValue>.ReplaySubjectWs,
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
        return observable.ShareCore2((bufferSize, window, scheduler), Lambdas<TValue>.ReplaySubject3);
    }

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        int bufferSize,
        TimeSpan window,
        IScheduler scheduler,
        TimeSpan disconnectDelay
    )
    {
        return observable.ShareCore2((bufferSize, window, scheduler), Lambdas<TValue>.ReplaySubject3, disconnectDelay);
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
                Lambdas<TValue>.ReplaySubject3,
                disconnectDelay,
                disconnectScheduler
            );
    }
}
