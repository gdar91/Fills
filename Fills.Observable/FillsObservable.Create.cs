namespace Fills;

public static partial class FillsObservable
{
    public static IObservable<TElement> Create<TState, TElement>(
        TState state,
        Func<TState, IObserver<TElement>, IDisposable> subscribe
    )
    {
        return new StateObservable<TState, TElement>(state, subscribe);
    }

    public static IObservable<TElement> Create<TState, TElement>(
        TState state,
        Func<TState, IObserver<TElement>, IDisposable> subscribe,
        Hint<TElement> hint
    )
    {
        return new StateObservable<TState, TElement>(state, subscribe);
    }


    public static IObservable<TElement> Create<TState, TElement>(
        TState state,
        Func<TState, IObserver<TElement>, CancellationToken, Task<IDisposable>> subscribeAsync
    )
    {
        return new StateTaskObservable<TState,TElement>(state, subscribeAsync);
    }

    public static IObservable<TElement> Create<TState, TElement>(
        TState state,
        Func<TState, IObserver<TElement>, CancellationToken, Task<IDisposable>> subscribeAsync,
        Hint<TElement> hint
    )
    {
        return new StateTaskObservable<TState,TElement>(state, subscribeAsync);
    }
}
