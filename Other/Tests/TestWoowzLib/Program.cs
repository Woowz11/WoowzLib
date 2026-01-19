using WL.WLO;
using Logger = WLO.Logger;

public static class Program{
    
    public static int Main(string[] Args){
        try{
            WL.WoowzLib.Start();

            Window W1 = new Window();
            
            Window W2 = new Window();

            W1.OnResize += (window, u, arg3) => {
                if(W2.Alive){
                    W2.Width = u;
                    W2.Height = arg3;
                    
                    W2.X = W1.X + (int)W1.Width + 32;
                    W2.Y = W1.Y;
                }  
            };

            W1.OnMove += (window, i, arg3) => {
                if(W2.Alive){
                    W2.X = W1.X + (int)W1.Width + 32;
                    W2.Y = W1.Y;
                }
            };
            
            while(W1.Alive || W2.Alive){
                WL.System.Tick.LimitFPS(1, 120, TD => {
                    if(W1.Alive){
                        W1.Title = WL.Math.Random.Fast_0_1().ToString();
                    }

                    if(W2.Alive){
                        W2.Title = TD.FPS.ToString();
                    }
                });
                
                WL.Window.Update();
            }
        }catch(Exception e){
            Logger.Fatal("ОШИБКА ВНУТРИ ПРИЛОЖЕНИЯ", e);
            return 1;
        }
        
        return 0;
    }
}