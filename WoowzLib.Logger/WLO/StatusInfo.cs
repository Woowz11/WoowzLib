namespace WLO;

public struct StatusInfo{
    public char      Symbol;
    public ANSI.Code Color;
    public ANSI.Code Color_Second;

    public static readonly StatusInfo Default = new StatusInfo{
        Symbol = ' ',
        Color = ANSI.Code.White,
        Color_Second = ANSI.Code.White
    };
}