using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Channels;

namespace Fills;

public static partial class FillsObservableExtensions
{
    private static readonly BoundedChannelOptions BoundedChannelOptionsOfOneDropOldest =
        new(1)
        {
            SingleReader = true,
            SingleWriter = true,
            FullMode = BoundedChannelFullMode.DropOldest
        };




    public static ChannelReader<T> RunAsChannelReader<T>(
        this IObservable<T> source,
        CancellationToken cancellationToken
    )
    {
        var channel = Channel.CreateUnbounded<T>();

        _ = StartProducer(source, channel.Writer, cancellationToken);

        return channel.Reader;
    }




    public static IObservable<TResult> SelectSampled<TArg, TElement, TResult>(
        this IObservable<TElement> source,
        TArg arg,
        Func<TArg, TElement, CancellationToken, Task<TResult>> selector
    )
    {
        return
            FillsObservable.Create(
                (source, arg, selector),
                static async (arg, observer, cancellationToken) =>
                {
                    var channel = Channel.CreateBounded<TElement>(BoundedChannelOptionsOfOneDropOldest);

                    var consumerTask = StartConsumer(observer, channel.Reader, arg.arg, arg.selector, cancellationToken);
                    var producerTask = StartProducer(arg.source, channel.Writer, cancellationToken);

                    await producerTask.ConfigureAwait(false);
                    await consumerTask.ConfigureAwait(false);

                    return Disposable.Empty;
                },
                Hint.Of<TResult>()
            );


        static async Task StartConsumer(
            IObserver<TResult> observer,
            ChannelReader<TElement> channelReader,
            TArg arg,
            Func<TArg, TElement, CancellationToken, Task<TResult>> selector,
            CancellationToken cancellationToken
        )
        {
            try
            {
                while (await channelReader.WaitToReadAsync(cancellationToken).ConfigureAwait(false))
                {
                    while (channelReader.TryRead(out var element))
                    {
                        cancellationToken.ThrowIfCancellationRequested();

                        var result = await selector(arg, element, cancellationToken).ConfigureAwait(false);

                        observer.OnNext(result);
                    }
                }

                observer.OnCompleted();
            }
            catch (Exception e)
            {
                observer.OnError(e);
            }
        }
    }




    public static IObservable<TResult> SelectSampled<TArg, TElement, TResult>(
        this IObservable<TElement> source,
        TArg arg,
        Func<TArg, TElement, TResult> selector
    )
    {
        return
            FillsObservable.Create(
                (source, arg, selector),
                static async (arg, observer, cancellationToken) =>
                {
                    var channel = Channel.CreateBounded<TElement>(BoundedChannelOptionsOfOneDropOldest);

                    var consumerTask = StartConsumer(observer, channel.Reader, arg.arg, arg.selector, cancellationToken);
                    var producerTask = StartProducer(arg.source, channel.Writer, cancellationToken);

                    await producerTask.ConfigureAwait(false);
                    await consumerTask.ConfigureAwait(false);

                    return Disposable.Empty;
                },
                Hint.Of<TResult>()
            );


        static async ValueTask StartConsumer(
            IObserver<TResult> observer,
            ChannelReader<TElement> channelReader,
            TArg arg,
            Func<TArg, TElement, TResult> selector,
            CancellationToken cancellationToken
        )
        {
            try
            {
                while (await channelReader.WaitToReadAsync(cancellationToken).ConfigureAwait(false))
                {
                    while (channelReader.TryRead(out var element))
                    {
                        cancellationToken.ThrowIfCancellationRequested();

                        var result = selector(arg, element);

                        observer.OnNext(result);
                    }
                }

                observer.OnCompleted();
            }
            catch (Exception e)
            {
                observer.OnError(e);
            }
        }
    }




    public static IObservable<TResult> Channeled<TArg, TElement, TResult>(
        this IObservable<TElement> source,
        TArg arg,
        Func<TArg, IObserver<TResult>, ChannelReader<TElement>, CancellationToken, Task> startConsumer,
        Hint<TResult> resultHint
    )
    {
        return
            FillsObservable.Create(
                (source, arg, startConsumer),
                static async (arg, observer, cancellationToken) =>
                {
                    var channel = Channel.CreateBounded<TElement>(BoundedChannelOptionsOfOneDropOldest);

                    var consumerTask = arg.startConsumer(arg.arg, observer, channel.Reader, cancellationToken);
                    var producerTask = StartProducer(arg.source, channel.Writer, cancellationToken);

                    await producerTask.ConfigureAwait(false);
                    await consumerTask.ConfigureAwait(false);

                    return Disposable.Empty;
                },
                Hint.Of<TResult>()
            );
    }




    public static IObservable<TResult> Channeled<TArg, TElement, TResult>(
        this IObservable<TElement> source,
        TArg arg,
        Func<TArg, Channel<TElement>> channelFactory,
        Func<TArg, IObserver<TResult>, ChannelReader<TElement>, CancellationToken, Task> startConsumer,
        Hint<TResult> resultHint
    )
    {
        return
            FillsObservable.Create(
                (source, arg, channelFactory, startConsumer),
                static async (arg, observer, cancellationToken) =>
                {
                    var channel = arg.channelFactory(arg.arg);

                    var consumerTask = arg.startConsumer(arg.arg, observer, channel.Reader, cancellationToken);
                    var producerTask = StartProducer(arg.source, channel.Writer, cancellationToken);

                    await producerTask.ConfigureAwait(false);
                    await consumerTask.ConfigureAwait(false);

                    return Disposable.Empty;
                },
                Hint.Of<TResult>()
            );
    }




    private static async ValueTask StartProducer<TElement>(
        IObservable<TElement> observable,
        ChannelWriter<TElement> channelWriter,
        CancellationToken cancellationToken
    )
    {
        try
        {
            await observable
                .Select(
                    channelWriter,
                    static (channelWriter, element) =>
                        Observable.FromAsync(cancellationToken =>
                            channelWriter.WriteAsync(element, cancellationToken).AsTask()
                        )
                )
                .Catch((Exception e) => Observable.Return(Observable.Throw<Unit>(e)))
                .Concat()
                .ToTask(cancellationToken)
                .ConfigureAwait(false);

            channelWriter.Complete();
        }
        catch (Exception e)
        {
            channelWriter.Complete(e);
        }
    }
}
