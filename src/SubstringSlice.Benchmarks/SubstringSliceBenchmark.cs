namespace SwitchSyntacticSugar.Benchmarks;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using NetEvolve.Benchmarks;

[Config(typeof(NetEvolveConfig))]
[SimpleJob(runtimeMoniker: RuntimeMoniker.Net70)]
public class SubstringSliceBenchmark
{
    public string Value { get; set; } = "https://www.google.com/";

    [Benchmark(Baseline = true)]
    public string Substring() => SubstringSliceCore.M1(Value);

    [Benchmark]
    public string RangeExpression() => SubstringSliceCore.M2(Value);

    [Benchmark]
    public string SpanToString() => SubstringSliceCore.M3(Value);

    [Benchmark]
    public string SpanNewString() => SubstringSliceCore.M4(Value);
}
