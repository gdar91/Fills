using Microsoft.FSharp.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fills
{
    public static class FSharpResultExtensions
    {
        public static FSharpResult<T, TError> NewOk<T, TError>(
            this Hint<FSharpResult<T, TError>> hint,
            T resultValue
        )
        {
            return FSharpResult<T, TError>.NewOk(resultValue);
        }

        public static FSharpResult<T, TError> NewError<T, TError>(
            this Hint<FSharpResult<T, TError>> hint,
            TError resultValue
        )
        {
            return FSharpResult<T, TError>.NewError(resultValue);
        }


        public static bool TryGetValue<T, TError>(this FSharpResult<T, TError> result, out T value)
        {
            if (result.IsOk)
            {
                value = result.ResultValue;

                return true;
            }

            value = default!;

            return false;
        }


        public static TResult Match<T, TError, TResult>(
            this FSharpResult<T, TError> result,
            Func<T, TResult> whenOk,
            Func<TError, TResult> whenError
        )
        {
            return
                result.IsOk
                    ? whenOk(result.ResultValue)
                    : whenError(result.ErrorValue);
        }


        public static FSharpResult<TResult, TError> Select<T, TError, TResult>(
            this FSharpResult<T, TError> result,
            Func<T, TResult> selector
        )
        {
            return
                result.Match(
                    value => FillsResult.Ok<TResult, TError>(selector(value)),
                    FillsResult.Error<TResult, TError>
                );
        }


        public static FSharpResult<TResult, TError> Apply<T, TError, TResult>(
            this FSharpResult<T, TError> result,
            FSharpResult<Func<T, TResult>, TError> selectorResult
        )
        {
            return
                result.Match(
                    value =>
                        selectorResult.Match(
                            selector => FillsResult.Ok<TResult, TError>(selector(value)),
                            FillsResult.Error<TResult, TError>
                        ),
                    FillsResult.Error<TResult, TError>
                );
        }


        public static FSharpResult<TResult, TError> Zip<T1, T2, TError, TResult>(
            this FSharpResult<T1, TError> result1,
            FSharpResult<T2, TError> result2,
            Func<T1, T2, TResult> selector
        )
        {
            return
                result1.Match(
                    value1 =>
                        result2.Match(
                            value2 => FillsResult.Ok<TResult, TError>(selector(value1, value2)),
                            FillsResult.Error<TResult, TError>
                        ),
                    FillsResult.Error<TResult, TError>
                );
        }


        public static FSharpResult<TResult, TError> SelectMany<T, TError, TResult>(
            this FSharpResult<T, TError> result,
            Func<T, FSharpResult<TResult, TError>> selector
        )
        {
            return
                result.Match(
                    value => selector(value),
                    FillsResult.Error<TResult, TError>
                );
        }


        public static FSharpResult<TResult, TError> SelectMany<T, TCollection, TError, TResult>(
            this FSharpResult<T, TError> result,
            Func<T, FSharpResult<TCollection, TError>> collectionSelector,
            Func<T, TCollection, TResult> resultSelector
        )
        {
            return
                result.Match(
                    value =>
                        collectionSelector(value)
                            .Select(collection =>
                                resultSelector(value, collection)
                            ),
                    FillsResult.Error<TResult, TError>
                );
        }


        public static FSharpResult<IEnumerable<T>, TError> WhenAll<T, TError>(
            this IEnumerable<FSharpResult<T, TError>> results
        )
        {
            return
                results.Aggregate(
                    FillsResult.Ok<IEnumerable<T>, TError>(Enumerable.Empty<T>()),
                    (state, element) => Zip(state, element, Enumerable.Append)
                );
        }
    }
}
