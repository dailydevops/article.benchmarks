namespace SearchValues.Benchmarks;

using BenchmarkDotNet.Running;

internal class Program
{
    public static void Main(string[] _) =>
        BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).RunAll();
}
