using System.Buffers.Binary;
using System.Runtime.InteropServices;
using Mauve.ByteCode;

namespace Mauve.Compiler;

public class Compiler
{
    private readonly List<byte> _generatedCode = new();

    public ReadOnlySpan<byte> GeneratedCode => CollectionsMarshal.AsSpan(_generatedCode);
    
    public void GeneratePushI32(int value)
    {
        if (value == 0)
        {
            _generatedCode.Add((byte)Operation.Load0I32);
            return;
        }

        _generatedCode.Add((byte)Operation.LoadI32);
        GenerateConstantI32(value);
    }
    
    public void GeneratePushI64(long value)
    {
        if (value == 0)
        {
            _generatedCode.Add((byte)Operation.Load0I64);
            return;
        }

        _generatedCode.Add((byte)Operation.LoadI64);
        GenerateConstantI64(value);
    }
    
    public void GeneratePushF32(float value)
    {
        if (value == 0)
        {
            _generatedCode.Add((byte)Operation.Load0F32);
            return;
        }

        _generatedCode.Add((byte)Operation.LoadF32);
        GenerateConstantF32(value);
    }
    
    public void GeneratePushF64(double value)
    {
        if (value == 0)
        {
            _generatedCode.Add((byte)Operation.Load0F64);
            return;
        }

        _generatedCode.Add((byte)Operation.LoadF64);
        GenerateConstantF64(value);
    }

    public void GeneratePrintI32()
    {
        _generatedCode.Add((byte)Operation.PrintI32);
    }

    public void GeneratePrintI64()
    {
        _generatedCode.Add((byte)Operation.PrintI64);
    }

    public void GeneratePrintF32()
    {
        _generatedCode.Add((byte)Operation.PrintF32);
    }

    public void GeneratePrintF64()
    {
        _generatedCode.Add((byte)Operation.PrintF64);
    }

    private void GenerateConstantI32(int value)
    {
        var insertAt = _generatedCode.Count;
        _generatedCode.AddRange(Enumerable.Repeat<byte>(0, sizeof(int)));
        var target = CollectionsMarshal.AsSpan(_generatedCode).Slice(insertAt, sizeof(int));
        BinaryPrimitives.WriteInt32LittleEndian(target, value);
    }

    private void GenerateConstantI64(long value)
    {
        var insertAt = _generatedCode.Count;
        _generatedCode.AddRange(Enumerable.Repeat<byte>(0, sizeof(long)));
        var target = CollectionsMarshal.AsSpan(_generatedCode).Slice(insertAt, sizeof(long));
        BinaryPrimitives.WriteInt64LittleEndian(target, value);
    }

    private void GenerateConstantF32(float value)
    {
        var insertAt = _generatedCode.Count;
        _generatedCode.AddRange(Enumerable.Repeat<byte>(0, sizeof(float)));
        var target = CollectionsMarshal.AsSpan(_generatedCode).Slice(insertAt, sizeof(float));
        BinaryPrimitives.WriteSingleLittleEndian(target, value);
    }

    private void GenerateConstantF64(double value)
    {
        var insertAt = _generatedCode.Count;
        _generatedCode.AddRange(Enumerable.Repeat<byte>(0, sizeof(double)));
        var target = CollectionsMarshal.AsSpan(_generatedCode).Slice(insertAt, sizeof(double));
        BinaryPrimitives.WriteDoubleLittleEndian(target, value);
    }
}