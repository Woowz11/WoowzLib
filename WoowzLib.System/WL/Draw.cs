using System.Numerics;
using WL;
using WLO;
using WLO.Rect;
using WLO.Vector;

namespace WLO{
    /// <summary>
    /// Тип кисти
    /// </summary>
    public enum BrushType : int{
        /// <summary>
        /// Сплошная линия
        /// </summary>
        Solid = 0,
        /// <summary>
        /// Пуктирная линяя
        /// </summary>
        Dash = 1,
        /// <summary>
        /// Точечная линия
        /// </summary>
        Dot = 2,
        /// <summary>
        /// Пуктирно-точечная линия
        /// </summary>
        DashDot = 3,
        /// <summary>
        /// Пуктирно-точечная-точечная линия
        /// </summary>
        DashDotDot = 4,
        /// <summary>
        /// Только контур
        /// </summary>
        Circuit = 5,
        /// <summary>
        /// Внутри контура
        /// </summary>
        InsideCircuit = 6
    }

    public enum CopyType : uint{
        /// <summary>
        /// Полное копирование, самое быстрое [old = new]
        /// </summary>
        FullCopy = Native.Raw.Windows.SRCCOPY,
        /// <summary>
        /// Применяет OR к пикселям, берёт самые яркие [old = new || old]
        /// </summary>
        OR = Native.Raw.Windows.SRCPAINT,
        /// <summary>
        /// Применяет AND к пикселям, тёмные удаляют [old = new && old]
        /// </summary>
        AND = Native.Raw.Windows.SRCAND,
        /// <summary>
        /// Применяет XOR к пикселям, повторный вызов восстанавливает изображение [old = new ^ old]
        /// </summary>
        XOR = Native.Raw.Windows.SRCINVERT,
        /// <summary>
        /// Вырезает часть изображения [old = new && !old]
        /// </summary>
        Erase = Native.Raw.Windows.SRCERASE,
        /// <summary>
        /// Инвертирует [old = !new]
        /// </summary>
        Invert = Native.Raw.Windows.NOTSRCCOPY,
        /// <summary>
        /// Инвертированный Erase [old = !(new || old)]
        /// </summary>
        InvertErase = Native.Raw.Windows.NOTSRCERASE,
        /// <summary>
        /// Заполняет чёрным [old = 0]
        /// </summary>
        Black = Native.Raw.Windows.BLACKNESS,
        /// <summary>
        /// Заполняет белым [old = 1]
        /// </summary>
        White = Native.Raw.Windows.WHITENESS,
        /// <summary>
        /// Используется для наложения текстуры/маски через кисть [old = new && brush]
        /// </summary>
        MergeCopy = Native.Raw.Windows.MERGECOPY,
        /// <summary>
        /// Осветляет изображение, инвертируя источник [old = !new || old]
        /// </summary>
        MergePaint = Native.Raw.Windows.MERGEPAINT,
        /// <summary>
        /// Просто заливает область текущей кистью [old = brush]
        /// </summary>
        PatternCopy = Native.Raw.Windows.PATCOPY,
        /// <summary>
        /// Сложная операция с кистью [old = (!new || brush) || old]
        /// </summary>
        PatternPaint = Native.Raw.Windows.PATPAINT,
        /// <summary>
        /// Инверсия с учётом кисти [old = brush ^ old]
        /// </summary>
        PatternInvert = Native.Raw.Windows.PATINVERT,
        /// <summary>
        /// Инвертирует текущее изображение [old = !old]
        /// </summary>
        InvertCurrent = Native.Raw.Windows.DSTINVERT
    }
}

namespace WL{
    public static partial class System{
        public static class Draw{
            
            /// <summary>
            /// Создаёт в памяти HDC ещё один HDC (нужно очищать!)
            /// </summary>
            /// <param name="HDC">Где создать новый HDC</param>
            /// <returns>Новый HDC в памяти</returns>
            public static IntPtr CreateMemoryHDC(IntPtr HDC) => WL.Native.Raw.Windows.CreateCompatibleDC(HDC);
            
            /// <summary>
            /// Уничтожает HDC
            /// </summary>
            /// <param name="HDC">Временный HDC</param>
            public static void DestroyHDC(IntPtr HDC){
                try{
                    if(!WL.Native.Raw.Windows.DeleteDC(HDC)){ throw new Exception("Произошла ошибка в DeleteObject!"); }
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при уничтожении Draw HDC [" + HDC + "]!", e);
                }
            }
            
            /// <summary>
            /// Получает текущий размер области HDC (с учётом вырезок)
            /// </summary>
            /// <param name="HDC">Сам HDC</param>
            /// <returns>Размер рисуемой области</returns>
            public static Vector2UI CurrentSize(IntPtr HDC) => new Vector2UI((uint)WL.Native.Raw.Windows.GetDeviceCaps(HDC, WL.Native.Raw.Windows.HORZRES), (uint)WL.Native.Raw.Windows.GetDeviceCaps(HDC, WL.Native.Raw.Windows.VERTRES));
            
            /// <summary>
            /// Копирует пиксели из одного HDC в другой HDC
            /// </summary>
            /// <param name="To">В этот HDC</param>
            /// <param name="From">Из этого HDC</param>
            /// <param name="Size">Размер области</param>
            /// <param name="ToPosition">Позиция в To</param>
            /// <param name="FromPosition">Позиция в From</param>
            /// <param name="Type">Тип копирования</param>
            public static void CopyHDC(IntPtr To, IntPtr From, Vector2UI Size, Vector2I ToPosition = default, Vector2I FromPosition = default, CopyType Type = CopyType.FullCopy){
                try{
                    if(!WL.Native.Raw.Windows.BitBlt(To, ToPosition.X, ToPosition.Y, (int)Size.W, (int)Size.H, From, FromPosition.X, FromPosition.Y, (uint)Type)){
                        throw new Exception("Произошла ошибка в BitBlt!");
                    }
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при копировании пикселей из одного HDC в другой HDC!\nВ: " + To + "\nИз: " + From + "\nРазмер: " + Size + "\n\"В\" позиция: " + ToPosition + "\n\"Из\" позиция: " + FromPosition + "\nТип копирования: " + Type, e);
                }
            }
            
            // ----------------------------------------------------------------------
            
            /// <summary>
            /// Создаёт в памяти HDC изображение (нужно очищать!)
            /// </summary>
            /// <param name="HDC">Где создать новое изображение</param>
            /// <param name="Size">Размер изображения</param>
            /// <returns>Новое изображение в памяти</returns>
            public static IntPtr CreateMemoryBitmap(IntPtr HDC, Vector2UI Size) => WL.Native.Raw.Windows.CreateCompatibleBitmap(HDC, (int)Size.X, (int)Size.Y);

            /// <summary>
            /// Выбирает изображение
            /// </summary>
            /// <param name="HDC">Где выбрать?</param>
            /// <param name="BitMap">Изображение</param>
            /// <returns>Старое выбранное изображение</returns>
            public static IntPtr SelectBitmap(IntPtr HDC, IntPtr BitMap) => WL.Native.Raw.Windows.SelectObject(HDC, BitMap);
            
            /// <summary>
            /// Уничтожает изображение
            /// </summary>
            /// <param name="Bitmap">Изображение</param>
            public static void DestroyBitmap(IntPtr Bitmap){
                try{
                    if(!WL.Native.Raw.Windows.DeleteObject(Bitmap)){ throw new Exception("Произошла ошибка в DeleteObject!"); }
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при уничтожении Draw изображения [" + Bitmap + "]!", e);
                }
            }
            
            // ----------------------------------------------------------------------
            
            /// <summary>
            /// Создаёт кисть (нужно очищать!)
            /// </summary>
            /// <param name="Color">Цвет кисти (BBGGRR, AABBGGRR)</param>
            /// <param name="Width">Ширина кисти (не все типы поддерживают)</param>
            /// <param name="Type">Тип кисти</param>
            /// <returns></returns>
            public static IntPtr CreateBrush(uint Color, uint Width = 1, BrushType Type = BrushType.Solid) => WL.Native.Raw.Windows.CreatePen((int)Type, (int)Width, Color);

            /// <summary>
            /// Уничтожает кисть
            /// </summary>
            /// <param name="Brush">Кисть</param>
            public static void DestroyBrush(IntPtr Brush){
                try{
                    if(!WL.Native.Raw.Windows.DeleteObject(Brush)){ throw new Exception("Произошла ошибка в DeleteObject!"); }
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при уничтожении Draw кисти [" + Brush + "]!", e);
                }
            }

            /// <summary>
            /// Выбирает кисть
            /// </summary>
            /// <param name="HDC">Где выбрать?</param>
            /// <param name="Brush">Кисть</param>
            /// <returns>Старая кисть, или текущая</returns>
            public static IntPtr SelectBrush(IntPtr HDC, IntPtr Brush){
                if(__CurrentBrush.TryGetValue(HDC, out IntPtr CurrentBrush) && CurrentBrush == Brush){ return CurrentBrush; }
                IntPtr OldBrush = WL.Native.Raw.Windows.SelectObject(HDC, Brush);
                __CurrentBrush[HDC] = Brush;
                return OldBrush;
            }
            private static readonly Dictionary<IntPtr, IntPtr> __CurrentBrush = [];

            // ----------------------------------------------------------------------
            
            /// <summary>
            /// Рисует линию
            /// </summary>
            /// <param name="HDC">Куда рисовать?</param>
            /// <param name="Start">Начало линии</param>
            /// <param name="End">Конец линии</param>
            public static void Line(IntPtr HDC, Vector2I Start, Vector2I End){
                WL.Native.Raw.Windows.MoveToEx(HDC, Start.X, Start.Y, out WL.Native.Raw.Windows.POINT _);
                WL.Native.Raw.Windows.LineTo(HDC, End.X, End.Y);
            }

            /// <summary>
            /// Закрашивает полностью всю область
            /// </summary>
            /// <param name="HDC">Куда рисовать?</param>
            /// <param name="Rect">Область</param>
            public static void Fill(IntPtr HDC, Rect2I Rect){
                WL.Native.Raw.Windows.RECT Rect__ = new WL.Native.Raw.Windows.RECT(Rect);
                WL.Native.Raw.Windows.FillRect(HDC, ref Rect__, __CurrentBrush[HDC]);
            }
        }
    }
}