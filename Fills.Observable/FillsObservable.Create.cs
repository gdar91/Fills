using System.Reactive;
using System.Reactive.Disposables;

namespace Fills;

public static partial class FillsObservable
{
    public static IObservable<TElement> Create<TArg, TElement>(
        TArg arg,
        Func<TArg, IObserver<TElement>, IDisposable> subscribe
    )
    {
        return new CreateObservable<TArg, TElement>(arg, subscribe);
    }

    public static IObservable<TElement> Create<TArg, TElement>(
        TArg arg,
        Func<TArg, IObserver<TElement>, IDisposable> subscribe,
        Hint<TElement> hint
    )
    {
        return new CreateObservable<TArg, TElement>(arg, subscribe);
    }


    private sealed class CreateObservable<TArg, TElement> : ObservableBase<TElement>
    {
        private readonly TArg arg;

        private readonly Func<TArg, IObserver<TElement>, IDisposable> subscribe;


        public CreateObservable(TArg arg, Func<TArg, IObserver<TElement>, IDisposable> subscribe)
        {
            this.arg = arg;
            this.subscribe = subscribe;
        }


        protected override IDisposable SubscribeCore(IObserver<TElement> observer) => subscribe(arg, observer);
    }




    public static IObservable<TElement> Create<TArg, TElement>(
        TArg arg,
        Func<TArg, IObserver<TElement>, CancellationToken, Task<IDisposable>> subscribeAsync
    )
    {
        return new CreateTaskObservable<TArg,TElement>(arg, subscribeAsync);
    }

    public static IObservable<TElement> Create<TArg, TElement>(
        TArg arg,
        Func<TArg, IObserver<TElement>, CancellationToken, Task<IDisposable>> subscribeAsync,
        Hint<TElement> hint
    )
    {
        return new CreateTaskObservable<TArg,TElement>(arg, subscribeAsync);
    }


    private sealed class CreateTaskObservable<TArg, TElement> : ObservableBase<TElement>
    {
        private readonly TArg arg;

        private readonly Func<TArg, IObserver<TElement>, CancellationToken, Task<IDisposable>> subscribeAsync;


        public CreateTaskObservable(
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
                CreateTaskObservable.EmitTaskResult(task, taskDisposeCompletionObserver);
            }
            else
            {
                task.ContinueWith(
                    CreateTaskObservable.Continuation,
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


    private static class CreateTaskObservable
    {
        public static readonly BooleanDisposable BooleanDisposableTrue;

        public static readonly Action<Task<IDisposable>, object?> Continuation;


        static CreateTaskObservable()
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


    private sealed class TaskDisposeCompletionObserver<TResult> : IObserver<IDisposable>, IDisposable
    {
        public static readonly
            Action<ValueTuple<TaskDisposeCompletionObserver<TResult>, CancellationTokenSource>>
            Action =
                static tuple =>
                {
                    tuple.Item2.Cancel();
                    tuple.Item1.Dispose();
                };


        private readonly IObserver<TResult> observer;

        private IDisposable? disposable;


        public TaskDisposeCompletionObserver(IObserver<TResult> observer)
        {
            this.observer = observer;
        }


        public void OnNext(IDisposable value)
        {
            var oldNullable = Interlocked.CompareExchange(ref disposable, value, null);

            if (oldNullable is { } old)
            {
                if (ReferenceEquals(old, CreateTaskObservable.BooleanDisposableTrue))
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
            observer.OnError(error);
        }

        public void OnCompleted()
        {
        }


        public void Dispose()
        {
            var old = Interlocked.Exchange(ref disposable, CreateTaskObservable.BooleanDisposableTrue);

            if (!ReferenceEquals(old, CreateTaskObservable.BooleanDisposableTrue))
            {
                old?.Dispose();
            }
        }
    }
}
