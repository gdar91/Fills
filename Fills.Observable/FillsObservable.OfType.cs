namespace Fills;

public static partial class FillsObservableExtensions
{
    public static IObservable<TResult> OfType<TElement, TCastElement, TResult>(
        this IObservable<TElement> source,
        Func<TCastElement, TResult> resultSelector
    )
    {
        return new OfTypeObservable<TElement, TCastElement, TResult>(source, resultSelector);
    }

    public static IObservable<TResult> OfType<TElement, TCastElement, TResult>(
        this IObservable<TElement> source,
        Hint<TCastElement> castElementHint,
        Func<TCastElement, TResult> resultSelector
    )
    {
        return new OfTypeObservable<TElement, TCastElement, TResult>(source, resultSelector);
    }


    private sealed class OfTypeObservable<TElement, TCastElement, TResult> : IObservable<TResult>
    {
        private readonly IObservable<TElement> source;

        private readonly Func<TCastElement, TResult> resultSelector;


        public OfTypeObservable(IObservable<TElement> source, Func<TCastElement, TResult> resultSelector)
        {
            this.source = source;
            this.resultSelector = resultSelector;
        }


        public IDisposable Subscribe(IObserver<TResult> observer) => source.Subscribe(new Observer(this, observer));


        private sealed class Observer : IObserver<TElement>
        {
            private readonly OfTypeObservable<TElement, TCastElement, TResult> parent;

            private readonly IObserver<TResult> destination;


            public Observer(OfTypeObservable<TElement, TCastElement, TResult> parent, IObserver<TResult> destination)
            {
                this.parent = parent;
                this.destination = destination;
            }


            public void OnNext(TElement value)
            {
                try
                {
                    if (value is TCastElement castElement)
                    {
                        destination.OnNext(parent.resultSelector(castElement));
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




    public static IObservable<TResult> OfType<TArg, TElement, TCastElement, TResult>(
        this IObservable<TElement> source,
        TArg arg,
        Func<TArg, TCastElement, TResult> resultSelector
    )
    {
        return new OfTypeObservable<TArg, TElement, TCastElement, TResult>(arg, source, resultSelector);
    }

    public static IObservable<TResult> OfType<TArg, TElement, TCastElement, TResult>(
        this IObservable<TElement> source,
        TArg arg,
        Hint<TCastElement> castElementHint,
        Func<TArg, TCastElement, TResult> resultSelector
    )
    {
        return new OfTypeObservable<TArg, TElement, TCastElement, TResult>(arg, source, resultSelector);
    }


    private sealed class OfTypeObservable<TArg, TElement, TCastElement, TResult> : IObservable<TResult>
    {
        private readonly TArg arg;

        private readonly IObservable<TElement> source;

        private readonly Func<TArg, TCastElement, TResult> resultSelector;


        public OfTypeObservable(
            TArg arg,
            IObservable<TElement> source,
            Func<TArg, TCastElement, TResult> resultSelector
        )
        {
            this.arg = arg;
            this.source = source;
            this.resultSelector = resultSelector;
        }


        public IDisposable Subscribe(IObserver<TResult> observer) => source.Subscribe(new Observer(this, observer));


        private sealed class Observer : IObserver<TElement>
        {
            private readonly OfTypeObservable<TArg, TElement, TCastElement, TResult> parent;

            private readonly IObserver<TResult> destination;


            public Observer(
                OfTypeObservable<TArg, TElement, TCastElement, TResult> parent,
                IObserver<TResult> destination
            )
            {
                this.parent = parent;
                this.destination = destination;
            }


            public void OnNext(TElement value)
            {
                try
                {
                    if (value is TCastElement castElement)
                    {
                        destination.OnNext(parent.resultSelector(parent.arg, castElement));
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
