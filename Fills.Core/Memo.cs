using System.Collections.Concurrent;

namespace Fills;

public sealed class Memo<TArg, TKey, TValue> : IKeyLookup<TKey, TValue>, IKeyRefLookup<TKey, TValue>, IArgRef<TArg>
    where TKey : notnull
{
    private readonly ConcurrentDictionary<TKey, TValue> dictionary = new();

    private readonly TArg arg;

    private readonly Func<TKey, TArg, TValue> func;


    public Memo(TArg arg, Func<TKey, TArg, TValue> func)
    {
        this.arg = arg;
        this.func = func;
    }

    public Memo(in TArg arg, Func<TKey, TArg, TValue> func)
    {
        this.arg = arg;
        this.func = func;
    }


    public ref readonly TArg ArgRef => ref arg;

    public TArg Arg => arg;


    public TValue this[TKey key] => dictionary.GetOrAdd(key, func, arg);

    public TValue this[in TKey key] => dictionary.GetOrAdd(key, func, arg);
}


public sealed class Memo<TKey, TValue> : IKeyLookup<TKey, TValue>, IKeyRefLookup<TKey, TValue> where TKey : notnull
{
    private readonly ConcurrentDictionary<TKey, TValue> dictionary = new();

    private readonly Func<TKey, TValue> func;


    public Memo(Func<TKey, TValue> func)
    {
        this.func = func;
    }


    public TValue this[TKey key] => dictionary.GetOrAdd(key, func);

    public TValue this[in TKey key] => dictionary.GetOrAdd(key, func);
}


public static class Memo<TKey> where TKey : notnull
{
    public static Memo<TArg, TKey, TValue> Of<TArg, TValue>(TArg arg, Func<TKey, TArg, TValue> func) =>
        new(arg, func);

    public static Memo<TArg, TKey, TValue> Of<TArg, TValue>(in TArg arg, Func<TKey, TArg, TValue> func) =>
        new(in arg, func);

    public static Memo<TKey, TValue> Of<TValue>(Func<TKey, TValue> func) =>
        new(func);
}
