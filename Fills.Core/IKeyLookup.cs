namespace Fills;

public interface IKeyLookup<in TKey, out TValue>
{
    TValue this[TKey key] { get; }
}
