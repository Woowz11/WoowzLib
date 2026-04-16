namespace WLO;

public struct StatusInfo{
    public char   Symbol;
    public string Color;
    public string Color_Second;

    public static readonly StatusInfo Default = new StatusInfo{
        Symbol = ' ',
        Color = ANSI_White,
        Color_Second = ANSI_White
    };

    public static string ToANSI(int ColorID) => $"\x1b[{ColorID}m";

    public static readonly string ANSI_End = ToANSI(0);
    
    public static readonly string ANSI_Default = ToANSI(39);
    public static readonly string ANSI_White   = ToANSI(37);
    public static readonly string ANSI_Black   = ToANSI(30);
    public static readonly string ANSI_Red     = ToANSI(31);
    public static readonly string ANSI_Yellow  = ToANSI(33);
    public static readonly string ANSI_Green   = ToANSI(32);
    public static readonly string ANSI_Cyan    = ToANSI(36);
    public static readonly string ANSI_Blue    = ToANSI(34);
    public static readonly string ANSI_Magenta = ToANSI(35);
    
    public static readonly string ANSI_Default_BG = ToANSI(49);
    public static readonly string ANSI_White_BG   = ToANSI(47);
    public static readonly string ANSI_Black_BG   = ToANSI(40);
    public static readonly string ANSI_Red_BG     = ToANSI(41);
    public static readonly string ANSI_Yellow_BG  = ToANSI(43);
    public static readonly string ANSI_Green_BG   = ToANSI(42);
    public static readonly string ANSI_Cyan_BG    = ToANSI(46);
    public static readonly string ANSI_Blue_BG    = ToANSI(44);
    public static readonly string ANSI_Magenta_BG = ToANSI(45);
}