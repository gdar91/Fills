namespace Fills;

public static partial class FillsObservableExtensions
{
    public static IObservable<TResult> Select<TArg, TElement, TResult>(
        this IObservable<TElement> source,
        TArg arg,
        Func<TArg, TElement, TResult> selector
    )
    {
        return new SelectObservable<TArg, TElement, TResult>(arg, source, selector);
    }


    public static IObservable<TResult> Select<TArg, TElement, TResult>(
        this IObservable<TElement> source,
        in TArg arg,
        Func<TArg, TElement, TResult> selector
    )
    {
        return new SelectObservable<TArg, TElement, TResult>(in arg, source, selector);
    }


    private sealed class SelectObservable<TArg, TElement, TResult> : IObservable<TResult>, IArgRef<TArg>
    {
        private readonly TArg arg;

        private readonly IObservable<TElement> source;

        private readonly Func<TArg, TElement, TResult> selector;


        public SelectObservable(TArg arg, IObservable<TElement> source, Func<TArg, TElement, TResult> selector)
        {
            this.arg = arg;
            this.source = source;
            this.selector = selector;
        }

        public SelectObservable(in TArg arg, IObservable<TElement> source, Func<TArg, TElement, TResult> selector)
        {
            this.arg = arg;
            this.source = source;
            this.selector = selector;
        }


        public ref readonly TArg ArgRef => ref arg;

        public TArg Arg => arg;


        public IDisposable Subscribe(IObserver<TResult> observer) => source.Subscribe(new Observer(this, observer));


        private sealed class Observer : IObserver<TElement>
        {
            private readonly SelectObservable<TArg, TElement, TResult> parent;

            private readonly IObserver<TResult> destination;


            public Observer(SelectObservable<TArg, TElement, TResult> parent, IObserver<TResult> destination)
            {
                this.parent = parent;
                this.destination = destination;
            }


            public void OnNext(TElement value)
            {
                try
                {
                    destination.OnNext(parent.selector(parent.arg, value));
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
