using System.Diagnostics.CodeAnalysis;

namespace WL{
    /// <summary>
    /// Математические функции и т.д
    /// </summary>
    [WLModule(-10000, 5)]
    public static class Math{
        /// <summary>
        /// Получить среднее число между двумя (Поддерживает большие числа)
        /// </summary>
        /// <returns>A + (B - A) * 0.5f</returns>
        public static float Average(float A, float B){ return A + (B - A) * 0.5f; }

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
            public static long ProgramLifeTick => WL.WoowzLib.Tick.ProgramLifeTick;
            
            /// <summary>
            /// Сколько миллисекунд прошло после запуска приложения
            /// </summary>
            public static double ProgramLifeTime => WL.WoowzLib.Tick.ProgramLifeTime;

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
        
        /// <summary>
        /// Для работы со случайными числами
        /// </summary>
        public static class Random{
            private static uint Fast_Seed = (uint)(Time.Ticks & 0xFFFFFFFF);
            
            /// <summary>
            /// Очень быстрое случайное число от 0 до 1 (Подходит для рендера, легко предугадать)
            /// </summary>
            /// <returns></returns>
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
            /// <returns></returns>
            public static float Fast_0_1(uint Seed){
                Seed ^= Seed << 13; 
                Seed ^= Seed >> 17;
                Seed ^= Seed << 5 ;
                return (Seed & 0xFFFFFF) / (float)0x1000000;
            }
        }
    }
}