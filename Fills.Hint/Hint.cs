using System;

namespace Fills
{
    public struct Hint<T>
    {
    }


    public static class Hint
    {
        public static Hint<T> Of<T>() => default;


        public static Hint<T> OfValue<T>(T value) => default;


        public static Hint<T> OfFunc<T>(Func<T, T> func) => default;


        public static Hint<Func<T, T>> Invariant<T>(this Hint<T> hint) => default;


        public static T Identity<T>(this Hint<T> hint, T value) => value;


        public static Hint<T> Composite<T1, T>(
            Hint<T1> hint1,
            Func<T1, T> func
        )
        {
            return default;
        }


        public static Hint<T> Composite<T1, T2, T>(
            Hint<T1> hint1,
            Hint<T2> hint2,
            Func<T1, T2, T> func
        )
        {
            return default;
        }


        public static Hint<T> Composite<T1, T2, T3, T>(
            Hint<T1> hint1,
            Hint<T2> hint2,
            Hint<T3> hint3,
            Func<T1, T2, T3, T> func
        )
        {
            return default;
        }


        public static Hint<T> Composite<T1, T2, T3, T4, T>(
            Hint<T1> hint1,
            Hint<T2> hint2,
            Hint<T3> hint3,
            Hint<T4> hint4,
            Func<T1, T2, T3, T4, T> func
        )
        {
            return default;
        }


        public static Hint<T> Composite<T1, T2, T3, T4, T5, T>(
            Hint<T1> hint1,
            Hint<T2> hint2,
            Hint<T3> hint3,
            Hint<T4> hint4,
            Hint<T5> hint5,
            Func<T1, T2, T3, T4, T5, T> func
        )
        {
            return default;
        }


        public static Hint<T> Composite<T1, T2, T3, T4, T5, T6, T>(
            Hint<T1> hint1,
            Hint<T2> hint2,
            Hint<T3> hint3,
            Hint<T4> hint4,
            Hint<T5> hint5,
            Hint<T6> hint6,
            Func<T1, T2, T3, T4, T5, T6, T> func
        )
        {
            return default;
        }


        public static Hint<T> Composite<T1, T2, T3, T4, T5, T6, T7, T>(
            Hint<T1> hint1,
            Hint<T2> hint2,
            Hint<T3> hint3,
            Hint<T4> hint4,
            Hint<T5> hint5,
            Hint<T6> hint6,
            Hint<T7> hint7,
            Func<T1, T2, T3, T4, T5, T6, T7, T> func
        )
        {
            return default;
        }


        public static Hint<T> Composite<T1, T2, T3, T4, T5, T6, T7, T8, T>(
            Hint<T1> hint1,
            Hint<T2> hint2,
            Hint<T3> hint3,
            Hint<T4> hint4,
            Hint<T5> hint5,
            Hint<T6> hint6,
            Hint<T7> hint7,
            Hint<T8> hint8,
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T> func
        )
        {
            return default;
        }


        public static Hint<T> Composite<T1, T2, T3, T4, T5, T6, T7, T8, T9, T>(
            Hint<T1> hint1,
            Hint<T2> hint2,
            Hint<T3> hint3,
            Hint<T4> hint4,
            Hint<T5> hint5,
            Hint<T6> hint6,
            Hint<T7> hint7,
            Hint<T8> hint8,
            Hint<T9> hint9,
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T> func
        )
        {
            return default;
        }


        public static Hint<T> Composite<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T>(
            Hint<T1> hint1,
            Hint<T2> hint2,
            Hint<T3> hint3,
            Hint<T4> hint4,
            Hint<T5> hint5,
            Hint<T6> hint6,
            Hint<T7> hint7,
            Hint<T8> hint8,
            Hint<T9> hint9,
            Hint<T10> hint10,
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T> func
        )
        {
            return default;
        }
    }
}
