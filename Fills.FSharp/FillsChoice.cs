using Microsoft.FSharp.Core;

namespace Fills;

public static class FillsChoice
{
    public static Hint<FSharpChoice<T1, T2>> Hint<T1, T2>(
        FSharpChoice<T1, T2> choice
    ) => default;

    public static Hint<T> Unhint<T1, T2, T>(
        FSharpChoice<T1, T2> choice,
        Func<Hint<T1>, Hint<T2>, Hint<T>> func
    ) => default;


    public static Hint<FSharpChoice<T1, T2, T3>> Hint<T1, T2, T3>(
        FSharpChoice<T1, T2, T3> choice
    ) => default;

    public static Hint<T> Unhint<T1, T2, T3, T>(
        FSharpChoice<T1, T2, T3> choice,
        Func<Hint<T1>, Hint<T2>, Hint<T3>, Hint<T>> func
    ) => default;


    public static Hint<FSharpChoice<T1, T2, T3, T4>> Hint<T1, T2, T3, T4>(
        FSharpChoice<T1, T2, T3, T4> choice
    ) => default;

    public static Hint<T> Unhint<T1, T2, T3, T4, T>(
        FSharpChoice<T1, T2, T3, T4> choice,
        Func<Hint<T1>, Hint<T2>, Hint<T3>, Hint<T4>, Hint<T>> func
    ) => default;


    public static Hint<FSharpChoice<T1, T2, T3, T4, T5>> Hint<T1, T2, T3, T4, T5>(
        FSharpChoice<T1, T2, T3, T4, T5> choice
    ) => default;

    public static Hint<T> Unhint<T1, T2, T3, T4, T5, T>(
        FSharpChoice<T1, T2, T3, T4, T5> choice,
        Func<Hint<T1>, Hint<T2>, Hint<T3>, Hint<T4>, Hint<T5>, Hint<T>> func
    ) => default;


    public static Hint<FSharpChoice<T1, T2, T3, T4, T5, T6>> Hint<T1, T2, T3, T4, T5, T6>(
        FSharpChoice<T1, T2, T3, T4, T5, T6> choice
    ) => default;

    public static Hint<T> Unhint<T1, T2, T3, T4, T5, T6, T>(
        FSharpChoice<T1, T2, T3, T4, T5, T6> choice,
        Func<Hint<T1>, Hint<T2>, Hint<T3>, Hint<T4>, Hint<T5>, Hint<T6>, Hint<T>> func
    ) => default;


    public static Hint<FSharpChoice<T1, T2, T3, T4, T5, T6, T7>> Hint<T1, T2, T3, T4, T5, T6, T7>(
        FSharpChoice<T1, T2, T3, T4, T5, T6, T7> choice
    ) => default;

    public static Hint<T> Unhint<T1, T2, T3, T4, T5, T6, T7, T>(
        FSharpChoice<T1, T2, T3, T4, T5, T6, T7> choice,
        Func<Hint<T1>, Hint<T2>, Hint<T3>, Hint<T4>, Hint<T5>, Hint<T6>, Hint<T7>, Hint<T>> func
    ) => default;
}
