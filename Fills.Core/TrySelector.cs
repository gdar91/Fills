namespace Fills;

public delegate bool TrySelector<in TElement, TResult>(TElement element, out TResult result);

public delegate bool TrySelector<in TState, in TElement, TResult>(TState state, TElement element, out TResult result);
