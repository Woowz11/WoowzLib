namespace WL;

public static partial class System{
    public static partial class Native{
        /// <summary>
        /// Возвращает младшие 16 бит (LOWORD) из ссылки
        /// </summary>
        /// <param name="Pointer">Ссылка</param>
        public static int LoWord(IntPtr Pointer) => (short)((long)Pointer & 0xFFFF);
        /// <summary>
        /// Возвращает старшие 16 бит (HIWORD) из ссылки
        /// </summary>
        /// <param name="Pointer">Ссылка</param>
        public static int HiWord(IntPtr Pointer) => (short)(((long)Pointer >> 16) & 0xFFFF);
    }
}