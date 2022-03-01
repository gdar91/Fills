using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Fills;

public sealed class ShareObservable<TSubjectState, TValue> : IObservable<TValue>
{
    private readonly IObservable<TValue> source;

    private readonly TSubjectState subjectState;

    private readonly Func<TSubjectState, ISubject<TValue>> subjectFactory;

    private readonly TimeSpan disconnectDelay;

    private readonly IScheduler disconnectScheduler;

    private readonly object gate;

    private readonly BehaviorSubject<ISubject<TValue>> subjects;

    private readonly IObservable<TValue> results;

    private readonly IObservable<TValue> observable;


    private State state;


    public ShareObservable(
        IObservable<TValue> source,
        TSubjectState subjectState,
        Func<TSubjectState, ISubject<TValue>> subjectFactory,
        TimeSpan disconnectDelay,
        IScheduler disconnectScheduler
    )
    {
        this.source = source;
        this.subjectState = subjectState;
        this.subjectFactory = subjectFactory;
        this.disconnectDelay = disconnectDelay;
        this.disconnectScheduler = disconnectScheduler;

        gate = new();
        subjects = new(CreateNewSubject());
        results = subjects.Switch();
        observable = Observable.Using(SubscriptionResource, _ => results);

        state = State.Initial;
    }


    public IDisposable Subscribe(IObserver<TValue> observer) => observable.SubscribeSafe(observer);


    private ISubject<TValue> CreateNewSubject() => subjectFactory(subjectState);


    private IDisposable SubscriptionResource()
    {
        lock (gate)
        {
            switch (state)
            {
                case { IsInitial: true }:
                    state = State.Connected(1L, Connect(), false);
                    break;

                case var s when s.IsConnected(out var subscriptions, out var connection, out var instantDisconnect):
                    state = State.Connected(subscriptions + 1L, connection, instantDisconnect);
                    break;

                case var s when s.IsDisconnecting(out var connection, out var disconnectionResource):
                    state = State.Connected(1L, connection, false);
                    disconnectionResource.Dispose();
                    break;
            }
        }

        return Disposable.Create(this, DisconnectActionLambda);
    }




    private IDisposable Connect()
    {
        var subject = subjects.Value;

        var connection =
            source.SubscribeSafe(
                FillsObserver.Create(
                    (this, subject),
                    OnNext,
                    OnError,
                    OnCompleted,
                    Hint.Of<TValue>()
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
                    state = State.Connected(subscriptions, connection, true);
                    return true;

                case var s when s.IsDisconnecting(out var connection, out var disconnectionResource):
                    connection.Dispose();
                    subjects.OnNext(CreateNewSubject());
                    state = State.Initial;
                    disconnectionResource.Dispose();
                    return false;

                default:
                    return true;
            }
        }
    }




    private static readonly Action<(ShareObservable<TSubjectState, TValue>, ISubject<TValue> subject), TValue> OnNext =
        static (tuple, next) => tuple.subject.OnNext(next);

    private static readonly
        Action<(ShareObservable<TSubjectState, TValue>, ISubject<TValue> subject), Exception>
        OnError =
            static (tuple, error) =>
            {
                if (tuple.Item1.OnFinal())
                {
                    tuple.subject.OnError(error);
                }
            };

    private static readonly Action<(ShareObservable<TSubjectState, TValue>, ISubject<TValue> subject)> OnCompleted =
        static tuple =>
        {
            if (tuple.Item1.OnFinal())
            {
                tuple.subject.OnCompleted();
            }
        };

    private static readonly Action<ShareObservable<TSubjectState, TValue>> DisconnectActionLambda =
        static parent =>
        {
            lock (parent.gate)
            {
                if (!parent.state.IsConnected(out var subscriptions, out var connection, out var instantDisconnect))
                {
                    return;
                }

                if (subscriptions > 1L)
                {
                    parent.state = State.Connected(subscriptions - 1L, connection, instantDisconnect);

                    return;
                }

                if (instantDisconnect || parent.disconnectDelay <= TimeSpan.Zero)
                {
                    connection.Dispose();
                    parent.subjects.OnNext(parent.CreateNewSubject());
                    parent.state = State.Initial;

                    return;
                }


                var resource = new DisposableReference();

                resource.Disposable =
                    parent.disconnectScheduler.Schedule(
                        (parent, resource),
                        parent.disconnectDelay,
                        static (_, tuple) =>
                        {
                            var (parent, resource) = tuple;

                            lock (parent.gate)
                            {
                                if (
                                    parent.state.IsDisconnecting(out var connection, out var disconnectionResource) &&
                                    ReferenceEquals(disconnectionResource, resource.Disposable)
                                )
                                {
                                    connection.Dispose();
                                    parent.subjects.OnNext(parent.CreateNewSubject());
                                    parent.state = State.Initial;
                                }
                            }

                            return Disposable.Empty;
                        }
                    );

                parent.state = State.Disconnecting(connection, resource.Disposable!);
            }
        };




    private sealed class DisposableReference
    {
        public IDisposable? Disposable { get; set; }
    }


    private readonly struct State
    {
        private readonly int tag;

        private readonly long subscriptions;

        private readonly IDisposable? connection;

        private readonly bool instantDisconnect;

        private readonly IDisposable? disconnectionResource;


        private State(
            int tag,
            long subscriptions,
            IDisposable? connection,
            bool instantDisconnect,
            IDisposable? disconnectionResource
        )
        {
            this.tag = tag;
            this.subscriptions = subscriptions;
            this.connection = connection;
            this.instantDisconnect = instantDisconnect;
            this.disconnectionResource = disconnectionResource;
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

        public bool IsDisconnecting(out IDisposable connection, out IDisposable disconnectionResource)
        {
            if (tag == 2)
            {
                connection = this.connection!;
                disconnectionResource = this.disconnectionResource!;

                return true;
            }

            connection = this.connection!;
            disconnectionResource = this.disconnectionResource!;

            return false;
        }


        public static State Initial { get; } = new(0, 0L, null, false, null);

        public static State Connected(long subscriptions, IDisposable connection, bool instantDisconnect) =>
            new(1, subscriptions, connection, instantDisconnect, null);

        public static State Disconnecting(IDisposable connection, IDisposable disconnectionResource) =>
            new(2, 0L, connection, false, disconnectionResource);
    }
}
