using System;
using System.Reactive.Linq;

namespace Fills
{
    public static partial class ExtensionsForObservable
    {
        public static IObservable<TResult> Bisect<TElement, TSeparator, TResult>(
            this IObservable<TElement> observable,
            IObservable<TSeparator> separator,
            Func<IObservable<TElement>, IObservable<TResult>> elementsBeforeSelector,
            Func<IObservable<TElement>, IObservable<TResult>> elementsAfterSelector
        )
        {
            return observable
                .Window(
                    separator
                        .Take(1)
                        .Concat(Observable.Never<TSeparator>())
                )
                .Select((window, index) =>
                    index == 0
                        ? elementsBeforeSelector(window)
                        : elementsAfterSelector(window)
                )
                .Concat();
        }
    }
}
