using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace Fills
{
    public static partial class ExtensionsForObservable
    {
        public static IObservable<TElement> KeepMany<TElement>(
            this IObservable<IEnumerable<IObservable<TElement>>> observable
        )
        {
            return KeepMany(
                observable,
                element => element,
                element => element
            );
        }


        public static IObservable<TResult> KeepMany<TElement, TResult>(
            this IObservable<IEnumerable<TElement>> observable,
            Func<TElement, IObservable<TResult>> observableSelector
        )
        {
            return KeepMany(
                observable,
                element => element,
                observableSelector
            );
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
                        Set = new HashSet<TKey>(),
                        ItemsRemoved = new HashSet<TKey>(),
                        ItemsAdded = new HashSet<TKey>()
                    },
                    (state, element) => (state.Set, element.Select(keySelector).ToHashSet()) switch
                    {
                        var (previousSet, currentSet) => new
                        {
                            Set = currentSet,
                            ItemsRemoved = previousSet.Except(currentSet).ToHashSet(),
                            ItemsAdded = currentSet.Except(previousSet).ToHashSet()
                        }
                    }
                )
                .Where(state => (state.ItemsRemoved.Count + state.ItemsAdded.Count) > 0)
                .Select(state =>
                    Observable.Concat(
                        state.ItemsRemoved.Select(itemRemoved => (itemRemoved, false)).ToObservable(),
                        state.ItemsAdded.Select(itemAdded => (itemAdded, true)).ToObservable()
                    )
                )
                .Concat()
                .GroupByUntil(
                    tuple => tuple.Item1,
                    group =>
                        group
                            .Where(tuple => !tuple.Item2)
                            .Take(1)
                )
                .SelectMany(group =>
                    group
                        .Where(tuple => tuple.Item2)
                        .Take(1)
                        .Select(tuple => observableSelector(tuple.Item1))
                        .Switch()
                        .TakeUntil(
                            group
                                .Where(tuple => !tuple.Item2)
                                .Take(1)
                        )
                );
        }
    }
}
