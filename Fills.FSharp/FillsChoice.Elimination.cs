using Microsoft.FSharp.Core;

namespace Fills;

public static partial class FillsChoiceExtensions
{
    public static TResult Match<T1, T2, TResult>(
        this FSharpChoice<T1, T2> choice,
        Func<T1, TResult> when1,
        Func<T2, TResult> when2
    )
    {
        return choice switch
        {
            FSharpChoice<T1, T2>.Choice1Of2 { Item: var value } => when1(value),
            FSharpChoice<T1, T2>.Choice2Of2 { Item: var value } => when2(value),
            var other => throw new NotImplementedException($"Unknown case {other}.")
        };
    }


    public static TResult Match<T1, T2, T3, TResult>(
        this FSharpChoice<T1, T2, T3> choice,
        Func<T1, TResult> when1,
        Func<T2, TResult> when2,
        Func<T3, TResult> when3
    )
    {
        return choice switch
        {
            FSharpChoice<T1, T2, T3>.Choice1Of3 { Item: var value } => when1(value),
            FSharpChoice<T1, T2, T3>.Choice2Of3 { Item: var value } => when2(value),
            FSharpChoice<T1, T2, T3>.Choice3Of3 { Item: var value } => when3(value),
            var other => throw new NotImplementedException($"Unknown case {other}.")
        };
    }


    public static TResult Match<T1, T2, T3, T4, TResult>(
        this FSharpChoice<T1, T2, T3, T4> choice,
        Func<T1, TResult> when1,
        Func<T2, TResult> when2,
        Func<T3, TResult> when3,
        Func<T4, TResult> when4
    )
    {
        return choice switch
        {
            FSharpChoice<T1, T2, T3, T4>.Choice1Of4 { Item: var value } => when1(value),
            FSharpChoice<T1, T2, T3, T4>.Choice2Of4 { Item: var value } => when2(value),
            FSharpChoice<T1, T2, T3, T4>.Choice3Of4 { Item: var value } => when3(value),
            FSharpChoice<T1, T2, T3, T4>.Choice4Of4 { Item: var value } => when4(value),
            var other => throw new NotImplementedException($"Unknown case {other}.")
        };
    }


    public static TResult Match<T1, T2, T3, T4, T5, TResult>(
        this FSharpChoice<T1, T2, T3, T4, T5> choice,
        Func<T1, TResult> when1,
        Func<T2, TResult> when2,
        Func<T3, TResult> when3,
        Func<T4, TResult> when4,
        Func<T5, TResult> when5
    )
    {
        return choice switch
        {
            FSharpChoice<T1, T2, T3, T4, T5>.Choice1Of5 { Item: var value } => when1(value),
            FSharpChoice<T1, T2, T3, T4, T5>.Choice2Of5 { Item: var value } => when2(value),
            FSharpChoice<T1, T2, T3, T4, T5>.Choice3Of5 { Item: var value } => when3(value),
            FSharpChoice<T1, T2, T3, T4, T5>.Choice4Of5 { Item: var value } => when4(value),
            FSharpChoice<T1, T2, T3, T4, T5>.Choice5Of5 { Item: var value } => when5(value),
            var other => throw new NotImplementedException($"Unknown case {other}.")
        };
    }


    public static TResult Match<T1, T2, T3, T4, T5, T6, TResult>(
        this FSharpChoice<T1, T2, T3, T4, T5, T6> choice,
        Func<T1, TResult> when1,
        Func<T2, TResult> when2,
        Func<T3, TResult> when3,
        Func<T4, TResult> when4,
        Func<T5, TResult> when5,
        Func<T6, TResult> when6
    )
    {
        return choice switch
        {
            FSharpChoice<T1, T2, T3, T4, T5, T6>.Choice1Of6 { Item: var value } => when1(value),
            FSharpChoice<T1, T2, T3, T4, T5, T6>.Choice2Of6 { Item: var value } => when2(value),
            FSharpChoice<T1, T2, T3, T4, T5, T6>.Choice3Of6 { Item: var value } => when3(value),
            FSharpChoice<T1, T2, T3, T4, T5, T6>.Choice4Of6 { Item: var value } => when4(value),
            FSharpChoice<T1, T2, T3, T4, T5, T6>.Choice5Of6 { Item: var value } => when5(value),
            FSharpChoice<T1, T2, T3, T4, T5, T6>.Choice6Of6 { Item: var value } => when6(value),
            var other => throw new NotImplementedException($"Unknown case {other}.")
        };
    }


    public static TResult Match<T1, T2, T3, T4, T5, T6, T7, TResult>(
        this FSharpChoice<T1, T2, T3, T4, T5, T6, T7> choice,
        Func<T1, TResult> when1,
        Func<T2, TResult> when2,
        Func<T3, TResult> when3,
        Func<T4, TResult> when4,
        Func<T5, TResult> when5,
        Func<T6, TResult> when6,
        Func<T7, TResult> when7
    )
    {
        return choice switch
        {
            FSharpChoice<T1, T2, T3, T4, T5, T6, T7>.Choice1Of7 { Item: var value } => when1(value),
            FSharpChoice<T1, T2, T3, T4, T5, T6, T7>.Choice2Of7 { Item: var value } => when2(value),
            FSharpChoice<T1, T2, T3, T4, T5, T6, T7>.Choice3Of7 { Item: var value } => when3(value),
            FSharpChoice<T1, T2, T3, T4, T5, T6, T7>.Choice4Of7 { Item: var value } => when4(value),
            FSharpChoice<T1, T2, T3, T4, T5, T6, T7>.Choice5Of7 { Item: var value } => when5(value),
            FSharpChoice<T1, T2, T3, T4, T5, T6, T7>.Choice6Of7 { Item: var value } => when6(value),
            FSharpChoice<T1, T2, T3, T4, T5, T6, T7>.Choice7Of7 { Item: var value } => when7(value),
            var other => throw new NotImplementedException($"Unknown case {other}.")
        };
    }




    public static bool Is1<T1, T2>(
        this FSharpChoice<T1, T2> choice,
        out T1 result
    )
    {
        if (choice is FSharpChoice<T1, T2>.Choice1Of2 { Item: var value })
        {
            result = value;

            return true;
        }

        result = default!;

        return false;
    }

    public static bool Is1<T1, T2, T3>(
        this FSharpChoice<T1, T2, T3> choice,
        out T1 result
    )
    {
        if (choice is FSharpChoice<T1, T2, T3>.Choice1Of3 { Item: var value })
        {
            result = value;

            return true;
        }

        result = default!;

        return false;
    }

    public static bool Is1<T1, T2, T3, T4>(
        this FSharpChoice<T1, T2, T3, T4> choice,
        out T1 result
    )
    {
        if (choice is FSharpChoice<T1, T2, T3, T4>.Choice1Of4 { Item: var value })
        {
            result = value;

            return true;
        }

        result = default!;

        return false;
    }

    public static bool Is1<T1, T2, T3, T4, T5>(
        this FSharpChoice<T1, T2, T3, T4, T5> choice,
        out T1 result
    )
    {
        if (choice is FSharpChoice<T1, T2, T3, T4, T5>.Choice1Of5 { Item: var value })
        {
            result = value;

            return true;
        }

        result = default!;

        return false;
    }

    public static bool Is1<T1, T2, T3, T4, T5, T6>(
        this FSharpChoice<T1, T2, T3, T4, T5, T6> choice,
        out T1 result
    )
    {
        if (choice is FSharpChoice<T1, T2, T3, T4, T5, T6>.Choice1Of6 { Item: var value })
        {
            result = value;

            return true;
        }

        result = default!;

        return false;
    }

    public static bool Is1<T1, T2, T3, T4, T5, T6, T7>(
        this FSharpChoice<T1, T2, T3, T4, T5, T6, T7> choice,
        out T1 result
    )
    {
        if (choice is FSharpChoice<T1, T2, T3, T4, T5, T6, T7>.Choice1Of7 { Item: var value })
        {
            result = value;

            return true;
        }

        result = default!;

        return false;
    }




    public static bool Is2<T1, T2>(
        this FSharpChoice<T1, T2> choice,
        out T2 result
    )
    {
        if (choice is FSharpChoice<T1, T2>.Choice2Of2 { Item: var value })
        {
            result = value;

            return true;
        }

        result = default!;

        return false;
    }

    public static bool Is2<T1, T2, T3>(
        this FSharpChoice<T1, T2, T3> choice,
        out T2 result
    )
    {
        if (choice is FSharpChoice<T1, T2, T3>.Choice2Of3 { Item: var value })
        {
            result = value;

            return true;
        }

        result = default!;

        return false;
    }

    public static bool Is2<T1, T2, T3, T4>(
        this FSharpChoice<T1, T2, T3, T4> choice,
        out T2 result
    )
    {
        if (choice is FSharpChoice<T1, T2, T3, T4>.Choice2Of4 { Item: var value })
        {
            result = value;

            return true;
        }

        result = default!;

        return false;
    }

    public static bool Is2<T1, T2, T3, T4, T5>(
        this FSharpChoice<T1, T2, T3, T4, T5> choice,
        out T2 result
    )
    {
        if (choice is FSharpChoice<T1, T2, T3, T4, T5>.Choice2Of5 { Item: var value })
        {
            result = value;

            return true;
        }

        result = default!;

        return false;
    }

    public static bool Is2<T1, T2, T3, T4, T5, T6>(
        this FSharpChoice<T1, T2, T3, T4, T5, T6> choice,
        out T2 result
    )
    {
        if (choice is FSharpChoice<T1, T2, T3, T4, T5, T6>.Choice2Of6 { Item: var value })
        {
            result = value;

            return true;
        }

        result = default!;

        return false;
    }

    public static bool Is2<T1, T2, T3, T4, T5, T6, T7>(
        this FSharpChoice<T1, T2, T3, T4, T5, T6, T7> choice,
        out T2 result
    )
    {
        if (choice is FSharpChoice<T1, T2, T3, T4, T5, T6, T7>.Choice2Of7 { Item: var value })
        {
            result = value;

            return true;
        }

        result = default!;

        return false;
    }




    public static bool Is3<T1, T2, T3>(
        this FSharpChoice<T1, T2, T3> choice,
        out T3 result
    )
    {
        if (choice is FSharpChoice<T1, T2, T3>.Choice3Of3 { Item: var value })
        {
            result = value;

            return true;
        }

        result = default!;

        return false;
    }

    public static bool Is3<T1, T2, T3, T4>(
        this FSharpChoice<T1, T2, T3, T4> choice,
        out T3 result
    )
    {
        if (choice is FSharpChoice<T1, T2, T3, T4>.Choice3Of4 { Item: var value })
        {
            result = value;

            return true;
        }

        result = default!;

        return false;
    }

    public static bool Is3<T1, T2, T3, T4, T5>(
        this FSharpChoice<T1, T2, T3, T4, T5> choice,
        out T3 result
    )
    {
        if (choice is FSharpChoice<T1, T2, T3, T4, T5>.Choice3Of5 { Item: var value })
        {
            result = value;

            return true;
        }

        result = default!;

        return false;
    }

    public static bool Is3<T1, T2, T3, T4, T5, T6>(
        this FSharpChoice<T1, T2, T3, T4, T5, T6> choice,
        out T3 result
    )
    {
        if (choice is FSharpChoice<T1, T2, T3, T4, T5, T6>.Choice3Of6 { Item: var value })
        {
            result = value;

            return true;
        }

        result = default!;

        return false;
    }

    public static bool Is3<T1, T2, T3, T4, T5, T6, T7>(
        this FSharpChoice<T1, T2, T3, T4, T5, T6, T7> choice,
        out T3 result
    )
    {
        if (choice is FSharpChoice<T1, T2, T3, T4, T5, T6, T7>.Choice3Of7 { Item: var value })
        {
            result = value;

            return true;
        }

        result = default!;

        return false;
    }




    public static bool Is4<T1, T2, T3, T4>(
        this FSharpChoice<T1, T2, T3, T4> choice,
        out T4 result
    )
    {
        if (choice is FSharpChoice<T1, T2, T3, T4>.Choice4Of4 { Item: var value })
        {
            result = value;

            return true;
        }

        result = default!;

        return false;
    }

    public static bool Is4<T1, T2, T3, T4, T5>(
        this FSharpChoice<T1, T2, T3, T4, T5> choice,
        out T4 result
    )
    {
        if (choice is FSharpChoice<T1, T2, T3, T4, T5>.Choice4Of5 { Item: var value })
        {
            result = value;

            return true;
        }

        result = default!;

        return false;
    }

    public static bool Is4<T1, T2, T3, T4, T5, T6>(
        this FSharpChoice<T1, T2, T3, T4, T5, T6> choice,
        out T4 result
    )
    {
        if (choice is FSharpChoice<T1, T2, T3, T4, T5, T6>.Choice4Of6 { Item: var value })
        {
            result = value;

            return true;
        }

        result = default!;

        return false;
    }

    public static bool Is4<T1, T2, T3, T4, T5, T6, T7>(
        this FSharpChoice<T1, T2, T3, T4, T5, T6, T7> choice,
        out T4 result
    )
    {
        if (choice is FSharpChoice<T1, T2, T3, T4, T5, T6, T7>.Choice4Of7 { Item: var value })
        {
            result = value;

            return true;
        }

        result = default!;

        return false;
    }




    public static bool Is5<T1, T2, T3, T4, T5>(
        this FSharpChoice<T1, T2, T3, T4, T5> choice,
        out T5 result
    )
    {
        if (choice is FSharpChoice<T1, T2, T3, T4, T5>.Choice5Of5 { Item: var value })
        {
            result = value;

            return true;
        }

        result = default!;

        return false;
    }

    public static bool Is5<T1, T2, T3, T4, T5, T6>(
        this FSharpChoice<T1, T2, T3, T4, T5, T6> choice,
        out T5 result
    )
    {
        if (choice is FSharpChoice<T1, T2, T3, T4, T5, T6>.Choice5Of6 { Item: var value })
        {
            result = value;

            return true;
        }

        result = default!;

        return false;
    }

    public static bool Is5<T1, T2, T3, T4, T5, T6, T7>(
        this FSharpChoice<T1, T2, T3, T4, T5, T6, T7> choice,
        out T5 result
    )
    {
        if (choice is FSharpChoice<T1, T2, T3, T4, T5, T6, T7>.Choice5Of7 { Item: var value })
        {
            result = value;

            return true;
        }

        result = default!;

        return false;
    }




    public static bool Is6<T1, T2, T3, T4, T5, T6>(
        this FSharpChoice<T1, T2, T3, T4, T5, T6> choice,
        out T6 result
    )
    {
        if (choice is FSharpChoice<T1, T2, T3, T4, T5, T6>.Choice6Of6 { Item: var value })
        {
            result = value;

            return true;
        }

        result = default!;

        return false;
    }

    public static bool Is6<T1, T2, T3, T4, T5, T6, T7>(
        this FSharpChoice<T1, T2, T3, T4, T5, T6, T7> choice,
        out T6 result
    )
    {
        if (choice is FSharpChoice<T1, T2, T3, T4, T5, T6, T7>.Choice6Of7 { Item: var value })
        {
            result = value;

            return true;
        }

        result = default!;

        return false;
    }




    public static bool Is7<T1, T2, T3, T4, T5, T6, T7>(
        this FSharpChoice<T1, T2, T3, T4, T5, T6, T7> choice,
        out T7 result
    )
    {
        if (choice is FSharpChoice<T1, T2, T3, T4, T5, T6, T7>.Choice7Of7 { Item: var value })
        {
            result = value;

            return true;
        }

        result = default!;

        return false;
    }
}
