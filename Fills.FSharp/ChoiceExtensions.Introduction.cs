using Microsoft.FSharp.Core;

namespace Fills
{
    public static partial class ChoiceExtensions
    {
        public static FSharpChoice<T1, T2> New1<T1, T2>(
            this Hint<FSharpChoice<T1, T2>> hint,
            T1 item
        )
        {
            return FSharpChoice<T1, T2>.NewChoice1Of2(item);
        }

        public static FSharpChoice<T1, T2> New2<T1, T2>(
            this Hint<FSharpChoice<T1, T2>> hint,
            T2 item
        )
        {
            return FSharpChoice<T1, T2>.NewChoice2Of2(item);
        }








        public static FSharpChoice<T1, T2, T3> New1<T1, T2, T3>(
            this Hint<FSharpChoice<T1, T2, T3>> hint,
            T1 item
        )
        {
            return FSharpChoice<T1, T2, T3>.NewChoice1Of3(item);
        }

        public static FSharpChoice<T1, T2, T3> New2<T1, T2, T3>(
            this Hint<FSharpChoice<T1, T2, T3>> hint,
            T2 item
        )
        {
            return FSharpChoice<T1, T2, T3>.NewChoice2Of3(item);
        }

        public static FSharpChoice<T1, T2, T3> New3<T1, T2, T3>(
            this Hint<FSharpChoice<T1, T2, T3>> hint,
            T3 item
        )
        {
            return FSharpChoice<T1, T2, T3>.NewChoice3Of3(item);
        }








        public static FSharpChoice<T1, T2, T3, T4> New1<T1, T2, T3, T4>(
            this Hint<FSharpChoice<T1, T2, T3, T4>> hint,
            T1 item
        )
        {
            return FSharpChoice<T1, T2, T3, T4>.NewChoice1Of4(item);
        }

        public static FSharpChoice<T1, T2, T3, T4> New2<T1, T2, T3, T4>(
            this Hint<FSharpChoice<T1, T2, T3, T4>> hint,
            T2 item
        )
        {
            return FSharpChoice<T1, T2, T3, T4>.NewChoice2Of4(item);
        }

        public static FSharpChoice<T1, T2, T3, T4> New3<T1, T2, T3, T4>(
            this Hint<FSharpChoice<T1, T2, T3, T4>> hint,
            T3 item
        )
        {
            return FSharpChoice<T1, T2, T3, T4>.NewChoice3Of4(item);
        }

        public static FSharpChoice<T1, T2, T3, T4> New4<T1, T2, T3, T4>(
            this Hint<FSharpChoice<T1, T2, T3, T4>> hint,
            T4 item
        )
        {
            return FSharpChoice<T1, T2, T3, T4>.NewChoice4Of4(item);
        }








        public static FSharpChoice<T1, T2, T3, T4, T5> New1<T1, T2, T3, T4, T5>(
            this Hint<FSharpChoice<T1, T2, T3, T4, T5>> hint,
            T1 item
        )
        {
            return FSharpChoice<T1, T2, T3, T4, T5>.NewChoice1Of5(item);
        }

        public static FSharpChoice<T1, T2, T3, T4, T5> New2<T1, T2, T3, T4, T5>(
            this Hint<FSharpChoice<T1, T2, T3, T4, T5>> hint,
            T2 item
        )
        {
            return FSharpChoice<T1, T2, T3, T4, T5>.NewChoice2Of5(item);
        }

        public static FSharpChoice<T1, T2, T3, T4, T5> New3<T1, T2, T3, T4, T5>(
            this Hint<FSharpChoice<T1, T2, T3, T4, T5>> hint,
            T3 item
        )
        {
            return FSharpChoice<T1, T2, T3, T4, T5>.NewChoice3Of5(item);
        }

        public static FSharpChoice<T1, T2, T3, T4, T5> New4<T1, T2, T3, T4, T5>(
            this Hint<FSharpChoice<T1, T2, T3, T4, T5>> hint,
            T4 item
        )
        {
            return FSharpChoice<T1, T2, T3, T4, T5>.NewChoice4Of5(item);
        }

        public static FSharpChoice<T1, T2, T3, T4, T5> New5<T1, T2, T3, T4, T5>(
            this Hint<FSharpChoice<T1, T2, T3, T4, T5>> hint,
            T5 item
        )
        {
            return FSharpChoice<T1, T2, T3, T4, T5>.NewChoice5Of5(item);
        }








        public static FSharpChoice<T1, T2, T3, T4, T5, T6> New1<T1, T2, T3, T4, T5, T6>(
            this Hint<FSharpChoice<T1, T2, T3, T4, T5, T6>> hint,
            T1 item
        )
        {
            return FSharpChoice<T1, T2, T3, T4, T5, T6>.NewChoice1Of6(item);
        }

        public static FSharpChoice<T1, T2, T3, T4, T5, T6> New2<T1, T2, T3, T4, T5, T6>(
            this Hint<FSharpChoice<T1, T2, T3, T4, T5, T6>> hint,
            T2 item
        )
        {
            return FSharpChoice<T1, T2, T3, T4, T5, T6>.NewChoice2Of6(item);
        }

        public static FSharpChoice<T1, T2, T3, T4, T5, T6> New3<T1, T2, T3, T4, T5, T6>(
            this Hint<FSharpChoice<T1, T2, T3, T4, T5, T6>> hint,
            T3 item
        )
        {
            return FSharpChoice<T1, T2, T3, T4, T5, T6>.NewChoice3Of6(item);
        }

        public static FSharpChoice<T1, T2, T3, T4, T5, T6> New4<T1, T2, T3, T4, T5, T6>(
            this Hint<FSharpChoice<T1, T2, T3, T4, T5, T6>> hint,
            T4 item
        )
        {
            return FSharpChoice<T1, T2, T3, T4, T5, T6>.NewChoice4Of6(item);
        }

        public static FSharpChoice<T1, T2, T3, T4, T5, T6> New5<T1, T2, T3, T4, T5, T6>(
            this Hint<FSharpChoice<T1, T2, T3, T4, T5, T6>> hint,
            T5 item
        )
        {
            return FSharpChoice<T1, T2, T3, T4, T5, T6>.NewChoice5Of6(item);
        }

        public static FSharpChoice<T1, T2, T3, T4, T5, T6> New6<T1, T2, T3, T4, T5, T6>(
            this Hint<FSharpChoice<T1, T2, T3, T4, T5, T6>> hint,
            T6 item
        )
        {
            return FSharpChoice<T1, T2, T3, T4, T5, T6>.NewChoice6Of6(item);
        }








        public static FSharpChoice<T1, T2, T3, T4, T5, T6, T7> New1<T1, T2, T3, T4, T5, T6, T7>(
            this Hint<FSharpChoice<T1, T2, T3, T4, T5, T6, T7>> hint,
            T1 item
        )
        {
            return FSharpChoice<T1, T2, T3, T4, T5, T6, T7>.NewChoice1Of7(item);
        }

        public static FSharpChoice<T1, T2, T3, T4, T5, T6, T7> New2<T1, T2, T3, T4, T5, T6, T7>(
            this Hint<FSharpChoice<T1, T2, T3, T4, T5, T6, T7>> hint,
            T2 item
        )
        {
            return FSharpChoice<T1, T2, T3, T4, T5, T6, T7>.NewChoice2Of7(item);
        }

        public static FSharpChoice<T1, T2, T3, T4, T5, T6, T7> New3<T1, T2, T3, T4, T5, T6, T7>(
            this Hint<FSharpChoice<T1, T2, T3, T4, T5, T6, T7>> hint,
            T3 item
        )
        {
            return FSharpChoice<T1, T2, T3, T4, T5, T6, T7>.NewChoice3Of7(item);
        }

        public static FSharpChoice<T1, T2, T3, T4, T5, T6, T7> New4<T1, T2, T3, T4, T5, T6, T7>(
            this Hint<FSharpChoice<T1, T2, T3, T4, T5, T6, T7>> hint,
            T4 item
        )
        {
            return FSharpChoice<T1, T2, T3, T4, T5, T6, T7>.NewChoice4Of7(item);
        }

        public static FSharpChoice<T1, T2, T3, T4, T5, T6, T7> New5<T1, T2, T3, T4, T5, T6, T7>(
            this Hint<FSharpChoice<T1, T2, T3, T4, T5, T6, T7>> hint,
            T5 item
        )
        {
            return FSharpChoice<T1, T2, T3, T4, T5, T6, T7>.NewChoice5Of7(item);
        }

        public static FSharpChoice<T1, T2, T3, T4, T5, T6, T7> New6<T1, T2, T3, T4, T5, T6, T7>(
            this Hint<FSharpChoice<T1, T2, T3, T4, T5, T6, T7>> hint,
            T6 item
        )
        {
            return FSharpChoice<T1, T2, T3, T4, T5, T6, T7>.NewChoice6Of7(item);
        }

        public static FSharpChoice<T1, T2, T3, T4, T5, T6, T7> New7<T1, T2, T3, T4, T5, T6, T7>(
            this Hint<FSharpChoice<T1, T2, T3, T4, T5, T6, T7>> hint,
            T7 item
        )
        {
            return FSharpChoice<T1, T2, T3, T4, T5, T6, T7>.NewChoice7Of7(item);
        }
    }
}
