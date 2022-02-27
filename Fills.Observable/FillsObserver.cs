namespace Fills;

public static class FillsObserver
{
    public static FillsStateObserver<TState, TElement> Create<TState, TElement>(
        TState state,
        Action<TState, TElement> onNext
    )
    {
        return
            new(
                state,
                onNext,
                FillsObservableExtensions.Cache<TState>.EmptyOnError,
                FillsObservableExtensions.Cache<TState>.EmptyOnCompleted
            );
    }

    public static FillsStateObserver<TState, TElement> Create<TState, TElement>(
        TState state,
        Action<TState, TElement> onNext,
        Action<TState, Exception> onError
    )
    {
        return new(state, onNext, onError, FillsObservableExtensions.Cache<TState>.EmptyOnCompleted);
    }

    public static FillsStateObserver<TState, TElement> Create<TState, TElement>(
        TState state,
        Action<TState, TElement> onNext,
        Action<TState> onCompleted
    )
    {
        return new(state, onNext, FillsObservableExtensions.Cache<TState>.EmptyOnError, onCompleted);
    }

    public static FillsStateObserver<TState, TElement> Create<TState, TElement>(
        TState state,
        Action<TState, TElement> onNext,
        Action<TState, Exception> onError,
        Action<TState> onCompleted
    )
    {
        return new(state, onNext, onError, onCompleted);
    }


    public static FillsStateObserver<TState, TElement> Create<TState, TElement>(
        TState state,
        Action<TState, TElement> onNext,
        Hint<TElement> hint
    )
    {
        return
            new(
                state,
                onNext,
                FillsObservableExtensions.Cache<TState>.EmptyOnError,
                FillsObservableExtensions.Cache<TState>.EmptyOnCompleted
            );
    }

    public static FillsStateObserver<TState, TElement> Create<TState, TElement>(
        TState state,
        Action<TState, TElement> onNext,
        Action<TState, Exception> onError,
        Hint<TElement> hint
    )
    {
        return new(state, onNext, onError, FillsObservableExtensions.Cache<TState>.EmptyOnCompleted);
    }

    public static FillsStateObserver<TState, TElement> Create<TState, TElement>(
        TState state,
        Action<TState, TElement> onNext,
        Action<TState> onCompleted,
        Hint<TElement> hint
    )
    {
        return new(state, onNext, FillsObservableExtensions.Cache<TState>.EmptyOnError, onCompleted);
    }

    public static FillsStateObserver<TState, TElement> Create<TState, TElement>(
        TState state,
        Action<TState, TElement> onNext,
        Action<TState, Exception> onError,
        Action<TState> onCompleted,
        Hint<TElement> hint
    )
    {
        return new(state, onNext, onError, onCompleted);
    }
}
