using Microsoft.FSharp.Core;

namespace Fills
{
    public static partial class ChoiceExtensions
    {
        public static Hint<FSharpChoice<T1, T2>> Of2<T1, T2>(
            Hint<T1> hint1,
            Hint<T2> hint2
        )
        {
            return default;
        }


        public static Hint<FSharpChoice<T1, T2, T3>> WithCase<T1, T2, T3>(
            this Hint<FSharpChoice<T1, T2>> hint,
            Hint<T3> caseHint
        )
        {
            return default;
        }


        public static Hint<FSharpChoice<T1, T2, T3, T4>> WithCase<T1, T2, T3, T4>(
            this Hint<FSharpChoice<T1, T2, T3>> hint,
            Hint<T4> caseHint
        )
        {
            return default;
        }


        public static Hint<FSharpChoice<T1, T2, T3, T4, T5>> WithCase<T1, T2, T3, T4, T5>(
            this Hint<FSharpChoice<T1, T2, T3, T4>> hint,
            Hint<T5> caseHint
        )
        {
            return default;
        }


        public static Hint<FSharpChoice<T1, T2, T3, T4, T5, T6>> WithCase<T1, T2, T3, T4, T5, T6>(
            this Hint<FSharpChoice<T1, T2, T3, T4, T5>> hint,
            Hint<T6> caseHint
        )
        {
            return default;
        }


        public static Hint<FSharpChoice<T1, T2, T3, T4, T5, T6, T7>> WithCase<T1, T2, T3, T4, T5, T6, T7>(
            this Hint<FSharpChoice<T1, T2, T3, T4, T5, T6>> hint,
            Hint<T7> caseHint
        )
        {
            return default;
        }
    }
}
