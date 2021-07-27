using Microsoft.FSharp.Core;
using System;

namespace Fills
{
    public static class FillsResult
    {
        public static FSharpResult<T, TError> Ok<T, TError>(T result) =>
            FSharpResult<T, TError>.NewOk(result);

        public static FSharpResult<T, TError> Ok<T, TError>(T result, Hint<TError> errorHint) =>
            FSharpResult<T, TError>.NewOk(result);


        public static FSharpResult<T, TError> Error<T, TError>(TError error) =>
            FSharpResult<T, TError>.NewError(error);

        public static FSharpResult<T, TError> Error<T, TError>(TError error, Hint<T> resultHint) =>
            FSharpResult<T, TError>.NewError(error);


        public static Hint<FSharpResult<T, TError>> Hint<T, TError>(
            Hint<T> resultHint,
            Hint<TError> errorHint
        )
        {
            return default;
        }


        public static Hint<TResult> Unhint<T, TError, TResult>(
            Hint<FSharpResult<T, TError>> hint,
            Func<Hint<T>, Hint<TError>, Hint<TResult>> func
        )
        {
            return default;
        }


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
