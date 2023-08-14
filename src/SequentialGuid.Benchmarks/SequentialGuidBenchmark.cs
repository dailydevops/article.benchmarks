namespace SequentialGuid.Benchmarks;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using NetEvolve.Benchmarks;

[Config(typeof(NetEvolveConfig))]
[SimpleJob(runtimeMoniker: RuntimeMoniker.Net80)]
[System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Performance",
    "CA1822:Mark members as static",
    Justification = "<Pending>"
)]
public class SequentialGuidBenchmark
{
    [Benchmark(Baseline = true)]
    public string GuidAsBase() => SequentialGuidCore.GuidAsBase();

    [Benchmark]
    public string Stackalloc() => SequentialGuidCore.Stackalloc();

    [Benchmark]
    public string StackallocHex() => SequentialGuidCore.StackallocHex();

    [Benchmark]
    public string StackallocToString() => SequentialGuidCore.StackallocToString();

    [Benchmark]
    public string CreateAdp() => SequentialGuidCore.CreateAdp();
}
