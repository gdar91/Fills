namespace Fills;

public static partial class FillsObservableExtensions
{
    public static IObservable<TResult> TrySelect<TElement, TResult>(
        this IObservable<TElement> source,
        TrySelector<TElement, TResult> trySelector
    )
    {
        return new TrySelectObservable<TElement, TResult>(source, trySelector);
    }

    public static IObservable<TResult> TrySelect<TArg, TElement, TResult>(
        this IObservable<TElement> source,
        TArg arg,
        TrySelector<TArg, TElement, TResult> trySelector
    )
    {
        return new TrySelectObservable<TArg, TElement, TResult>(source, arg, trySelector);
    }

    public static IObservable<TResult> TrySelect<TElement, TResult>(
        this IObservable<TElement> source,
        TrySelector<TElement, TResult> trySelector,
        Hint<TResult> resultHint
    )
    {
        return new TrySelectObservable<TElement, TResult>(source, trySelector);
    }

    public static IObservable<TResult> TrySelect<TArg, TElement, TResult>(
        this IObservable<TElement> source,
        TArg arg,
        TrySelector<TArg, TElement, TResult> trySelector,
        Hint<TResult> resultHint
    )
    {
        return new TrySelectObservable<TArg, TElement, TResult>(source, arg, trySelector);
    }
}
