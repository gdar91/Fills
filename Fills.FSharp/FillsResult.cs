using Microsoft.FSharp.Core;

namespace Fills;

public static class FillsResult
{
    public static FSharpResult<T, TError> Return<T, TError>(T result) => FSharpResult<T, TError>.NewOk(result);

    public static FSharpResult<T, TError> Return<T, TError>(T result, Hint<TError> errorHint) =>
        FSharpResult<T, TError>.NewOk(result);


    public static FSharpResult<T, TError> Throw<T, TError>(TError error) => FSharpResult<T, TError>.NewError(error);

    public static FSharpResult<T, TError> Throw<T, TError>(TError error, Hint<T> resultHint) =>
        FSharpResult<T, TError>.NewError(error);


    public static Hint<FSharpResult<T, TError>> Hint<T, TError>(Hint<T> resultHint, Hint<TError> errorHint) => default;

    public static Hint<TResult> UnHint<T, TError, TResult>(
        Hint<FSharpResult<T, TError>> hint,
        Func<Hint<T>, Hint<TError>, Hint<TResult>> func
    )
    {
        return default;
    }
}
