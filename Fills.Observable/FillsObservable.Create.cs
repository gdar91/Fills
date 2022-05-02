namespace Fills;

public static partial class FillsObservable
{
    public static IObservable<TElement> Create<TArg, TElement>(
        TArg arg,
        Func<TArg, IObserver<TElement>, IDisposable> subscribe
    )
    {
        return new ArgObservable<TArg, TElement>(arg, subscribe);
    }

    public static IObservable<TElement> Create<TArg, TElement>(
        TArg arg,
        Func<TArg, IObserver<TElement>, IDisposable> subscribe,
        Hint<TElement> hint
    )
    {
        return new ArgObservable<TArg, TElement>(arg, subscribe);
    }


    public static IObservable<TElement> Create<TArg, TElement>(
        TArg arg,
        Func<TArg, IObserver<TElement>, CancellationToken, Task<IDisposable>> subscribeAsync
    )
    {
        return new ArgTaskObservable<TArg,TElement>(arg, subscribeAsync);
    }

    public static IObservable<TElement> Create<TArg, TElement>(
        TArg arg,
        Func<TArg, IObserver<TElement>, CancellationToken, Task<IDisposable>> subscribeAsync,
        Hint<TElement> hint
    )
    {
        return new ArgTaskObservable<TArg,TElement>(arg, subscribeAsync);
    }
}
