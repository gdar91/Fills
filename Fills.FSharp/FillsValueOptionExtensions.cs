using Microsoft.FSharp.Core;

namespace Fills;

public static class FillsValueOptionExtensions
{
    public static FSharpValueOption<T> NewValueSome<T>(this Hint<FSharpValueOption<T>> hint, T value) =>
        FSharpValueOption<T>.NewValueSome(value);

    public static FSharpValueOption<T> NewValueNone<T>(this Hint<FSharpValueOption<T>> hint) =>
        FSharpValueOption<T>.ValueNone;


    public static bool TryGetValue<T>(this FSharpValueOption<T> option, out T value)
    {
        if (option.IsNone)
        {
            value = default!;

            return false;
        }

        value = option.Value;

        return true;
    }


    public static TResult Match<T, TResult>(
        this FSharpValueOption<T> option,
        Func<T, TResult> whenSome,
        Func<TResult> whenNone
    )
    {
        return option.IsSome
            ? whenSome(option.Value)
            : whenNone();
    }


    public static TResult Match<T, TResult>(
        this FSharpValueOption<T> option,
        Func<T, TResult> whenSome,
        TResult whenNoneValue
    )
    {
        return option.IsSome
            ? whenSome(option.Value)
            : whenNoneValue;
    }


    public static T ValueOrDefault<T>(this FSharpValueOption<T> option, T defaultValue)
    {
        return option.IsSome
            ? option.Value
            : defaultValue;
    }


    public static T ValueOrDefault<T>(this FSharpValueOption<T> option, Func<T> defaultValueFactory)
    {
        return option.IsSome
            ? option.Value
            : defaultValueFactory();
    }


    public static FSharpValueOption<TResult> Select<T, TResult>(
        this FSharpValueOption<T> option,
        Func<T, TResult> selector
    )
    {
        return option.IsSome
            ? FSharpValueOption<TResult>.NewValueSome(selector(option.Value))
            : FSharpValueOption<TResult>.ValueNone;
    }


    public static FSharpValueOption<TResult> SelectMany<T, TResult>(
        this FSharpValueOption<T> option,
        Func<T, FSharpValueOption<TResult>> selector
    )
    {
        return option.IsSome
            ? selector(option.Value)
            : FSharpValueOption<TResult>.ValueNone;
    }


    public static FSharpValueOption<TResult> SelectMany<T, TCollection, TResult>(
        this FSharpValueOption<T> option,
        Func<T, FSharpValueOption<TCollection>> collectionSelector,
        Func<T, TCollection, TResult> resultSelector
    )
    {
        return option.IsSome
            ? collectionSelector(option.Value).Select(collection => resultSelector(option.Value, collection))
            : FSharpValueOption<TResult>.ValueNone;
    }
}
