namespace Mauve.VirtualMachine.UnitTests;

public class StackMemoryTests
{
    private const int StackSize = 128;
    private readonly StackMemory _stack = new StackMemory(StackSize);
    
    [Fact]
    public void PushShortWord_GrowStackPointer()
    {
        const int expectedPointer = sizeof(int);
        
        _stack.PushShortWord(0);
        
        Assert.Equal(expectedPointer, _stack.Pointer);
    }
    
    [Fact]
    public void PushLongWord_GrowStackPointer()
    {
        const int expectedPointer = sizeof(long);
        
        _stack.PushLongWord(0);
        
        Assert.Equal(expectedPointer, _stack.Pointer);
    }
    
    [Fact]
    public void PopShortWord_ShrinkStackPointer()
    {
        const int expectedPointer = 0;
        
        _stack.PushShortWord(0);
        _stack.PopShortWord();
        
        Assert.Equal(expectedPointer, _stack.Pointer);
    }
    
    [Fact]
    public void PopLongWord_ShrinkPointer()
    {
        const int expectedPointer = 0;
        
        _stack.PushLongWord(0);
        _stack.PopLongWord();
        
        Assert.Equal(expectedPointer, _stack.Pointer);
    }
    
    [Fact]
    public void DropShortWord_ShrinkStackPointer()
    {
        const int expectedPointer = 0;
        
        _stack.PushShortWord(0);
        _stack.DropShortWord();
        
        Assert.Equal(expectedPointer, _stack.Pointer);
    }
    
    [Fact]
    public void DropLongWord_ShrinkPointer()
    {
        const int expectedPointer = 0;
        
        _stack.PushLongWord(0);
        _stack.DropLongWord();
        
        Assert.Equal(expectedPointer, _stack.Pointer);
    }
}