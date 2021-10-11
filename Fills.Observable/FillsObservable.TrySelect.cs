using System.Reactive.Linq;

namespace Fills;

public static partial class FillsObservableExtensions
{
    public static IObservable<TResult> TrySelect<TElement, TResult>(
        this IObservable<TElement> source,
        TrySelector<TElement, TResult> trySelector
    )
    {
        return Observable.Create<TResult>(observer =>
        {
            return source.Subscribe(
                element =>
                {
                    if (!trySelector(element, out var result))
                    {
                        return;
                    }

                    observer.OnNext(result);
                },
                observer.OnError,
                observer.OnCompleted
            );
        });
    }

    public static IObservable<TResult> TrySelect<TElement, TResult>(
        this IObservable<TElement> source,
        TrySelector<TElement, TResult> trySelector,
        Hint<TResult> resultHint
    )
    {
        return Observable.Create<TResult>(observer =>
        {
            return source.Subscribe(
                element =>
                {
                    if (!trySelector(element, out var result))
                    {
                        return;
                    }

                    observer.OnNext(result);
                },
                observer.OnError,
                observer.OnCompleted
            );
        });
    }
}
