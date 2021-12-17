﻿using Microsoft.FSharp.Core;

namespace Fills;

public static class FillsVOptionExtensions
{
    public static FSharpValueOption<T> NewSome<T>(this Hint<FSharpValueOption<T>> hint, T value) =>
        FSharpValueOption<T>.NewValueSome(value);

    public static FSharpValueOption<T> NewNone<T>(this Hint<FSharpValueOption<T>> hint) =>
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
        return option.IsSome ? whenSome(option.Value) : whenNone();
    }
    
    public static TResult Match<TState, T, TResult>(
        this FSharpValueOption<T> option,
        TState state,
        Func<TState, T, TResult> whenSome,
        Func<TState, TResult> whenNone
    )
    {
        return option.IsSome ? whenSome(state, option.Value) : whenNone(state);
    }


    public static TResult Match<T, TResult>(
        this FSharpValueOption<T> option,
        Func<T, TResult> whenSome,
        TResult whenNoneValue
    )
    {
        return option.IsSome ? whenSome(option.Value) : whenNoneValue;
    }
    
    public static TResult Match<TState, T, TResult>(
        this FSharpValueOption<T> option,
        TState state,
        Func<TState, T, TResult> whenSome,
        TResult whenNoneValue
    )
    {
        return option.IsSome ? whenSome(state, option.Value) : whenNoneValue;
    }


    public static T ValueOrDefault<T>(this FSharpValueOption<T> option, T defaultValue) =>
        option.IsSome ? option.Value : defaultValue;


    public static T ValueOrDefault<T>(this FSharpValueOption<T> option, Func<T> defaultValueFactory) =>
        option.IsSome ? option.Value : defaultValueFactory();


    public static FSharpValueOption<TResult> Select<T, TResult>(
        this FSharpValueOption<T> option,
        Func<T, TResult> selector
    )
    {
        return option.IsSome
            ? FSharpValueOption<TResult>.NewValueSome(selector(option.Value))
            : FSharpValueOption<TResult>.ValueNone;
    }
    
    public static FSharpValueOption<TResult> Select<TState, T, TResult>(
        this FSharpValueOption<T> option,
        TState state,
        Func<TState, T, TResult> selector
    )
    {
        return option.IsSome
            ? FSharpValueOption<TResult>.NewValueSome(selector(state, option.Value))
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
    
    public static FSharpValueOption<TResult> SelectMany<TState, T, TResult>(
        this FSharpValueOption<T> option,
        TState state,
        Func<TState, T, FSharpValueOption<TResult>> selector
    )
    {
        return option.IsSome
            ? selector(state, option.Value)
            : FSharpValueOption<TResult>.ValueNone;
    }


    public static FSharpValueOption<TResult> SelectMany<T, TCollection, TResult>(
        this FSharpValueOption<T> option,
        Func<T, FSharpValueOption<TCollection>> collectionSelector,
        Func<T, TCollection, TResult> resultSelector
    )
    {
        if (option.IsNone)
        {
            return FSharpValueOption<TResult>.ValueNone;
        }

        var value = option.Value;
        var collectionOption = collectionSelector(value);
        
        return collectionOption.IsSome
            ? FSharpValueOption<TResult>.NewValueSome(resultSelector(value, collectionOption.Value))
            : FSharpValueOption<TResult>.ValueNone;
    }
}
