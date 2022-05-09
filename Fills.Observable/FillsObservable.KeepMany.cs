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
                KeepManyHelpers<IObservable<TElement>>.Identity,
                KeepManyHelpers<IObservable<TElement>>.Identity
            );
    }

    public static IObservable<TResult> KeepMany<TElement, TResult>(
        this IObservable<IEnumerable<TElement>> observable,
        Func<TElement, IObservable<TResult>> observableSelector
    )
    {
        return KeepMany(observable, KeepManyHelpers<TElement>.Identity, observableSelector);
    }

    public static IObservable<TResult> KeepMany<TElement, TKey, TResult>(
        this IObservable<IEnumerable<TElement>> observable,
        Func<TElement, TKey> keySelector,
        Func<TKey, IObservable<TResult>> observableSelector
    )
    {
        return observable
            .Scan(
                KeepManyAccumulate<TElement, TKey, TResult>.Initial(keySelector, observableSelector),
                KeepManyAccumulate<TElement, TKey, TResult>.Accumulator
            )
            .Select(KeepManyAccumulate<TElement, TKey, TResult>.SignalsOf)
            .Concat()
            .GroupByUntil(KeepManySignal<TKey, TResult>.KeyOf, KeepManySignal<TKey, TResult>.GroupDurationSelector)
            .SelectMany(KeepManySignal<TKey, TResult>.GroupSelector);
    }


    private readonly record struct KeepManyAccumulate<TElement, TKey, TResult>(
        Func<TElement, TKey> KeySelector,
        Func<TKey, IObservable<TResult>> ObservableSelector,
        ImmutableHashSet<TKey> Set,
        IObservable<KeepManySignal<TKey, TResult>> Signals
    )
    {
        public static readonly
            Func<
                KeepManyAccumulate<TElement, TKey, TResult>,
                IEnumerable<TElement>,
                KeepManyAccumulate<TElement, TKey, TResult>
            >
            Accumulator =
                static (accumulate, element) =>
                {
                    var previousSet = accumulate.Set;
                    var set = element.Select(accumulate.KeySelector).ToImmutableHashSet();
                    var itemsRemoved = previousSet.Except(set);
                    var itemsAdded = set.Except(previousSet);

                    var signals =
                        Observable.Concat(
                            itemsRemoved
                                .ToObservable()
                                .Select(static item =>
                                    new KeepManySignal<TKey, TResult>(item, Observable.Empty<TResult>(), true)
                                ),
                            itemsAdded
                                .ToObservable()
                                .Select(
                                    accumulate.ObservableSelector,
                                    static (observableSelector, item) =>
                                        new KeepManySignal<TKey, TResult>(item, observableSelector(item), false)
                                )
                        );

                    return accumulate with { Set = set, Signals = signals };
                };


        public static readonly
            Func<KeepManyAccumulate<TElement, TKey, TResult>, IObservable<KeepManySignal<TKey, TResult>>>
            SignalsOf =
                static accumulate => accumulate.Signals;


        public static KeepManyAccumulate<TElement, TKey, TResult> Initial(
            Func<TElement, TKey> keySelector,
            Func<TKey, IObservable<TResult>> observableSelector
        )
        {
            return
                new(
                    keySelector,
                    observableSelector,
                    ImmutableHashSet<TKey>.Empty,
                    Observable.Empty<KeepManySignal<TKey, TResult>>()
                );
        }
    }


    private readonly record struct KeepManySignal<TKey, TResult>(
        TKey Key,
        IObservable<TResult> Observable,
        bool IsFinal
    )
    {
        public static readonly Func<KeepManySignal<TKey, TResult>, TKey> KeyOf;

        public static readonly Func<KeepManySignal<TKey, TResult>, IObservable<TResult>> ObservableOf;

        public static readonly Func<KeepManySignal<TKey, TResult>, bool> IsFinalOf;

        public static readonly
            Func<IGroupedObservable<TKey, KeepManySignal<TKey, TResult>>, IObservable<KeepManySignal<TKey, TResult>>>
            GroupDurationSelector;

        public static readonly
            Func<IGroupedObservable<TKey, KeepManySignal<TKey, TResult>>, IObservable<TResult>>
            GroupSelector;


        static KeepManySignal()
        {
            KeyOf = static signal => signal.Key; 
            ObservableOf = static signal => signal.Observable;
            IsFinalOf = static signal => signal.IsFinal; 
            GroupDurationSelector = static group => group.Where(IsFinalOf); 
            GroupSelector = static group => group.Select(ObservableOf).Switch();
        }
    }


    private static class KeepManyHelpers<T>
    {
        public static readonly Func<T, T> Identity = static a => a;
    }
}
