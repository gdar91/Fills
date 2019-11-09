using System;
using System.Collections.Generic;

namespace Fills.Enumerable
{
    public static class FillsEnumerable
    {
        public static IEnumerable<TElement> Return<TElement>(TElement element)
        {
            yield return element;
        }




        public static IEnumerable<TElement> From<TElement>(params TElement[] elements)
        {
            foreach (var item in elements)
            {
                yield return item;
            }
        }




        public static IEnumerable<TElement> Defer<TElement>(Func<IEnumerable<TElement>> factory)
        {
            var enumerable = factory();

            foreach (var item in enumerable)
            {
                yield return item;
            }
        }




        public delegate bool Generator<TState, TElement>(
            TState previousState,
            out TElement element,
            out TState state
        );


        public static IEnumerable<TElement> Unfold<TState, TElement>(
            TState initialState,
            Generator<TState, TElement> generator
        )
        {
            var previousState = initialState;

            while (generator(previousState, out var element, out var state))
            {
                yield return element;

                previousState = state;
            }
        }


        public static IEnumerable<TElement> Unfold<TState, TElement>(
            TState initialState,
            Func<TState, Tuple<TElement, TState>?> generator
        )
        {
            var previousState = initialState;

            while (generator(previousState) is Tuple<TElement, TState> (var element, var state))
            {
                yield return element;

                previousState = state;
            }
        }
    }
}
