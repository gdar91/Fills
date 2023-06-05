namespace Fills;

public interface IKeyLookup<TKey, out TValue>
{
    TValue this[in TKey key] { get; }
}
