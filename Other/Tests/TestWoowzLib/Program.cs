using System.Drawing;
using System.Globalization;
using WL.WLO;
using WLO;
using Logger = WLO.Logger;

public static class Program{
    
    public static int Main(string[] Args){
        try{
            WL.Render.Debug.LogMain = true;
            
            WL.WoowzLib.Start(new WoowzLibInfo(
                Name  : "Test WoowzLib",
                Author: "Woowz11"
            ));
            
            Window W1 = new Window();
            Window W2 = new Window();

            Panel P = new Panel(Color: ColorF.Gray);
            W1.Add(P);

            P.Anchor_X = 0;
            P.Anchor_Y = 0;
            
            P.Anchor_Width  = 0.9f;
            P.Anchor_Height = 0.9f;

            RenderPanel RP = new RenderPanel();
            P.Add(RP);
            
            RP.Anchor_X = 0;
            RP.Anchor_Y = 0;
            
            RP.Anchor_Width  = 0.9f;
            RP.Anchor_Height = 0.9f;
            
            double d = 2;
            string FPS = "";
            bool dodo = false;
            while(W1.Alive || W2.Alive){
                WL.System.Tick.LimitFPS(1, 300, TD => {
                    if(W1.Alive){
                        d += TD.DeltaTimeS;
                        if(d > 0.5f){
                            FPS = TD.FPS.ToString(); d = 0;
                            
                            if(W1.Alive && W2.Alive){
                                dodo = !dodo;

                                Logger.Info(dodo ? W2 : W1);
                                P.ToWindow(dodo ? W2 : W1);
                            }
                        }

                        W1.Title = FPS + " | " + W1.CursorInside;
                        
                        W1.Render();
                    }
                    if(W2.Alive){
                        
                        W2.Render();
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