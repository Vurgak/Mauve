namespace Mauve.ByteCode;

public enum Operation : byte
{
    NoOperation = 0x00,
    
    LoadI32,
    Load0I32,
    LoadI64,
    Load0I64,
    LoadF32,
    Load0F32,
    LoadF64,
    Load0F64,
    
    /// <summary>
    /// Drop top 4 bytes from the stack.
    /// </summary>
    Drop32,
    
    /// <summary>
    /// Drop top 8 bytes from the stack.
    /// </summary>
    Drop64,
    
    // Temporary operations for debugging:
    PrintI32,
    PrintU32,
    PrintI64,
    PrintU64,
    PrintF32,
    PrintF64,
}