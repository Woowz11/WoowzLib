using WLO;
using WLO.GLFW;

public static class Program{
    public static int Main(string[] Args){
        try{
            WL.WoowzLib.Start();
        
            WL.GLFW.Start();

            Window<GL> AAA = new Window<GL>();
            Window<GL> BBB = new Window<GL>();
            Window<GL> CCC = new Window<GL>();
            
            while(!AAA.ShouldDestroy || !BBB.ShouldDestroy || !CCC.ShouldDestroy){
                if(!AAA.Destroyed){
                    AAA.Title = DateTime.Now.ToString("T");
                    
                    AAA.Render.Test(1,0,0);
                    
                    AAA.FinishRender();
                }

                if(!BBB.Destroyed){
                    BBB.Title = CCC.ToString();
                    
                    BBB.Render.Test(0,0,1);
                    
                    BBB.FinishRender();
                }

                if(!CCC.Destroyed){
                    CCC.Render.Test(0,WL.Math.Random.Fast_0_1(),0);
                    
                    CCC.FinishRender();
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