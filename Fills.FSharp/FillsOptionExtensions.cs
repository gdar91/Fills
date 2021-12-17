using Microsoft.FSharp.Core;

namespace Fills;

public static class FillsOptionExtensions
{
    public static FSharpOption<T> NewSome<T>(this Hint<FSharpOption<T>> hint, T value) => FSharpOption<T>.Some(value);

    public static FSharpOption<T> NewNone<T>(this Hint<FSharpOption<T>> hint) => FSharpOption<T>.None;


    public static bool TryGetValue<T>(this FSharpOption<T> option, out T value)
    {
        if (OptionModule.IsNone(option))
        {
            value = default!;

            return false;
        }

        value = option.Value;

        return true;
    }


    public static TResult Match<T, TResult>(
        this FSharpOption<T> option,
        Func<T, TResult> whenSome,
        Func<TResult> whenNone
    )
    {
        return option.TryGetValue(out var value) ? whenSome(value) : whenNone();
    }


    public static TResult Match<T, TResult>(
        this FSharpOption<T> option,
        Func<T, TResult> whenSome,
        TResult whenNoneValue
    )
    {
        return option.TryGetValue(out var value) ? whenSome(value) : whenNoneValue;
    }


    public static T ValueOrDefault<T>(this FSharpOption<T> option, T defaultValue) =>
        option.TryGetValue(out var value) ? value : defaultValue;


    public static T ValueOrDefault<T>(this FSharpOption<T> option, Func<T> defaultValueFactory) =>
        option.TryGetValue(out var value) ? value : defaultValueFactory();


    public static FSharpOption<TResult> Select<T, TResult>(this FSharpOption<T> option, Func<T, TResult> selector)
    {
        return option.TryGetValue(out var value)
            ? FSharpOption<TResult>.Some(selector(value))
            : FSharpOption<TResult>.None;
    }


    public static FSharpOption<TResult> SelectMany<T, TResult>(
        this FSharpOption<T> option,
        Func<T, FSharpOption<TResult>> selector
    )
    {
        return option.TryGetValue(out var value) ? selector(value) : FSharpOption<TResult>.None;
    }


    public static FSharpOption<TResult> SelectMany<T, TCollection, TResult>(
        this FSharpOption<T> option,
        Func<T, FSharpOption<TCollection>> collectionSelector,
        Func<T, TCollection, TResult> resultSelector
    )
    {
        return option.TryGetValue(out var value)
            ? collectionSelector(value).Select(collection => resultSelector(value, collection))
            : FSharpOption<TResult>.None;
    }
}
