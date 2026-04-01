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
}

namespace WL{
    public static partial class System{
        public static class Draw{
            /// <summary>
            /// Создаёт кисть (нужно очищать!)
            /// </summary>
            /// <param name="Color">Цвет кисти (BBGGRR, AABBGGRR)</param>
            /// <param name="Type">Тип кисти</param>
            /// <param name="Width">Ширина кисти (не все типы поддерживают)</param>
            /// <returns></returns>
            public static IntPtr CreateBrush(uint Color, BrushType Type = BrushType.Solid, uint Width = 1) => WL.Native.Raw.Windows.CreatePen((int)Type, (int)Width, Color);

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
            public static void SelectBrush(IntPtr HDC, IntPtr Brush){
                if(__CurrentBrush.TryGetValue(HDC, out IntPtr CurrentBrush) && CurrentBrush == Brush){ return; }
                WL.Native.Raw.Windows.SelectObject(HDC, Brush);
                __CurrentBrush[HDC] = Brush;
            }
            private static Dictionary<IntPtr, IntPtr> __CurrentBrush = [];

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