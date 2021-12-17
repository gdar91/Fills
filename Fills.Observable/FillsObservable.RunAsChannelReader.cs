using System.Reactive.Linq;
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

        _ = Task.Run(
            () => observable
                .Do(
                    static next => { },
                    error => channel.Writer.Complete(error),
                    () => channel.Writer.Complete()
                )
                .Select(next =>
                    Observable.FromAsync(cancellationToken =>
                        channel.Writer
                            .WriteAsync(next, cancellationToken)
                            .AsTask()
                    )
                )
                .Concat()
                .Finally(() => channel.Writer.Complete())
                .RunAsync(cancellationToken),
            cancellationToken
        );

        return channel.Reader;
    }
}
