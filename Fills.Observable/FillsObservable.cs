using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace Fills;

public static partial class FillsObservable
{
    public static IObservable<TElement> Return<TElement>(TElement value) => Observable.Return(value);

    public static IObservable<TElement> Return<TElement>(TElement value, IScheduler scheduler) =>
        Observable.Return(value, scheduler);


    public static IObservable<TElement> Empty<TElement>() => Observable.Empty<TElement>();

    public static IObservable<TElement> Empty<TElement>(IScheduler scheduler) => Observable.Empty<TElement>(scheduler);

    public static IObservable<TElement> Empty<TElement>(Hint<TElement> hint) => Observable.Empty<TElement>();

    public static IObservable<TElement> Empty<TElement>(IScheduler scheduler, Hint<TElement> hint) =>
        Observable.Empty<TElement>(scheduler);


    public static IObservable<TElement> Never<TElement>() => Observable.Never<TElement>();

    public static IObservable<TElement> Never<TElement>(Hint<TElement> hint) => Observable.Never<TElement>();


    public static IObservable<TElement> Throw<TElement>(Exception error) => Observable.Throw<TElement>(error);

    public static IObservable<TElement> Throw<TElement>(Exception error, Hint<TElement> hint) =>
        Observable.Throw<TElement>(error);

    public static IObservable<TElement> Throw<TElement>(Exception error, IScheduler scheduler) =>
        Observable.Throw<TElement>(error, scheduler);

    public static IObservable<TElement> Throw<TElement>(Exception error, IScheduler scheduler, Hint<TElement> hint) =>
        Observable.Throw<TElement>(error, scheduler);


    public static Hint<IObservable<TElement>> Hint<TElement>(Hint<TElement> hint) => default;

    public static Hint<TElement> UnHint<TElement>(Hint<IObservable<TElement>> hint) => default;
}
