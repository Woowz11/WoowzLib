 using System.Runtime.InteropServices;
 using WL;
 using WLO.GLFW;

 public static class Program{
    public static int Main(string[] Args){
        WL.WoowzLib.Start();
        
        WL.GLFW.Start();

        Window W = new Window();

        WL.GLFW.Native.glfwMakeContextCurrent(W.Hanlde);
        WL.GLFW.Native.glfwShowWindow(W.Hanlde);

        while(WL.GLFW.Native.glfwWindowShouldClose(W.Hanlde) == 0){
            WL.GLFW.Native.glfwPollEvents();

            W.Title = DateTime.Now.ToString("T");
        }
        
        WL.GLFW.Stop();
        
        return 0;
    }
}