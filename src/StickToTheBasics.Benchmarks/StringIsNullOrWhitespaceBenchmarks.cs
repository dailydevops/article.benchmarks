namespace StickToTheBasics.Benchmarks;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using NetEvolve.Benchmarks;

[Config(typeof(NetEvolveConfig))]
[SimpleJob(runtimeMoniker: RuntimeMoniker.Net50)]
[SimpleJob(runtimeMoniker: RuntimeMoniker.Net60, baseline: true)]
[SimpleJob(runtimeMoniker: RuntimeMoniker.Net70)]
[SimpleJob(runtimeMoniker: RuntimeMoniker.Net80)]
public class StringIsNullOrWhitespaceBenchmarks
{
    [Params(null, "\t", "Hello World!")]
    public string? Value { get; set; }

    [Benchmark]
    public bool IsNullOrWhiteSpace() => string.IsNullOrWhiteSpace(Value);
}
