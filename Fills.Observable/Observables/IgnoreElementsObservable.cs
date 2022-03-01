using System.Reactive;

namespace Fills;

public sealed class IgnoreElementsObservable<TElement, TResult> : ObservableBase<TResult>
{
    private readonly IObservable<TElement> observable;


    public IgnoreElementsObservable(IObservable<TElement> observable)
    {
        this.observable = observable;
    }


    protected override IDisposable SubscribeCore(IObserver<TResult> observer) =>
        observable.Subscribe(new Observer(observer));


    private sealed class Observer : ObserverBase<TElement>
    {
        private readonly IObserver<TResult> observer;


        public Observer(IObserver<TResult> observer)
        {
            this.observer = observer;
        }


        protected override void OnNextCore(TElement value)
        {
        }

        protected override void OnErrorCore(Exception error) => observer.OnError(error);

        protected override void OnCompletedCore() => observer.OnCompleted();
    }
}
