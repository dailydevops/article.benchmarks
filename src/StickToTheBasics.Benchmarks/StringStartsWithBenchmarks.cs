namespace StickToTheBasics.Benchmarks;

using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using NetEvolve.Benchmarks;

[Config(typeof(NetEvolveConfig))]
[SimpleJob(runtimeMoniker: RuntimeMoniker.Net50)]
[SimpleJob(runtimeMoniker: RuntimeMoniker.Net60)]
[SimpleJob(runtimeMoniker: RuntimeMoniker.Net70)]
[SimpleJob(runtimeMoniker: RuntimeMoniker.Net80)]
public class StringStartsWithBenchmarks
{
    public string Value { get; set; } = "https://hello.world";

    [Benchmark]
    public bool StartsWithTest() =>
        Value.StartsWith("https://", StringComparison.OrdinalIgnoreCase);

    [Benchmark(Baseline = true)]
    public bool StartsWithOpenCoded() =>
        Value.Length >= 8
        && (Value[0] | 0x20) == 'h'
        && (Value[1] | 0x20) == 't'
        && (Value[2] | 0x20) == 't'
        && (Value[3] | 0x20) == 'p'
        && (Value[4] | 0x20) == 's'
        && Value[5] == ':'
        && Value[6] == '/'
        && Value[7] == '/';
}
