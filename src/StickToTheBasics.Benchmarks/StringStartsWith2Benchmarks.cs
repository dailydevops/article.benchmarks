namespace StickToTheBasics.Benchmarks;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using NetEvolve.Benchmarks;
using System;

[Config(typeof(NetEvolveConfig))]
[SimpleJob(runtimeMoniker: RuntimeMoniker.Net50)]
[SimpleJob(runtimeMoniker: RuntimeMoniker.Net60, baseline: true)]
[SimpleJob(runtimeMoniker: RuntimeMoniker.Net70)]
[SimpleJob(runtimeMoniker: RuntimeMoniker.Net80)]
public class StringStartsWith2Benchmarks
{
    public string Value { get; set; } = "https://hello.world";

    [Benchmark]
    public bool StartsWithTest() =>
        Value.StartsWith("https://", StringComparison.OrdinalIgnoreCase);
}
