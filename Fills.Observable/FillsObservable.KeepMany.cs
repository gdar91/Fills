using System.Collections.Immutable;
using System.Reactive.Linq;

namespace Fills;

public static partial class FillsObservableExtensions
{
    public static IObservable<TElement> KeepMany<TElement>(
        this IObservable<IEnumerable<IObservable<TElement>>> observable
    )
    {
        return KeepMany(observable, Cache<IObservable<TElement>>.Identity, Cache<IObservable<TElement>>.Identity);
    }


    public static IObservable<TResult> KeepMany<TElement, TResult>(
        this IObservable<IEnumerable<TElement>> observable,
        Func<TElement, IObservable<TResult>> observableSelector
    )
    {
        return KeepMany(observable, Cache<TElement>.Identity, observableSelector);
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
                Cache<TElement, TKey>.KeepManyStateFolder
            )
            .TrySelect(Cache<TElement, TKey>.KeepManyStateTrySelector)
            .Concat()
            .GroupByUntil(Cache<TKey>.KeepManyKeySelector, Cache<TKey>.KeepManyDurationSelector)
            .SelectMany(group =>
                group
                    .TrySelect(observableSelector, Cache<TKey, TResult>.KeepManyGroupTrySelector)
                    .Take(1)
                    .Switch()
                    .TakeUntil(group.Where(Cache<TKey>.KeepManyGroupNegativePredicate).Take(1))
            );
    }


    internal sealed record KeepManyState<TElement, TKey>(
        Func<TElement, TKey> KeySelector,
        ImmutableHashSet<TKey> Set,
        ImmutableHashSet<TKey> ItemsRemoved,
        ImmutableHashSet<TKey> ItemsAdded
    );
}
