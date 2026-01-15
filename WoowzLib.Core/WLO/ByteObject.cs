namespace WLO;

public enum ByteSize : int{
    Null   = 0,
    Int    = sizeof(int   ),
    UInt   = sizeof(uint  ),
    Float  = sizeof(float ),
    Double = sizeof(double),
    Byte   = sizeof(byte  ),
    SByte  = sizeof(sbyte ),
    Short  = sizeof(short ),
    UShort = sizeof(ushort),
    Long   = sizeof(long  ),
    ULong  = sizeof(ulong ),
    Char   = sizeof(char  ),
    Bool   = sizeof(bool  ),
}

public interface ByteObject{
    /// <summary>
    /// Размер объекта в байтах
    /// </summary>
    public int BSize();
}