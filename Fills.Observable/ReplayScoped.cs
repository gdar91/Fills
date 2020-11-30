using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Fills
{
    public static partial class ExtensionsForObservable
    {
        public static IObservable<TElement> ReplayScoped<TElement>(
            this IObservable<TElement> observable,
            int bufferSize
        )
        {
            var connectableObservable =
                new ConnectableObservable<TElement, TElement>(
                    observable,
                    () => new ReplaySubject<TElement>(bufferSize)
                );

            return connectableObservable;
        }
    }


    internal sealed class ConnectableObservable<TSource, TResult> : IConnectableObservable<TResult>
    {
        private readonly Func<ISubject<TSource, TResult>> _subjectFactory;
        private readonly BehaviorSubject<ISubject<TSource, TResult>> _subjects;
        private readonly IObservable<TResult> _results;
        private readonly IObservable<TSource> _source;
        private readonly object _gate;

        private Connection? _connection;

        public ConnectableObservable(
            IObservable<TSource> source,
            Func<ISubject<TSource, TResult>> subjectFactory
        )
        {
            _subjectFactory = subjectFactory;
            _subjects = new BehaviorSubject<ISubject<TSource, TResult>>(_subjectFactory());
            _results = _subjects.Switch();
            _source = source.AsObservable();
            _gate = new object();
        }

        public IDisposable Connect()
        {
            lock (_gate)
            {
                if (_connection == null)
                {
                    var subscription = _source.SubscribeSafe(_subjects.Value);
                    var disposable = Disposable.Create(() =>
                    {
                        lock (_gate)
                        {
                            _subjects.OnNext(_subjectFactory());
                            subscription.Dispose();
                        }
                    });
                    _connection = new Connection(this, disposable);
                }

                return _connection;
            }
        }

        private sealed class Connection : IDisposable
        {
            private readonly ConnectableObservable<TSource, TResult> _parent;
            private IDisposable? _subscription;

            public Connection(
                ConnectableObservable<TSource, TResult> parent,
                IDisposable subscription
            )
            {
                _parent = parent;
                _subscription = subscription;
            }

            public void Dispose()
            {
                lock (_parent._gate)
                {
                    if (_subscription != null)
                    {
                        _subscription.Dispose();
                        _subscription = null;

                        _parent._connection = null;
                    }
                }
            }
        }

        public IDisposable Subscribe(IObserver<TResult> observer)
        {
            if (observer == null)
            {
                throw new ArgumentNullException(nameof(observer));
            }

            return _results.SubscribeSafe(observer);
        }
    }
}
