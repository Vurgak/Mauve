namespace Mauve.VirtualMachine;

internal class HeapMemory
{
    private readonly byte[] _heap;

    private int _bumpPointer;
    
    public HeapMemory(int size)
    {
        _heap = new byte[size];
    }

    /// <summary>
    ///     Allocate the given amount of bytes on the heap.
    /// </summary>
    /// <param name="size">Number of bytes to allocate.</param>
    /// <returns>Pointer to the start of the allocated area.</returns>
    public int Allocate(int size)
    {
        var pointer = AlignPointer(_bumpPointer);
        if (pointer + size >= _heap.Length)
            throw new OutOfMemoryException();

        _bumpPointer = pointer + size;
        return pointer;
    }

    private static int AlignPointer(int pointer)
    {
        var aligned = pointer + sizeof(long) - 1 / sizeof(long);
        return aligned;
    }
}