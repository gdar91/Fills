using System.Reactive.Linq;

namespace Fills;

file sealed class SubscriptionState
{
    public int IsActive;
}

public static partial class FillsObservableExtensions
{
    public static IObservable<T> Exhaust<T>(this IObservable<IObservable<T>> source) =>
        FillsObservable.Defer(
            source,
            static source =>
            {
                var subscriptionState = new SubscriptionState();

                return source
                    .TrySelect(
                        subscriptionState,
                        static (SubscriptionState state, IObservable<T> observable, out IObservable<T> result) =>
                        {
                            if (Interlocked.CompareExchange(ref state.IsActive, 1, 0) == 0)
                            {
                                result = observable.Finally(() => Interlocked.Exchange(ref state.IsActive, 0));
                                return true;
                            }

                            result = default!;
                            return false;
                        }
                    )
                    .Switch();
            }
        );
}
