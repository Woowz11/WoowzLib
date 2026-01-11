 using System.Runtime.InteropServices;
 using WL;

 public static class Program{
    public static int Main(string[] Args){
        WL.WoowzLib.Start();
        
        WL.GLFW.Start();

        IntPtr titleutf8 = Marshal.StringToHGlobalAnsi("WoowzLib Test GLFW Window Legenda");
        
        IntPtr w = WL.GLFW.Native.glfwCreateWindow(800, 600, titleutf8, IntPtr.Zero, IntPtr.Zero);
        
        Marshal.FreeHGlobal(titleutf8);

        WL.GLFW.Native.glfwMakeContextCurrent(w);
        WL.GLFW.Native.glfwShowWindow(w);

        while(WL.GLFW.Native.glfwWindowShouldClose(w) == 0){
            WL.GLFW.Native.glfwPollEvents();
        }
        
        WL.GLFW.Stop();
        
        return 0;
    }
}