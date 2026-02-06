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
    /// Цвета (RGBA)
    /// </summary>
    public byte[] Pixels_RGBA;

    /// <summary>
    /// Превращает в изображение
    /// </summary>
    public Image ToImage() => new Image(Width, Height, Pixels_RGBA);

    public override string ToString() => Format + "(" + Width + "x" + Height + ")";
}