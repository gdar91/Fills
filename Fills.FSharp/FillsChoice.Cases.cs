using Microsoft.FSharp.Core;

namespace Fills;

public static partial class FillsChoiceExtensions
{
    public static Hint<FSharpChoice<T1, T2, T3>> WithCase<T1, T2, T3>(
        this Hint<FSharpChoice<T1, T2>> hint,
        Hint<T3> caseHint
    ) => default;

    public static Hint<FSharpChoice<T1, T2>> WithoutCase<T1, T2, T3>(
        this Hint<FSharpChoice<T1, T2, T3>> hint
    ) => default;


    public static Hint<FSharpChoice<T1, T2, T3, T4>> WithCase<T1, T2, T3, T4>(
        this Hint<FSharpChoice<T1, T2, T3>> hint,
        Hint<T4> caseHint
    ) => default;

    public static Hint<FSharpChoice<T1, T2, T3>> WithoutCase<T1, T2, T3, T4>(
        this Hint<FSharpChoice<T1, T2, T3, T4>> hint
    ) => default;


    public static Hint<FSharpChoice<T1, T2, T3, T4, T5>> WithCase<T1, T2, T3, T4, T5>(
        this Hint<FSharpChoice<T1, T2, T3, T4>> hint,
        Hint<T5> caseHint
    ) => default;

    public static Hint<FSharpChoice<T1, T2, T3, T4>> WithoutCase<T1, T2, T3, T4, T5>(
        this Hint<FSharpChoice<T1, T2, T3, T4, T5>> hint
    ) => default;


    public static Hint<FSharpChoice<T1, T2, T3, T4, T5, T6>> WithCase<T1, T2, T3, T4, T5, T6>(
        this Hint<FSharpChoice<T1, T2, T3, T4, T5>> hint,
        Hint<T6> caseHint
    ) => default;

    public static Hint<FSharpChoice<T1, T2, T3, T4, T5>> WithoutCase<T1, T2, T3, T4, T5, T6>(
        this Hint<FSharpChoice<T1, T2, T3, T4, T5, T6>> hint
    ) => default;


    public static Hint<FSharpChoice<T1, T2, T3, T4, T5, T6, T7>> WithCase<T1, T2, T3, T4, T5, T6, T7>(
        this Hint<FSharpChoice<T1, T2, T3, T4, T5, T6>> hint,
        Hint<T7> caseHint
    ) => default;

    public static Hint<FSharpChoice<T1, T2, T3, T4, T5, T6>> WithoutCase<T1, T2, T3, T4, T5, T6, T7>(
        this Hint<FSharpChoice<T1, T2, T3, T4, T5, T6, T7>> hint
    ) => default;
}
