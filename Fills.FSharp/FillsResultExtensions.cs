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
    
    public static TResult Match<TState, T, TError, TResult>(
        this FSharpResult<T, TError> result,
        TState state,
        Func<TState, T, TResult> whenOk,
        Func<TState, TError, TResult> whenError
    )
    {
        return result.IsOk
            ? whenOk(state, result.ResultValue)
            : whenError(state, result.ErrorValue);
    }


    public static FSharpResult<TResult, TError> Select<T, TError, TResult>(
        this FSharpResult<T, TError> result,
        Func<T, TResult> selector
    )
    {
        return result.IsOk
            ? FillsResult.Ok<TResult, TError>(selector(result.ResultValue))
            : FillsResult.Error<TResult, TError>(result.ErrorValue);
    }
    
    public static FSharpResult<TResult, TError> Select<TState, T, TError, TResult>(
        this FSharpResult<T, TError> result,
        TState state,
        Func<TState, T, TResult> selector
    )
    {
        return result.IsOk
            ? FillsResult.Ok<TResult, TError>(selector(state, result.ResultValue))
            : FillsResult.Error<TResult, TError>(result.ErrorValue);
    }


    public static FSharpResult<TResult, TError> SelectMany<T, TError, TResult>(
        this FSharpResult<T, TError> result,
        Func<T, FSharpResult<TResult, TError>> selector
    )
    {
        return result.IsOk
            ? selector(result.ResultValue)
            : FillsResult.Error<TResult, TError>(result.ErrorValue);
    }
    
    public static FSharpResult<TResult, TError> SelectMany<TState, T, TError, TResult>(
        this FSharpResult<T, TError> result,
        TState state,
        Func<TState, T, FSharpResult<TResult, TError>> selector
    )
    {
        return result.IsOk
            ? selector(state, result.ResultValue)
            : FillsResult.Error<TResult, TError>(result.ErrorValue);
    }


    public static FSharpResult<TResult, TError> SelectMany<T, TCollection, TError, TResult>(
        this FSharpResult<T, TError> result,
        Func<T, FSharpResult<TCollection, TError>> collectionSelector,
        Func<T, TCollection, TResult> resultSelector
    )
    {
        if (result.IsError)
        {
            return FillsResult.Error<TResult, TError>(result.ErrorValue);
        }

        var value = result.ResultValue;
        var collectionResult = collectionSelector(value);
        
        return collectionResult.IsOk
            ? FillsResult.Ok<TResult, TError>(resultSelector(value, collectionResult.ResultValue))
            : FillsResult.Error<TResult, TError>(collectionResult.ErrorValue);
    }
}
