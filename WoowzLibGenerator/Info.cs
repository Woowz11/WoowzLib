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

    public static readonly ValueType[] Numbers = [
        ValueType.Float, ValueType.Double, ValueType.Int, ValueType.UInt, ValueType.Short, ValueType.UShort, ValueType.Long, ValueType.ULong, ValueType.Byte, ValueType.SByte, ValueType.Decimal
    ];

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
    
    private static readonly string[] __ValueType_One = [
        "~",  // Bool
        "1",      // Float
        "1",      // Double
        "1",      // Int
        "1",      // UInt
        "1",      // Short
        "1",      // UShort
        "1",      // Long
        "1",      // ULong
        "255",      // Byte
        "127",      // SByte
        "~",   // Char
        "~",   // String
        "1",      // Decimal
        "~",   // Object
        "~" // DateTime
    ];
    public static string ValueType_One(ValueType VT) => __ValueType_One[(int)VT];
    
    private static readonly string[] __ValueType_One_Detail = [
        "~",  // Bool
        "1f",      // Float
        "1.0",      // Double
        "1",      // Int
        "1",      // UInt
        "1",      // Short
        "1",      // UShort
        "1",      // Long
        "1",      // ULong
        "255",      // Byte
        "127",      // SByte
        "~",   // Char
        "~",   // String
        "1m",      // Decimal
        "~",   // Object
        "~" // DateTime
    ];
    public static string ValueType_One_Detail(ValueType VT) => __ValueType_One_Detail[(int)VT];
    
    private static readonly string[] __ValueType_Half = [
        "~",  // Bool
        "0.5f",      // Float
        "0.5",      // Double
        "~",      // Int
        "~",      // UInt
        "~",      // Short
        "~",      // UShort
        "~",      // Long
        "~",      // ULong
        "127",      // Byte
        "63",      // SByte
        "~",   // Char
        "~",   // String
        "0.5m",      // Decimal
        "~",   // Object
        "~" // DateTime
    ];
    public static string ValueType_Half(ValueType VT) => __ValueType_Half[(int)VT];
    
    private static readonly string[] __ValueType_Quarter = [
        "~",  // Bool
        "0.25f",      // Float
        "0.25",      // Double
        "~",      // Int
        "~",      // UInt
        "~",      // Short
        "~",      // UShort
        "~",      // Long
        "~",      // ULong
        "63",      // Byte
        "31",      // SByte
        "~",   // Char
        "~",   // String
        "0.25m",      // Decimal
        "~",   // Object
        "~" // DateTime
    ];
    public static string ValueType_Quarter(ValueType VT) => __ValueType_Quarter[(int)VT];
    
    private static readonly string[] __ValueType_Double = [
        "~",  // Bool
        "2",      // Float
        "2",      // Double
        "2",      // Int
        "2",      // UInt
        "2",      // Short
        "2",      // UShort
        "2",      // Long
        "2",      // ULong
        "~",      // Byte
        "~",      // SByte
        "~",   // Char
        "~",   // String
        "2",      // Decimal
        "~",   // Object
        "~" // DateTime
    ];
    public static string ValueType_Double(ValueType VT) => __ValueType_Double[(int)VT];
    
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
    
    private static readonly bool[] __ValueType_SupportNegative = [
        false,  // Bool
        true,      // Float
        true,      // Double
        true,      // Int
        false,      // UInt
        true,      // Short
        false,      // UShort
        true,      // Long
        false,      // ULong
        false,      // Byte
        true,      // SByte
        false,   // Char
        false,   // String
        true,      // Decimal
        false,   // Object
        false // DateTime
    ];
    public static bool ValueType_SupportNegative(ValueType VT) => __ValueType_SupportNegative[(int)VT];
    
    private static readonly bool[] __ValueType_SupportFraction = [
        false,  // Bool
        true,      // Float
        true,      // Double
        false,      // Int
        false,      // UInt
        false,      // Short
        false,      // UShort
        false,      // Long
        false,      // ULong
        true,      // Byte
        true,      // SByte
        false,   // Char
        false,   // String
        true,      // Decimal
        false,   // Object
        false // DateTime
    ];
    public static bool ValueType_SupportFraction(ValueType VT) => __ValueType_SupportFraction[(int)VT];
}