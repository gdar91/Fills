using System.Reactive;
using System.Reactive.Disposables;

namespace Fills;

public sealed class ArgTaskObservable<TArg, TElement> : ObservableBase<TElement>
{
    private readonly TArg arg;

    private readonly Func<TArg, IObserver<TElement>, CancellationToken, Task<IDisposable>> subscribeAsync;


    public ArgTaskObservable(
        TArg arg,
        Func<TArg, IObserver<TElement>, CancellationToken, Task<IDisposable>> subscribeAsync
    )
    {
        this.arg = arg;
        this.subscribeAsync = subscribeAsync;
    }


    protected override IDisposable SubscribeCore(IObserver<TElement> observer)
    {
        var taskDisposeCompletionObserver = new TaskDisposeCompletionObserver<TElement>(observer);
        var cancellationTokenSource = new CancellationTokenSource();

        var task = subscribeAsync(arg, observer, cancellationTokenSource.Token);

        if (task.IsCompleted)
        {
            ArgTaskObservable.EmitTaskResult(task, taskDisposeCompletionObserver);
        }
        else
        {
            task.ContinueWith(
                ArgTaskObservable.Continuation,
                taskDisposeCompletionObserver,
                CancellationToken.None,
                TaskContinuationOptions.ExecuteSynchronously,
                TaskScheduler.Current
            );
        }

        var disposable =
            Disposable.Create(
                (taskDisposeCompletionObserver, cancellationTokenSource),
                TaskDisposeCompletionObserver<TElement>.Action
            );

        return disposable;
    }
}


internal static class ArgTaskObservable
{
    public static readonly BooleanDisposable BooleanDisposableTrue;

    public static readonly Action<Task<IDisposable>, object?> Continuation;


    static ArgTaskObservable()
    {
        var booleanDisposableTrue = new BooleanDisposable();
        booleanDisposableTrue.Dispose();
        BooleanDisposableTrue = booleanDisposableTrue;

        Continuation = static (t, observerObject) => EmitTaskResult(t, (IObserver<IDisposable>) observerObject!);
    }


    public static void EmitTaskResult<TResult>(Task<TResult> task, IObserver<TResult> subject)
    {
        switch (task.Status)
        {
            case TaskStatus.RanToCompletion:
                subject.OnNext(task.Result);
                subject.OnCompleted();
                break;
            case TaskStatus.Faulted:
                subject.OnError(
                    task.Exception!.InnerException is { } exception
                        ? exception
                        : task.Exception
                );
                break;
            case TaskStatus.Canceled:
                subject.OnError(new TaskCanceledException(task));
                break;
        }
    }
}


internal sealed class TaskDisposeCompletionObserver<TResult> : IObserver<IDisposable>, IDisposable
{
    public static readonly Action<ValueTuple<TaskDisposeCompletionObserver<TResult>, CancellationTokenSource>> Action =
        static tuple =>
        {
            tuple.Item2.Cancel();
            tuple.Item1.Dispose();
        };




    private readonly IObserver<TResult> _observer;

    private IDisposable? _disposable;


    public TaskDisposeCompletionObserver(IObserver<TResult> observer)
    {
        _observer = observer;
    }


    public void OnNext(IDisposable value)
    {
        var oldNullable = Interlocked.CompareExchange(ref _disposable, value, null);

        if (oldNullable is { } old)
        {
            if (ReferenceEquals(old, ArgTaskObservable.BooleanDisposableTrue))
            {
                value.Dispose();
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }

    public void OnError(Exception error)
    {
        _observer.OnError(error);
    }

    public void OnCompleted()
    {
    }


    public void Dispose()
    {
        var old = Interlocked.Exchange(ref _disposable, ArgTaskObservable.BooleanDisposableTrue);

        if (!ReferenceEquals(old, ArgTaskObservable.BooleanDisposableTrue))
        {
            old?.Dispose();
        }
    }
}
