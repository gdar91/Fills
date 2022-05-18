using System.Reactive.Linq;

namespace Fills;

public static partial class FillsObservableExtensions
{
    public static IObservable<TElement> SampleWhileThrottling<TElement>(
        this IObservable<TElement> observable,
        TimeSpan sampleInterval,
        TimeSpan throttleDueTime
    )
    {
        return observable.Publish(sharedObservable =>
        {
            var timer = Observable.Timer(TimeSpan.Zero, sampleInterval);
            var windowClosings = sharedObservable.Throttle(throttleDueTime);
            var windowClosingsSelector = () => windowClosings;

            return sharedObservable
                .Window(windowClosingsSelector)
                .SelectMany(window =>
                    window.Sample(
                        window
                            .DistinctUntilChanged(static _ => 0L)
                            .Select(timer, static (timer, _) => timer)
                            .Append(Observable.Empty<long>())
                            .Switch()
                    )
                );
        });
    }


    public static IObservable<TElement> SampleWhileThrottling<TElement>(
        this IObservable<TElement> observable,
        TimeSpan sampleInterval
    )
    {
        return SampleWhileThrottling(observable, sampleInterval, sampleInterval);
    }
}
