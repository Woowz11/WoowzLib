namespace WL{
    /// <summary>
    /// Математические функции и т.д
    /// </summary>
    [WLModule(0)]
    public static class Math{
        /// <summary>
        /// Для работы со временем
        /// </summary>
        public static class Time{
            
        }
        
        /// <summary>
        /// Для работы со случайными числами
        /// </summary>
        public static class Random{
            private static uint Fast_Seed = (uint)(DateTime.Now.Ticks & 0xFFFFFFFF);
            
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