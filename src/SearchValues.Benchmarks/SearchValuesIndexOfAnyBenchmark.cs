namespace SearchValues.Benchmarks;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using NetEvolve.Benchmarks;

[Config(typeof(NetEvolveConfig))]
[SimpleJob(runtimeMoniker: RuntimeMoniker.Net80)]
public class SearchValuesIndexOfAnyBenchmark
{
    [Params("abcxyz", "mnopqr")]
    public string? Text { get; set; }

    [Benchmark(Baseline = true)]
    public int IndexOfAny() => SearchValuesIndexOfAnyCore.IndexOfAny(Text!);

    [Benchmark]
    public int ReadOnlySpanIndexOfAny() => SearchValuesIndexOfAnyCore.ReadOnlySpanIndexOfAny(Text);

    [Benchmark]
    public int SearchValuesIndexOfAny() => SearchValuesIndexOfAnyCore.SearchValuesIndexOfAny(Text);
}
