using System.Reactive.Linq;

namespace Fills;

public static partial class FillsObservableExtensions
{
    public static IObservable<TElement> KeepMany<TElement>(
        this IObservable<IEnumerable<IObservable<TElement>>> observable
    )
    {
        return KeepMany(observable, static element => element, static element => element);
    }


    public static IObservable<TResult> KeepMany<TElement, TResult>(
        this IObservable<IEnumerable<TElement>> observable,
        Func<TElement, IObservable<TResult>> observableSelector
    )
    {
        return KeepMany(observable, static element => element, observableSelector);
    }


    public static IObservable<TResult> KeepMany<TElement, TKey, TResult>(
        this IObservable<IEnumerable<TElement>> observable,
        Func<TElement, TKey> keySelector,
        Func<TKey, IObservable<TResult>> observableSelector
    )
    {
        return observable
            .Scan(
                new
                {
                    KeySelector = keySelector,
                    Set = new HashSet<TKey>(),
                    ItemsRemoved = new HashSet<TKey>(),
                    ItemsAdded = new HashSet<TKey>()
                },
                static (state, element) => (state.Set, element.Select(state.KeySelector).ToHashSet()) switch
                {
                    var (previousSet, currentSet) => new
                    {
                        state.KeySelector,
                        Set = currentSet,
                        ItemsRemoved = previousSet.Except(currentSet).ToHashSet(),
                        ItemsAdded = currentSet.Except(previousSet).ToHashSet()
                    }
                }
            )
            .Where(static state => state.ItemsRemoved.Count + state.ItemsAdded.Count > 0)
            .Select(static state =>
                Observable.Concat(
                    state.ItemsRemoved.Select(static itemRemoved => (itemRemoved, false)).ToObservable(),
                    state.ItemsAdded.Select(static itemAdded => (itemAdded, true)).ToObservable()
                )
            )
            .Concat()
            .GroupByUntil(
                static tuple => tuple.Item1,
                static group =>
                    group
                        .Where(static tuple => !tuple.Item2)
                        .Take(1)
            )
            .SelectMany(group =>
                group
                    .Where(static tuple => tuple.Item2)
                    .Take(1)
                    .Select(tuple => observableSelector(tuple.Item1))
                    .Switch()
                    .TakeUntil(
                        group
                            .Where(static tuple => !tuple.Item2)
                            .Take(1)
                    )
            );
    }
}
