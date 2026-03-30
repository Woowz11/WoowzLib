namespace WoowzLibGenerator;

public static class Info{
    public enum ValueType{
        Bool,
        Float,
        Double,
        Int,
        UInt,
        Short,
        UShort,
        Long,
        ULong,
        Byte,
        SByte,
        Char,
        String,
        Decimal,
        Object,
        DateTime
    }

    private static readonly string[] __ValueType_Name = [
        "BL","F","D","I","UI","S","US","L","UL","B","SB","C","ST","DE","O","DT"
    ];
    public static string ValueType_Name(ValueType VT) => __ValueType_Name[(int)VT];

    private static readonly string[] __ValueType_Primitive = [
        "bool","float","double","int","uint","short","ushort",
        "long","ulong","byte","sbyte","char","string","decimal",
        "object","DateTime"
    ];
    public static string ValueType_Primitive(ValueType VT) => __ValueType_Primitive[(int)VT];

    private static readonly string[] __ValueType_Zero = [
        "~",  // Bool
        "0",      // Float
        "0",      // Double
        "0",      // Int
        "0",      // UInt
        "0",      // Short
        "0",      // UShort
        "0",      // Long
        "0",      // ULong
        "0",      // Byte
        "0",      // SByte
        "~",   // Char
        "~",   // String
        "0",      // Decimal
        "~",   // Object
        "~" // DateTime
    ];
    public static string ValueType_Zero(ValueType VT) => __ValueType_Zero[(int)VT];
    
    private static readonly string[] __ValueType_Default = [
        "false",  // Bool
        ValueType_Zero(ValueType.Float),      // Float
        ValueType_Zero(ValueType.Double),      // Double
        ValueType_Zero(ValueType.Int),      // Int
        ValueType_Zero(ValueType.UInt),      // UInt
        ValueType_Zero(ValueType.Short),      // Short
        ValueType_Zero(ValueType.UShort),      // UShort
        ValueType_Zero(ValueType.Long),      // Long
        ValueType_Zero(ValueType.ULong),      // ULong
        ValueType_Zero(ValueType.Byte),      // Byte
        ValueType_Zero(ValueType.SByte),      // SByte
        "'\0'",   // Char
        "\"\"",   // String
        ValueType_Zero(ValueType.Decimal),      // Decimal
        "null",   // Object
        "default" // DateTime
    ];
    public static string ValueType_Default(ValueType VT) => __ValueType_Default[(int)VT];
}