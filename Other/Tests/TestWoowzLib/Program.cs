using WLO;
using WLO.GLFW;
using File = WLO.File;

public static class Program{
    public static int Main(string[] Args){
        try{
            WL.WoowzLib.Start();
        
            WL.GLFW.Start();

            Window<GL> AAA = new Window<GL>(Resizable: false);
            Window<GL> BBB = new Window<GL>();
            Window<GL> CCC = new Window<GL>();

            CCC.Render.Viewport = new RectI(0, 0, 32, 32);

            Console.WriteLine(AAA.Render.Viewport);
            
            int i = 5125;
            
            Vector2I v = new Vector2I() + new Vector2I();
            
            while(!AAA.ShouldDestroy || !BBB.ShouldDestroy || !CCC.ShouldDestroy){
                if(!AAA.Destroyed){
                    AAA.Title = WL.Math.Time.Format("T");

                    AAA.Size = new Vector2U((uint)Math.Abs(800 * Math.Sin(WL.Math.Time.Ticks / 10000000.0)), 600);
                    
                    AAA.Render.BackgroundColor = new ColorF(WL.Math.Random.Fast_0_1());
                    
                    AAA.Render.Clear();
                    
                    AAA.FinishRender();
                }

                if(!BBB.Destroyed){
                    BBB.Title = CCC.ToString();
                    
                    BBB.Render.BackgroundColor = ColorF.Transparent;
                    
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