namespace WL;

public static partial class System{
    public static class Time{
        static Time(){
            WL.Native.Raw.Windows.QueryPerformanceFrequency(out Frequency);
        }
        private static readonly long Frequency;

        /// <summary>
        /// Время с запуска приложения в секундах с высокой точностью
        /// </summary>
        public static double HighLifeS{
            get{
                WL.Native.Raw.Windows.QueryPerformanceCounter(out long Count);
                return (double)Count / Frequency;
            }
        }

        /// <summary>
        /// Время с запуска приложения в миллисекундах с высокой точностью
        /// </summary>
        public static long HighLifeMS => (long)(HighLifeS * 1000);
        
        /// <summary>
        /// Сколько по времени запущен компьютер в миллисекундах
        /// </summary>
        public static long ComputerLifeMS => Environment.TickCount64;
    }
}