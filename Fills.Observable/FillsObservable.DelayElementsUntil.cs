using System.Reactive.Linq;

namespace Fills;

public static partial class FillsObservableExtensions
{
    public static IObservable<TElement> DelayElementsUntil<TElement, TSignal>(
        this IObservable<TElement> source,
        IObservable<TSignal> signals
    )
    {
        return source
            .Merge(Observable.Never<TElement>().TakeUntil(signals))
            .Window(signals.Take(1).Concat(Observable.Never<TSignal>()))
            .SelectMany(static (window, index) =>
                index == 0
                    ? window
                        .ToArray()
                        .SelectMany(element => element)
                    : window
            );
    }
}
