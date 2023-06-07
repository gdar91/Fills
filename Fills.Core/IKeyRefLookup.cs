namespace Fills;

public interface IKeyRefLookup<TKey, out TValue>
{
    TValue this[in TKey key] { get; }
}
