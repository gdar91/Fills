using System.Reactive;
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
            .Do(new RunAsChannelReaderCompletionObserver<T>(channel))
            .Select(
                channel,
                static (channel, next) =>
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


    private sealed class RunAsChannelReaderCompletionObserver<T> : ObserverBase<T>
    {
        private readonly Channel<T> channel;


        public RunAsChannelReaderCompletionObserver(Channel<T> channel)
        {
            this.channel = channel;
        }


        protected override void OnNextCore(T value)
        {
        }

        protected override void OnErrorCore(Exception error)
        {
            channel.Writer.TryComplete(error);
        }

        protected override void OnCompletedCore()
        {
            channel.Writer.TryComplete();
        }
    }
}