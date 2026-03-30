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

    public static string ValueType_Name(ValueType VT) => VT switch{
        ValueType.Bool => "BL",
        ValueType.Float => "F",
        ValueType.Double => "D",
        ValueType.Int => "I",
        ValueType.UInt => "UI",
        ValueType.Short => "S",
        ValueType.UShort => "US",
        ValueType.Long => "L",
        ValueType.ULong => "UL",
        ValueType.Byte => "B",
        ValueType.SByte => "SB",
        ValueType.Char => "C",
        ValueType.String => "ST",
        ValueType.Decimal => "DE",
        ValueType.Object => "O",
        ValueType.DateTime => "DT"
    };
}