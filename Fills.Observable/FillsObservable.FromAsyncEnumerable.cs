using System.Reactive.Disposables;

namespace Fills;

public static partial class FillsObservable
{
    public static IObservable<TElement> FromAsyncEnumerable<TElement>(
        Func<CancellationToken, IAsyncEnumerable<TElement>> asyncEnumerableFactory
    )
    {
        return
            Create(
                asyncEnumerableFactory,
                static async (asyncEnumerableFactory, observer, cancellationToken) =>
                {
                    var asyncEnumerable = asyncEnumerableFactory(cancellationToken);

                    await foreach (var item in asyncEnumerable.WithCancellation(cancellationToken))
                    {
                        observer.OnNext(item);
                    }

                    observer.OnCompleted();

                    return Disposable.Empty;
                },
                Fills.Hint.Of<TElement>()
            );
    }


    public static IObservable<TElement> FromAsyncEnumerable<TArg, TElement>(
        TArg arg,
        Func<TArg, CancellationToken, IAsyncEnumerable<TElement>> asyncEnumerableFactory
    )
    {
        return
            Create(
                (arg, asyncEnumerableFactory),
                static async (tuple, observer, cancellationToken) =>
                {
                    var asyncEnumerable = tuple.asyncEnumerableFactory(tuple.arg, cancellationToken);

                    await foreach (var item in asyncEnumerable.WithCancellation(cancellationToken))
                    {
                        observer.OnNext(item);
                    }

                    observer.OnCompleted();

                    return Disposable.Empty;
                },
                Fills.Hint.Of<TElement>()
            );
    }
}
