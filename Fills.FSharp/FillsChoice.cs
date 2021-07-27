using Microsoft.FSharp.Core;

namespace Fills
{
    public static partial class FillsChoice
    {
        public static Hint<FSharpChoice<T1, T2>> Hint<T1, T2>(
            Hint<T1> hint1,
            Hint<T2> hint2
        )
        {
            return default;
        }


        public static Hint<FSharpChoice<T1, T2, T3>> Hint<T1, T2, T3>(
            Hint<T1> hint1,
            Hint<T2> hint2,
            Hint<T3> hint3
        )
        {
            return default;
        }


        public static Hint<FSharpChoice<T1, T2, T3, T4>> Hint<T1, T2, T3, T4>(
            Hint<T1> hint1,
            Hint<T2> hint2,
            Hint<T3> hint3,
            Hint<T4> hint4
        )
        {
            return default;
        }


        public static Hint<FSharpChoice<T1, T2, T3, T4, T5>> Hint<T1, T2, T3, T4, T5>(
            Hint<T1> hint1,
            Hint<T2> hint2,
            Hint<T3> hint3,
            Hint<T4> hint4,
            Hint<T5> hint5
        )
        {
            return default;
        }


        public static Hint<FSharpChoice<T1, T2, T3, T4, T5, T6>> Hint<T1, T2, T3, T4, T5, T6>(
            Hint<T1> hint1,
            Hint<T2> hint2,
            Hint<T3> hint3,
            Hint<T4> hint4,
            Hint<T5> hint5,
            Hint<T6> hint6
        )
        {
            return default;
        }


        public static Hint<FSharpChoice<T1, T2, T3, T4, T5 ,T6, T7>> Hint<T1, T2, T3, T4, T5, T6, T7>(
            Hint<T1> hint1,
            Hint<T2> hint2,
            Hint<T3> hint3,
            Hint<T4> hint4,
            Hint<T5> hint5,
            Hint<T6> hint6,
            Hint<T7> hint7
        )
        {
            return default;
        }
    }
}
