using System.Collections.Concurrent;

namespace Fills;

public interface IMemo<in TKey, out TValue>
{
    TValue this[TKey key] { get; }
}


public sealed class Memo<TKey, TValue> : IMemo<TKey, TValue> where TKey : notnull
{
    private readonly ConcurrentDictionary<TKey, TValue> dictionary = new();

    private readonly Func<TKey, TValue> func;


    public Memo(Func<TKey, TValue> func)
    {
        this.func = func;
    }


    public TValue this[TKey key] => dictionary.GetOrAdd(key, func);
}


public static class Memo<TKey> where TKey : notnull
{
    public static Memo<TKey, TValue> Of<TValue>(Func<TKey, TValue> func) => new(func);
}
