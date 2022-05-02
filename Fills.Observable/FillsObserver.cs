namespace Fills;

public static class FillsObserver
{
    public static ArgObserver<TArg, TElement> Create<TArg, TElement>(TArg arg, Action<TArg, TElement> onNext) =>
        new(arg, onNext, CreateModule<TArg>.EmptyOnError, CreateModule<TArg>.EmptyOnCompleted);

    public static ArgObserver<TArg, TElement> Create<TArg, TElement>(
        TArg arg,
        Action<TArg, TElement> onNext,
        Action<TArg, Exception> onError
    )
    {
        return new(arg, onNext, onError, CreateModule<TArg>.EmptyOnCompleted);
    }

    public static ArgObserver<TArg, TElement> Create<TArg, TElement>(
        TArg arg,
        Action<TArg, TElement> onNext,
        Action<TArg> onCompleted
    )
    {
        return new(arg, onNext, CreateModule<TArg>.EmptyOnError, onCompleted);
    }

    public static ArgObserver<TArg, TElement> Create<TArg, TElement>(
        TArg arg,
        Action<TArg, TElement> onNext,
        Action<TArg, Exception> onError,
        Action<TArg> onCompleted
    )
    {
        return new(arg, onNext, onError, onCompleted);
    }


    public static ArgObserver<TArg, TElement> Create<TArg, TElement>(
        TArg arg,
        Action<TArg, TElement> onNext,
        Hint<TElement> hint
    )
    {
        return new(arg, onNext, CreateModule<TArg>.EmptyOnError, CreateModule<TArg>.EmptyOnCompleted);
    }

    public static ArgObserver<TArg, TElement> Create<TArg, TElement>(
        TArg arg,
        Action<TArg, TElement> onNext,
        Action<TArg, Exception> onError,
        Hint<TElement> hint
    )
    {
        return new(arg, onNext, onError, CreateModule<TArg>.EmptyOnCompleted);
    }

    public static ArgObserver<TArg, TElement> Create<TArg, TElement>(
        TArg arg,
        Action<TArg, TElement> onNext,
        Action<TArg> onCompleted,
        Hint<TElement> hint
    )
    {
        return new(arg, onNext, CreateModule<TArg>.EmptyOnError, onCompleted);
    }

    public static ArgObserver<TArg, TElement> Create<TArg, TElement>(
        TArg arg,
        Action<TArg, TElement> onNext,
        Action<TArg, Exception> onError,
        Action<TArg> onCompleted,
        Hint<TElement> hint
    )
    {
        return new(arg, onNext, onError, onCompleted);
    }


    private static class CreateModule<T>
    {
        public static readonly Action<T, Exception> EmptyOnError = static (_, _) => { };

        public static readonly Action<T> EmptyOnCompleted = static _ => { };
    }
}
