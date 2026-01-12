using WLO;
using WLO.GLFW;

public static class Program{
    public static int Main(string[] Args){
        try{
            WL.WoowzLib.Start();
        
            WL.GLFW.Start();

            Window<GL> W1 = new Window<GL>();

            WL.GLFW.Native.glfwMakeContextCurrent(W1.Handle);
            
            while(!W1.ShouldDestroy){
                W1.Title = DateTime.Now.ToString("T");
                
                UPD(W1);
                
                WL.GLFW.Tick();
            }
            
            WL.GLFW.Stop();
        }catch(Exception e){
            throw new Exception("ОШИБКА ВНУТРИ ПРИЛОЖЕНИЯ!", e);
        }
        
        return 0;
    }

    private static void UPD(Window<GL> W1){
        W1.Render.Test();
    }
}