using Microsoft.FSharp.Core;
using System;

namespace Fills
{
    public static class FillsResult
    {
        public static FSharpResult<T, TError> Ok<T, TError>(T value) =>
            FSharpResult<T, TError>.NewOk(value);

        public static FSharpResult<T, TError> Ok<T, TError>(T value, TError errorWitness) =>
            FSharpResult<T, TError>.NewOk(value);


        public static FSharpResult<T, TError> Error<T, TError>(TError error) =>
            FSharpResult<T, TError>.NewError(error);

        public static FSharpResult<T, TError> Error<T, TError>(TError error, T valueWitness) =>
            FSharpResult<T, TError>.NewError(error);


        public static FSharpResult<TResult, TError> Zip<T1, T2, TError, TResult>(
            FSharpResult<T1, TError> result1,
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
    }
}
