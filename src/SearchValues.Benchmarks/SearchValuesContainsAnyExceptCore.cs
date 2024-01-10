namespace SearchValues.Benchmarks;

using System;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;

[SuppressMessage(
    "Performance",
    "CA1870:Use a cached 'SearchValues' instance",
    Justification = "Benchmark"
)]
public static class SearchValuesContainsAnyExceptCore
{
    private static readonly char[] _values = new[] { 'a', 'b', 'c', 'x', 'y', 'z' };
    private static readonly SearchValues<char> _searchValues = SearchValues.Create(_values);

    public static bool ReadOnlySpanContainsAnyExcept(ReadOnlySpan<char> text) =>
        text.ContainsAnyExcept(_values);

    public static bool SearchValuesContainsAnyExcept(ReadOnlySpan<char> text) =>
        text.ContainsAnyExcept(_searchValues);
}
