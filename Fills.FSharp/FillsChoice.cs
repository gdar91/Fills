using Microsoft.FSharp.Core;

namespace Fills;

public static class FillsChoice
{
    public static Hint<FSharpChoice<T1, T2>> Hint<T1, T2>(
        Hint<T1> hint1, Hint<T2> hint2
    ) => default;

    public static Hint<T> UnHint<T1, T2, T>(
        FSharpChoice<T1, T2> choice,
        Func<Hint<T1>, Hint<T2>, Hint<T>> func
    ) => default;


    public static Hint<FSharpChoice<T1, T2, T3>> Hint<T1, T2, T3>(
        Hint<T1> hint1, Hint<T2> hint2, Hint<T3> hint3
    ) => default;

    public static Hint<T> UnHint<T1, T2, T3, T>(
        FSharpChoice<T1, T2, T3> choice,
        Func<Hint<T1>, Hint<T2>, Hint<T3>, Hint<T>> func
    ) => default;


    public static Hint<FSharpChoice<T1, T2, T3, T4>> Hint<T1, T2, T3, T4>(
        Hint<T1> hint1, Hint<T2> hint2, Hint<T3> hint3, Hint<T4> hint4
    ) => default;

    public static Hint<T> UnHint<T1, T2, T3, T4, T>(
        FSharpChoice<T1, T2, T3, T4> choice,
        Func<Hint<T1>, Hint<T2>, Hint<T3>, Hint<T4>, Hint<T>> func
    ) => default;


    public static Hint<FSharpChoice<T1, T2, T3, T4, T5>> Hint<T1, T2, T3, T4, T5>(
        Hint<T1> hint1, Hint<T2> hint2, Hint<T3> hint3, Hint<T4> hint4,
        Hint<T5> hint5
    ) => default;

    public static Hint<T> UnHint<T1, T2, T3, T4, T5, T>(
        FSharpChoice<T1, T2, T3, T4, T5> choice,
        Func<Hint<T1>, Hint<T2>, Hint<T3>, Hint<T4>, Hint<T5>, Hint<T>> func
    ) => default;


    public static Hint<FSharpChoice<T1, T2, T3, T4, T5, T6>> Hint<T1, T2, T3, T4, T5, T6>(
        Hint<T1> hint1, Hint<T2> hint2, Hint<T3> hint3, Hint<T4> hint4,
        Hint<T5> hint5, Hint<T6> hint6
    ) => default;

    public static Hint<T> UnHint<T1, T2, T3, T4, T5, T6, T>(
        FSharpChoice<T1, T2, T3, T4, T5, T6> choice,
        Func<Hint<T1>, Hint<T2>, Hint<T3>, Hint<T4>, Hint<T5>, Hint<T6>, Hint<T>> func
    ) => default;


    public static Hint<FSharpChoice<T1, T2, T3, T4, T5, T6, T7>> Hint<T1, T2, T3, T4, T5, T6, T7>(
        Hint<T1> hint1, Hint<T2> hint2, Hint<T3> hint3, Hint<T4> hint4,
        Hint<T5> hint5, Hint<T6> hint6, Hint<T7> hint7
    ) => default;

    public static Hint<T> UnHint<T1, T2, T3, T4, T5, T6, T7, T>(
        FSharpChoice<T1, T2, T3, T4, T5, T6, T7> choice,
        Func<Hint<T1>, Hint<T2>, Hint<T3>, Hint<T4>, Hint<T5>, Hint<T6>, Hint<T7>, Hint<T>> func
    ) => default;
}
