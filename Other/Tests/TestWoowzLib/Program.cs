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

            Vector3F vec = new Vector3F(0, 10);

            vec++;
            
            Console.WriteLine(vec + new Vector3F(5));
            
            while(!AAA.ShouldDestroy || !BBB.ShouldDestroy || !CCC.ShouldDestroy){
                if(!AAA.Destroyed){
                    AAA.Title = DateTime.Now.ToString("T");

                    AAA.Render.BackgroundColor = ColorF.Red;

                    AAA.Render.Clear();
                    
                    AAA.FinishRender();
                }

                if(!BBB.Destroyed){
                    BBB.Title = CCC.ToString();
                    
                    BBB.Render.BackgroundColor = ColorF.Green;
                    
                    BBB.Render.Clear();
                    
                    BBB.FinishRender();
                }

                if(!CCC.Destroyed){
                    CCC.Render.Clear();
                    
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