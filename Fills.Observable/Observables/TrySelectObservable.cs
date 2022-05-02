using System.Reactive;

namespace Fills;

public sealed class TrySelectObservable<TElement, TResult> : ObservableBase<TResult>
{
    private readonly IObservable<TElement> observable;

    private readonly TrySelector<TElement, TResult> trySelector;


    public TrySelectObservable(IObservable<TElement> observable, TrySelector<TElement, TResult> trySelector)
    {
        this.observable = observable;
        this.trySelector = trySelector;
    }


    protected override IDisposable SubscribeCore(IObserver<TResult> observer) =>
        observable.Subscribe(new Observer(this, observer));


    private sealed class Observer : ObserverBase<TElement>
    {
        private readonly TrySelectObservable<TElement, TResult> parent;

        private readonly IObserver<TResult> observer;


        public Observer(TrySelectObservable<TElement, TResult> parent, IObserver<TResult> observer)
        {
            this.parent = parent;
            this.observer = observer;
        }


        protected override void OnNextCore(TElement value)
        {
            try
            {
                if (parent.trySelector(value, out var result))
                {
                    observer.OnNext(result);
                }
            }
            catch (Exception e)
            {
                observer.OnError(e);
            }
        }

        protected override void OnErrorCore(Exception error) => observer.OnError(error);

        protected override void OnCompletedCore() => observer.OnCompleted();
    }
}


public sealed class TrySelectObservable<TArg, TElement, TResult> : ObservableBase<TResult>
{
    private readonly IObservable<TElement> observable;
    
    private readonly TArg arg;
    
    private readonly TrySelector<TArg, TElement, TResult> trySelector;


    public TrySelectObservable(
        IObservable<TElement> observable,
        TArg arg,
        TrySelector<TArg, TElement, TResult> trySelector
    )
    {
        this.observable = observable;
        this.arg = arg;
        this.trySelector = trySelector;
    }


    protected override IDisposable SubscribeCore(IObserver<TResult> observer) =>
        observable.Subscribe(new Observer(this, observer));


    private sealed class Observer : ObserverBase<TElement>
    {
        private readonly TrySelectObservable<TArg, TElement, TResult> parent;

        private readonly IObserver<TResult> observer;


        public Observer(TrySelectObservable<TArg, TElement, TResult> parent, IObserver<TResult> observer)
        {
            this.parent = parent;
            this.observer = observer;
        }


        protected override void OnNextCore(TElement value)
        {
            try
            {
                if (parent.trySelector(parent.arg, value, out var result))
                {
                    observer.OnNext(result);
                }
            }
            catch (Exception e)
            {
                observer.OnError(e);
            }
        }

        protected override void OnErrorCore(Exception error) => observer.OnError(error);

        protected override void OnCompletedCore() => observer.OnCompleted();
    }
}
