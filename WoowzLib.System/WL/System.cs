using System.Runtime.InteropServices;

namespace WL{
    public static partial class System{
        /// <summary>
        /// Последняя ошибка Windows
        /// </summary>
        public static int LastOSError() => Marshal.GetLastWin32Error();
    }
}