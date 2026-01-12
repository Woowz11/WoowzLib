namespace WL{
    [WLModule(35)]
    public static class GL{
        static GL(){
            DLL = WL.Native.LoadSystem("opengl32.dll");
        }
        
        // <summary>
        /// Текущий opengl32.dll
        /// </summary>
        private static IntPtr DLL;
    }
}