using System.Collections.Immutable;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Fills;

public static partial class FillsObservableExtensions
{
    private static class Lambdas<TA, TB>
    {
        public static readonly Func<TA, TB> Default;

        public static readonly
            Func<KeepManyState<TA, TB>, IEnumerable<TA>, KeepManyState<TA, TB>>
            KeepManyStateFolder;
        public static readonly Func<KeepManyState<TA, TB>, bool> KeepManyStatePredicate;
        public static readonly Func<KeepManyState<TA, TB>, IObservable<ValueTuple<TB, bool>>> KeepManyStateSelector;


        static Lambdas()
        {
            Default = static _ => default!;

            KeepManyStateFolder =
                static (state, element) =>
                {
                    var previousSet = state.Set;
                    var currentSet = element.Select(state.KeySelector).ToImmutableHashSet();

                    return new KeepManyState<TA, TB>(
                        state.KeySelector,
                        currentSet,
                        previousSet.Except(currentSet),
                        currentSet.Except(previousSet)
                    );
                };
            KeepManyStatePredicate = static state => state.ItemsRemoved.Count + state.ItemsAdded.Count > 0;
            KeepManyStateSelector =
                static state =>
                    Observable.Concat(
                        state.ItemsRemoved.Select(static value => (value, false)).ToObservable(),
                        state.ItemsAdded.Select(static value => (value, true)).ToObservable()
                    );
        }
    }


    private static class Lambdas<T>
    {
        public static readonly Func<T, T> Identity;
        public static readonly Action<T> Ignore;


        public static readonly Func<ValueTuple<T, bool>, T> KeepManyKeySelector;
        public static readonly
            Func<IGroupedObservable<T, ValueTuple<T, bool>>, IObservable<ValueTuple<T, bool>>>
            KeepManyDurationSelector;
        public static readonly Func<ValueTuple<T, bool>, bool> KeepManyGroupPredicate;
        public static readonly Func<ValueTuple<T, bool>, bool> KeepManyGroupNegativePredicate;


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


        static Lambdas()
        {
            Identity = static value => value;
            Ignore = static _ => { };

            KeepManyGroupPredicate = static valueTuple => valueTuple.Item2;
            KeepManyGroupNegativePredicate = static valueTuple => !valueTuple.Item2;
            KeepManyKeySelector = static valueTuple => valueTuple.Item1;
            KeepManyDurationSelector = static group => group.Where(KeepManyGroupNegativePredicate).Take(1);

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
        }
    }
}
