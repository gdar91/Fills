using Microsoft.FSharp.Core;

namespace Fills
{
    public static class ChoiceFactory
    {
        public static ChoiceFactory<T1, T2> Like<T1, T2>(
            FSharpChoice<T1, T2> witness
        ) =>
            default;

        public static ChoiceFactory<T1, T2, T3> Like<T1, T2, T3>(
            FSharpChoice<T1, T2, T3> witness
        ) =>
            default;

        public static ChoiceFactory<T1, T2, T3, T4> Like<T1, T2, T3, T4>(
            FSharpChoice<T1, T2, T3, T4> witness
        ) =>
            default;

        public static ChoiceFactory<T1, T2, T3, T4, T5> Like<T1, T2, T3, T4, T5>(
            FSharpChoice<T1, T2, T3, T4, T5> witness
        ) =>
            default;

        public static ChoiceFactory<T1, T2, T3, T4, T5, T6> Like<T1, T2, T3, T4, T5, T6>(
            FSharpChoice<T1, T2, T3, T4, T5, T6> witness
        ) =>
            default;

        public static ChoiceFactory<T1, T2, T3, T4, T5, T6, T7> Like<T1, T2, T3, T4, T5, T6, T7>(
            FSharpChoice<T1, T2, T3, T4, T5, T6, T7> witness
        ) =>
            default;


        public static ChoiceFactory<T1> AddCase<T1>() => default;
    }


    public struct ChoiceFactory<T1>
    {
        public T1 New1(T1 value) => value;

        public ChoiceFactory<T1, T2> AddCase<T2>() => default;
    }


    public struct ChoiceFactory<T1, T2>
    {
        public FSharpChoice<T1, T2> New1(T1 value) =>
            FSharpChoice<T1, T2>.NewChoice1Of2(value);

        public FSharpChoice<T1, T2> New2(T2 value) =>
            FSharpChoice<T1, T2>.NewChoice2Of2(value);

        public ChoiceFactory<T1, T2, T3> AddCase<T3>() => default;

        public ChoiceFactory<T1> RemoveCase() => default;
    }


    public struct ChoiceFactory<T1, T2, T3>
    {
        public FSharpChoice<T1, T2, T3> New1(T1 value) =>
            FSharpChoice<T1, T2, T3>.NewChoice1Of3(value);

        public FSharpChoice<T1, T2, T3> New2(T2 value) =>
            FSharpChoice<T1, T2, T3>.NewChoice2Of3(value);

        public FSharpChoice<T1, T2, T3> New3(T3 value) =>
            FSharpChoice<T1, T2, T3>.NewChoice3Of3(value);

        public ChoiceFactory<T1, T2, T3, T4> AddCase<T4>() => default;

        public ChoiceFactory<T1, T2> RemoveCase() => default;
    }


    public struct ChoiceFactory<T1, T2, T3, T4>
    {
        public FSharpChoice<T1, T2, T3, T4> New1(T1 value) =>
            FSharpChoice<T1, T2, T3, T4>.NewChoice1Of4(value);

        public FSharpChoice<T1, T2, T3, T4> New2(T2 value) =>
            FSharpChoice<T1, T2, T3, T4>.NewChoice2Of4(value);

        public FSharpChoice<T1, T2, T3, T4> New3(T3 value) =>
            FSharpChoice<T1, T2, T3, T4>.NewChoice3Of4(value);

        public FSharpChoice<T1, T2, T3, T4> New4(T4 value) =>
            FSharpChoice<T1, T2, T3, T4>.NewChoice4Of4(value);

        public ChoiceFactory<T1, T2, T3, T4, T5> AddCase<T5>() => default;

        public ChoiceFactory<T1, T2, T3> RemoveCase() => default;
    }


    public struct ChoiceFactory<T1, T2, T3, T4, T5>
    {
        public FSharpChoice<T1, T2, T3, T4, T5> New1(T1 value) =>
            FSharpChoice<T1, T2, T3, T4, T5>.NewChoice1Of5(value);

        public FSharpChoice<T1, T2, T3, T4, T5> New2(T2 value) =>
            FSharpChoice<T1, T2, T3, T4, T5>.NewChoice2Of5(value);

        public FSharpChoice<T1, T2, T3, T4, T5> New3(T3 value) =>
            FSharpChoice<T1, T2, T3, T4, T5>.NewChoice3Of5(value);

        public FSharpChoice<T1, T2, T3, T4, T5> New4(T4 value) =>
            FSharpChoice<T1, T2, T3, T4, T5>.NewChoice4Of5(value);

        public FSharpChoice<T1, T2, T3, T4, T5> New5(T5 value) =>
            FSharpChoice<T1, T2, T3, T4, T5>.NewChoice5Of5(value);

        public ChoiceFactory<T1, T2, T3, T4, T5, T6> AddCase<T6>() => default;

        public ChoiceFactory<T1, T2, T3, T4> RemoveCase() => default;
    }


    public struct ChoiceFactory<T1, T2, T3, T4, T5, T6>
    {
        public FSharpChoice<T1, T2, T3, T4, T5, T6> New1(T1 value) =>
            FSharpChoice<T1, T2, T3, T4, T5, T6>.NewChoice1Of6(value);

        public FSharpChoice<T1, T2, T3, T4, T5, T6> New2(T2 value) =>
            FSharpChoice<T1, T2, T3, T4, T5, T6>.NewChoice2Of6(value);

        public FSharpChoice<T1, T2, T3, T4, T5, T6> New3(T3 value) =>
            FSharpChoice<T1, T2, T3, T4, T5, T6>.NewChoice3Of6(value);

        public FSharpChoice<T1, T2, T3, T4, T5, T6> New4(T4 value) =>
            FSharpChoice<T1, T2, T3, T4, T5, T6>.NewChoice4Of6(value);

        public FSharpChoice<T1, T2, T3, T4, T5, T6> New5(T5 value) =>
            FSharpChoice<T1, T2, T3, T4, T5, T6>.NewChoice5Of6(value);

        public FSharpChoice<T1, T2, T3, T4, T5, T6> New6(T6 value) =>
            FSharpChoice<T1, T2, T3, T4, T5, T6>.NewChoice6Of6(value);

        public ChoiceFactory<T1, T2, T3, T4, T5, T6, T7> AddCase<T7>() => default;

        public ChoiceFactory<T1, T2, T3, T4, T5> RemoveCase() => default;
    }


    public struct ChoiceFactory<T1, T2, T3, T4, T5, T6, T7>
    {
        public FSharpChoice<T1, T2, T3, T4, T5, T6, T7> New1(T1 value) =>
            FSharpChoice<T1, T2, T3, T4, T5, T6, T7>.NewChoice1Of7(value);

        public FSharpChoice<T1, T2, T3, T4, T5, T6, T7> New2(T2 value) =>
            FSharpChoice<T1, T2, T3, T4, T5, T6, T7>.NewChoice2Of7(value);

        public FSharpChoice<T1, T2, T3, T4, T5, T6, T7> New3(T3 value) =>
            FSharpChoice<T1, T2, T3, T4, T5, T6, T7>.NewChoice3Of7(value);

        public FSharpChoice<T1, T2, T3, T4, T5, T6, T7> New4(T4 value) =>
            FSharpChoice<T1, T2, T3, T4, T5, T6, T7>.NewChoice4Of7(value);

        public FSharpChoice<T1, T2, T3, T4, T5, T6, T7> New5(T5 value) =>
            FSharpChoice<T1, T2, T3, T4, T5, T6, T7>.NewChoice5Of7(value);

        public FSharpChoice<T1, T2, T3, T4, T5, T6, T7> New6(T6 value) =>
            FSharpChoice<T1, T2, T3, T4, T5, T6, T7>.NewChoice6Of7(value);

        public FSharpChoice<T1, T2, T3, T4, T5, T6, T7> New7(T7 value) =>
            FSharpChoice<T1, T2, T3, T4, T5, T6, T7>.NewChoice7Of7(value);

        public ChoiceFactory<T1, T2, T3, T4, T5, T6> RemoveCase() => default;
    }
}
