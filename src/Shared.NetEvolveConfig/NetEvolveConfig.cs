namespace Benchmark.Shuffle.Benchmarks;

using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Reports;

internal sealed class NetEvolveConfig : ManualConfig
{
    public NetEvolveConfig()
    {
        SummaryStyle = SummaryStyle.Default.WithRatioStyle(RatioStyle.Trend);

        _ = AddDiagnoser(new MemoryDiagnoser(new MemoryDiagnoserConfig()));
    }
}
