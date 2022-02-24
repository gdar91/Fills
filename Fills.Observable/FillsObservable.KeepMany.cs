using System.Collections.Immutable;
using System.Reactive.Linq;

namespace Fills;

public static partial class FillsObservableExtensions
{
    public static IObservable<TElement> KeepMany<TElement>(
        this IObservable<IEnumerable<IObservable<TElement>>> observable
    )
    {
        return KeepMany(observable, Lambdas<IObservable<TElement>>.Identity, Lambdas<IObservable<TElement>>.Identity);
    }


    public static IObservable<TResult> KeepMany<TElement, TResult>(
        this IObservable<IEnumerable<TElement>> observable,
        Func<TElement, IObservable<TResult>> observableSelector
    )
    {
        return KeepMany(observable, Lambdas<TElement>.Identity, observableSelector);
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
                Lambdas<TElement, TKey>.KeepManyStateFolder
            )
            .Where(Lambdas<TElement, TKey>.KeepManyStatePredicate)
            .Select(Lambdas<TElement, TKey>.KeepManyStateSelector)
            .Concat()
            .GroupByUntil(Lambdas<TKey>.KeepManyKeySelector, Lambdas<TKey>.KeepManyDurationSelector)
            .SelectMany(group =>
                group
                    .Where(Lambdas<TKey>.KeepManyGroupPredicate)
                    .Take(1)
                    .Select(tuple => observableSelector(tuple.Item1))
                    .Switch()
                    .TakeUntil(group.Where(Lambdas<TKey>.KeepManyGroupNegativePredicate).Take(1))
            );
    }


    private sealed record KeepManyState<TElement, TKey>(
        Func<TElement, TKey> KeySelector,
        ImmutableHashSet<TKey> Set,
        ImmutableHashSet<TKey> ItemsRemoved,
        ImmutableHashSet<TKey> ItemsAdded
    );
}
