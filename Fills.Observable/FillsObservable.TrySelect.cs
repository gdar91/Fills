namespace Fills;

public static partial class FillsObservableExtensions
{
    public static IObservable<TResult> TrySelect<TElement, TResult>(
        this IObservable<TElement> source,
        TrySelector<TElement, TResult> trySelector
    )
    {
        return new TrySelectObservable<TElement, TResult>(source, trySelector);
    }

    public static IObservable<TResult> TrySelect<TElement, TResult>(
        this IObservable<TElement> source,
        TrySelector<TElement, TResult> trySelector,
        Hint<TResult> resultHint
    )
    {
        return new TrySelectObservable<TElement, TResult>(source, trySelector);
    }


    private sealed class TrySelectObservable<TElement, TResult> : IObservable<TResult>
    {
        private readonly IObservable<TElement> source;

        private readonly TrySelector< TElement, TResult> trySelector;


        public TrySelectObservable(IObservable<TElement> source, TrySelector<TElement, TResult> trySelector)
        {
            this.source = source;
            this.trySelector = trySelector;
        }


        public IDisposable Subscribe(IObserver<TResult> observer) => source.Subscribe(new Observer(this, observer));


        private sealed class Observer : IObserver<TElement>
        {
            private readonly TrySelectObservable<TElement, TResult> parent;

            private readonly IObserver<TResult> destination;


            public Observer(TrySelectObservable<TElement, TResult> parent, IObserver<TResult> destination)
            {
                this.parent = parent;
                this.destination = destination;
            }


            public void OnNext(TElement value)
            {
                try
                {
                    if (parent.trySelector(value, out var result))
                    {
                        destination.OnNext(result);
                    }
                }
                catch (Exception error)
                {
                    destination.OnError(error);
                }
            }

            public void OnError(Exception error) => destination.OnError(error);

            public void OnCompleted() => destination.OnCompleted();
        }
    }




    public static IObservable<TResult> TrySelect<TArg, TElement, TResult>(
        this IObservable<TElement> source,
        TArg arg,
        TrySelector<TArg, TElement, TResult> trySelector
    )
    {
        return new TrySelectObservable<TArg, TElement, TResult>(arg, source, trySelector);
    }

    public static IObservable<TResult> TrySelect<TArg, TElement, TResult>(
        this IObservable<TElement> source,
        TArg arg,
        TrySelector<TArg, TElement, TResult> trySelector,
        Hint<TResult> resultHint
    )
    {
        return new TrySelectObservable<TArg, TElement, TResult>(arg, source, trySelector);
    }


    private sealed class TrySelectObservable<TArg, TElement, TResult> : IObservable<TResult>
    {
        private readonly TArg arg;

        private readonly IObservable<TElement> source;

        private readonly TrySelector<TArg, TElement, TResult> trySelector;


        public TrySelectObservable(
            TArg arg,
            IObservable<TElement> source,
            TrySelector<TArg, TElement, TResult> trySelector
        )
        {
            this.arg = arg;
            this.source = source;
            this.trySelector = trySelector;
        }


        public IDisposable Subscribe(IObserver<TResult> observer) => source.Subscribe(new Observer(this, observer));


        private sealed class Observer : IObserver<TElement>
        {
            private readonly TrySelectObservable<TArg, TElement, TResult> parent;

            private readonly IObserver<TResult> destination;


            public Observer(TrySelectObservable<TArg, TElement, TResult> parent, IObserver<TResult> destination)
            {
                this.parent = parent;
                this.destination = destination;
            }


            public void OnNext(TElement value)
            {
                try
                {
                    if (parent.trySelector(parent.arg, value, out var result))
                    {
                        destination.OnNext(result);
                    }
                }
                catch (Exception error)
                {
                    destination.OnError(error);
                }
            }

            public void OnError(Exception error) => destination.OnError(error);

            public void OnCompleted() => destination.OnCompleted();
        }
    }
}
