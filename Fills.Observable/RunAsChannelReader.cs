using System;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Fills
{
    public static partial class ExtensionsForObservable
    {
        public static ChannelReader<T> RunAsChannelReader<T>(
            this IObservable<T> observable,
            CancellationToken cancellationToken
        )
        {
            var channel = Channel.CreateUnbounded<T>();

            var task = Task.Run(() =>
                observable
                    .Do(
                        next => { },
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
                    .RunAsync(cancellationToken)
            );

            return channel.Reader;
        }
    }
}
