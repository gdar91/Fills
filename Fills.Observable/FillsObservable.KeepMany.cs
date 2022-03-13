using System.Collections.Immutable;
using System.Reactive.Linq;

namespace Fills;

public static partial class FillsObservableExtensions
{
    public static IObservable<TElement> KeepMany<TElement>(
        this IObservable<IEnumerable<IObservable<TElement>>> observable
    )
    {
        return
            KeepMany(
                observable,
                KeepManyModule<IObservable<TElement>>.Identity,
                KeepManyModule<IObservable<TElement>>.Identity
            );
    }


    public static IObservable<TResult> KeepMany<TElement, TResult>(
        this IObservable<IEnumerable<TElement>> observable,
        Func<TElement, IObservable<TResult>> observableSelector
    )
    {
        return KeepMany(observable, KeepManyModule<TElement>.Identity, observableSelector);
    }


    public static IObservable<TResult> KeepMany<TElement, TKey, TResult>(
        this IObservable<IEnumerable<TElement>> observable,
        Func<TElement, TKey> keySelector,
        Func<TKey, IObservable<TResult>> observableSelector
    )
    {
        return observable
            .Scan(
                new KeepManyState<TElement, TKey>(
                    keySelector,
                    ImmutableHashSet<TKey>.Empty,
                    ImmutableHashSet<TKey>.Empty,
                    ImmutableHashSet<TKey>.Empty
                ),
                KeepManyModule<TElement, TKey>.StateAccumulator
            )
            .TrySelect(KeepManyModule<TElement, TKey>.StateTrySelector)
            .Concat()
            .GroupByUntil(KeepManyModule<TKey>.KeySelector, KeepManyModule<TKey>.DurationSelector)
            .SelectMany(group =>
                group
                    .TrySelect(observableSelector, KeepManyModule<TKey, TResult>.GroupTrySelector)
                    .Take(1)
                    .Switch()
                    .TakeUntil(group.Where(KeepManyModule<TKey>.GroupNegativePredicate).Take(1))
            );
    }


    private sealed record KeepManyState<TElement, TKey>(
        Func<TElement, TKey> KeySelector,
        ImmutableHashSet<TKey> Set,
        ImmutableHashSet<TKey> ItemsRemoved,
        ImmutableHashSet<TKey> ItemsAdded
    );


    private static class KeepManyModule<T>
    {
        public static readonly Func<T, T> Identity;

        public static readonly Func<ValueTuple<T, bool>, T> KeySelector;

        public static readonly
            Func<IGroupedObservable<T, ValueTuple<T, bool>>, IObservable<ValueTuple<T, bool>>>
            DurationSelector;

        public static readonly Func<ValueTuple<T, bool>, bool> GroupNegativePredicate;

        public static readonly Func<T, ValueTuple<T, bool>> TupleWithFalse;

        public static readonly Func<T, ValueTuple<T, bool>> TupleWithTrue;


        static KeepManyModule()
        {
            Identity = static value => value;
            GroupNegativePredicate = static valueTuple => !valueTuple.Item2;
            KeySelector = static valueTuple => valueTuple.Item1;
            DurationSelector = static group => group.Where(GroupNegativePredicate).Take(1);
            TupleWithFalse = static value => (value, false);
            TupleWithTrue = static value => (value, true);
        }
    }

    
    private static class KeepManyModule<T1, T2>
    {
        public static readonly Func<KeepManyState<T1, T2>, IEnumerable<T1>, KeepManyState<T1, T2>> StateAccumulator =
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

        public static readonly TrySelector<KeepManyState<T1, T2>, IObservable<ValueTuple<T2, bool>>> StateTrySelector =
            static (KeepManyState<T1, T2> state, out IObservable<(T2, bool)> result) =>
            {
                if (state.ItemsRemoved.Count + state.ItemsAdded.Count > 0)
                {
                    result =
                        Observable.Concat(
                            state.ItemsRemoved.Select(KeepManyModule<T2>.TupleWithFalse).ToObservable(),
                            state.ItemsAdded.Select(KeepManyModule<T2>.TupleWithTrue).ToObservable()
                        );

                    return true;
                }

                result = default!;

                return false;
            };

        public static readonly
            TrySelector<Func<T1, IObservable<T2>>, ValueTuple<T1, bool>, IObservable<T2>>
            GroupTrySelector =
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
    }
}
