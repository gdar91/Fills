using System.Reactive.Disposables;

namespace Fills;

public static partial class FillsObservable
{
    public static IObservable<TElement> Defer<TArg, TElement>(
        TArg arg,
        Func<TArg, IObservable<TElement>> observableFactory
    )
    {
        return
            Create(
                (arg, observableFactory),
                static (tuple, observer) =>
                {
                    try
                    {
                        return tuple.observableFactory(tuple.arg).Subscribe(observer);
                    }
                    catch (Exception exception)
                    {
                        observer.OnError(exception);

                        return Disposable.Empty;
                    }
                },
                Fills.Hint.Of<TElement>()
            );
    }


    public static IObservable<TElement> Defer<TArg, TElement>(
        TArg arg,
        Func<TArg, CancellationToken, Task<IObservable<TElement>>> observableFactoryAsync
    )
    {
        return
            Create(
                (arg, observableFactoryAsync),
                static async (tuple, observer, cancellationToken) =>
                {
                    try
                    {
                        var observable =
                            await tuple
                                .observableFactoryAsync(tuple.arg, cancellationToken)
                                .ConfigureAwait(false);

                        return observable.Subscribe(observer);
                    }
                    catch (Exception exception)
                    {
                        observer.OnError(exception);

                        return Disposable.Empty;
                    }
                },
                Fills.Hint.Of<TElement>()
            );
    }
}
