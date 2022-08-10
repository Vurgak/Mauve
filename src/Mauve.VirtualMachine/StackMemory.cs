﻿using System.Buffers.Binary;

namespace Mauve.VirtualMachine;

internal class StackMemory
{
    /// <summary>
    /// Stack memory blob.
    /// </summary>
    private readonly byte[] _memory;
    
    /// <summary>
    /// Stack pointer.
    /// </summary>
    public int Pointer { get; private set; }
    
    public StackMemory(int size)
    {
        _memory = new byte[size];
    }

    /// <summary>
    /// Push a 32-bit value onto the top of the stack.
    /// </summary>
    /// <param name="value">The 32-bit value to be pushed.</param>
    public void PushShortWord(int value)
    {
        var target = _memory.AsSpan(Pointer, sizeof(int));
        BinaryPrimitives.WriteInt32LittleEndian(target, value);
        Pointer += sizeof(int);
    }

    /// <summary>
    /// Push a 64-bit value onto the top of the stack.
    /// </summary>
    /// <param name="value">The 64-bit value to be pushed.</param>
    public void PushLongWord(long value)
    {
        var target = _memory.AsSpan(Pointer, sizeof(long));
        BinaryPrimitives.WriteInt64LittleEndian(target, value);
        Pointer += sizeof(long);
    }

    public int PopShortWord()
    {
        Pointer -= sizeof(int);

        var bytes = _memory.AsSpan(Pointer, sizeof(int));
        var value = BinaryPrimitives.ReadInt32LittleEndian(bytes);
        return value;
    }

    public long PopLongWord()
    {
        Pointer -= sizeof(long);

        var bytes = _memory.AsSpan(Pointer, sizeof(long));
        var value = BinaryPrimitives.ReadInt64LittleEndian(bytes);
        return value;
    }
    
    public void DropShortWord()
    {
        Pointer -= sizeof(int);
    }

    public void DropLongWord()
    {
        Pointer -= sizeof(long);
    }
}