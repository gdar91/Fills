using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Subjects;

namespace Fills;

public sealed class ShareObservable<TArg, TElement> : IObservable<TElement>, IArgRef<TArg>
{
    private readonly IObservable<TElement> source;

    private readonly TArg arg;

    private readonly Func<TArg, ISubject<TElement>> subjectFactory;

    private readonly TimeSpan disconnectDelay;

    private readonly IScheduler disconnectScheduler;


    private readonly object gate;

    private State state;


    public ShareObservable(
        IObservable<TElement> source,
        TArg arg,
        Func<TArg, ISubject<TElement>> subjectFactory,
        TimeSpan disconnectDelay,
        IScheduler disconnectScheduler
    )
    {
        this.source = source;
        this.arg = arg;
        this.subjectFactory = subjectFactory;
        this.disconnectDelay = disconnectDelay;
        this.disconnectScheduler = disconnectScheduler;

        gate = new();
        state = State.Initial;
    }

    public ShareObservable(
        IObservable<TElement> source,
        in TArg arg,
        Func<TArg, ISubject<TElement>> subjectFactory,
        TimeSpan disconnectDelay,
        IScheduler disconnectScheduler
    )
    {
        this.source = source;
        this.arg = arg;
        this.subjectFactory = subjectFactory;
        this.disconnectDelay = disconnectDelay;
        this.disconnectScheduler = disconnectScheduler;

        gate = new();
        state = State.Initial;
    }


    public ref readonly TArg ArgRef => ref arg;

    public TArg Arg => arg;


    public IDisposable Subscribe(IObserver<TElement> observer)
    {
        IDisposable subscription;

        ConnectionInfo? nullableConnectionInfo = null;

        lock (gate)
        {
            switch (state)
            {
                case { IsInitial: true }:
                {
                    var subject = subjectFactory(arg);
                    var connection = new SingleAssignmentDisposable();

                    subscription = subject.Subscribe(observer);
                    nullableConnectionInfo = new ConnectionInfo(subject, connection);

                    state = State.Connected(1L, subject, connection, false);
                }
                    break;

                case var s when
                    s.IsConnected(out var subscriptions, out var subject, out var connection, out var disconnected):
                {
                    subscription = subject.Subscribe(observer);
                    state = State.Connected(subscriptions + 1L, subject, connection, disconnected);
                }
                    break;

                case var s when s.IsDisconnecting(out var subject, out var connection, out var disposableReference):
                {
                    using var disconnectionDisposableResource = disposableReference.Disposable;

                    subscription = subject.Subscribe(observer);
                    state = State.Connected(1L, subject, connection, false);
                }
                    break;

                default:
                    throw new InvalidOperationException();
            }

            if (nullableConnectionInfo is { } connectionInfo)
            {
                connectionInfo.SingleAssignmentDisposable.Disposable =
                    source.SubscribeSafe(
                        FillsObserver.Create((this, connectionInfo.Subject), OnNext, OnError, OnCompleted)
                    );
            }
        }

        return Disposable.Create((this, subscription), Unsubscribe);
    }


    private bool OnFinal()
    {
        lock (gate)
        {
            switch (state)
            {
                case var s when s.IsConnected(out var subscriptions, out var subject, out var connection, out _):
                    state = State.Connected(subscriptions, subject, connection, true);
                    return true;

                case var s when s.IsDisconnecting(out _, out var connection, out var disposableReference):
                {
                    using var disconnectionDisposableResource = disposableReference.Disposable;
                    using var connectionResource = connection;
                    state = State.Initial;

                    return false;
                }

                default:
                    return true;
            }
        }
    }


    private static readonly
        Action<(ShareObservable<TArg, TElement> @this, ISubject<TElement> subject), TElement>
        OnNext;

    private static readonly
        Action<(ShareObservable<TArg, TElement> @this, ISubject<TElement> subject), Exception>
        OnError;

    private static readonly
        Action<(ShareObservable<TArg, TElement> @this, ISubject<TElement> subject)>
        OnCompleted;

    private static readonly
        Func<IScheduler, (ShareObservable<TArg, TElement> @this, DisposableReference disposableReference), IDisposable>
        Disconnect;

    private static readonly
        Action<(ShareObservable<TArg, TElement>, IDisposable subscription)>
        Unsubscribe;


    static ShareObservable()
    {
        OnNext = static (tuple, next) => tuple.subject.OnNext(next);

        OnError =
            static (tuple, error) =>
            {
                if (tuple.@this.OnFinal())
                {
                    tuple.subject.OnError(error);
                }
            };

        OnCompleted =
            static tuple =>
            {
                if (tuple.@this.OnFinal())
                {
                    tuple.subject.OnCompleted();
                }
            };

        Disconnect =
            static (_, tuple) =>
            {
                var (@this, disposableReference) = tuple;

                lock (@this.gate)
                {
                    if (
                        @this.state.IsDisconnecting(out var _, out var connection, out var storedDisposableReference) &&
                        ReferenceEquals(disposableReference, storedDisposableReference)
                    )
                    {
                        using var connectionResource = connection;
                        @this.state = State.Initial;
                    }
                }

                return Disposable.Empty;
            };

        Unsubscribe =
            static tuple =>
            {
                var (@this, subscription) = tuple;

                using var subscriptionResource = subscription;

                lock (@this.gate)
                {
                    if (
                        !@this.state.IsConnected(
                            out var subscriptions,
                            out var subject,
                            out var connection,
                            out var disconnected
                        )
                    )
                    {
                        return;
                    }

                    if (subscriptions > 1)
                    {
                        @this.state = State.Connected(subscriptions - 1L, subject, connection, disconnected);

                        return;
                    }

                    if (disconnected || @this.disconnectDelay == TimeSpan.Zero)
                    {
                        using var connectionResource = connection;
                        @this.state = State.Initial;

                        return;
                    }

                    var disposableReference = new DisposableReference();

                    @this.state = State.Disconnecting(subject, connection, disposableReference);

                    disposableReference.Disposable =
                        @this.disconnectScheduler.Schedule(
                            (@this, disposableReference),
                            @this.disconnectDelay,
                            Disconnect
                        );
                }
            };
    }


    private sealed class DisposableReference
    {
        public IDisposable Disposable { get; set; } = default!;
    }


    private readonly record struct ConnectionInfo(
        ISubject<TElement> Subject,
        SingleAssignmentDisposable SingleAssignmentDisposable
    );


    private readonly struct State
    {
        private readonly int tag;

        private readonly long subscriptions;

        private readonly ISubject<TElement>? subject;

        private readonly IDisposable? connection;

        private readonly bool disconnected;

        private readonly DisposableReference? disconnectingDisposableReference;


        private State(
            int tag,
            long subscriptions,
            ISubject<TElement>? subject,
            IDisposable? connection,
            bool disconnected,
            DisposableReference? disconnectingDisposableReference
        )
        {
            this.tag = tag;
            this.subscriptions = subscriptions;
            this.subject = subject;
            this.connection = connection;
            this.disconnected = disconnected;
            this.disconnectingDisposableReference = disconnectingDisposableReference;
        }


        public static State Initial { get; } = new(0, default, default, default, default, default);

        public bool IsInitial => tag == 0;


        public static State Connected(
            long subscriptions,
            ISubject<TElement> subject,
            IDisposable? connection,
            bool disconnected
        )
        {
            return new(1, subscriptions, subject, connection, disconnected, default);
        }

        public bool IsConnected(
            out long subscriptions,
            out ISubject<TElement> subject,
            out IDisposable connection,
            out bool disconnected
        )
        {
            if (tag == 1)
            {
                subscriptions = this.subscriptions;
                subject = this.subject!;
                connection = this.connection!;
                disconnected = this.disconnected;

                return true;
            }

            subscriptions = default;
            subject = default!;
            connection = default!;
            disconnected = default;

            return false;
        }


        public static State Disconnecting(
            ISubject<TElement> subject,
            IDisposable connection,
            DisposableReference disposableReference
        )
        {
            return new(2, default, subject, connection, default, disposableReference);
        }

        public bool IsDisconnecting(
            out ISubject<TElement> subject,
            out IDisposable connection,
            out DisposableReference disposableReference
        )
        {
            if (tag == 2)
            {
                subject = this.subject!;
                connection = this.connection!;
                disposableReference = disconnectingDisposableReference!;

                return true;
            }

            subject = default!;
            connection = default!;
            disposableReference = default!;

            return false;
        }
    }
}
