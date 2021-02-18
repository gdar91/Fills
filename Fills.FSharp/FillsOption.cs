using Microsoft.FSharp.Core;
using System;

namespace Fills
{
    public static class FillsOption
    {
        public static FSharpOption<T> Some<T>(T value) => FSharpOption<T>.Some(value);


        public static FSharpOption<T> None<T>() => FSharpOption<T>.None;

        public static FSharpOption<T> None<T>(T witness) => FSharpOption<T>.None;


        public static FSharpOption<TResult> Zip<T1, T2, TResult>(
            FSharpOption<T1> option1,
            FSharpOption<T2> option2,
            Func<T1, T2, TResult> selector
        )
        {
            return
                option1.TryGetValue(out var value1) && option2.TryGetValue(out var value2)
                    ? selector(value1, value2)
                    : FillsOption.None<TResult>();
        }
    }
}
