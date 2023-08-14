namespace Shuffle.Benchmarks;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

[SuppressMessage("Security", "CA5394:Do not use insecure randomness", Justification = "Intended")]
public static class ShuffleExtensions
{
    public static IEnumerable<T> ShuffleLinqOrderBy<T>(
        this IEnumerable<T> source,
        [NotNull] Random rng
    ) => source.OrderBy(_ => rng.Next());

    public static IEnumerable<T> ShuffleLinqOrderByGuid<T>(this IEnumerable<T> source) =>
        source.OrderBy(_ => Guid.NewGuid());

    public static IEnumerable<T> ShuffleFYDToList<T>(
        this IEnumerable<T> source,
        [NotNull] Random rng
    )
    {
        var buffer = source.ToList();
        for (var i = 0; i < buffer.Count; i++)
        {
            var j = rng.Next(i, buffer.Count);
            yield return buffer[j];

            buffer[j] = buffer[i];
        }
    }

    public static IEnumerable<T> ShuffleFYDToArray<T>(
        this IEnumerable<T> source,
        [NotNull] Random rng
    )
    {
        var buffer = source.ToArray();
        for (var i = 0; i < buffer.Length; i++)
        {
            var j = rng.Next(i, buffer.Length);
            yield return buffer[j];

            buffer[j] = buffer[i];
        }
    }

    public static IEnumerable<T> ShuffleGiesel<T>(this IEnumerable<T> source, [NotNull] Random rng)
    {
        var elements = source.ToArray();
        for (var i = elements.Length - 1; i > 0; i--)
        {
            var swapIndex = rng.Next(i + 1);
            (elements[i], elements[swapIndex]) = (elements[swapIndex], elements[i]);
        }

        return elements;
    }

    public static IEnumerable<T> ShuffleGieselOptimized<T>(
        this IEnumerable<T> source,
        [NotNull] Random rng
    )
    {
        var elements = source.ToArray().AsSpan();

        T temp;
        for (var i = elements.Length - 1; i > 0; i--)
        {
            var swapIndex = rng.Next(i + 1);

            temp = elements[swapIndex];
            elements[swapIndex] = elements[i];
            elements[i] = temp;
        }

        return elements.ToArray();
    }
}
