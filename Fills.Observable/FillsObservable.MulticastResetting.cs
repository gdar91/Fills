using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Fills;

public static partial class FillsObservableExtensions
{
    public static IObservable<TElement> MulticastResetting<TElement>(
        this IObservable<TElement> observable,
        Func<ISubject<TElement>> subjectFactory
    )
    {
        return observable.Multicast(new ResettingSubject<TElement>(subjectFactory));
    }




    public static IObservable<TElement> PublishResetting<TElement>(
        this IObservable<TElement> observable
    )
    {
        return observable.MulticastResetting(() => new Subject<TElement>());
    }

    public static IObservable<TElement> PublishResetting<TElement>(
        this IObservable<TElement> observable,
        TElement initialValue
    )
    {
        return observable.MulticastResetting(() => new BehaviorSubject<TElement>(initialValue));
    }




    public static IObservable<TElement> ReplayResetting<TElement>(
        this IObservable<TElement> observable
    )
    {
        return observable.MulticastResetting(() => new ReplaySubject<TElement>());
    }

    public static IObservable<TElement> ReplayResetting<TElement>(
        this IObservable<TElement> observable,
        int bufferSize
    )
    {
        return observable.MulticastResetting(() => new ReplaySubject<TElement>(bufferSize));
    }

    public static IObservable<TElement> ReplayResetting<TElement>(
        this IObservable<TElement> observable,
        IScheduler scheduler
    )
    {
        return observable.MulticastResetting(() => new ReplaySubject<TElement>(scheduler));
    }

    public static IObservable<TElement> ReplayResetting<TElement>(
        this IObservable<TElement> observable,
        TimeSpan window
    )
    {
        return observable.MulticastResetting(() => new ReplaySubject<TElement>(window));
    }

    public static IObservable<TElement> ReplayResetting<TElement>(
        this IObservable<TElement> observable,
        int bufferSize,
        IScheduler scheduler
    )
    {
        return observable.MulticastResetting(() => new ReplaySubject<TElement>(bufferSize, scheduler));
    }

    public static IObservable<TElement> ReplayResetting<TElement>(
        this IObservable<TElement> observable,
        int bufferSize,
        TimeSpan window
    )
    {
        return observable.MulticastResetting(() => new ReplaySubject<TElement>(bufferSize, window));
    }

    public static IObservable<TElement> ReplayResetting<TElement>(
        this IObservable<TElement> observable,
        TimeSpan window,
        IScheduler scheduler
    )
    {
        return observable.MulticastResetting(() => new ReplaySubject<TElement>(window, scheduler));
    }

    public static IObservable<TElement> ReplayResetting<TElement>(
        this IObservable<TElement> observable,
        int bufferSize,
        TimeSpan window,
        IScheduler scheduler
    )
    {
        return observable.MulticastResetting(() => new ReplaySubject<TElement>(bufferSize, window, scheduler));
    }
}
