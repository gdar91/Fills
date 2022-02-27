namespace Fills;

public static partial class FillsObservableExtensions
{
    public static IObservable<TResult> IgnoreElements<TElement, TResult>(
        this IObservable<TElement> source,
        Hint<TResult> resultHint
    )
    {
        return FillsObservable.Create(source, Cache<TElement, TResult>.IgnoreElementsSubscribe, Hint.Of<TResult>());
    }
}
