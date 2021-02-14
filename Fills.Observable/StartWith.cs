using System;
using System.Reactive.Linq;

namespace Fills
{
    public static partial class ExtensionsForObservable
    {
        public static IObservable<TElement> StartWith<TElement>(
            this IObservable<TElement> observable,
            Func<TElement> elementFactory
        )
        {
            return Observable.Defer(() =>
            {
                var element = elementFactory();

                var resultObservable = observable.StartWith(element);

                return resultObservable;
            });
        }
    }
}
