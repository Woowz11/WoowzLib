 using System.Runtime.InteropServices;
 using WL;
 using WLO.GLFW;

 public static class Program{
    public static int Main(string[] Args){
        try{
            WL.WoowzLib.Start();
        
            WL.GLFW.Start();

            Window W = new Window();
        
            while(!W.ShouldDestroy){
                WL.GLFW.Native.glfwPollEvents();
            
                W.Title = DateTime.Now.ToString("T");
            }
            
            WL.GLFW.Stop();
        }catch(Exception e){
            throw new Exception("ОШИБКА ВНУТРИ ПРИЛОЖЕНИЯ!", e);
        }
        
        return 0;
    }
}