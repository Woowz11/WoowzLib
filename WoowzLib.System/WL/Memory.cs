using System.Diagnostics.CodeAnalysis;
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
        /// Получает строку из памяти
        /// </summary>
        /// <param name="Pointer">Ссылка</param>
        /// <returns>Строка</returns>
        public static string? LoadString(IntPtr Pointer) => Marshal.PtrToStringUni(Pointer);

        /// <summary>
        /// Сохраняет функцию в память (не нужно освобождать), функция должна находится в managed-поле, что-бы GC не удалил её
        /// </summary>
        /// <param name="Delegate">Функция</param>
        /// <returns>Ссылка</returns>
        public static IntPtr SaveDelegate(Delegate Delegate) => Marshal.GetFunctionPointerForDelegate(Delegate);

        /// <summary>
        /// Записывает Struct в память
        /// </summary>
        /// <param name="Pointer">Ссылка</param>
        /// <param name="Struct">Struct</param>
        /// <typeparam name="S">Тип Struct</typeparam>
        public static void SetStruct<S>(IntPtr Pointer, [DisallowNull] S Struct) => Marshal.StructureToPtr(Struct, Pointer, true);
        
        /// <summary>
        /// Получает Struct из памяти
        /// </summary>
        /// <param name="Pointer">Ссылка</param>
        /// <typeparam name="S">Тип Struct</typeparam>
        /// <returns>Struct</returns>
        public static S? LoadStruct<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)]S>(IntPtr Pointer) => Marshal.PtrToStructure<S>(Pointer);
        
        /// <summary>
        /// Освобождает выделенную память
        /// </summary>
        public static void Free(IntPtr Pointer){
            if(Pointer != IntPtr.Zero){ Marshal.FreeHGlobal(Pointer); }
        }
    }
}