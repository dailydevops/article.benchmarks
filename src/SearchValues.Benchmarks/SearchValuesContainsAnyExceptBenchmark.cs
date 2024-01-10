namespace SearchValues.Benchmarks;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using NetEvolve.Benchmarks;

[Config(typeof(NetEvolveConfig))]
[SimpleJob(runtimeMoniker: RuntimeMoniker.Net80)]
public class SearchValuesContainsAnyExceptBenchmark
{
    [Params("abcxyz", "mnopqr")]
    public string? Text { get; set; }

    [Benchmark(Baseline = true)]
    public bool ReadOnlySpanContainsAnyExcept() =>
        SearchValuesContainsAnyExceptCore.ReadOnlySpanContainsAnyExcept(Text);

    [Benchmark]
    public bool SearchValuesContainsAnyExcept() =>
        SearchValuesContainsAnyExceptCore.SearchValuesContainsAnyExcept(Text);
}
