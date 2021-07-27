using Microsoft.FSharp.Core;

namespace Fills
{
    public static partial class ChoiceExtensions
    {
        public static Hint<FSharpChoice<T1, T2, T3, T4, T5, T6>> WithoutCase<T1, T2, T3, T4, T5, T6, T7>(
            this Hint<FSharpChoice<T1, T2, T3, T4, T5, T6, T7>> hint
        )
        {
            return default;
        }


        public static Hint<FSharpChoice<T1, T2, T3, T4, T5>> WithoutCase<T1, T2, T3, T4, T5, T6>(
            this Hint<FSharpChoice<T1, T2, T3, T4, T5, T6>> hint
        )
        {
            return default;
        }


        public static Hint<FSharpChoice<T1, T2, T3, T4>> WithoutCase<T1, T2, T3, T4, T5>(
            this Hint<FSharpChoice<T1, T2, T3, T4, T5>> hint
        )
        {
            return default;
        }


        public static Hint<FSharpChoice<T1, T2, T3>> WithoutCase<T1, T2, T3, T4>(
            this Hint<FSharpChoice<T1, T2, T3, T4>> hint
        )
        {
            return default;
        }


        public static Hint<FSharpChoice<T1, T2>> WithoutCase<T1, T2, T3>(
            this Hint<FSharpChoice<T1, T2, T3>> hint
        )
        {
            return default;
        }
    }
}
