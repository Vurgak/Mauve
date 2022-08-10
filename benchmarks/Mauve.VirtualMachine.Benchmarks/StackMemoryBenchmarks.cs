using BenchmarkDotNet.Attributes;

namespace Mauve.VirtualMachine.Benchmarks;

public class StackMemoryBenchmarks
{
    private const int StackSize = 1024;
    private const int ShortWord = -1347856651;
    private const long LongWord = -8256785960430895956;

    private StackMemory _stack;

    [IterationSetup]
    public void IterationSetup()
    {
        _stack = new StackMemory(StackSize);
    }

    [Benchmark]
    public void PushShortWord() => _stack.PushShortWord(ShortWord);

    [Benchmark]
    public void PushLongWord() => _stack.PushLongWord(LongWord);
}