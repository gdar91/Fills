using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Channels;

namespace Fills;

public static partial class FillsObservableExtensions
{
    private static readonly BoundedChannelOptions BoundedChannelOptionsOfOneDropOldest =
        new(1) { FullMode = BoundedChannelFullMode.DropOldest };




    public static IObservable<TResult> SelectSampled<TArg, TElement, TResult>(
        this IObservable<TElement> source,
        TArg arg,
        Func<TArg, TElement, TResult> selector
    )
    {
        return
            SelectSampledCore(
                source,
                (arg, selector),
                static (arg, channel) =>
                    FillsObservable.Create(
                        (arg.arg, arg.selector, channel),
                        static async (arg, observer, cancellationToken) =>
                        {
                            while (true)
                            {
                                var element = await arg.channel.Reader.ReadAsync(cancellationToken);
                                var result = arg.selector(arg.arg, element);
                                observer.OnNext(result);
                            }
                        },
                        Hint.Of<TResult>()
                    )
            );
    }




    public static IObservable<TResult> SelectSampled<TArg, TElement, TResult>(
        this IObservable<TElement> source,
        TArg arg,
        Func<TArg, TElement, CancellationToken, Task<TResult>> selector
    )
    {
        return
            SelectSampledCore(
                source,
                (arg, selector),
                static (arg, channel) =>
                    FillsObservable.Create(
                        (arg.arg, arg.selector, channel),
                        static async (arg, observer, cancellationToken) =>
                        {
                            while (true)
                            {
                                var element = await arg.channel.Reader.ReadAsync(cancellationToken);
                                var result = await arg.selector(arg.arg, element, cancellationToken);
                                observer.OnNext(result);
                            }
                        },
                        Hint.Of<TResult>()
                    )
            );
    }




    private static IObservable<TResult> SelectSampledCore<TArg, TElement, TResult>(
        IObservable<TElement> source,
        TArg arg,
        Func<TArg, Channel<TElement>, IObservable<TResult>> consumerObservableFactory
    )
    {
        return FillsObservable.Create(
            (source, arg, consumerObservableFactory),
            static async (arg, observer, cancellationToken) =>
            {
                var channel = Channel.CreateBounded<TElement>(BoundedChannelOptionsOfOneDropOldest);

                var consumerSubscription =
                    Observable
                        .FromAsync(async cancellationToken =>
                        {
                            await channel.Reader.Completion;
                            return Observable.Empty<TResult>();
                        })
                        .Catch((Exception e) => Observable.Return(Observable.Throw<TResult>(e)))
                        .StartWith(arg.consumerObservableFactory(arg.arg, channel))
                        .Switch()
                        .Subscribe(observer);

                try
                {
                    await arg.source
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
            Hint.Of<TResult>()
        );
    }
}
