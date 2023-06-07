using System.Reactive;

namespace Fills;

public static class FillsObserver
{
    public static IObserver<TElement> Create<TArg, TElement>(TArg arg, Action<TArg, TElement> onNext)
    {
        return
            new CreateObserver<TArg, TElement>(
                arg,
                onNext,
                CreateHelpers<TArg>.EmptyOnError,
                CreateHelpers<TArg>.EmptyOnCompleted
            );
    }

    public static IObserver<TElement> Create<TArg, TElement>(
        TArg arg,
        Action<TArg, TElement> onNext,
        Hint<TElement> hint
    )
    {
        return
            new CreateObserver<TArg, TElement>(
                arg,
                onNext,
                CreateHelpers<TArg>.EmptyOnError,
                CreateHelpers<TArg>.EmptyOnCompleted
            );
    }


    public static IObserver<TElement> Create<TArg, TElement>(
        TArg arg,
        Action<TArg, TElement> onNext,
        Action<TArg> onCompleted
    )
    {
        return new CreateObserver<TArg, TElement>(arg, onNext, CreateHelpers<TArg>.EmptyOnError, onCompleted);
    }

    public static IObserver<TElement> Create<TArg, TElement>(
        TArg arg,
        Action<TArg, TElement> onNext,
        Action<TArg> onCompleted,
        Hint<TElement> hint
    )
    {
        return new CreateObserver<TArg, TElement>(arg, onNext, CreateHelpers<TArg>.EmptyOnError, onCompleted);
    }


    public static IObserver<TElement> Create<TArg, TElement>(
        TArg arg,
        Action<TArg, TElement> onNext,
        Action<TArg, Exception> onError
    )
    {
        return new CreateObserver<TArg, TElement>(arg, onNext, onError, CreateHelpers<TArg>.EmptyOnCompleted);
    }

    public static IObserver<TElement> Create<TArg, TElement>(
        TArg arg,
        Action<TArg, TElement> onNext,
        Action<TArg, Exception> onError,
        Hint<TElement> hint
    )
    {
        return new CreateObserver<TArg, TElement>(arg, onNext, onError, CreateHelpers<TArg>.EmptyOnCompleted);
    }


    public static IObserver<TElement> Create<TArg, TElement>(
        TArg arg,
        Action<TArg, TElement> onNext,
        Action<TArg, Exception> onError,
        Action<TArg> onCompleted
    )
    {
        return new CreateObserver<TArg, TElement>(arg, onNext, onError, onCompleted);
    }

    public static IObserver<TElement> Create<TArg, TElement>(
        TArg arg,
        Action<TArg, TElement> onNext,
        Action<TArg, Exception> onError,
        Action<TArg> onCompleted,
        Hint<TElement> hint
    )
    {
        return new CreateObserver<TArg, TElement>(arg, onNext, onError, onCompleted);
    }


    private sealed class CreateObserver<TArg, TElement> : ObserverBase<TElement>, IArgRef<TArg>
    {
        private readonly TArg arg;

        private readonly Action<TArg, TElement> onNext;

        private readonly Action<TArg, Exception> onError;

        private readonly Action<TArg> onCompleted;


        public CreateObserver(
            TArg arg,
            Action<TArg, TElement> onNext,
            Action<TArg, Exception> onError,
            Action<TArg> onCompleted
        )
        {
            this.arg = arg;
            this.onNext = onNext;
            this.onError = onError;
            this.onCompleted = onCompleted;
        }

        public CreateObserver(
            in TArg arg,
            Action<TArg, TElement> onNext,
            Action<TArg, Exception> onError,
            Action<TArg> onCompleted
        )
        {
            this.arg = arg;
            this.onNext = onNext;
            this.onError = onError;
            this.onCompleted = onCompleted;
        }


        public ref readonly TArg ArgRef => ref arg;

        public TArg Arg => arg;


        protected override void OnNextCore(TElement value) => onNext(arg, value);

        protected override void OnErrorCore(Exception error) => onError(arg, error);

        protected override void OnCompletedCore() => onCompleted(arg);
    }


    private static class CreateHelpers<TArg>
    {
        public static readonly Action<TArg, Exception> EmptyOnError = static (_, _) => { };

        public static readonly Action<TArg> EmptyOnCompleted = static _ => { };
    }
}
