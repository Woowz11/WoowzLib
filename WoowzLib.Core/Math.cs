using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using WLO;

namespace WL{
    /// <summary>
    /// Математические функции и т.д
    /// </summary>
    [WLModule(-10000, 18)]
    public static class Math{
        /// <summary>
        /// Ноль
        /// </summary>
        public const float Zero = 0;

        /// <summary>
        /// Один
        /// </summary>
        public const float One = 1;

        /// <summary>
        /// Минус один
        /// </summary>
        public const float NegativeOne = -1;
        
        /// <summary>
        /// Возвращается как ошибочное значение Float
        /// </summary>
        public const float ErrorFloat = float.NaN;

        /// <summary>
        /// Максимальное значение Float
        /// </summary>
        public const float MaxFloat = float.MaxValue;
        
        /// <summary>
        /// Минимальное значение Float
        /// </summary>
        public const float MinFloat = float.MinValue;

        /// <summary>
        /// Число PI (~3.14)
        /// </summary>
        public const float PI = (float)global::System.Math.PI;
        
        /// <summary>
        /// Половина числа PI (~1.57)
        /// </summary>
        public const float HalfPI = PI / 2;

        /// <summary>
        /// Два числа PI (~6.28)
        /// </summary>
        public const float TwoPI = PI * 2;

        /// <summary>
        /// Визуальные эффекты, UI (0.001)
        /// </summary>
        public const float Epsilon_VeryWeak   = 1e-3f;
        /// <summary>
        /// Игра, Рендер, Базовые физические эффекты (0.0001)
        /// </summary>
        public const float Epsilon_Weak       = 1e-4f;
        /// <summary>
        /// Мелкая анимация, Движение камеры, Частицы (0.00001)
        /// </summary>
        public const float Epsilon            = 1e-5f;
        /// <summary>
        /// Физика, Тригонометрия (0.000001)
        /// </summary>
        public const float Epsilon_Strong     = 1e-6f;
        /// <summary>
        /// Научные расчёты (0.0000001)
        /// </summary>
        public const float Epsilon_VeryStrong = 1e-7f;
        
        /// <summary>
        /// Возвращает максимальное число из 2 чисел
        /// </summary>
        public static float Max(float A, float B) => float.Max(A, B);
        /// <summary>
        /// Возвращает максимальное число из 2 чисел
        /// </summary>
        public static int MaxI(int A, int B) => int.Max(A, B);

        /// <summary>
        /// Возвращает максимальное число из нескольких чисел
        /// </summary>
        public static float Max(params float[] Values){
            if(Values.Length == 0){ return ErrorFloat; }
            float Max = Values[0];
            for(int i = 1; i < Values.Length; i++){
                Max = Math.Max(Max, Values[i]);
            }
            return Max;
        }
        
        /// <summary>
        /// Возвращает минимальное число из 2 чисел
        /// </summary>
        public static float Min(float A, float B) => float.Min(A, B);
        /// <summary>
        /// Возвращает минимальное число из 2 чисел
        /// </summary>
        public static int MinI(int A, int B) => int.Min(A, B);

        /// <summary>
        /// Возвращает минимальное число из нескольких чисел
        /// </summary>
        public static float Min(params float[] Values){
            if(Values.Length == 0){ return ErrorFloat; }
            float Min = Values[0];
            for(int i = 1; i < Values.Length; i++){
                Min = Math.Min(Min, Values[i]);
            }
            return Min;
        }

        /// <summary>
        /// Ограничить число между Min и Max
        /// </summary>
        public static float Clamp(float V, float Min, float Max) => global::System.Math.Clamp(V, Min, Max);

        /// <summary>
        /// Ограничить число между 0 и 1
        /// </summary>
        public static float Clamp01(float V) => Clamp(V, 0, 1);

        /// <summary>
        /// Возвращает число между A и B по T (0-1 (нет ограничений!))
        /// </summary>
        public static float Lerp(float A, float B, float T) => float.Lerp(A, B, T);
        /// <summary>
        /// Возвращает число между A и B по T (0-1 (нет ограничений!))
        /// </summary>
        [Obsolete("ХЗЗЗЗЗЗ, хуйня какая-то, надо подумать как правильно это сделать")]
        public static int LerpI(int A, int B, float T) => (int)(Lerp(A, B, T));
        /// <summary>
        /// Возвращает число между A и B по T (0-1 (нет ограничений!))
        /// </summary>
        public static double LerpD(double A, double B, float T) => double.Lerp(A, B, T);
        /// <summary>
        /// Возвращает число между A и B по T (0-1 (нет ограничений!))
        /// </summary>
        public static byte LerpB(byte A, byte B, float T) => (byte)LerpI(A, B, T);
        /// <summary>
        /// Возвращает число между A и B по T (0-1 (нет ограничений!))
        /// </summary>
        public static uint LerpU(uint A, uint B, float T) => (uint)LerpI((int)A, (int)B, T);

        /// <summary>
        /// Убирает отрицание
        /// </summary>
        public static float Abs(float V) => global::System.Math.Abs(V);

        /// <summary>
        /// Число положительное?
        /// </summary>
        public static bool IsPositive(float V) => V > 0;
        
        /// <summary>
        /// Число отрицательное?
        /// </summary>
        public static bool IsNegative(float V) => V < 0;

        /// <summary>
        /// Знак числа (+ или -)
        /// </summary>
        public static int Sign(float V) => IsNegative(V) ? -1 : 1;
        
        /// <summary>
        /// Добавляет число B числу A (A + B)
        /// </summary>
        public static float Add(float A, float B) => A + B;
        
        /// <summary>
        /// Вычитает число B из числа A (A - B)
        /// </summary>
        public static float Sub(float A, float B) => A - B;
        
        /// <summary>
        /// Умножает число B на число A (A * B)
        /// </summary>
        public static float Mul(float A, float B) => A * B;
        
        /// <summary>
        /// Делит число B на число A (A / B)
        /// </summary>
        public static float Div(float A, float B) => A / B;

        /// <summary>
        /// Возводит в степень число V на Exponent
        /// </summary>
        public static float Pow(float V, float Exponent){
            V        = Round(V, 2);
            Exponent = Round(Exponent, 2);
            (float, float) Key = (V, Exponent);

            return __Pow.GetOrAdd(Key, K => (float)global::System.Math.Pow(K.Item1, K.Item2));
        }
        private static readonly ConcurrentDictionary<(float, float), float> __Pow = new ConcurrentDictionary<(float, float), float>();
        
        
        /// <summary>
        /// Возводит число в квадрат
        /// </summary>
        public static float Sqr(float V) => V * V;

        /// <summary>
        /// Возводит число в куб
        /// </summary>
        public static float Cube(float V) => V * V * V;

        /// <summary>
        /// Корень N степени числа V
        /// </summary>
        public static float Root(float V, float N){
            if(IsZero(N) || IsNegative(V) && Mod(N, 2) == 0){ return ErrorFloat; } // Невозможен корень чётной степени из отрицательного числа
            return Pow(V, 1f / N);
        }

        /// <summary>
        /// Квадратный корень
        /// </summary>
        public static float Sqrt(float V) => Root(V, 2);

        /// <summary>
        /// Кубический корень
        /// </summary>
        public static float Cbrt(float V) => Root(V, 3);
        
        /// <summary>
        /// Округляет число (0.3 -> 0, 0.5 -> 1, 0.7 -> 1, -0.3 -> 0)
        /// <param name="Digits">До скольки округлять: 4 -> 0.0001</param>
        /// </summary>
        public static float Round(float V, int Digits = 0) => (float)global::System.Math.Round(V, Digits);
        
        /// <summary>
        /// Округляет число (0.3 -> 1, 0.5 -> 1, 0.7 -> 1, -0.3 -> 0)
        /// </summary>
        public static float Ceil(float V) => (float)global::System.Math.Ceiling(V);
        
        /// <summary>
        /// Округляет число (0.3 -> 0, 0.5 -> 0, 0.7 -> 0, -0.3 -> -1)
        /// </summary>
        public static float Floor(float V) => (float)global::System.Math.Floor(V);

        /// <summary>
        /// Убирает дробную часть из числа (0.3 -> 0, -2.6 -> -2, 0.99 -> 0)
        /// </summary>
        public static float Truncate(float V) => (float)global::System.Math.Truncate(V);

        /// <summary>
        /// Если есть дробное число, то делает целым и на 1 больше (0.1 -> 1, -0.1 -> -1, 0 -> 0)
        /// </summary>
        public static float Above(float V) => Ceil(Abs(V)) * Sign(V);
        
        /// <summary>
        /// Получить среднее число между двумя (Поддерживает большие числа)
        /// </summary>
        /// <returns>A + (B - A) * 0.5f</returns>
        public static float Average(float A, float B) => A + (B - A) * 0.5f;
        
        /// <summary>
        /// Синус
        /// </summary>
        public static float Sin(float Rad){
            Rad = Mod(Rad, TwoPI);
            if(IsNegative(Rad)){ Rad += TwoPI; }

            float Key = Round(Rad, 2);

            return __Sin.GetOrAdd(Key, K => (float)global::System.Math.Sin(K));
        }
        private static readonly ConcurrentDictionary<float, float> __Sin = new ConcurrentDictionary<float, float>();

        /// <summary>
        /// Косинус
        /// </summary>
        public static float Cos(float Rad) => Sin(Rad + HalfPI);

        /// <summary>
        /// Тангенс
        /// </summary>
        public static float Tan(float Rad){
            float Cos = Math.Cos(Rad);
            if(IsZero(Cos)){ return ErrorFloat; }
            return Sin(Rad) / Cos;
        }
        
        /// <summary>
        /// Котангенс
        /// </summary>
        public static float Cot(float Rad){
            float Sin = Math.Sin(Rad);
            if(IsZero(Sin)){ return ErrorFloat; }
            return Cos(Rad) / Sin;
        }

        /// <summary>
        /// Синус от 0 до 1
        /// </summary>
        public static float DSin(float Rad) => 0.5f + Sin(Rad) * 0.5f;
        
        /// <summary>
        /// Косинус от 0 до 1
        /// </summary>
        public static float DCos(float Rad) => 0.5f + Cos(Rad) * 0.5f;

        /// <summary>
        /// Число близко к нулю?
        /// </summary>
        public static bool IsZero(float V, float Epsilon) => IsNear(V, 0, Epsilon);
        
        /// <summary>
        /// Число близко к нулю?
        /// </summary>
        public static bool IsZero(float V) => IsZero(V, Epsilon_Strong);

        /// <summary>
        /// Число A близко к числу B?
        /// </summary>
        public static bool IsNear(float A, float B, float Epsilon) => Abs(A - B) < Epsilon;
        
        /// <summary>
        /// Число A близко к числу B?
        /// </summary>
        public static bool IsNear(float A, float B) => IsNear(A, B, Epsilon_Strong);

        /// <summary>
        /// Дробная часть числа (3.75 -> 0.75, -1.25 -> -0.25) [Сохраняет знак]
        public static float Frac(float V) => V - Truncate(V);

        /// <summary>
        /// Остаток от деления (A % B) [Сохраняет знак]
        /// </summary>
        public static float Mod(float A, float B) => A % B;
        
        /// <summary>
        /// Число V чётное Divisor?
        /// </summary>
        public static bool Evan(float V, int Divisor = 2) => Divisor != 0 && Mod(V, Divisor) == 0;

        /// <summary>
        /// Превратить радианы в градусы
        /// </summary>
        public static float ToDeg(float Rad) => Rad * (180f / PI);

        /// <summary>
        /// Превратить градусы в радианы
        /// </summary>
        public static float ToRad(float Deg) => Deg * (PI / 180f);

        /// <summary>
        /// Умножение с последующим сложением (A * B + C)
        /// </summary>
        public static float Fma(float A, float B, float C) => float.FusedMultiplyAdd(A, B, C);
        
        /// <summary>
        /// Для работы с байтами
        /// </summary>
        public static class Byte{
            /// <summary>
            /// Создаёт число из 4-х байтов (AA, BB, CC, DD) -> 0xAABBCCDD
            /// </summary>
            public static uint Byte4(byte B1, byte B2, byte B3, byte B4) => (uint)(B1 << 24 | B2 << 16 | B3 << 8 | B4);

            public static byte Byte4_1(uint V) => (byte)((V >> 24) & 0xFF);
            public static byte Byte4_2(uint V) => (byte)((V >> 16) & 0xFF);
            public static byte Byte4_3(uint V) => (byte)((V >> 8 ) & 0xFF);
            public static byte Byte4_4(uint V) => (byte)( V        & 0xFF);

            /// <summary>
            /// Создаёт RGBA
            /// </summary>
            public static uint RGBA(byte R, byte G, byte B, byte A) => Byte4(R, G, B, A);
                
            /// <summary>
            /// Создаёт ABGR
            /// </summary>
            public static uint ABGR(byte A, byte B, byte G, byte R) => Byte4(A, B, G, R);

            /// <summary>
            /// Превращает 0xAABBCCDD -> 0xDDCCBBAA 
            /// </summary>
            public static uint Byte4_Inverse(uint V) => Byte4(Byte4_4(V), Byte4_3(V), Byte4_2(V), Byte4_1(V));

            /// <summary>
            /// Превращает RGBA -> ABGR
            /// </summary>
            public static uint RGBA_To_ABGR(uint RGBA) => Byte.Byte4_Inverse(RGBA);

            /// <summary>
            /// Превращает ABGR -> RGBA
            /// </summary>
            public static uint ABGR_To_RGBA(uint ABGR) => Byte.Byte4_Inverse(ABGR);

            public static byte ToColorByte(byte   V) =>        V       ;
            public static byte ToColorByte(int    V) => (byte) V       ;
            public static byte ToColorByte(float  V) => (byte)(V * 255);
            public static byte ToColorByte(double V) => (byte)(V * 255);

            /// <summary>
            /// Вычисляет размер объекта в байтах
            /// </summary>
            public static int Size(object Obj) => Marshal.SizeOf(Obj);
            
            /// <summary>
            /// Вычисляет размер объекта в байтах
            /// </summary>
            public static int Size<T>() => Marshal.SizeOf<T>();
        }
        
        /// <summary>
        /// Для работы со случайными числами
        /// </summary>
        public static class Random{
            private static uint Fast_Seed = (uint)(Time.Ticks & 0xFFFFFFFF);
            
            /// <summary>
            /// Очень быстрое случайное число от 0 до 1 (Подходит для рендера, легко предугадать)
            /// </summary>
            public static float Fast_0_1(){
                Fast_Seed ^= Fast_Seed << 13;
                Fast_Seed ^= Fast_Seed >> 17;
                Fast_Seed ^= Fast_Seed << 5 ;
                return (Fast_Seed & 0xFFFFFF) / (float)0x1000000;
            }
            
            /// <summary>
            /// Очень быстрое случайное число от 0 до 1 (Подходит для рендера, легко предугадать)
            /// </summary>
            /// <param name="Seed">Сид [<c>123456789</c>]</param>
            public static float Fast_0_1(uint Seed){
                Seed ^= Seed << 13; 
                Seed ^= Seed >> 17;
                Seed ^= Seed << 5 ;
                return (Seed & 0xFFFFFF) / (float)0x1000000;
            }
            
            /// <summary>
            /// Очень быстрое случайное целое число от Min до Max (Подходит для рендера, легко предугадать)
            /// </summary>
            public static int Fast_Int(int Min, int Max){
                if(Min > Max){ (Min, Max) = (Max, Min); }

                Fast_Seed ^= Fast_Seed << 13;
                Fast_Seed ^= Fast_Seed >> 17;
                Fast_Seed ^= Fast_Seed << 5 ;
                return Min + (((int)(Fast_Seed & 0x7FFFFFFF)) % (Max - Min + 1));
            }
            
            /// <summary>
            /// Очень быстрый случайный байт от 0 до 255 (Подходит для рендера, легко предугадать)
            /// </summary>
            public static byte Fast_Byte(){
                Fast_Seed ^= Fast_Seed << 13;
                Fast_Seed ^= Fast_Seed >> 17;
                Fast_Seed ^= Fast_Seed << 5;

                return (byte)(Fast_Seed & 0xFF);
            }

            /// <summary>
            /// Очень быстро возвращает случайно true или false 50/50 (Подходит для рендера, легко предугадать)
            /// </summary>
            public static bool Fast_Bool(){ return Fast_Byte() > 127; }

            /// <summary>
            /// Очень быстро возвращает случайно true или false (Подходит для рендера, легко предугадать)
            /// </summary>
            /// <param name="TrueChance">Шанс на true (0.5 = шанс 50/50)</param>
            public static bool Fast_Bool(float TrueChance){ return Fast_0_1() < TrueChance; }
        }
        
        /// <summary>
        /// Для работы со временем
        /// </summary>
        public static class Time{
            /// <summary>
            /// Текущее время на компьютере
            /// </summary>
            public static DateTime Now => DateTime.Now;

            /// <summary>
            /// Текущее время в Великобритании (UTC, -3 по Московскому)
            /// </summary>
            public static DateTime UTC => DateTime.UtcNow;

            /// <summary>
            /// Тики времени UTC
            /// </summary>
            public static long Ticks => UTC.Ticks;

            /// <summary>
            /// Текущий год
            /// </summary>
            public static int Year => Now.Year;

            /// <summary>
            /// Текущий месяц
            /// </summary>
            public static int Month => Now.Month;

            /// <summary>
            /// Текущий день
            /// </summary>
            public static int Day => Now.Day;

            /// <summary>
            /// Текущий час
            /// </summary>
            public static int Hour => Now.Hour;

            /// <summary>
            /// Текущая минута
            /// </summary>
            public static int Minute => Now.Minute;

            /// <summary>
            /// Текущая секунда
            /// </summary>
            public static int Second => Now.Second;

            /// <summary>
            /// Текущая миллисекунда
            /// </summary>
            public static int Millisecond => Now.Millisecond;

            /// <summary>
            /// Текущий день недели
            /// </summary>
            public static DayOfWeek WeekDay => Now.DayOfWeek;

            /// <summary>
            /// Текущий день года
            /// </summary>
            public static int YearDay => Now.DayOfYear;
            
            /// <summary>
            /// Сколько прошло миллисекунд с момента запуска системы?
            /// </summary>
            public static long LifeTime => Environment.TickCount64;

            /// <summary>
            /// Сколько ТИКОВ прошло после запуска приложения
            /// </summary>
            public static long ProgramLifeTick => WL.System.Tick.ProgramLifeTick;
            
            /// <summary>
            /// Сколько миллисекунд прошло после запуска приложения
            /// </summary>
            public static double ProgramLifeTime => WL.System.Tick.ProgramLifeTime;

            /// <summary>
            /// Вычисляет разницу между двумя моментами времени
            /// </summary>
            /// <returns>Разница</returns>
            public static TimeSpan Delta(DateTime From, DateTime To) => To - From;

            /// <summary>
            /// Форматирует строку времени
            /// </summary>
            /// <param name="Time">Время</param>
            /// <param name="Format">Формат</param>
            public static string Format(DateTime Time, [StringSyntax(StringSyntaxAttribute.DateTimeFormat)] string Format){ return Time.ToString(Format); }

            /// <summary>
            /// Форматирует строку времени (от текущего времени)
            /// </summary>
            /// <param name="Format">Формат</param>
            public static string Format([StringSyntax(StringSyntaxAttribute.DateTimeFormat)] string Format){ return Time.Format(Now, Format); }
        }
    }
}