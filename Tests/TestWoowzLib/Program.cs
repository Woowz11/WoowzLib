 using System.Runtime.InteropServices;
 using WL;
 using WLO.GLFW;

 public static class Program{
    public static int Main(string[] Args){
        try{
            WL.WoowzLib.Start();
        
            WL.GLFW.Start();

            Window W1 = new Window();
            Window W2 = new Window(Title: "window 2");
        
            while(!W1.ShouldDestroy || !W2.ShouldDestroy){
                if(!W1.Destroyed){ W1.Title = DateTime.Now.ToString("T"); }

                WL.GLFW.Tick();
            }
            
            WL.GLFW.Stop();
        }catch(Exception e){
            throw new Exception("ОШИБКА ВНУТРИ ПРИЛОЖЕНИЯ!", e);
        }
        
        return 0;
    }
}