using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fills;

namespace Tst
{
    class Program
    {
        static async Task Main(string[] args)
        {
            FillsEnumerable
                .Return(1)
                .Expand(e => FillsEnumerable.Return(e * 2))
                .Take(10)
                .Pipe(e => Console.WriteLine(string.Join(", ", e)));

            FillsEnumerable
                .From(10, 10)
                .Expand(item => item > 0 ? FillsEnumerable.Return(item / 2) : Enumerable.Empty<int>())
                .Pipe(e => Console.WriteLine(string.Join(", ", e)));

            var e1 = new[] { "1", "joni", "2", "jimi", "magari", "10" };
            var r1 = e1.TrySelect<string, int>(int.TryParse);

            var fibonacci = FillsEnumerable.Unfold(
                (0, 1),
                ((int, int) state, out int nextElement, out (int, int) nextState) =>
                {
                    nextElement = state.Item1 + state.Item2;
                    nextState = (nextElement, state.Item1);
                    return true;
                }
            );


            static string BinaryRepresentation(int number) =>
                number < 0
                    ? $"-{BinaryRepresentation(-number)}"
                    : FillsEnumerable
                        .Unfold<int, int>(number, NextBit)
                        .EmptyCoalesce(FillsEnumerable.Return(0))
                        .Aggregate(string.Empty, (state, element) => $"{element}{state}");

            static bool NextBit(int number, out int nextBit, out int nextNumber)
            {
                nextBit = number % 2;
                nextNumber = number / 2;

                return number > 0;
            }

            System.Console.WriteLine($"14 in binary: {BinaryRepresentation(14)}");
            System.Console.WriteLine($"-14 in binary: {BinaryRepresentation(-14)}");


            fibonacci.Take(10).Pipe(e => string.Join(", ", e));



            System.Console.WriteLine(string.Join(", ", r1));



            var r2 = new[] { 1, 2, 3 }.Scan(0, (state, element) => state + element);

            System.Console.WriteLine(string.Join(", ", r2));



            var r3 = new[] { 0, 1, 2, 1, 0, -1, 0, 0, 0 }.SequentialGroupBy((a, b) => Math.Sign(a) == Math.Sign(b));

            System.Console.WriteLine(string.Join(";    ", r3.Select(e => string.Join(", ", e))));



            Enumerable.Range(8, 3).ForEach(next => Console.WriteLine($"Joni: {next}"));


            var r4 = new int[0].EmptyCoalesce(new [] { 103, 104 });
            var r5 = new [] { 1000 }.EmptyCoalesce(new [] { 103, 104 });

            System.Console.WriteLine(string.Join(", ", r4));
            System.Console.WriteLine(string.Join(", ", r5));



            Func<IEnumerable<string>> joni = () =>
            {
                System.Console.WriteLine("Jondi!");

                return new[] { "joni", "jimi", "jini" };
            };

            var j = FillsEnumerable.Defer(joni);

            System.Console.WriteLine(string.Join(", ", j));





            var fib = FillsEnumerable.Unfold(
                ((int p1, int p2)) (0, 1),
                state => Tuple.Create(state.p1 + state.p2, (state.p1 + state.p2, state.p1))
            );

            System.Console.WriteLine(string.Join(", ", fib.Take(5)));


            string Bin(int number) => FillsEnumerable
                .Unfold(number, (int state, out int nextElement, out int nextState) =>
                {
                    if (state == 0)
                    {
                        nextElement = default;
                        nextState = default;
                        return false;
                    }

                    nextElement = state % 2;
                    nextState = state / 2;

                    return true;
                })
                // .Unfold(number, state => state == 0 ? default : Tuple.Create(state % 2, state / 2))
                .EmptyCoalesce(FillsEnumerable.Return(0))
                .Aggregate(string.Empty, (state, element) => $"{element}{state}");


            System.Console.WriteLine(Bin(103));



            new[] { 1, 2, 10 }
                .Pipe(x => string.Join(", ", x)
                .Pipe(Console.WriteLine));
        }
    }
}
