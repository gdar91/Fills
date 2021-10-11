namespace Fills;

public delegate bool TrySelector<TElement, TResult>(TElement element, out TResult result);
