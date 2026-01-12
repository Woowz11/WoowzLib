using System.Runtime.InteropServices;

namespace WL{
    [WLModule(35)]
    public static class GL{
        static GL(){
            DLL = WL.Native.LoadSystem("opengl32.dll");
            
            Native.glClear      = WL.Native.DelegateFunction<Native.D_glClear     >("glClear"      ,DLL);
            Native.glClearColor = WL.Native.DelegateFunction<Native.D_glClearColor>("glClearColor" ,DLL);
        }
        
        // <summary>
        /// Текущий opengl32.dll
        /// </summary>
        private static IntPtr DLL;

        public static class Native{
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glClearColor(float r, float g, float b, float a);
            public static D_glClearColor glClearColor = null!;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glClear(uint mask);
            public static D_glClear glClear = null!;

            public const uint GL_COLOR_BUFFER_BIT   = 0x00004000;
            public const uint GL_DEPTH_BUFFER_BIT   = 0x00000100;
            public const uint GL_STENCIL_BUFFER_BIT = 0x00000400;
        }
    }
}