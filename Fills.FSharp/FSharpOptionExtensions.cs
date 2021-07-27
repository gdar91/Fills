using Microsoft.FSharp.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fills
{
    public static class FSharpOptionExtensions
    {
        public static FSharpOption<T> NewSome<T>(this Hint<FSharpOption<T>> hint, T value) =>
            FSharpOption<T>.Some(value);

        public static FSharpOption<T> NewNone<T>(this Hint<FSharpOption<T>> hint) =>
            FSharpOption<T>.None;


        public static bool TryGetValue<T>(this FSharpOption<T> option, out T value)
        {
            if (OptionModule.IsSome(option))
            {
                value = option.Value;

                return true;
            }


            value = default!;

            return false;
        }


        public static TResult Match<T, TResult>(
            this FSharpOption<T> option,
            Func<T, TResult> whenSome,
            Func<TResult> whenNone
        )
        {
            return
                option.TryGetValue(out var value)
                    ? whenSome(value)
                    : whenNone();
        }


        public static TResult Match<T, TResult>(
            this FSharpOption<T> option,
            Func<T, TResult> whenSome,
            TResult whenNoneValue
        )
        {
            return
                option.TryGetValue(out var value)
                    ? whenSome(value)
                    : whenNoneValue;
        }


        public static T ValueOrDefault<T>(this FSharpOption<T> option, T defaultValue)
        {
            return
                option.TryGetValue(out var value)
                    ? value
                    : defaultValue;
        }


        public static T ValueOrDefault<T>(this FSharpOption<T> option, Func<T> defaultValueFactory)
        {
            return
                option.TryGetValue(out var value)
                    ? value
                    : defaultValueFactory();
        }


        public static FSharpOption<TResult> Select<T, TResult>(
            this FSharpOption<T> option,
            Func<T, TResult> selector
        )
        {
            return
                option.TryGetValue(out var value)
                    ? FillsOption.Some(selector(value))
                    : FillsOption.None<TResult>();
        }


        public static FSharpOption<TResult> Apply<T, TResult>(
            this FSharpOption<T> option,
            FSharpOption<Func<T, TResult>> selectorOption
        )
        {
            return
                option.TryGetValue(out var value) && selectorOption.TryGetValue(out var selector)
                    ? selector(value)
                    : FillsOption.None<TResult>();
        }


        public static FSharpOption<TResult> Zip<T1, T2, TResult>(
            this FSharpOption<T1> option1,
            FSharpOption<T2> option2,
            Func<T1, T2, TResult> selector
        )
        {
            return
                option1.TryGetValue(out var value1) && option2.TryGetValue(out var value2)
                    ? selector(value1, value2)
                    : FillsOption.None<TResult>();
        }


        public static FSharpOption<TResult> SelectMany<T, TResult>(
            this FSharpOption<T> option,
            Func<T, FSharpOption<TResult>> selector
        )
        {
            return
                option.TryGetValue(out var value)
                    ? selector(value)
                    : FillsOption.None<TResult>();
        }


        public static FSharpOption<TResult> SelectMany<T, TCollection, TResult>(
            this FSharpOption<T> option,
            Func<T, FSharpOption<TCollection>> collectionSelector,
            Func<T, TCollection, TResult> resultSelector
        )
        {
            return
                option.TryGetValue(out var value)
                    ? collectionSelector(value)
                        .Select(collection =>
                            resultSelector(value, collection)
                        )
                    : FillsOption.None<TResult>();
        }


        public static FSharpOption<IEnumerable<T>> WhenAll<T>(this IEnumerable<FSharpOption<T>> options)
        {
            return
                options.Aggregate(
                    FillsOption.Some(Enumerable.Empty<T>()),
                    (state, element) => Zip(state, element, Enumerable.Append)
                );
        }
    }
}
