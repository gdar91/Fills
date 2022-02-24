using System.Reactive.Linq;

namespace Fills;

public static partial class FillsObservableExtensions
{
    public static IObservable<TResult> IgnoreElements<TElement, TResult>(
        this IObservable<TElement> source,
        Hint<TResult> resultHint
    )
    {
        return source.IgnoreElements().Select(Lambdas<TElement, TResult>.Default);
    }
}
