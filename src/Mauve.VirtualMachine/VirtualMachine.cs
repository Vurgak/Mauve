using System.Buffers.Binary;
using Mauve.ByteCode;

namespace Mauve.VirtualMachine;

public class VirtualMachine
{
    /// <summary>
    ///     Program's heap memory.
    /// </summary>
    private readonly byte[] _heap;

    /// <summary>
    ///     Program's stack memory.
    /// </summary>
    private readonly StackMemory _stack;

    public VirtualMachine(int stackSize, int heapSize)
    {
        _stack = new StackMemory(stackSize);
        _heap = new byte[heapSize];
    }

    public void Execute(ReadOnlySpan<byte> byteCode)
    {
        var instructionPointer = 0;

        while (instructionPointer < byteCode.Length)
        {
            var operation = FetchOperation(byteCode, ref instructionPointer);
            switch (operation)
            {
                case Operation.NoOperation:
                    continue;

                case Operation.LoadI32:
                case Operation.LoadF32:
                {
                    var operand = FetchShortWord(byteCode, ref instructionPointer);
                    _stack.PushShortWord(operand);
                    continue;
                }

                case Operation.Load0I32:
                case Operation.Load0F32:
                    _stack.PushShortWord(0);
                    continue;

                case Operation.LoadI64:
                case Operation.LoadF64:
                {
                    var operand = FetchLongWord(byteCode, ref instructionPointer);
                    _stack.PushLongWord(operand);
                    continue;
                }

                case Operation.Load0I64:
                case Operation.Load0F64:
                    _stack.PushLongWord(0);
                    continue;

                case Operation.Drop32:
                    _stack.DropShortWord();
                    continue;

                case Operation.Drop64:
                    _stack.DropLongWord();
                    continue;

                case Operation.PrintI32:
                {
                    var value = _stack.PopShortWord();
                    Console.WriteLine(value);
                    continue;
                }

                case Operation.PrintU32:
                {
                    var value = (uint)_stack.PopShortWord();
                    Console.WriteLine(value);
                    continue;
                }

                case Operation.PrintI64:
                {
                    var value = (uint)_stack.PopLongWord();
                    Console.WriteLine(value);
                    continue;
                }

                case Operation.PrintU64:
                {
                    var value = (ulong)_stack.PopLongWord();
                    Console.WriteLine(value);
                    continue;
                }

                case Operation.PrintF32:
                {
                    var value = BitConverter.ToSingle(BitConverter.GetBytes(_stack.PopShortWord()));
                    Console.WriteLine(value);
                    continue;
                }

                case Operation.PrintF64:
                {
                    var value = BitConverter.ToDouble(BitConverter.GetBytes(_stack.PopLongWord()));
                    Console.WriteLine(value);
                    continue;
                }   
                    
                default:
                    throw new ArgumentOutOfRangeException(nameof(operation), operation, "Invalid operation code");
            }
        }
    }

    private Operation FetchOperation(ReadOnlySpan<byte> byteCode, ref int instructionPointer)
    {
        var operationCode = byteCode[instructionPointer];
        instructionPointer += sizeof(Operation);

        var operation = (Operation)operationCode;
        return operation;
    }

    private int FetchShortWord(ReadOnlySpan<byte> byteCode, ref int instructionPointer)
    {
        var bytes = byteCode.Slice(instructionPointer, sizeof(int));
        instructionPointer += sizeof(int);

        var value = BinaryPrimitives.ReadInt32LittleEndian(bytes);
        return value;
    }

    private long FetchLongWord(ReadOnlySpan<byte> byteCode, ref int instructionPointer)
    {
        var bytes = byteCode.Slice(instructionPointer, sizeof(long));
        instructionPointer += sizeof(long);

        var value = BinaryPrimitives.ReadInt64LittleEndian(bytes);
        return value;
    }
}