using System.Runtime.CompilerServices;

namespace Mauve.VirtualMachine;

public struct ShortWord
{
    private int _value;

    public ShortWord(int value)
    {
        _value = value;
    }

    public int I32 => _value;
    
    public uint U32 => (uint)_value;

    public float F32 => Unsafe.As<int, float>(ref _value);
}