﻿using System;
using System.Collections.Generic;

namespace Fills
{
    public static class FillsEnumerable
    {
        public static IEnumerable<TElement> Return<TElement>(TElement element)
        {
            yield return element;
        }




        public static IEnumerable<TElement> Empty<TElement>()
        {
            yield break;
        }


        public static IEnumerable<TElement> Empty<TElement>(Hint<TElement> hint)
        {
            yield break;
        }




        public static Hint<IEnumerable<TElement>> Hint<TElement>(Hint<TElement> hint) => default;


        public static Hint<TElement> Unhint<TElement>(Hint<IEnumerable<TElement>> hint) => default;




        public static IEnumerable<TElement> From<TElement>(params TElement[] elements)
        {
            foreach (var item in elements)
            {
                yield return item;
            }
        }




        public static IEnumerable<TElement> From<TElement>(Func<TElement> elementFactory)
        {
            var element = elementFactory();

            yield return element;
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
