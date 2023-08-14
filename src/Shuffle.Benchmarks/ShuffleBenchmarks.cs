namespace Shuffle.Benchmarks;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using NetEvolve.Benchmarks;
using System;
using System.Linq;

[Config(typeof(NetEvolveConfig))]
[SimpleJob(runtimeMoniker: RuntimeMoniker.Net80)]
public class ShuffleBenchmarks
{
    private readonly int[] _values;
    private readonly Consumer _consumer;
    private readonly Random _random;

    public ShuffleBenchmarks()
    {
        _values = Enumerable.Range(1, 50).ToArray();
        _consumer = new Consumer();

        _random = Random.Shared;
    }

    [Benchmark(Baseline = true)]
    public void ShuffleLinqOrderBy() => _values.ShuffleLinqOrderBy(_random).Consume(_consumer);

    [Benchmark]
    public void ShuffleLinqOrderByGuid() => _values.ShuffleLinqOrderByGuid().Consume(_consumer);

    [Benchmark]
    public void ShuffleFYDToList() => _values.ShuffleFYDToList(_random).Consume(_consumer);

    [Benchmark]
    public void ShuffleFYDToArray() => _values.ShuffleFYDToArray(_random).Consume(_consumer);

    [Benchmark]
    public void ShuffleMoreLinq() =>
        MoreLinq.MoreEnumerable.Shuffle(_values, _random).Consume(_consumer);

    [Benchmark]
    public void ShuffleGiesel() => _values.ShuffleGiesel(_random).Consume(_consumer);

    [Benchmark]
    public void ShuffleGieselOptimized() =>
        _values.ShuffleGieselOptimized(_random).Consume(_consumer);

    [Benchmark]
#pragma warning disable CA5394 // Do not use insecure randomness
#pragma warning disable CA1711 // Identifiers should not have incorrect suffix
    public void ShuffleDotnetImpl() => _random.Shuffle(_values);
#pragma warning restore CA1711 // Identifiers should not have incorrect suffix
#pragma warning restore CA5394 // Do not use insecure randomness
}
