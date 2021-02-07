﻿using System;
using System.Collections.Concurrent;

namespace Fills
{
    public interface IMemo<TKey, TValue>
    {
        TValue this[TKey key] { get; }
    }


    public sealed class Memo<TKey, TValue> : IMemo<TKey, TValue>
    {
        private readonly ConcurrentDictionary<TKey, TValue> dictionary =
            new ConcurrentDictionary<TKey, TValue>();

        private readonly Func<TKey, TValue> func;


        public Memo(Func<TKey, TValue> func)
        {
            this.func = func;
        }


        public TValue this[TKey key] => dictionary.GetOrAdd(key, func);
    }


    public static class Memo<TKey>
    {
        public static Memo<TKey, TValue> Of<TValue>(Func<TKey, TValue> func) =>
            new Memo<TKey, TValue>(func);
    }
}
