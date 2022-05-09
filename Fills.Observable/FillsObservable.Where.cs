namespace Fills;

public static partial class FillsObservableExtensions
{
    public static IObservable<TElement> Where<TArg, TElement>(
        this IObservable<TElement> source,
        TArg arg,
        Func<TArg, TElement, bool> predicate
    )
    {
        return new WhereObservable<TArg, TElement>(arg, source, predicate);
    }


    private sealed class WhereObservable<TArg, TElement> : IObservable<TElement>
    {
        private readonly TArg arg;

        private readonly IObservable<TElement> source;

        private readonly Func<TArg, TElement, bool> predicate;


        public WhereObservable(TArg arg, IObservable<TElement> source, Func<TArg, TElement, bool> predicate)
        {
            this.arg = arg;
            this.source = source;
            this.predicate = predicate;
        }


        public IDisposable Subscribe(IObserver<TElement> observer) => source.Subscribe(new Observer(this, observer));


        private sealed class Observer : IObserver<TElement>
        {
            private readonly WhereObservable<TArg, TElement> parent;

            private readonly IObserver<TElement> destination;


            public Observer(WhereObservable<TArg, TElement> parent, IObserver<TElement> destination)
            {
                this.parent = parent;
                this.destination = destination;
            }


            public void OnNext(TElement value)
            {
                try
                {
                    if (parent.predicate(parent.arg, value))
                    {
                        destination.OnNext(value);
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
