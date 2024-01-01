namespace SwitchSyntacticSugar.Benchmarks;

using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using NetEvolve.Benchmarks;

[Config(typeof(NetEvolveConfig))]
[SimpleJob(runtimeMoniker: RuntimeMoniker.Net80)]
public class SwitchBenchmarks
{
    [Params(DayOfWeek.Thursday, DayOfWeek.Saturday)]
    public DayOfWeek Value { get; set; }

    [Benchmark(Baseline = true)]
    public string SwitchStatement() => SwitchCore.GetWorkTimeStatement(Value);

    [Benchmark]
    public string SwitchExpression() => SwitchCore.GetWorkTimeExpression(Value);

    [Benchmark]
    public string SwitchDictionary() => SwitchCore.GetWorkTimeDictionary(Value);
}
