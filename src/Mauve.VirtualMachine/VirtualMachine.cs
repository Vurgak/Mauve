using System.Buffers.Binary;
using Mauve.ByteCode;

namespace Mauve.VirtualMachine;

public class VirtualMachine
{
    /// <summary>
    ///     Program's heap memory.
    /// </summary>
    private readonly HeapMemory _heap;

    /// <summary>
    ///     Program's stack memory.
    /// </summary>
    private readonly StackMemory _stack;

    public VirtualMachine(int stackSize, int heapSize)
    {
        _stack = new StackMemory(stackSize);
        _heap = new HeapMemory(heapSize);
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

                case Operation.AddI32:
                {
                    var right = _stack.PopShortWord().I32;
                    var left = _stack.PopShortWord().I32;
                    var result = left + right;
                    _stack.PushShortWord(result);
                    continue;
                }

                case Operation.AddI64:
                {
                    var right = _stack.PopLongWord().I64;
                    var left = _stack.PopLongWord().I64;
                    var result = left + right;
                    _stack.PushLongWord(result);
                    continue;
                }

                case Operation.AddF32:
                {
                    var right = _stack.PopShortWord().F32;
                    var left = _stack.PopShortWord().F32;
                    var result = left + right;
                    _stack.PushShortWord(result);
                    continue;
                }

                case Operation.AddF64:
                {
                    var right = _stack.PopLongWord().F64;
                    var left = _stack.PopLongWord().F64;
                    var result = left + right;
                    _stack.PushLongWord(result);
                    continue;
                }

                case Operation.SubI32:
                {
                    var right = _stack.PopShortWord().I32;
                    var left = _stack.PopShortWord().I32;
                    var result = left - right;
                    _stack.PushShortWord(result);
                    continue;
                }

                case Operation.SubI64:
                {
                    var right = _stack.PopLongWord().I64;
                    var left = _stack.PopLongWord().I64;
                    var result = left - right;
                    _stack.PushLongWord(result);
                    continue;
                }

                case Operation.SubF32:
                {
                    var right = _stack.PopShortWord().F32;
                    var left = _stack.PopShortWord().F32;
                    var result = left - right;
                    _stack.PushShortWord(result);
                    continue;
                }

                case Operation.SubF64:
                {
                    var right = _stack.PopLongWord().F64;
                    var left = _stack.PopLongWord().F64;
                    var result = left - right;
                    _stack.PushLongWord(result);
                    continue;
                }

                case Operation.MulI32:
                {
                    var right = _stack.PopShortWord().I32;
                    var left = _stack.PopShortWord().I32;
                    var result = left * right;
                    _stack.PushShortWord(result);
                    continue;
                }

                case Operation.MulI64:
                {
                    var right = _stack.PopLongWord().I64;
                    var left = _stack.PopLongWord().I64;
                    var result = left * right;
                    _stack.PushLongWord(result);
                    continue;
                }

                case Operation.MulF32:
                {
                    var right = _stack.PopShortWord().F32;
                    var left = _stack.PopShortWord().F32;
                    var result = left * right;
                    _stack.PushShortWord(result);
                    continue;
                }

                case Operation.MulF64:
                {
                    var right = _stack.PopLongWord().F64;
                    var left = _stack.PopLongWord().F64;
                    var result = left * right;
                    _stack.PushLongWord(result);
                    continue;
                }

                case Operation.DivI32:
                {
                    var right = _stack.PopShortWord().I32;
                    var left = _stack.PopShortWord().I32;
                    var result = left / right;
                    _stack.PushShortWord(result);
                    continue;
                }

                case Operation.DivU32:
                {
                    var right = (uint)_stack.PopShortWord().I32;
                    var left = (uint)_stack.PopShortWord().I32;
                    var result = left / right;
                    _stack.PushShortWord(result);
                    continue;
                }

                case Operation.DivI64:
                {
                    var right = _stack.PopLongWord().I64;
                    var left = _stack.PopLongWord().I64;
                    var result = left / right;
                    _stack.PushLongWord(result);
                    continue;
                }

                case Operation.DivU64:
                {
                    var right = _stack.PopLongWord().U64;
                    var left = _stack.PopLongWord().U64;
                    var result = left / right;
                    _stack.PushLongWord(result);
                    continue;
                }

                case Operation.DivF32:
                {
                    var right = _stack.PopShortWord().F32;
                    var left = _stack.PopShortWord().F32;
                    var result = left / right;
                    _stack.PushShortWord(result);
                    continue;
                }

                case Operation.DivF64:
                {
                    var right = _stack.PopLongWord().F64;
                    var left = _stack.PopLongWord().F64;
                    var result = left / right;
                    _stack.PushLongWord(result);
                    continue;
                }

                case Operation.RemI32:
                {
                    var right = _stack.PopShortWord().I32;
                    var left = _stack.PopShortWord().I32;
                    var result = left % right;
                    _stack.PushShortWord(result);
                    continue;
                }

                case Operation.RemU32:
                {
                    var right = _stack.PopShortWord().U32;
                    var left = _stack.PopShortWord().U32;
                    var result = left % right;
                    _stack.PushShortWord(result);
                    continue;
                }

                case Operation.RemI64:
                {
                    var right = _stack.PopLongWord().I64;
                    var left = _stack.PopLongWord().I64;
                    var result = left % right;
                    _stack.PushLongWord(result);
                    continue;
                }

                case Operation.RemU64:
                {
                    var right = _stack.PopLongWord().U64;
                    var left = _stack.PopLongWord().U64;
                    var result = left % right;
                    _stack.PushLongWord(result);
                    continue;
                }

                case Operation.Allocate:
                {
                    var size = FetchShortWord(byteCode, ref instructionPointer);
                    var pointer = _heap.Allocate(size);
                    _stack.PushShortWord(pointer);
                    continue;
                }

                case Operation.PrintI32:
                {
                    var value = _stack.PopShortWord().I32;
                    Console.WriteLine(value);
                    continue;
                }

                case Operation.PrintU32:
                {
                    var value = _stack.PopShortWord().U32;
                    Console.WriteLine(value);
                    continue;
                }

                case Operation.PrintI64:
                {
                    var value = _stack.PopLongWord().U64;
                    Console.WriteLine(value);
                    continue;
                }

                case Operation.PrintU64:
                {
                    var value = _stack.PopLongWord().U64;
                    Console.WriteLine(value);
                    continue;
                }

                case Operation.PrintF32:
                {
                    var value = _stack.PopShortWord().F32;
                    Console.WriteLine(value);
                    continue;
                }

                case Operation.PrintF64:
                {
                    var value = _stack.PopLongWord().F64;
                    Console.WriteLine(value);
                    continue;
                }

                default:
                    throw new ArgumentOutOfRangeException(nameof(operation), operation, "Invalid operation code");
            }
        }
    }

    private static Operation FetchOperation(ReadOnlySpan<byte> byteCode, ref int instructionPointer)
    {
        var operationCode = byteCode[instructionPointer];
        instructionPointer += sizeof(Operation);

        var operation = (Operation)operationCode;
        return operation;
    }

    private static int FetchShortWord(ReadOnlySpan<byte> byteCode, ref int instructionPointer)
    {
        var bytes = byteCode.Slice(instructionPointer, sizeof(int));
        instructionPointer += sizeof(int);

        var value = BinaryPrimitives.ReadInt32LittleEndian(bytes);
        return value;
    }

    private static long FetchLongWord(ReadOnlySpan<byte> byteCode, ref int instructionPointer)
    {
        var bytes = byteCode.Slice(instructionPointer, sizeof(long));
        instructionPointer += sizeof(long);

        var value = BinaryPrimitives.ReadInt64LittleEndian(bytes);
        return value;
    }
}