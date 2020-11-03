using System;
using System.Collections.Concurrent;

namespace Fills
{
    public sealed class ObservableGroup<TKey, TValue>
    {
        private readonly ConcurrentDictionary<TKey, IObservable<TValue>> dictionary =
            new ConcurrentDictionary<TKey, IObservable<TValue>>();

        private readonly Func<TKey, IObservable<TValue>> factory;


        public ObservableGroup(Func<TKey, IObservable<TValue>> factory)
        {
            this.factory = factory;
        }


        public IObservable<TValue> this[TKey key] => dictionary.GetOrAdd(key, factory);
    }
}
