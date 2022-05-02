using System.Reactive;

namespace Fills;

public sealed class ArgObserver<TArg, TElement> : ObserverBase<TElement>
{
    private readonly TArg arg;

    private readonly Action<TArg, TElement> onNext;

    private readonly Action<TArg, Exception> onError;

    private readonly Action<TArg> onCompleted;


    public ArgObserver(
        TArg arg,
        Action<TArg, TElement> onNext,
        Action<TArg, Exception> onError,
        Action<TArg> onCompleted
    )
    {
        this.arg = arg;
        this.onNext = onNext;
        this.onError = onError;
        this.onCompleted = onCompleted;
    }


    protected override void OnNextCore(TElement value) => onNext(arg, value);

    protected override void OnErrorCore(Exception error) => onError(arg, error);

    protected override void OnCompletedCore() => onCompleted(arg);
}
