using System.Collections.Immutable;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Channels;

namespace Fills;

public static partial class FillsObservableExtensions
{
    internal static class Cache<T1, T2, T3>
    {
        public static readonly
            Func<(IObservable<T2> source, T1 state, Func<T1, T2, T3> selector), IObserver<T3>, IDisposable>
            SelectSubscribe;

        public static readonly
            Func<(IObservable<T2> source, T1 state, TrySelector<T1, T2, T3> trySelector), IObserver<T3>, IDisposable>
            TrySelectSubscribe;


        static Cache()
        {
            SelectSubscribe =
                static (tuple, observer) =>
                    tuple.source.Subscribe(
                        FillsObserver.Create(
                            (tuple.state, tuple.selector, observer),
                            static (tuple, element) => tuple.observer.OnNext(tuple.selector(tuple.state, element)),
                            static (tuple, error) => tuple.observer.OnError(error),
                            static tuple => tuple.observer.OnCompleted(),
                            Hint.Of<T2>()
                        )
                    );

            TrySelectSubscribe =
                static (tuple, observer) =>
                    tuple.source.Subscribe(
                        FillsObserver.Create(
                            (tuple.state, tuple.trySelector, observer),
                            static (tuple, element) =>
                            {
                                if (tuple.trySelector(tuple.state, element, out var result))
                                {
                                    tuple.observer.OnNext(result);
                                }
                            },
                            static (tuple, error) => tuple.observer.OnError(error),
                            static tuple => tuple.observer.OnCompleted(),
                            Hint.Of<T2>()
                        )
                    );
        }
    }


    internal static class Cache<T1, T2>
    {
        public static readonly
            Func<(T1 state, Func<T1, IObservable<T2>> observableFactory), IObserver<T2>, IDisposable>
            DeferSubscribe;

        public static readonly
            Func<
                (T1 state, Func<T1, CancellationToken, Task<IObservable<T2>>> observableFactoryAsync),
                IObserver<T2>,
                CancellationToken,
                Task<IDisposable>
            >
            DeferSubscribeAsync;

        public static readonly
            Func<
                (T1 state, Func<T1, CancellationToken, Task<T2>> funcAsync),
                IObserver<T2>,
                CancellationToken,
                Task<IDisposable>
            >
            FromAsyncSubscribeAsync;

        public static readonly
            Func<
                (T1 state,  Func<T1, CancellationToken, IAsyncEnumerable<T2>> asyncEnumerableFactory),
                IObserver<T2>,
                CancellationToken,
                Task<IDisposable>
            >
            FromAsyncEnumerableSubscribeAsync;

        public static readonly Func<IObservable<T1>, IObserver<T2>, IDisposable> IgnoreElementsSubscribe;

        public static readonly Func<KeepManyState<T1, T2>, IEnumerable<T1>, KeepManyState<T1, T2>> KeepManyStateFolder;
        public static readonly
            TrySelector<KeepManyState<T1, T2>, IObservable<ValueTuple<T2, bool>>>
            KeepManyStateTrySelector;
        public static readonly
            TrySelector<Func<T1, IObservable<T2>>, ValueTuple<T1, bool>, IObservable<T2>>
            KeepManyGroupTrySelector;

        public static readonly
            Func<(IObservable<T2> source, T1 state, Func<T1, T2, bool> predicate), IObserver<T2>, IDisposable>
            WhereSubscribe;

        public static readonly
            Func<(IObservable<T1> source, TrySelector<T1, T2> trySelector), IObserver<T2>, IDisposable>
            TrySelectSubscribe;


        static Cache()
        {
            DeferSubscribe =
                static (tuple, observer) =>
                {
                    try
                    {
                        return tuple.observableFactory(tuple.state).Subscribe(observer);
                    }
                    catch (Exception exception)
                    {
                        observer.OnError(exception);

                        return Disposable.Empty;
                    }
                };

            DeferSubscribeAsync =
                static async (tuple, observer, cancellationToken) =>
                {
                    try
                    {
                        var observable =
                            await tuple
                                .observableFactoryAsync(tuple.state, cancellationToken)
                                .ConfigureAwait(false);

                        return observable.Subscribe(observer);
                    }
                    catch (Exception exception)
                    {
                        observer.OnError(exception);

                        return Disposable.Empty;
                    }
                };

            FromAsyncSubscribeAsync =
                static async (tuple, observer, cancellationToken) =>
                {
                    try
                    {
                        var element = await tuple.funcAsync(tuple.state, cancellationToken).ConfigureAwait(false);

                        observer.OnNext(element);
                        observer.OnCompleted();
                    }
                    catch (Exception exception)
                    {
                        observer.OnError(exception);
                    }

                    return Disposable.Empty;
                };

            FromAsyncEnumerableSubscribeAsync =
                static async (tuple, observer, cancellationToken) =>
                {
                    var asyncEnumerable = tuple.asyncEnumerableFactory(tuple.state, cancellationToken);

                    await foreach (var item in asyncEnumerable.WithCancellation(cancellationToken))
                    {
                        observer.OnNext(item);
                    }

                    observer.OnCompleted();

                    return Disposable.Empty;
                };

            IgnoreElementsSubscribe =
                static (source, observer) =>
                    source.Subscribe(
                        FillsObserver.Create(
                            observer,
                            static (_, _) => { },
                            static (observer, error) => observer.OnError(error),
                            static observer => observer.OnCompleted(),
                            Hint.Of<T1>()
                        )
                    );

            KeepManyStateFolder =
                static (state, element) =>
                {
                    var previousSet = state.Set;
                    var currentSet = element.Select(state.KeySelector).ToImmutableHashSet();
                    return
                        new(
                            state.KeySelector,
                            currentSet,
                            previousSet.Except(currentSet),
                            currentSet.Except(previousSet)
                        );
                };
            KeepManyStateTrySelector =
                static (KeepManyState<T1, T2> state, out IObservable<(T2, bool)> result) =>
                {
                    if (state.ItemsRemoved.Count + state.ItemsAdded.Count > 0)
                    {
                        result =
                            Observable.Concat(
                                state.ItemsRemoved.Select(Cache<T2>.TupleWithFalse).ToObservable(),
                                state.ItemsAdded.Select(Cache<T2>.TupleWithTrue).ToObservable()
                            );
                        return true;
                    }
                    result = default!;
                    return false;
                };
            KeepManyGroupTrySelector =
                static (Func<T1, IObservable<T2>> observableSelector, (T1, bool) tuple, out IObservable<T2> result) =>
                {
                    if (tuple.Item2)
                    {
                        result = observableSelector(tuple.Item1);
                        return true;
                    }
                    result = default!;
                    return false;
                };

            WhereSubscribe =
                static (tuple, observer) =>
                    tuple.source.Subscribe(
                        FillsObserver.Create(
                            (tuple.state, tuple.predicate, observer),
                            static (tuple, element) =>
                            {
                                if (tuple.predicate(tuple.state, element))
                                {
                                    tuple.observer.OnNext(element);
                                }
                            },
                            static (tuple, error) => tuple.observer.OnError(error),
                            static tuple => tuple.observer.OnCompleted(),
                            Hint.Of<T2>()
                        )
                    );

            TrySelectSubscribe =
                static (tuple, observer) =>
                    tuple.source.Subscribe(
                        FillsObserver.Create(
                            (tuple.trySelector, observer),
                            static (tuple, element) =>
                            {
                                if (tuple.trySelector(element, out var result))
                                {
                                    tuple.observer.OnNext(result);
                                }
                            },
                            static (tuple, error) => tuple.observer.OnError(error),
                            static tuple => tuple.observer.OnCompleted(),
                            Hint.Of<T1>()
                        )
                    );
        }
    }


    internal static class Cache<T>
    {
        public static readonly Func<T, T> Identity;

        public static readonly Func<T, ValueTuple<T, bool>> TupleWithFalse;
        public static readonly Func<T, ValueTuple<T, bool>> TupleWithTrue;

        public static readonly Func<ValueTuple<T, bool>, T> KeepManyKeySelector;
        public static readonly
            Func<IGroupedObservable<T, ValueTuple<T, bool>>, IObservable<ValueTuple<T, bool>>>
            KeepManyDurationSelector;
        public static readonly Func<ValueTuple<T, bool>, bool> KeepManyGroupNegativePredicate;

        public static readonly Action<Channel<T>, T> RunAsChannelReaderOnNext;
        public static readonly Action<Channel<T>, Exception> RunAsChannelReaderOnError;
        public static readonly Action<Channel<T>> RunAsChannelReaderOnCompleted;

        public static readonly
            Func<Func<CancellationToken, IAsyncEnumerable<T>>, IObserver<T>, CancellationToken, Task<IDisposable>>
            FromAsyncEnumerableSubscribeAsync;

        public static readonly Func<int, Subject<T>> Subject;
        public static readonly Func<T, BehaviorSubject<T>> BehaviorSubject;
        public static readonly Func<int, ReplaySubject<T>> ReplaySubject0;
        public static readonly Func<int, ReplaySubject<T>> ReplaySubjectB;
        public static readonly Func<TimeSpan, ReplaySubject<T>> ReplaySubjectW;
        public static readonly Func<IScheduler, ReplaySubject<T>> ReplaySubjectS;
        public static readonly Func<(int bufferSize, TimeSpan window), ReplaySubject<T>> ReplaySubjectBw;
        public static readonly Func<(int bufferSize, IScheduler scheduler), ReplaySubject<T>> ReplaySubjectBs;
        public static readonly Func<(TimeSpan window, IScheduler scheduler), ReplaySubject<T>> ReplaySubjectWs;
        public static readonly
            Func<(int bufferSize, TimeSpan window, IScheduler scheduler), ReplaySubject<T>>
            ReplaySubject3;

        public static readonly Action<T, Exception> EmptyOnError;
        public static readonly Action<T> EmptyOnCompleted;


        static Cache()
        {
            Identity = static value => value;

            TupleWithFalse = static value => (value, false);
            TupleWithTrue = static value => (value, true);

            KeepManyGroupNegativePredicate = static valueTuple => !valueTuple.Item2;
            KeepManyKeySelector = static valueTuple => valueTuple.Item1;
            KeepManyDurationSelector = static group => group.Where(KeepManyGroupNegativePredicate).Take(1);

            RunAsChannelReaderOnNext = static (_, _) => { };
            RunAsChannelReaderOnError = static (channel, error) => channel.Writer.TryComplete(error);
            RunAsChannelReaderOnCompleted = static channel => channel.Writer.TryComplete();

            FromAsyncEnumerableSubscribeAsync =
                static async (asyncEnumerableFactory, observer, cancellationToken) =>
                {
                    var asyncEnumerable = asyncEnumerableFactory(cancellationToken);

                    await foreach (var item in asyncEnumerable.WithCancellation(cancellationToken))
                    {
                        observer.OnNext(item);
                    }

                    observer.OnCompleted();

                    return Disposable.Empty;
                };

            Subject = static _ => new Subject<T>();
            BehaviorSubject = static initialValue => new BehaviorSubject<T>(initialValue);
            ReplaySubject0 = static _ => new ReplaySubject<T>();
            ReplaySubjectB = static bufferSize => new ReplaySubject<T>(bufferSize);
            ReplaySubjectW = static window => new ReplaySubject<T>(window);
            ReplaySubjectS = static scheduler => new ReplaySubject<T>(scheduler);
            ReplaySubjectBw = static tuple => new ReplaySubject<T>(tuple.bufferSize, tuple.window);
            ReplaySubjectBs = static tuple => new ReplaySubject<T>(tuple.bufferSize, tuple.scheduler);
            ReplaySubjectWs = static tuple => new ReplaySubject<T>(tuple.window, tuple.scheduler);
            ReplaySubject3 = static tuple => new ReplaySubject<T>(tuple.bufferSize, tuple.window, tuple.scheduler);

            EmptyOnError = static (_, _) => { };
            EmptyOnCompleted = static _ => { };
        }
    }
}
