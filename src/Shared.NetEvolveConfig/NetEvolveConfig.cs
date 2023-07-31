namespace NetEvolve.Benchmarks;

using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Reports;

internal sealed class NetEvolveConfig : ManualConfig
{
    public NetEvolveConfig()
    {
        SummaryStyle = SummaryStyle.Default.WithRatioStyle(RatioStyle.Trend);

        _ = AddDiagnoser(new MemoryDiagnoser(new MemoryDiagnoserConfig()));
        _ = AddDiagnoser(new DisassemblyDiagnoser(new DisassemblyDiagnoserConfig()));

        Orderer = new DefaultOrderer(SummaryOrderPolicy.FastestToSlowest);
    }
}
