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

            Panel MP = new Panel(Width: 256, Height: 256);

            W1.Add(MP);
            
            Panel[] PANELS2 = new Panel[30];

            for(int i = 0; i < PANELS2.Length; i++){
                Panel P = new Panel();
                P.Color = ColorF.Random;
                MP.Add(P);
                PANELS2[i] = P;
            }
            
            Panel[] PANELS = new Panel[1000];

            for(int i = 0; i < PANELS.Length; i++){
                Panel P = new Panel();
                P.Color = ColorF.Random;
                W1.Add(P);
                PANELS[i] = P;
            }
            
            double d = 2;
            while(W1.Alive){
                WL.System.Tick.LimitFPS(1, 300, TD => {
                    if(W1.Alive){
                        W1.BackgroundColor = ColorF.Random;
                        
                        d += TD.DeltaTimeS;
                        if(d > 0.5f){ W1.Title = TD.FPS.ToString(); d = 0; }

                        for(int i = 0; i < PANELS.Length; i++){
                            Panel P = PANELS[i];
                            
                            P.X = ((int)(W1.Width/2) + (int)(Math.Sin(TD.DeltaTick + ((float)i/PANELS.Length) * Math.PI * 2) * W1.Width/2)) - (int)(P.Width/2);
                            P.Y = ((int)(W1.Height/2) + (int)(-Math.Cos(TD.DeltaTick + ((float)i/PANELS.Length) * Math.PI * 2) * W1.Height/2)) - (int)(P.Height/2);
                        }

                        MP.X = (int)(W1.Width / 2) - (int)(MP.Width/2);
                        MP.Y = (int)(W1.Height / 2) - (int)(MP.Height/2);
                        
                        for(int i = 0; i < PANELS2.Length; i++){
                            Panel P = PANELS2[i];
                            
                            P.X = MP.X + ((int)(MP.Width/2) + (int)(Math.Sin(TD.DeltaTick + ((float)i/PANELS2.Length) * Math.PI * 2) * MP.Width/2)) - (int)(P.Width/2);
                            P.Y = MP.Y + ((int)(MP.Height/2) + (int)(-Math.Cos(TD.DeltaTick + ((float)i/PANELS2.Length) * Math.PI * 2) * MP.Height/2)) - (int)(P.Height/2);
                        }

                        W1.Render();
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