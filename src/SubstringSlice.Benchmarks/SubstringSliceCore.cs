namespace SwitchSyntacticSugar.Benchmarks;

using System;

internal static class SubstringSliceCore
{
    public static string M1(string longValue) => longValue.Substring(0, 10);

    public static string M2(string longValue) => longValue[0..10];

    public static string M3(string longValue) => longValue.AsSpan().Slice(0, 10).ToString();

    public static string M4(string longValue) => new string(longValue.AsSpan().Slice(0, 10));
}
