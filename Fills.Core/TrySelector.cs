namespace Fills;

public delegate bool TrySelector<in TElement, TResult>(TElement element, out TResult result);

public delegate bool TrySelector<in TArg, in TElement, TResult>(TArg arg, TElement element, out TResult result);
