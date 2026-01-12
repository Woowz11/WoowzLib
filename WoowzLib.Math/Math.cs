namespace WL{
    [WLModule(0)]
    public static class Math{
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
        }
    }
}