using System;
using System.Reactive.Linq;

namespace Fills
{
    public static partial class FillsObservable
    {
        public static IObservable<TElement> From<TElement>(Func<TElement> elementFactory)
        {
            return Observable.Defer(() =>
            {
                var element = elementFactory();

                var resultObservable = Observable.Return(element);

                return resultObservable;
            });
        }
    }
}
