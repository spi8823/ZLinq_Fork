using ZLinq;
using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<Benchmark>();

[ShortRunJob]
[MemoryDiagnoser]
[HtmlExporter]
public class  Benchmark
{
    public int[] array = ValueEnumerable.Range(0, 10000).ToArray();
    public int count = 1000;

    [Benchmark]
    public void System_OrderBy()
    {
        for (int i = 0; i < count; i++)
        {
            var seq = array.OrderBy(x => x);
            foreach (var item in seq) { }
        }
    }

    [Benchmark]
    public void ZLinq_OrderBy()
    {
        for (int i = 0; i < count; i++)
        {
            var seq = array.AsValueEnumerable().OrderBy(x => x);
            foreach (var item in seq) { }
        }
    }
}
