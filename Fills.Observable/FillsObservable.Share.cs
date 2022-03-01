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
            new ShareObservable<TSubjectState, TValue>(
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
            new ShareObservable<TSubjectState, TValue>(
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
            new ShareObservable<TSubjectState, TValue>(
                observable,
                subjectState,
                subjectFactory,
                disconnectDelay,
                disconnectScheduler
            );
    }


    private static class ShareModule<T>
    {
        public static readonly Func<int, Subject<T>> Subject = static _ => new Subject<T>();

        public static readonly Func<T, BehaviorSubject<T>> BehaviorSubject =
            static initialValue => new BehaviorSubject<T>(initialValue);

        public static readonly Func<int, ReplaySubject<T>> ReplaySubject0 = static _ => new ReplaySubject<T>();

        public static readonly Func<int, ReplaySubject<T>> ReplaySubjectB =
            static bufferSize => new ReplaySubject<T>(bufferSize);

        public static readonly Func<TimeSpan, ReplaySubject<T>> ReplaySubjectW =
            static window => new ReplaySubject<T>(window);

        public static readonly Func<IScheduler, ReplaySubject<T>> ReplaySubjectS =
            static scheduler => new ReplaySubject<T>(scheduler);

        public static readonly Func<(int bufferSize, TimeSpan window), ReplaySubject<T>> ReplaySubjectBw =
            static tuple => new ReplaySubject<T>(tuple.bufferSize, tuple.window);

        public static readonly Func<(int bufferSize, IScheduler scheduler), ReplaySubject<T>> ReplaySubjectBs =
            static tuple => new ReplaySubject<T>(tuple.bufferSize, tuple.scheduler);

        public static readonly Func<(TimeSpan window, IScheduler scheduler), ReplaySubject<T>> ReplaySubjectWs =
            static tuple => new ReplaySubject<T>(tuple.window, tuple.scheduler);

        public static readonly
            Func<(int bufferSize, TimeSpan window, IScheduler scheduler), ReplaySubject<T>>
            ReplaySubject3 =
                static tuple => new ReplaySubject<T>(tuple.bufferSize, tuple.window, tuple.scheduler);
    }




    public static IObservable<TValue> Share<TValue>(this IObservable<TValue> observable) =>
        observable.ShareCore(0, ShareModule<TValue>.Subject);

    public static IObservable<TValue> ShareKeep<TValue>(this IObservable<TValue> observable, TimeSpan disconnectDelay)
    {
        return observable.ShareCore(0, ShareModule<TValue>.Subject, disconnectDelay);
    }

    public static IObservable<TValue> ShareKeep<TValue>(
        this IObservable<TValue> observable,
        TimeSpan disconnectDelay,
        IScheduler disconnectScheduler
    )
    {
        return observable.ShareCore(0, ShareModule<TValue>.Subject, disconnectDelay, disconnectScheduler);
    }


    public static IObservable<TValue> Share<TValue>(this IObservable<TValue> observable, TValue initialValue) =>
        observable.ShareCore(initialValue, ShareModule<TValue>.BehaviorSubject);

    public static IObservable<TValue> ShareKeep<TValue>(
        this IObservable<TValue> observable,
        TValue initialValue,
        TimeSpan disconnectDelay
    )
    {
        return observable.ShareCore(initialValue, ShareModule<TValue>.BehaviorSubject, disconnectDelay);
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
                ShareModule<TValue>.BehaviorSubject,
                disconnectDelay,
                disconnectScheduler
            );
    }




    public static IObservable<TValue> ShareReplay<TValue>(this IObservable<TValue> observable) =>
        observable.ShareCore(0, ShareModule<TValue>.ReplaySubject0);

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        TimeSpan disconnectDelay
    )
    {
        return observable.ShareCore(0, ShareModule<TValue>.ReplaySubject0, disconnectDelay);
    }

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        TimeSpan disconnectDelay,
        IScheduler disconnectScheduler
    )
    {
        return observable.ShareCore(0, ShareModule<TValue>.ReplaySubject0, disconnectDelay, disconnectScheduler);
    }


    public static IObservable<TValue> ShareReplay<TValue>(this IObservable<TValue> observable, int bufferSize) =>
        observable.ShareCore(bufferSize, ShareModule<TValue>.ReplaySubjectB);

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        int bufferSize,
        TimeSpan disconnectDelay
    )
    {
        return observable.ShareCore(bufferSize, ShareModule<TValue>.ReplaySubjectB, disconnectDelay);
    }

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        int bufferSize,
        TimeSpan disconnectDelay,
        IScheduler disconnectScheduler
    )
    {
        return observable.ShareCore(bufferSize, ShareModule<TValue>.ReplaySubjectB, disconnectDelay, disconnectScheduler);
    }


    public static IObservable<TValue> ShareReplay<TValue>(this IObservable<TValue> observable, TimeSpan window) =>
        observable.ShareCore(window, ShareModule<TValue>.ReplaySubjectW);

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        TimeSpan window,
        TimeSpan disconnectDelay
    )
    {
        return observable.ShareCore(window, ShareModule<TValue>.ReplaySubjectW, disconnectDelay);
    }

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        TimeSpan window,
        TimeSpan disconnectDelay,
        IScheduler disconnectScheduler
    )
    {
        return observable.ShareCore(window, ShareModule<TValue>.ReplaySubjectW, disconnectDelay, disconnectScheduler);
    }


    public static IObservable<TValue> ShareReplay<TValue>(this IObservable<TValue> observable, IScheduler scheduler) =>
        observable.ShareCore(scheduler, ShareModule<TValue>.ReplaySubjectS);

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        IScheduler scheduler,
        TimeSpan disconnectDelay
    )
    {
        return observable.ShareCore(scheduler, ShareModule<TValue>.ReplaySubjectS, disconnectDelay);
    }

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        IScheduler scheduler,
        TimeSpan disconnectDelay,
        IScheduler disconnectScheduler
    )
    {
        return observable.ShareCore(scheduler, ShareModule<TValue>.ReplaySubjectS, disconnectDelay, disconnectScheduler);
    }


    public static IObservable<TValue> ShareReplay<TValue>(
        this IObservable<TValue> observable,
        int bufferSize,
        TimeSpan window
    )
    {
        return observable.ShareCore((bufferSize, window), ShareModule<TValue>.ReplaySubjectBw);
    }

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        int bufferSize,
        TimeSpan window,
        TimeSpan disconnectDelay
    )
    {
        return observable.ShareCore((bufferSize, window), ShareModule<TValue>.ReplaySubjectBw, disconnectDelay);
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
                ShareModule<TValue>.ReplaySubjectBw,
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
        return observable.ShareCore((bufferSize, scheduler), ShareModule<TValue>.ReplaySubjectBs);
    }

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        int bufferSize,
        IScheduler scheduler,
        TimeSpan disconnectDelay
    )
    {
        return observable.ShareCore((bufferSize, scheduler), ShareModule<TValue>.ReplaySubjectBs, disconnectDelay);
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
                ShareModule<TValue>.ReplaySubjectBs,
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
        return observable.ShareCore((window, scheduler), ShareModule<TValue>.ReplaySubjectWs);
    }

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        TimeSpan window,
        IScheduler scheduler,
        TimeSpan disconnectDelay
    )
    {
        return observable.ShareCore((window, scheduler), ShareModule<TValue>.ReplaySubjectWs, disconnectDelay);
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
                ShareModule<TValue>.ReplaySubjectWs,
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
        return observable.ShareCore((bufferSize, window, scheduler), ShareModule<TValue>.ReplaySubject3);
    }

    public static IObservable<TValue> ShareReplayKeep<TValue>(
        this IObservable<TValue> observable,
        int bufferSize,
        TimeSpan window,
        IScheduler scheduler,
        TimeSpan disconnectDelay
    )
    {
        return observable.ShareCore((bufferSize, window, scheduler), ShareModule<TValue>.ReplaySubject3, disconnectDelay);
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
                ShareModule<TValue>.ReplaySubject3,
                disconnectDelay,
                disconnectScheduler
            );
    }
}
