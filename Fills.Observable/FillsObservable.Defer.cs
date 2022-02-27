namespace Fills;

public static partial class FillsObservable
{
    public static IObservable<TElement> Defer<TState, TElement>(
        TState state,
        Func<TState, IObservable<TElement>> observableFactory
    )
    {
        return
            Create(
                (state, observableFactory),
                FillsObservableExtensions.Cache<TState, TElement>.DeferSubscribe,
                Fills.Hint.Of<TElement>()
            );
    }


    public static IObservable<TElement> Defer<TState, TElement>(
        TState state,
        Func<TState, CancellationToken, Task<IObservable<TElement>>> observableFactoryAsync
    )
    {
        return
            Create(
                (state, observableFactoryAsync),
                FillsObservableExtensions.Cache<TState, TElement>.DeferSubscribeAsync,
                Fills.Hint.Of<TElement>()
            );
    }
}
