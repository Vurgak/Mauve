using System.Runtime.CompilerServices;

namespace Mauve.VirtualMachine;

public struct LongWord
{
    private long _value;

    public LongWord(long value)
    {
        _value = value;
    }

    public long I64 => _value;
    
    public ulong U64 => (ulong)_value;

    public double F64 => Unsafe.As<long, double>(ref _value);
}