using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Fills;

public static partial class FillsObservableExtensions
{
    public static IObservable<TElement> KeepSubscription<TElement>(
        this IObservable<TElement> observable,
        DateTimeOffset dueTime
    )
    {
        return Observable.Create<TElement>(observer =>
        {
            var subscription = observable.Subscribe(observer);

            return Disposable.Create(() =>
                Observable
                    .Timer(dueTime)
                    .Finally(subscription.Dispose)
                    .Subscribe()
            );
        });
    }


    public static IObservable<TElement> KeepSubscription<TElement>(
        this IObservable<TElement> observable,
        TimeSpan dueTime
    )
    {
        return Observable.Create<TElement>(observer =>
        {
            var subscription = observable.Subscribe(observer);

            return Disposable.Create(() =>
                Observable
                    .Timer(dueTime)
                    .Finally(subscription.Dispose)
                    .Subscribe()
            );
        });
    }


    public static IObservable<TElement> KeepSubscription<TElement>(
        this IObservable<TElement> observable,
        DateTimeOffset dueTime,
        IScheduler scheduler
    )
    {
        return Observable.Create<TElement>(observer =>
        {
            var subscription = observable.Subscribe(observer);

            return Disposable.Create(() =>
                Observable
                    .Timer(dueTime, scheduler)
                    .Finally(subscription.Dispose)
                    .Subscribe()
            );
        });
    }


    public static IObservable<TElement> KeepSubscription<TElement>(
        this IObservable<TElement> observable,
        TimeSpan dueTime,
        IScheduler scheduler
    )
    {
        return Observable.Create<TElement>(observer =>
        {
            var subscription = observable.Subscribe(observer);

            return Disposable.Create(() =>
                Observable
                    .Timer(dueTime, scheduler)
                    .Finally(subscription.Dispose)
                    .Subscribe()
            );
        });
    }
}
