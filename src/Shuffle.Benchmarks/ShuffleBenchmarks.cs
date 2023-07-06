namespace Benchmark.Shuffle.Benchmarks;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;

[Config(typeof(NetEvolveConfig))]
[SimpleJob(runtimeMoniker: RuntimeMoniker.Net80)]
public class ShuffleBenchmarks
{
    private readonly List<int> _values;
    private readonly Consumer _consumer;
    private readonly Random _random;

    public ShuffleBenchmarks()
    {
        _values = Enumerable.Range(1, 50).ToList();
        _consumer = new Consumer();

        _random = Random.Shared;
    }

    [Benchmark(Baseline = true)]
    public void ShuffleLinqOrderBy() =>
        ShuffleExtensions.ShuffleLinqOrderBy(_values, _random).Consume(_consumer);

    [Benchmark]
    public void ShuffleLinqOrderByGuid() =>
        ShuffleExtensions.ShuffleLinqOrderByGuid(_values).Consume(_consumer);

    [Benchmark]
    public void ShuffleFYDToList() =>
        ShuffleExtensions.ShuffleFYDToList(_values, _random).Consume(_consumer);

    [Benchmark]
    public void ShuffleFYDToArray() =>
        ShuffleExtensions.ShuffleFYDToArray(_values, _random).Consume(_consumer);

    [Benchmark]
    public void ShuffleMoreLinq() =>
        MoreLinq.MoreEnumerable.Shuffle(_values, _random).Consume(_consumer);

    [Benchmark]
    public void ShuffleGiesel() =>
        ShuffleExtensions.ShuffleGiesel(_values, _random).Consume(_consumer);

    [Benchmark]
    public void ShuffleGieselOptimized() =>
        ShuffleExtensions.ShuffleGieselOptimized(_values, _random).Consume(_consumer);
}
