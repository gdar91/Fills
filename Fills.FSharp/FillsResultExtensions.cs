using Microsoft.FSharp.Core;

namespace Fills;

public static class FillsResultExtensions
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
        if (result.IsError)
        {
            value = default!;

            return false;
        }

        value = result.ResultValue;

        return true;
    }


    public static TResult Match<T, TError, TResult>(
        this FSharpResult<T, TError> result,
        Func<T, TResult> whenOk,
        Func<TError, TResult> whenError
    )
    {
        return result.IsOk
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


    public static FSharpResult<TResult, TError> SelectMany<T, TError, TResult>(
        this FSharpResult<T, TError> result,
        Func<T, FSharpResult<TResult, TError>> selector
    )
    {
        return result.Match(selector, FillsResult.Error<TResult, TError>);
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
                    collectionSelector(value).Select(collection =>
                        resultSelector(value, collection)
                    ),
                FillsResult.Error<TResult, TError>
            );
    }
}
