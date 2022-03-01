namespace Fills;

public static class FillsObserver
{
    public static StateObserver<TState, TElement> Create<TState, TElement>(
        TState state,
        Action<TState, TElement> onNext
    )
    {
        return new(state, onNext, CreateModule<TState>.EmptyOnError, CreateModule<TState>.EmptyOnCompleted);
    }

    public static StateObserver<TState, TElement> Create<TState, TElement>(
        TState state,
        Action<TState, TElement> onNext,
        Action<TState, Exception> onError
    )
    {
        return new(state, onNext, onError, CreateModule<TState>.EmptyOnCompleted);
    }

    public static StateObserver<TState, TElement> Create<TState, TElement>(
        TState state,
        Action<TState, TElement> onNext,
        Action<TState> onCompleted
    )
    {
        return new(state, onNext, CreateModule<TState>.EmptyOnError, onCompleted);
    }

    public static StateObserver<TState, TElement> Create<TState, TElement>(
        TState state,
        Action<TState, TElement> onNext,
        Action<TState, Exception> onError,
        Action<TState> onCompleted
    )
    {
        return new(state, onNext, onError, onCompleted);
    }


    public static StateObserver<TState, TElement> Create<TState, TElement>(
        TState state,
        Action<TState, TElement> onNext,
        Hint<TElement> hint
    )
    {
        return new(state, onNext, CreateModule<TState>.EmptyOnError, CreateModule<TState>.EmptyOnCompleted);
    }

    public static StateObserver<TState, TElement> Create<TState, TElement>(
        TState state,
        Action<TState, TElement> onNext,
        Action<TState, Exception> onError,
        Hint<TElement> hint
    )
    {
        return new(state, onNext, onError, CreateModule<TState>.EmptyOnCompleted);
    }

    public static StateObserver<TState, TElement> Create<TState, TElement>(
        TState state,
        Action<TState, TElement> onNext,
        Action<TState> onCompleted,
        Hint<TElement> hint
    )
    {
        return new(state, onNext, CreateModule<TState>.EmptyOnError, onCompleted);
    }

    public static StateObserver<TState, TElement> Create<TState, TElement>(
        TState state,
        Action<TState, TElement> onNext,
        Action<TState, Exception> onError,
        Action<TState> onCompleted,
        Hint<TElement> hint
    )
    {
        return new(state, onNext, onError, onCompleted);
    }


    private static class CreateModule<T>
    {
        public static readonly Action<T, Exception> EmptyOnError = static (_, _) => { };

        public static readonly Action<T> EmptyOnCompleted = static _ => { };
    }
}
