namespace WLO;

public abstract class ParsedContainer_Image : ParsedContainer{
    /// <summary>
    /// Ширина
    /// </summary>
    public uint Width;

    /// <summary>
    /// Высота
    /// </summary>
    public uint Height;

    /// <summary>
    /// Кол-во каналов (байтов на пиксель)
    /// </summary>
    public ushort Channels => (ushort)(BitsPerPixel / 8);

    /// <summary>
    /// Кол-во бит на пиксель (1: Чёрный и белый (0.125), 8: Градации чёрно-белого (1), 24: RGB (3), 32: RGBA (4))
    /// </summary>
    public ushort BitsPerPixel;

    /// <summary>
    /// Цвета
    /// </summary>
    public byte[] Pixels;

    public override string ToString() => Format + "(" + Width + "x" + Height + "x" + Channels + ")";
}