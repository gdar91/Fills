using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Channels;

namespace Fills;

public static partial class FillsObservableExtensions
{
    public static ChannelReader<T> RunAsChannelReader<T>(
        this IObservable<T> observable,
        CancellationToken cancellationToken
    )
    {
        var channel = Channel.CreateUnbounded<T>();

        _ = observable
            .Do(
                FillsObserver.Create(
                    channel,
                    Cache<T>.RunAsChannelReaderOnNext,
                    Cache<T>.RunAsChannelReaderOnError,
                    Cache<T>.RunAsChannelReaderOnCompleted,
                    Hint.Of<T>()
                )
            )
            .Select(next =>
                Observable.FromAsync(cancellationToken =>
                    channel.Writer
                        .WriteAsync(next, cancellationToken)
                        .AsTask()
                )
            )
            .Concat()
            .Finally(() => channel.Writer.TryComplete())
            .ToTask(cancellationToken);

        return channel.Reader;
    }
}
