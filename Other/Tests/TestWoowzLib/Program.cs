using WL;
using WLO;
using WLO.GLFW;
using WLO.Render;
using WLO.GL;
using WLO.GLFW;
using GL = WLO.Render.GL;
using Logger = WLO.Logger;

public static class Program{
    
    public static int Main(string[] Args){
        try{
            WL.WoowzLib.Start();

            WL.GLFW.Start();
            
            Window W = new Window();

            GL G = new GL();
            
            while(!W.ShouldDestroy){
                G.Context(W.Drawable, () => {
                    G.BackgroundColor = ColorF.Random;
                    G.Clear();

                    W.StopRender();
                });
                
                WL.GLFW.Tick();
            }
            
            WL.GLFW.Stop();

        }catch(Exception e){
            Logger.Fatal("ОШИБКА ВНУТРИ ПРИЛОЖЕНИЯ", e);
            return 1;
        }
        
        return 0;
    }
}