using System.Reactive;

namespace Fills;

public sealed class StateObservable<TState, TElement> : ObservableBase<TElement>
{
    private readonly TState state;

    private readonly Func<TState, IObserver<TElement>, IDisposable> subscribe;


    public StateObservable(TState state, Func<TState, IObserver<TElement>, IDisposable> subscribe)
    {
        this.state = state;
        this.subscribe = subscribe;
    }


    protected override IDisposable SubscribeCore(IObserver<TElement> observer) => subscribe(state, observer);
}