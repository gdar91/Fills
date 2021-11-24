using Microsoft.FSharp.Core;

namespace Fills;

public static class FillsVOptionExtensions
{
    public static FSharpValueOption<T> NewSome<T>(this Hint<FSharpValueOption<T>> hint, T value) =>
        FSharpValueOption<T>.NewValueSome(value);

    public static FSharpValueOption<T> NewNone<T>(this Hint<FSharpValueOption<T>> hint) =>
        FSharpValueOption<T>.ValueNone;


    public static bool TryGetValue<T>(this FSharpValueOption<T> option, out T value)
    {
        if (ValueOption.IsNone(option))
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
        return option.TryGetValue(out var value) ? whenSome(value) : whenNone();
    }


    public static TResult Match<T, TResult>(
        this FSharpValueOption<T> option,
        Func<T, TResult> whenSome,
        TResult whenNoneValue
    )
    {
        return option.TryGetValue(out var value) ? whenSome(value) : whenNoneValue;
    }


    public static T ValueOrDefault<T>(this FSharpValueOption<T> option, T defaultValue) =>
        option.TryGetValue(out var value) ? value : defaultValue;


    public static T ValueOrDefault<T>(this FSharpValueOption<T> option, Func<T> defaultValueFactory) =>
        option.TryGetValue(out var value) ? value : defaultValueFactory();


    public static FSharpValueOption<TResult> Select<T, TResult>(
        this FSharpValueOption<T> option,
        Func<T, TResult> selector
    )
    {
        return option.TryGetValue(out var value)
            ? FSharpValueOption<TResult>.NewValueSome(selector(value))
            : FSharpValueOption<TResult>.ValueNone;
    }


    public static FSharpValueOption<TResult> SelectMany<T, TResult>(
        this FSharpValueOption<T> option,
        Func<T, FSharpValueOption<TResult>> selector
    )
    {
        return option.TryGetValue(out var value) ? selector(value) : FSharpValueOption<TResult>.None;
    }


    public static FSharpValueOption<TResult> SelectMany<T, TCollection, TResult>(
        this FSharpValueOption<T> option,
        Func<T, FSharpValueOption<TCollection>> collectionSelector,
        Func<T, TCollection, TResult> resultSelector
    )
    {
        return option.TryGetValue(out var value)
            ? collectionSelector(value).Select(collection => resultSelector(value, collection))
            : FSharpValueOption<TResult>.ValueNone;
    }
}
