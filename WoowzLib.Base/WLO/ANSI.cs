namespace WLO;

public static class ANSI{
    public enum Code : uint{
        /// <summary>
        /// Сбрасывает все эффекты ANSI
        /// </summary>
        Reset = 0,
        
        /// <summary>
        /// Делает текст жирнее
        /// </summary>
        Bold = 1,
        /// <summary>
        /// Двойное подчёркивание, или не жирный текст (не работает в Rider?)
        /// </summary>
        BoldNo = 21,
        /// <summary>
        /// Делает текст наклонным
        /// </summary>
        Italic = 3,
        /// <summary>
        /// Делает текст не наклонённым (не работает в Rider?)
        /// </summary>
        ItalicNo = 23,
        /// <summary>
        /// Подчёркивает текст
        /// </summary>
        Underline = 4,
        /// <summary>
        /// Делает текст не подчёркнутым (не работает в Rider?)
        /// </summary>
        UnderlineNo = 24,
        /// <summary>
        /// Зачёркивает текст (не работает в Rider)
        /// </summary>
        Strike = 9,
        /// <summary>
        /// Делает текст не зачёркнутым (не работает в Rider?)
        /// </summary>
        StrikeNo = 29,
        
        // ----------------------------------------------------------------------
        
        /// <summary>
        /// Делает текст бледнее (прозрачнее)
        /// </summary>
        Transparent = 2,
        /// <summary>
        /// Делает текст не бледным (не работает в Rider?)
        /// </summary>
        TransparentNo = 22,
        /// <summary>
        /// Делает текст мигающим (не работает в Rider)
        /// </summary>
        Blinking = 5,
        /// <summary>
        /// Делает текст мигающим (быстрее) (не работает в Rider)
        /// </summary>
        BlinkingFast = 6,
        /// <summary>
        /// Делает текст не мигающим (не работает в Rider?)
        /// </summary>
        BlinkingNo = 25,
        /// <summary>
        /// Инвертирует цвета текста
        /// </summary>
        Inversion = 7,
        /// <summary>
        /// Делает текст не инвертированным (не работает в Rider?)
        /// </summary>
        InversionNo = 27,
        /// <summary>
        /// Прячет текст (не работает в Rider)
        /// </summary>
        Hide = 8,
        /// <summary>
        /// Показывает текст (делает текст не спрятанным) (не работает в Rider)
        /// </summary>
        Show = 28,
        /// <summary>
        /// Делает текст monospace (не работает почти везде)
        /// </summary>
        Monospace = 50,
        /// <summary>
        /// Делает текст не monospace (не работает почти везде)
        /// </summary>
        MonospaceNo = 26,
        /// <summary>
        /// Делает текст в рамке (не работает в Rider)
        /// </summary>
        Framed = 51,
        /// <summary>
        /// Делает текст не в рамке/кругах (не работает в Rider)
        /// </summary>
        FramedNo = 54,
        /// <summary>
        /// Делает текст в кругах (не работает в Rider)
        /// </summary>
        Encircled = 52,
        /// <summary>
        /// Делает линию над текстом (не работает в Rider)
        /// </summary>
        Overline = 53,
        /// <summary>
        /// Убирает линию над текстом (не работает в Rider)
        /// </summary>
        OverlineNo = 55,
        
        // ----------------------------------------------------------------------
        
        /// <summary>
        /// Делает шрифт дефолтным
        /// </summary>
        FontDefault = 10,
        /// <summary>
        /// Устанавливает 1-й альтернативный шрифт (не работает в Rider)
        /// </summary>
        Font1 = 11,
        /// <summary>
        /// Устанавливает 2-й альтернативный шрифт (не работает в Rider)
        /// </summary>
        Font2 = 12,
        /// <summary>
        /// Устанавливает 3-й альтернативный шрифт (не работает в Rider)
        /// </summary>
        Font3 = 13,
        /// <summary>
        /// Устанавливает 4-й альтернативный шрифт (не работает в Rider)
        /// </summary>
        Font4 = 14,
        /// <summary>
        /// Устанавливает 5-й альтернативный шрифт (не работает в Rider)
        /// </summary>
        Font5 = 15,
        /// <summary>
        /// Устанавливает 6-й альтернативный шрифт (не работает в Rider)
        /// </summary>
        Font6 = 16,
        /// <summary>
        /// Устанавливает 7-й альтернативный шрифт (не работает в Rider)
        /// </summary>
        Font7 = 17,
        /// <summary>
        /// Устанавливает 8-й альтернативный шрифт (не работает в Rider)
        /// </summary>
        Font8 = 18,
        /// <summary>
        /// Устанавливает 9-й альтернативный шрифт (не работает в Rider)
        /// </summary>
        Font9 = 19,
        /// <summary>
        /// Устанавливает готический шрифт (не работает в Rider)
        /// </summary>
        FontGothic = 20,
        
        // ----------------------------------------------------------------------
        
        /// <summary>
        /// Сбрасывает цвет текста
        /// </summary>
        Default = 39,
        /// <summary>
        /// Сбрасывает цвет фона
        /// </summary>
        Default_BG = 59,
        
        /// <summary>
        /// Делает текст чёрного цвета
        /// </summary>
        Black = 30,
        /// <summary>
        /// Делает фон чёрного цвета
        /// </summary>
        Black_BG = 40,
        
        /// <summary>
        /// Делает текст серого цвета
        /// </summary>
        Gray = 90,
        /// <summary>
        /// Делает фон серого цвета
        /// </summary>
        Gray_BG = 100,
        
        /// <summary>
        /// Делает текст светло-серого цвета
        /// </summary>
        GrayB = 37,
        /// <summary>
        /// Делает фон светло-серого цвета
        /// </summary>
        GrayB_BG = 47,
        
        /// <summary>
        /// Делает текст белого цвета
        /// </summary>
        White = 97,
        /// <summary>
        /// Делает фон белого цвета
        /// </summary>
        White_BG = 107,
        
        /// <summary>
        /// Делает текст красного цвета
        /// </summary>
        Red = 31,
        /// <summary>
        /// Делает фон красного цвета
        /// </summary>
        Red_BG = 41,
        
        /// <summary>
        /// Делает текст светло-красного цвета
        /// </summary>
        RedB = 91,
        /// <summary>
        /// Делает фон светло-красного цвета
        /// </summary>
        RedB_BG = 101,
        
        /// <summary>
        /// Делает текст жёлтого цвета
        /// </summary>
        Yellow = 33,
        /// <summary>
        /// Делает фон жёлтого цвета
        /// </summary>
        Yellow_BG = 43,
        
        /// <summary>
        /// Делает текст светло-жёлтого цвета
        /// </summary>
        YellowB = 93,
        /// <summary>
        /// Делает фон светло-жёлтого цвета
        /// </summary>
        YellowB_BG = 103,
        
        /// <summary>
        /// Делает текст зелёного цвета
        /// </summary>
        Green = 32,
        /// <summary>
        /// Делает фон зелёного цвета
        /// </summary>
        Green_BG = 42,
        
        /// <summary>
        /// Делает текст светло-зелёного цвета
        /// </summary>
        GreenB = 92,
        /// <summary>
        /// Делает фон светло-зелёного цвета
        /// </summary>
        GreenB_BG = 102,
        
        /// <summary>
        /// Делает текст голубого цвета
        /// </summary>
        Cyan = 36,
        /// <summary>
        /// Делает фон голубого цвета
        /// </summary>
        Cyan_BG = 46,
        
        /// <summary>
        /// Делает текст светло-голубого цвета
        /// </summary>
        CyanB = 96,
        /// <summary>
        /// Делает фон светло-голубого цвета
        /// </summary>
        CyanB_BG = 106,
        
        /// <summary>
        /// Делает текст синего цвета
        /// </summary>
        Blue = 34,
        /// <summary>
        /// Делает фон синего цвета
        /// </summary>
        Blue_BG = 44,
        
        /// <summary>
        /// Делает текст светло-синего цвета
        /// </summary>
        BlueB = 94,
        /// <summary>
        /// Делает фон светло-синего цвета
        /// </summary>
        BlueB_BG = 104,
        
        /// <summary>
        /// Делает текст пурпурного цвета
        /// </summary>
        Magenta = 35,
        /// <summary>
        /// Делает фон пурпурного цвета
        /// </summary>
        Magenta_BG = 45,
        
        /// <summary>
        /// Делает текст светло-пурпурного цвета
        /// </summary>
        MagentaB = 95,
        /// <summary>
        /// Делает фон светло-пурпурного цвета
        /// </summary>
        MagentaB_BG = 105,
        
        /// <summary>
        /// Делает текст собственного цвета (ToColorANSI)
        /// </summary>
        Custom = 38,
        /// <summary>
        /// Делает фон собственного цвета (ToColorANSI)
        /// </summary>
        Custom_BG = 48
    }

    public static string ToANSI(params uint[] Codes) => $"\x1b[{(Codes.Length switch{
        0     => 0,
        1     => Codes[0],
        var _ => string.Join(";", Codes)
    })}m";

    public static string ToANSI(params Code[] Codes) => ToANSI(Codes.Select(C => (uint)C).ToArray());

    public static string ToColorANSI(uint Code, byte Color) => $"\x1b[{Code};5;{Color}m";
    
    public static string ToColorANSI(Code Code, byte Color) => ToColorANSI((uint)Code, Color);
    
    public static string ToColorANSI(uint Code, byte R, byte G, byte B) => $"\x1b[{Code};2;{R};{G};{B}m";
    
    public static string ToColorANSI(Code Code, byte R, byte G, byte B) => ToColorANSI((uint)Code, R, G, B);
}