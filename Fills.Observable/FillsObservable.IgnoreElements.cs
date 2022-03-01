namespace Fills;

public static partial class FillsObservableExtensions
{
    public static IObservable<TResult> IgnoreElements<TElement, TResult>(this IObservable<TElement> source)
    {
        return new IgnoreElementsObservable<TElement, TResult>(source);
    }

    public static IObservable<TResult> IgnoreElements<TElement, TResult>(
        this IObservable<TElement> source,
        Hint<TResult> resultHint
    )
    {
        return new IgnoreElementsObservable<TElement, TResult>(source);
    }
}
