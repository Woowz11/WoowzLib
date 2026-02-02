using WLO;

namespace WL{
    /// <summary>
    /// Управление с вводом
    /// </summary>
    [WLModule(-150, 1)]
    public static class Input{
        public static class Cursor{
            /// <summary>
            /// Получает или устанавливает глобальную позицию курсора мыши (Нулевая точка слева-сверху)
            /// </summary>
            public static Vector2I Position{
                get{
                    try{
                        if(!System.Native.Windows.GetCursorPos(out System.Native.Windows.POINT P)){
                            System.Native.Windows.ThrowWin32Error();
                        }

                        return new Vector2I(P);
                    }catch(Exception e){
                        throw new Exception("Произошла ошибка при получении позиции курсора!", e);
                    }
                }
                set{
                    try{
                        if(!System.Native.Windows.SetCursorPos(value.X, value.Y)){
                            System.Native.Windows.ThrowWin32Error();
                        }
                    }catch(Exception e){
                        throw new Exception("Произошла ошибка при установке позиции курсора!\nПозиция: " + value, e);
                    }
                }
            }
        }
    }
}