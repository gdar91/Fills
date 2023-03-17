using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Channels;

namespace Fills;

public static partial class FillsObservableExtensions
{
    public static IObservable<T> SampleChannel<T>(this IObservable<T> observable)
    {
        return FillsObservable.Create(
            observable,
            static async (observable, observer, cancellationToken) =>
            {
                var channel =
                    Channel.CreateBounded<T>(
                        new BoundedChannelOptions(1)
                        {
                            FullMode = BoundedChannelFullMode.DropOldest
                        }
                    );

                var consumerSubscription =
                    Observable
                        .FromAsync(async cancellationToken =>
                        {
                            await channel.Reader.Completion;
                            return Observable.Empty<T>();
                        })
                        .Catch((Exception e) => Observable.Return(Observable.Throw<T>(e)))
                        .StartWith(
                            FillsObservable.Create(
                                channel,
                                static async (channel, observer, cancellationToken) =>
                                {
                                    while (true)
                                    {
                                        var element = await channel.Reader.ReadAsync(cancellationToken);
                                        observer.OnNext(element);
                                    }
                                },
                                Hint.Of<T>()
                            )
                        )
                        .Switch()
                        .Subscribe(observer);

                try
                {
                    var producerTask =
                        await observable
                            .Select(
                                channel,
                                static (channel, element) =>
                                    Observable.FromAsync(async cancellationToken =>
                                        await channel.Writer.WriteAsync(element, cancellationToken)
                                    )
                            )
                            .Concat()
                            .ToTask(cancellationToken);

                    channel.Writer.Complete();
                }
                catch (Exception e)
                {
                    channel.Writer.Complete(e);
                    throw;
                }

                return consumerSubscription;
            },
            Hint.Of<T>()
        );
    }
}
