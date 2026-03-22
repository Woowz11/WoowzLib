using System.Runtime.InteropServices;

namespace WL;

public static partial class System{
    public static class Memory{
        /// <summary>
        /// Размер структуры типа S в байтах
        /// </summary>
        public static uint StructSize<S>() where S : struct => (uint)Marshal.SizeOf<S>();

        // ----------------------------------------------------------------------

        /// <summary>
        /// Сохраняет строку в память (нужно освобождать!)
        /// </summary>
        /// <param name="Value">Строка</param>
        /// <returns>Ссылка</returns>
        public static IntPtr SaveString(string Value) => Marshal.StringToHGlobalUni(Value);

        /// <summary>
        /// Сохраняет функцию в память (не нужно освобождать), функция должна находится в managed-поле, что-бы GC не удалил её
        /// </summary>
        /// <param name="Delegate">Функция</param>
        /// <returns>Ссылка</returns>
        public static IntPtr SaveDelegate(Delegate Delegate) => Marshal.GetFunctionPointerForDelegate(Delegate);
        
        /// <summary>
        /// Освобождает выделенную память
        /// </summary>
        public static void Free(IntPtr Pointer){
            if(Pointer != IntPtr.Zero){ Marshal.FreeHGlobal(Pointer); }
        }
    }
}