using WLO;

namespace WL{
    /// <summary>
    /// Управление с вводом
    /// </summary>
    [WLModule(int.MinValue + 4, 13)]
    public static class Input{
        public static class Mouse{
            /// <summary>
            /// Получает или устанавливает глобальную позицию мыши (Нулевая точка слева-сверху)
            /// </summary>
            public static Vector2I Position{
                get{
                    try{
                        if(!System.Native.Windows.GetCursorPos(out System.Native.Windows.POINT P)){
                            System.Native.Windows.ThrowWin32Error("Получение глобальной позиции мыши");
                        }

                        return new Vector2I(P);
                    }catch(Exception e){
                        throw new Exception("Произошла ошибка при получении позиции мыши!", e);
                    }
                }
                set{
                    try{
                        if(!System.Native.Windows.SetCursorPos(value.X, value.Y)){
                            System.Native.Windows.ThrowWin32Error("Установка глобальной позиции мыши");
                        }
                    }catch(Exception e){
                        throw new Exception("Произошла ошибка при установке позиции мыши!\nПозиция: " + value, e);
                    }
                }
            }
        }
        
        public static class Keyboard{
            /// <summary>
            /// Получает клавишу из кода
            /// </summary>
            public static Key GetKey(int Code){
                return Code switch{
                    160   => Key.Shift,
                    161   => Key.Shift,
                    162   => Key.Control,
                    163   => Key.Control,
                    164   => Key.Alt,
                    165   => Key.Alt,
                    92    => Key.Win,
                    var _ => Enum.IsDefined(typeof(Key), Code) ? (Key)Code : Key.Unknown
                };
            }
            
            /// <summary>
            /// Нажатые клавиши
            /// </summary>
            public static readonly HashSet<int> __PressedKeys = new HashSet<int>();
            
            /// <summary>
            /// Клавиша зажатая?
            /// </summary>
            public static bool KeyPressed(int Code) => __PressedKeys.Contains(Code);
            /// <summary>
            /// Клавиша зажатая?
            /// </summary>
            public static bool KeyPressed(Key Key) => KeyPressed((int)Key);
            
            /// <summary>
            /// Вызывается при нажатии клавиши [Клавиша, Код клавиши], возвращает [Блокировать следующее нажатие?]
            /// </summary>
            public static event Func<Key, int, bool>? OnDown;
            public static bool __InvokeOnDown(Key Key, int Code) => OnDown?.Invoke(Key, Code) ?? false;
        
            /// <summary>
            /// Вызывается при отжатии клавиши [Клавиша, Код клавиши]
            /// </summary>
            public static event Action<Key, int>? OnUp;
            public static void __InvokeOnUp(Key Key, int Code) => OnUp?.Invoke(Key, Code);
        }
    }

    public enum Key : int{
        Unknown = -1,
        None     = 0,
        
        Backspace = 8,
        Tab       = 9,
        Escape    = 27,
        CapsLock  = 20,
        Enter     = 13,
        Shift     = 16,
        Control   = 17,
        Alt       = 18,
        Insert    = 45,
        Home      = 36,
        Delete    = 46,
        PageUp    = 33,
        PageDown  = 34,
        End       = 35,
        
        PrintScreen = 44,
        ScrollLock  = 145,
        PauseBreak  = 19,
        
        Left  = 37,
        Right = 39,
        Up    = 38,
        Down  = 40,
        
        Win  = 91,
        Menu = 93,
        
        D0 = 48,
        D1 = 49,
        D2 = 50,
        D3 = 51,
        D4 = 52,
        D5 = 53,
        D6 = 54,
        D7 = 55,
        D8 = 56,
        D9 = 57,
        
        F1  = 112,
        F2  = 113,
        F3  = 114,
        F4  = 115,
        F5  = 116,
        F6  = 117,
        F7  = 118,
        F8  = 119,
        F9  = 120,
        F10 = 121,
        F11 = 122,
        F12 = 123,
        
        N0 = 96,
        N1 = 97,
        N2 = 98,
        N3 = 99,
        N4 = 100,
        N5 = 101,
        N6 = 102,
        N7 = 103,
        N8 = 104,
        N9 = 105,
        
        NLock     = 144,
        NSlash    = 111,
        NAsterisk = 106,
        NMinus    = 109,
        NPlus     = 107,
        
        Space = 32,
        Minus = 189,
        Plus  = 187,
        Slash = 220,
        
        MediaUp       = 175,
        MediaDown     = 174,
        MediaNext     = 176,
        MediaPrevious = 177,
        MediaPause    = 179,
        MediaPlayer   = 181,
        
        Q = 81,
        W = 87,
        E = 69,
        R = 82,
        T = 84,
        Y = 89,
        U = 85,
        I = 73,
        O = 79,
        P = 80,
        A = 65,
        S = 83,
        D = 68,
        F = 70,
        G = 71,
        H = 72,
        J = 74,
        K = 75,
        L = 76,
        Z = 90,
        X = 88,
        C = 67,
        V = 86,
        B = 66,
        N = 78,
        M = 77,
        
        BracketLeft  = 219,
        BracketRight = 221,
        Colon        = 186,
        Quotes       = 222,
        AngleLeft    = 188,
        AngleRight   = 190,
        Question     = 191,
        Tilde        = 192,
    }
}