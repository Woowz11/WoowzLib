using WLO;
using WLO.Render;
//using WLO.GL;
using WLO.GLFW;

public static class Program{
    public static int Main(string[] Args){
        try{
            WL.WoowzLib.Start();
        
            WL.GLFW.Start();

            Window<EmptyRender> AAA = new Window<EmptyRender>();
            
            while(!AAA.ShouldDestroy){
                if(!AAA.Destroyed){
                    //AAA.Title = WL.Math.Time.Format("T");
                    
                    //AAA.Render.BackgroundColor = ColorF.Red;
                    
                    //AAA.Render.Clear();
                    
                    AAA.FinishRender();
                }

                WL.GLFW.Tick();
            }
            
            WL.GLFW.Stop();
        }catch(Exception e){
            throw new Exception("ОШИБКА ВНУТРИ ПРИЛОЖЕНИЯ!", e);
        }
        
        return 0;
    }
}