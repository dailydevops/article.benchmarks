namespace SequentialGuid.Benchmarks;

using System;
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
    public Guid NewGuid() => Guid.NewGuid();

    [Benchmark]
    public Guid GuidAsBase() => SequentialGuidCore.GuidAsBase();

    [Benchmark]
    public Guid Stackalloc() => SequentialGuidCore.Stackalloc();

    [Benchmark]
    public Guid StackallocWithRandom() => SequentialGuidCore.StackallocWithRandom();

    [Benchmark]
    public Guid CreateAdp() => SequentialGuidCore.CreateAdp();
}
