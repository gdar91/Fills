using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Fills;

public sealed class SharedObservable<TSubjectState, TValue> : IObservable<TValue>
{
    private readonly IObservable<TValue> source;

    private readonly TSubjectState subjectState;

    private readonly Func<TSubjectState, ISubject<TValue>> subjectFactory;

    private readonly TimeSpan disconnectDelay;

    private readonly object gate;

    private readonly BehaviorSubject<ISubject<TValue>> subjects;

    private readonly IObservable<TValue> results;


    private State state;


    public SharedObservable(
        IObservable<TValue> source,
        TSubjectState subjectState,
        Func<TSubjectState, ISubject<TValue>> subjectFactory,
        TimeSpan disconnectDelay
    )
    {
        this.source = source;
        this.subjectState = subjectState;
        this.subjectFactory = subjectFactory;
        this.disconnectDelay = disconnectDelay;
        gate = new();
        subjects = new(CreateNewSubject());
        results = subjects.Switch();
        state = State.Initial;
    }


    private ISubject<TValue> CreateNewSubject() => subjectFactory(subjectState);


    public IDisposable Subscribe(IObserver<TValue> observer)
    {
        var subscription = Observable.Using(SubscriptionResource, _ => results).SubscribeSafe(observer);

        lock (gate)
        {
            switch (state)
            {
                case { IsInitial: true }:
                {
                    var connection = Connect();
                    state = State.Connected(1L, connection, false);
                }
                    break;

                case var s when s.IsConnected(out var subscriptions, out var connection, out var instantDisconnect):
                {
                    state = State.Connected(subscriptions + 1L, connection, instantDisconnect);
                }
                    break;

                case var s when s.IsDisconnecting(out var connection, out var cancellationTokenSource):
                {
                    state = State.Connected(1L, connection, false);
                    cancellationTokenSource.Cancel();
                }
                    break;
            }
        }

        return subscription;
    }




    private IDisposable Connect()
    {
        var subject = subjects.Value;

        var connection =
            source.SubscribeSafe(
                Observer.Create<TValue>(
                    subject.OnNext,
                    error =>
                    {
                        if (OnFinal())
                        {
                            subject.OnError(error);
                        }
                    },
                    () =>
                    {
                        if (OnFinal())
                        {
                            subject.OnCompleted();
                        }
                    }
                )
            );

        return connection;
    }


    private bool OnFinal()
    {
        lock (gate)
        {
            switch (state)
            {
                case var s when s.IsConnected(out var subscriptions, out var connection, out _):
                {
                    state = State.Connected(subscriptions, connection, true);

                    return true;
                }

                case var s when s.IsDisconnecting(out var connection, out var cancellationTokenSource):
                {
                    connection.Dispose();
                    subjects.OnNext(CreateNewSubject());
                    state = State.Initial;
                    cancellationTokenSource.Cancel();

                    return false;
                }

                default:
                    return true;
            }
        }
    }


    private IDisposable SubscriptionResource()
    {
        return Disposable.Create(this, static parent =>
        {
            lock (parent.gate)
            {
                if (parent.state.IsConnected(out var subscriptions, out var connection, out var instantDisconnect))
                {
                    if (subscriptions > 1L)
                    {
                        parent.state = State.Connected(subscriptions - 1L, connection, instantDisconnect);
                    }
                    else
                    {
                        if (instantDisconnect || parent.disconnectDelay <= TimeSpan.Zero)
                        {
                            connection.Dispose();
                            parent.subjects.OnNext(parent.CreateNewSubject());
                            parent.state = State.Initial;
                        }
                        else
                        {
                            var cancellationTokenSource = new CancellationTokenSource();

                            parent.state = State.Disconnecting(connection, cancellationTokenSource);

                            Task.Run(
                                async () =>
                                {
                                    await Task.Delay(parent.disconnectDelay, cancellationTokenSource.Token);

                                    lock (parent.gate)
                                    {
                                        if (parent.state.IsDisconnecting(out var connection, out _))
                                        {
                                            connection.Dispose();
                                            parent.subjects.OnNext(parent.CreateNewSubject());
                                            parent.state = State.Initial;
                                        }
                                    }
                                },
                                cancellationTokenSource.Token
                            );
                        }
                    }
                }
            }
        });
    }




    private readonly struct State
    {
        private readonly int tag;

        private readonly long subscriptions;

        private readonly IDisposable? connection;

        private readonly bool instantDisconnect;

        private readonly CancellationTokenSource? cancellationTokenSource;


        private State(
            int tag,
            long subscriptions,
            IDisposable? connection,
            bool instantDisconnect,
            CancellationTokenSource? cancellationTokenSource
        )
        {
            this.tag = tag;
            this.subscriptions = subscriptions;
            this.connection = connection;
            this.instantDisconnect = instantDisconnect;
            this.cancellationTokenSource = cancellationTokenSource;
        }


        public bool IsInitial => tag == 0;

        public bool IsConnected(out long subscriptions, out IDisposable connection, out bool instantDisconnect)
        {
            if (tag == 1)
            {
                subscriptions = this.subscriptions;
                connection = this.connection!;
                instantDisconnect = this.instantDisconnect;

                return true;
            }
            
            subscriptions = default;
            connection = default!;
            instantDisconnect = default;

            return false;
        }

        public bool IsDisconnecting(out IDisposable connection, out CancellationTokenSource cancellationTokenSource)
        {
            if (tag == 2)
            {
                connection = this.connection!;
                cancellationTokenSource = this.cancellationTokenSource!;

                return true;
            }
            
            connection = this.connection!;
            cancellationTokenSource = this.cancellationTokenSource!;

            return false;
        }


        public static State Initial { get; } = new(0, 0L, null, false, null);

        public static State Connected(long subscriptions, IDisposable connection, bool instantDisconnect) =>
            new(1, subscriptions, connection, instantDisconnect, null);

        public static State Disconnecting(IDisposable connection, CancellationTokenSource cancellationTokenSource) =>
            new(2, 0L, connection, false, cancellationTokenSource);
    }
}
