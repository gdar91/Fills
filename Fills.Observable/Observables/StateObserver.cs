using System.Reactive;

namespace Fills;

public sealed class StateObserver<TState, TElement> : ObserverBase<TElement>
{
    private readonly TState state;

    private readonly Action<TState, TElement> onNext;

    private readonly Action<TState, Exception> onError;

    private readonly Action<TState> onCompleted;


    public StateObserver(
        TState state,
        Action<TState, TElement> onNext,
        Action<TState, Exception> onError,
        Action<TState> onCompleted
    )
    {
        this.state = state;
        this.onNext = onNext;
        this.onError = onError;
        this.onCompleted = onCompleted;
    }


    protected override void OnNextCore(TElement value) => onNext(state, value);

    protected override void OnErrorCore(Exception error) => onError(state, error);

    protected override void OnCompletedCore() => onCompleted(state);
}
