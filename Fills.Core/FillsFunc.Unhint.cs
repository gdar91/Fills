namespace Fills;

public static partial class FillsFunc
{
    public static Hint<TResult> Unhint<TResult>(Hint<Func<TResult>> hint) => default;

    public static Hint<TValue>
        Unhint<T, TResult, TValue>(
            Hint<Func<T, TResult>> hint,
            Func<
                Hint<T>,
                Hint<TResult>,
                Hint<TValue>
            > func
        ) => default;

    public static Hint<TValue>
        Unhint<T1, T2, TResult, TValue>(
            Hint<Func<T1, T2, TResult>> hint,
            Func<
                Hint<T1>, Hint<T2>,
                Hint<TResult>,
                Hint<TValue>
            > func
        ) => default;

    public static Hint<TValue>
        Unhint<T1, T2, T3, TResult, TValue>(
            Hint<Func<T1, T2, T3, TResult>> hint,
            Func<
                Hint<T1>, Hint<T2>, Hint<T3>,
                Hint<TResult>,
                Hint<TValue>
            > func
        ) => default;

    public static Hint<TValue>
        Unhint<T1, T2, T3, T4, TResult, TValue>(
            Hint<Func<T1, T2, T3, T4, TResult>> hint,
            Func<
                Hint<T1>, Hint<T2>, Hint<T3>, Hint<T4>,
                Hint<TResult>,
                Hint<TValue>
            > func
        ) => default;

    public static Hint<TValue>
        Unhint<T1, T2, T3, T4, T5, TResult, TValue>(
            Hint<Func<T1, T2, T3, T4, T5, TResult>> hint,
            Func<
                Hint<T1>, Hint<T2>, Hint<T3>, Hint<T4>,
                Hint<T5>,
                Hint<TResult>,
                Hint<TValue>
            > func
        ) => default;

    public static Hint<TValue>
        Unhint<T1, T2, T3, T4, T5, T6, TResult, TValue>(
            Hint<Func<T1, T2, T3, T4, T5, T6, TResult>> hint,
            Func<
                Hint<T1>, Hint<T2>, Hint<T3>, Hint<T4>,
                Hint<T5>, Hint<T6>,
                Hint<TResult>,
                Hint<TValue>
            > func
        ) => default;

    public static Hint<TValue>
        Unhint<T1, T2, T3, T4, T5, T6, T7, TResult, TValue>(
            Hint<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> hint,
            Func<
                Hint<T1>, Hint<T2>, Hint<T3>, Hint<T4>,
                Hint<T5>, Hint<T6>, Hint<T7>,
                Hint<TResult>,
                Hint<TValue>
            > func
        ) => default;

    public static Hint<TValue>
        Unhint<T1, T2, T3, T4, T5, T6, T7, T8, TResult, TValue>(
            Hint<Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>> hint,
            Func<
                Hint<T1>, Hint<T2>, Hint<T3>, Hint<T4>,
                Hint<T5>, Hint<T6>, Hint<T7>, Hint<T8>,
                Hint<TResult>,
                Hint<TValue>
            > func
        ) => default;

    public static Hint<TValue>
        Unhint<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult, TValue>(
            Hint<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>> hint,
            Func<
                Hint<T1>, Hint<T2>, Hint<T3>, Hint<T4>,
                Hint<T5>, Hint<T6>, Hint<T7>, Hint<T8>,
                Hint<T9>,
                Hint<TResult>,
                Hint<TValue>
            > func
        ) => default;

    public static Hint<TValue>
        Unhint<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult, TValue>(
            Hint<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>> hint,
            Func<
                Hint<T1>, Hint<T2>, Hint<T3>, Hint<T4>,
                Hint<T5>, Hint<T6>, Hint<T7>, Hint<T8>,
                Hint<T9>, Hint<T10>,
                Hint<TResult>,
                Hint<TValue>
            > func
        ) => default;

    public static Hint<TValue>
        Unhint<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult, TValue>(
            Hint<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>> hint,
            Func<
                Hint<T1>, Hint<T2>, Hint<T3>, Hint<T4>,
                Hint<T5>, Hint<T6>, Hint<T7>, Hint<T8>,
                Hint<T9>, Hint<T10>, Hint<T11>,
                Hint<TResult>,
                Hint<TValue>
            > func
        ) => default;

    public static Hint<TValue>
        Unhint<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult, TValue>(
            Hint<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>> hint,
            Func<
                Hint<T1>, Hint<T2>, Hint<T3>, Hint<T4>,
                Hint<T5>, Hint<T6>, Hint<T7>, Hint<T8>,
                Hint<T9>, Hint<T10>, Hint<T11>, Hint<T12>,
                Hint<TResult>,
                Hint<TValue>
            > func
        ) => default;

    public static Hint<TValue>
        Unhint<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult, TValue>(
            Hint<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>> hint,
            Func<
                Hint<T1>, Hint<T2>, Hint<T3>, Hint<T4>,
                Hint<T5>, Hint<T6>, Hint<T7>, Hint<T8>,
                Hint<T9>, Hint<T10>, Hint<T11>, Hint<T12>,
                Hint<T13>,
                Hint<TResult>,
                Hint<TValue>
            > func
        ) => default;

    public static Hint<TValue>
        Unhint<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult, TValue>(
            Hint<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>> hint,
            Func<
                Hint<T1>, Hint<T2>, Hint<T3>, Hint<T4>,
                Hint<T5>, Hint<T6>, Hint<T7>, Hint<T8>,
                Hint<T9>, Hint<T10>, Hint<T11>, Hint<T12>,
                Hint<T13>, Hint<T14>,
                Hint<TResult>,
                Hint<TValue>
            > func
        ) => default;

    public static Hint<TValue>
        Unhint<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult, TValue>(
            Hint<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>> hint,
            Func<
                Hint<T1>, Hint<T2>, Hint<T3>, Hint<T4>,
                Hint<T5>, Hint<T6>, Hint<T7>, Hint<T8>,
                Hint<T9>, Hint<T10>, Hint<T11>, Hint<T12>,
                Hint<T13>, Hint<T14>, Hint<T15>,
                Hint<TResult>,
                Hint<TValue>
            > func
        ) => default;

    public static Hint<TValue>
        Unhint<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult, TValue>(
            Hint<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>> hint,
            Func<
                Hint<T1>, Hint<T2>, Hint<T3>, Hint<T4>,
                Hint<T5>, Hint<T6>, Hint<T7>, Hint<T8>,
                Hint<T9>, Hint<T10>, Hint<T11>, Hint<T12>,
                Hint<T13>, Hint<T14>, Hint<T15>,
                Hint<Func<T16, TResult>>,
                Hint<TValue>
            > func
        ) => default;
}
