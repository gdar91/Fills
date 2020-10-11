using System;
using System.Collections.Generic;
using System.Linq;

namespace Fills
{
    public static partial class ExtensionsForEnumerable
    {
        public static IEnumerable<TElement[]> SequentialGroupBy<TElement>(
            this IEnumerable<TElement> source,
            Func<TElement, TElement, bool> predicate
        )
        {
            var enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                yield break;
            }

            var previous = enumerator.Current;

            var moreElementsRemaining = true;

            var chunk = NextChunk();

            yield return chunk;

            while (moreElementsRemaining)
            {
                chunk = NextChunk();

                yield return chunk;
            }


            TElement[] NextChunk() => NextChunkEnumerable().ToArray();

            IEnumerable<TElement> NextChunkEnumerable()
            {
                yield return previous;

                while (enumerator.MoveNext())
                {
                    var current = enumerator.Current;

                    var take = predicate(previous, current);

                    previous = current;

                    if (!take)
                    {
                        yield break;
                    }

                    yield return current;
                }

                moreElementsRemaining = false;
            }
        }
    }
}
