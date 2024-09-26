using BenchmarkDotNet.Running;

namespace JsonBenchmarks;

internal class Program
{
	static void Main(string[] args)
	{
		BenchmarkRunner.Run<Benchmarks>();
	}
}