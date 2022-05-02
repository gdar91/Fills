using System.Reactive;

namespace Fills;

public sealed class ArgObservable<TArg, TElement> : ObservableBase<TElement>
{
    private readonly TArg arg;

    private readonly Func<TArg, IObserver<TElement>, IDisposable> subscribe;


    public ArgObservable(TArg arg, Func<TArg, IObserver<TElement>, IDisposable> subscribe)
    {
        this.arg = arg;
        this.subscribe = subscribe;
    }


    protected override IDisposable SubscribeCore(IObserver<TElement> observer) => subscribe(arg, observer);
}
