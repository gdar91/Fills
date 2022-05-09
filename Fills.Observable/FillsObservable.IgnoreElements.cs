namespace Fills;

public static partial class FillsObservableExtensions
{
    public static IObservable<TResult> IgnoreElements<TElement, TResult>(this IObservable<TElement> source)
    {
        return new IgnoreElementsObservable<TElement, TResult>(source);
    }

    public static IObservable<TResult> IgnoreElements<TElement, TResult>(
        this IObservable<TElement> source,
        Hint<TResult> resultHint
    )
    {
        return new IgnoreElementsObservable<TElement, TResult>(source);
    }


    private sealed class IgnoreElementsObservable<TElement, TResult> : IObservable<TResult>
    {
        private readonly IObservable<TElement> source;


        public IgnoreElementsObservable(IObservable<TElement> source)
        {
            this.source = source;
        }


        public IDisposable Subscribe(IObserver<TResult> observer) => source.Subscribe(new Observer(observer));


        private sealed class Observer : IObserver<TElement>
        {
            private readonly IObserver<TResult> destination;


            public Observer(IObserver<TResult> destination)
            {
                this.destination = destination;
            }


            public void OnNext(TElement value)
            {
            }

            public void OnError(Exception error) => destination.OnError(error);

            public void OnCompleted() => destination.OnCompleted();
        }
    }
}
