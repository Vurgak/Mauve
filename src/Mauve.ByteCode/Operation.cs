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
    
    AddI32,
    AddI64,
    AddF32,
    AddF64,
    
    SubI32,
    SubI64,
    SubF32,
    SubF64,
    
    MulI32,
    MulI64,
    MulF32,
    MulF64,
    
    DivI32,
    DivU32,
    DivI64,
    DivU64,
    DivF32,
    DivF64,
    
    RemI32,
    RemU32,
    RemI64,
    RemU64,
    
    Allocate,
    
    /// <summary>
    /// Unconditional jump to the specified address.
    /// </summary>
    Jump,
    
    // Temporary operations for debugging:
    PrintI32,
    PrintU32,
    PrintI64,
    PrintU64,
    PrintF32,
    PrintF64,
}