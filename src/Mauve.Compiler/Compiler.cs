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

    public void GenerateAddI32()
    {
        _generatedCode.Add((byte)Operation.AddI32);
    }

    public void GenerateAddI64()
    {
        _generatedCode.Add((byte)Operation.AddI64);
    }

    public void GenerateAddF32()
    {
        _generatedCode.Add((byte)Operation.AddF32);
    }

    public void GenerateAddF64()
    {
        _generatedCode.Add((byte)Operation.AddF64);
    }

    public void GenerateSubI32()
    {
        _generatedCode.Add((byte)Operation.SubI32);
    }

    public void GenerateSubI64()
    {
        _generatedCode.Add((byte)Operation.SubI64);
    }

    public void GenerateSubF32()
    {
        _generatedCode.Add((byte)Operation.SubF32);
    }

    public void GenerateSubF64()
    {
        _generatedCode.Add((byte)Operation.SubF64);
    }

    public void GenerateMulI32()
    {
        _generatedCode.Add((byte)Operation.MulI32);
    }

    public void GenerateMulI64()
    {
        _generatedCode.Add((byte)Operation.MulI64);
    }

    public void GenerateMulF32()
    {
        _generatedCode.Add((byte)Operation.MulF32);
    }

    public void GenerateMulF64()
    {
        _generatedCode.Add((byte)Operation.MulF64);
    }

    public void GenerateDivI32()
    {
        _generatedCode.Add((byte)Operation.DivI32);
    }

    public void GenerateDivU32()
    {
        _generatedCode.Add((byte)Operation.DivU32);
    }

    public void GenerateDivI64()
    {
        _generatedCode.Add((byte)Operation.DivI64);
    }

    public void GenerateDivU64()
    {
        _generatedCode.Add((byte)Operation.DivU64);
    }

    public void GenerateDivF32()
    {
        _generatedCode.Add((byte)Operation.DivF32);
    }

    public void GenerateDivF64()
    {
        _generatedCode.Add((byte)Operation.DivF64);
    }

    public void GenerateRemI32()
    {
        _generatedCode.Add((byte)Operation.RemI32);
    }

    public void GenerateRemU32()
    {
        _generatedCode.Add((byte)Operation.RemU32);
    }

    public void GenerateRemI64()
    {
        _generatedCode.Add((byte)Operation.RemI64);
    }

    public void GenerateRemU64()
    {
        _generatedCode.Add((byte)Operation.RemU64);
    }

    public void GenerateJump(long address)
    {
        _generatedCode.Add((byte)Operation.Jump);
        GenerateConstantI64(address);
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