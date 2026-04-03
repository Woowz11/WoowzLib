using System.Runtime.CompilerServices;
using WL;
using WLO;
using WLO.Attribute;
using WLO.Color;
using WLO.Rect;
using WLO.Vector;

namespace WLO{
    /// <summary>
    /// Тип кисти при рисовании контура
    /// </summary>
    public enum BrushContourType : int{
        /// <summary>
        /// Сплошная линия
        /// </summary>
        Solid = 0,
        /// <summary>
        /// Пунктирная линяя
        /// </summary>
        Dash = 1,
        /// <summary>
        /// Точечная линия
        /// </summary>
        Dot = 2,
        /// <summary>
        /// Пунктирно-точечная линия
        /// </summary>
        DashDot = 3,
        /// <summary>
        /// Пунктирно-точечная-точечная линия
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

    /// <summary>
    /// Тип кисти при заливке фона
    /// </summary>
    public enum BrushFillType{
        /// <summary>
        /// Сплошной цвет
        /// </summary>
        Solid
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

    public interface IBrush{
        uint __Color{ get; }
    }
    
    public readonly struct BrushContour : IBrush, IEquatable<BrushContour>{
        public BrushContour(Color4B Color, uint Width = 1, BrushContourType Type = BrushContourType.Solid){
            this.Color = Color;
            this.Width = Width;
            this.Type  = Type;
            
            __Color = Color.AiBGR;
        }

        public readonly Color4B          Color;
        public readonly uint             Width;
        public readonly BrushContourType Type;

        public uint __Color{ get; }
        
        // ----------------------------------------------------------------------
        
        public override string ToString() => "BrushContour(" + Color + ", " + Width + ", " + Type + ")";
	
        public bool Equals(BrushContour Other) => __Color == Other.__Color && Width == Other.Width && Type == Other.Type;
        public override bool Equals(object? Object) => Object is BrushContour Other && Equals(Other);
	
        public override int GetHashCode() => HashCode.Combine(__Color, Width, Type);
	
        // ----------------------------------------------------------------------
	
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(BrushContour L, BrushContour R) => L.Equals(R);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(BrushContour L, BrushContour R) => !L.Equals(R);
    }

    public readonly struct BrushFill : IBrush, IEquatable<BrushFill>{
        public BrushFill(Color4B Color, BrushFillType Type = BrushFillType.Solid){
            this.Color = Color;
            this.Type  = Type;

            __Color = Color.AiBGR;
        }

        public readonly Color4B       Color;
        public readonly BrushFillType Type;
        
        public uint __Color{ get; }
        
        // ----------------------------------------------------------------------
        
        public override string ToString() => "BrushFill(" + Color + ", " + Type + ")";
	
        public bool Equals(BrushFill Other) => __Color == Other.__Color && Type == Other.Type;
        public override bool Equals(object? Object) => Object is BrushContour Other && Equals(Other);
	
        public override int GetHashCode() => HashCode.Combine(__Color, Type);
	
        // ----------------------------------------------------------------------
	
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(BrushFill L, BrushFill R) => L.Equals(R);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(BrushFill L, BrushFill R) => !L.Equals(R);
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
                    if(!WL.Native.Raw.Windows.DeleteDC(HDC)){ throw new Exception("Произошла ошибка в DeleteObject!\nОшибка: " + WL.System.LastOSError()); }
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
                        throw new Exception("Произошла ошибка в BitBlt!\nОшибка: " + WL.System.LastOSError());
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
            public static IntPtr CreateMemoryBitmap(IntPtr HDC, Vector2UI Size) => WL.Native.Raw.Windows.CreateCompatibleBitmap(HDC, (int)Size.W, (int)Size.H);

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
            /// Получает старую контурную кисть (если разные, то удаляет и создаёт новую) или создаёт новую
            /// </summary>
            /// <param name="Info">Информация об кисти</param>
            /// <param name="HDC">Если указан, то автоматически выбрать кисть в нём</param>
            public static IntPtr CreateBrushContour(BrushContour Info, IntPtr? HDC = null){
                try{
                    if(__CurrentBrushContour.HasValue){
                        if(__CurrentBrushContour.Value.Info == Info){
                            if(HDC != null){ WL.Native.Raw.Windows.SelectObject(HDC.Value, __CurrentBrushContour.Value.Brush); }
                            return __CurrentBrushContour.Value.Brush;
                        }
                        
                        WL.Native.Raw.Windows.DeleteObject(__CurrentBrushContour.Value.Brush);
                    }

                    __CurrentBrushContour = (Info, WL.Native.Raw.Windows.CreatePen((int)Info.Type, (int)Info.Width, Info.__Color));
                    
                    if(HDC != null){ WL.Native.Raw.Windows.SelectObject(HDC.Value, __CurrentBrushContour.Value.Brush); }

                    return __CurrentBrushContour.Value.Brush;
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при создании контурной кисти в Draw!\nИнформация: " + Info + "\nHDC: " + WL.String.ToString(HDC), e);
                }
            }
            private static (BrushContour Info, IntPtr Brush)?  __CurrentBrushContour;
            
            /// <summary>
            /// Получает старую заполняющую кисть (если разные, то удаляет и создаёт новую) или создаёт новую
            /// </summary>
            /// <param name="Info">Информация об кисти</param>
            /// <param name="HDC">Если указан, то автоматически выбрать кисть в нём</param>
            [WoowzLibHint(Information.WorkInProgress, "не указаны другие типы пока-что")]
            public static IntPtr CreateBrushFill(BrushFill Info, IntPtr? HDC = null){
                try{
                    if(__CurrentBrushFill.HasValue){
                        if(__CurrentBrushFill.Value.Info == Info){
                            if(HDC != null){ WL.Native.Raw.Windows.SelectObject(HDC.Value, __CurrentBrushFill.Value.Brush); }
                            return __CurrentBrushFill.Value.Brush;
                        }
                        
                        WL.Native.Raw.Windows.DeleteObject(__CurrentBrushFill.Value.Brush);
                    }

                    __CurrentBrushFill = (Info, WL.Native.Raw.Windows.CreateSolidBrush(Info.__Color));
                    
                    if(HDC != null){ WL.Native.Raw.Windows.SelectObject(HDC.Value, __CurrentBrushFill.Value.Brush); }

                    return __CurrentBrushFill.Value.Brush;
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при создании заполняющей кисти в Draw!\nИнформация: " + Info + "\nHDC: " + WL.String.ToString(HDC), e);
                }
            }
            private static (BrushFill Info, IntPtr Brush)? __CurrentBrushFill;
            
            /// <summary>
            /// Получает старую контурную/заполняющую кисть (если разные, то удаляет и создаёт новую) или создаёт новую
            /// </summary>
            /// <param name="ContourInfo">Информация об контурной кисти</param>
            /// <param name="FillInfo">Информация об заполняющей кисти</param>
            /// <param name="HDC">Если указан, то автоматически выбрать кисть в нём</param>
            public static (IntPtr Contour, IntPtr Fill) CreateBrush(BrushContour ContourInfo, BrushFill FillInfo, IntPtr? HDC = null) => (CreateBrushContour(ContourInfo, HDC), CreateBrushFill(FillInfo, HDC));
            
            // ----------------------------------------------------------------------

            /// <summary>
            /// Закрашивает область (быстро)
            /// </summary>
            /// <param name="HDC">Куда рисовать?</param>
            /// <param name="Rect">Область</param>
            public static void Fill(IntPtr HDC, Rect2I Rect, BrushFill Fill){
                WL.Native.Raw.Windows.RECT Rect__ = new WL.Native.Raw.Windows.RECT(Rect);
                WL.Native.Raw.Windows.FillRect(HDC, ref Rect__, CreateBrushFill(Fill));
            }
            
            /// <summary>
            /// Рисует линию
            /// </summary>
            /// <param name="HDC">Куда рисовать?</param>
            /// <param name="Start">Начало линии</param>
            /// <param name="End">Конец линии</param>
            public static void Line(IntPtr HDC, Vector2I Start, Vector2I End, BrushContour Contour){
                CreateBrushContour(Contour, HDC);
                WL.Native.Raw.Windows.MoveToEx(HDC, Start.X, Start.Y, out WL.Native.Raw.Windows.POINT _);
                WL.Native.Raw.Windows.LineTo(HDC, End.X, End.Y);
            }

            /// <summary>
            /// Рисует прямоугольник
            /// </summary>
            /// <param name="HDC">Куда рисовать?</param>
            /// <param name="Rect">Прямоугольник</param>
            public static void Rectangle(IntPtr HDC, Rect2I Rect, BrushContour Contour, BrushFill Fill){
                CreateBrush(Contour, Fill, HDC);
                WL.Native.Raw.Windows.Rectangle(HDC, Rect.Left, Rect.Bottom, Rect.Right, Rect.Top);
            }

            /// <summary>
            /// Рисует круг
            /// </summary>
            /// <param name="HDC">Куда рисовать?</param>
            /// <param name="Ellipse">Круг</param>
            public static void Ellipse(IntPtr HDC, Rect2I Ellipse, BrushContour Contour, BrushFill Fill){
                CreateBrush(Contour, Fill, HDC);
                WL.Native.Raw.Windows.Ellipse(HDC, Ellipse.Left, Ellipse.Bottom, Ellipse.Right, Ellipse.Top);
            }

            /// <summary>
            /// Рисует пиксель
            /// </summary>
            /// <param name="HDC">Куда рисовать?</param>
            /// <param name="Position">Позиция</param>
            public static void Pixel(IntPtr HDC, Vector2I Position, IBrush Brush) => WL.Native.Raw.Windows.SetPixel(HDC, Position.X, Position.Y, Brush.__Color);

            /// <summary>
            /// Рисует полигон
            /// </summary>
            /// <param name="HDC">Куда рисовать?</param>
            /// <param name="Points">Точки</param>
            public static void Polygon(IntPtr HDC, Vector2I[] Points, BrushContour Contour, BrushFill Fill){
                CreateBrush(Contour, Fill, HDC);
                
                WL.Native.Raw.Windows.POINT[] Points__ = new WL.Native.Raw.Windows.POINT[Points.Length];
                for(int i = 0; i < Points.Length; i++){ Points__[i] = new WL.Native.Raw.Windows.POINT(Points[i]); }
                WL.Native.Raw.Windows.Polygon(HDC, Points__, Points__.Length);
            }

            /// <summary>
            /// Рисует линию (по указанным точкам)
            /// </summary>
            /// <param name="HDC">КУда рисовать?</param>
            /// <param name="Points">Точки</param>
            public static void Line(IntPtr HDC, Vector2I[] Points, BrushContour Contour){
                CreateBrushContour(Contour, HDC);
                
                WL.Native.Raw.Windows.POINT[] Points__ = new WL.Native.Raw.Windows.POINT[Points.Length];
                for(int i = 0; i < Points.Length; i++){ Points__[i] = new WL.Native.Raw.Windows.POINT(Points[i]); }
                WL.Native.Raw.Windows.Polyline(HDC, Points__, Points__.Length);
            }

            /// <summary>
            /// Рисует треугольник
            /// </summary>
            /// <param name="HDC">Куда рисовать?</param>
            /// <param name="A">Точка 1</param>
            /// <param name="B">Точка 2</param>
            /// <param name="C">Точка 3</param>
            public static void Triangle(IntPtr HDC, Vector2I A, Vector2I B, Vector2I C, BrushContour Contour, BrushFill Fill) => Polygon(HDC, [A, B, C], Contour, Fill);
            
            /// <summary>
            /// Рисует параллелограмм
            /// </summary>
            /// <param name="HDC">Куда рисовать?</param>
            /// <param name="A">Точка 1</param>
            /// <param name="B">Точка 2</param>
            /// <param name="C">Точка 3</param>
            /// <param name="D">Точка 4</param>
            public static void Parallelogram(IntPtr HDC, Vector2I A, Vector2I B, Vector2I C, Vector2I D, BrushContour Contour, BrushFill Fill) => Polygon(HDC, [A, B, C, D], Contour, Fill);
        }
    }
}