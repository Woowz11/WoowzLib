using WLO;
using WLO.GLFW;

public static class Program{
    public static int Main(string[] Args){
        try{
            WL.WoowzLib.Start();
        
            WL.GLFW.Start();

            Window<GL> W1 = new Window<GL>();
            Window<GL> W2 = new Window<GL>();
            Window<GL> W3 = new Window<GL>(Render: W1.Render);
            
            while(!W1.ShouldDestroy || !W2.ShouldDestroy || !W3.ShouldDestroy){
                if(!W1.Destroyed){
                    W1.Title = DateTime.Now.ToString("T");
                    
                    W1.Render.Test(1,0,0);
                    
                    W1.FinishRender();
                }

                if(!W2.Destroyed){
                    W2.Render.Test(0,0,1);
                    
                    W2.FinishRender();
                }

                if(!W3.Destroyed){
                    W3.FinishRender();
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