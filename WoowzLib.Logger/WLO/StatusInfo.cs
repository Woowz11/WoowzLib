namespace WLO;

public struct StatusInfo{
    public char Symbol;

    public static readonly StatusInfo Default = new StatusInfo{
        Symbol = ' '
    };
}