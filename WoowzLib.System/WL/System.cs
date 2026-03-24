using System.Runtime.InteropServices;

namespace WL{
    public static partial class System{
        static System(){
            Instance = WL.Native.Raw.Windows.GetModuleHandle(null);
        }
        
        /// <summary>
        /// Последняя ошибка Windows
        /// </summary>
        public static int LastOSError() => Marshal.GetLastWin32Error();
        
        /// <summary>
        /// Дескриптор текущего исполняемого модуля
        /// </summary>
        public static readonly IntPtr Instance;
    }
}