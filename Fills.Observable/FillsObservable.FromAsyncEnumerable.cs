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
                    var asyncEnumerable =
                        asyncEnumerableFactory(cancellationToken)
                            .WithCancellation(cancellationToken)
                            .ConfigureAwait(false);

                    await foreach (var item in asyncEnumerable)
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
                static async (arg, observer, cancellationToken) =>
                {
                    var asyncEnumerable =
                        arg
                            .asyncEnumerableFactory(arg.arg, cancellationToken)
                            .WithCancellation(cancellationToken)
                            .ConfigureAwait(false);

                    await foreach (var item in asyncEnumerable)
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
