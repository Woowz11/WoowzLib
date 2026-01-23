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
            
            W1.BackgroundColor = ColorF.Black;
            
            double d = 2;
            while(W1.Alive){
                WL.System.Tick.LimitFPS(1, 300, TD => {
                    if(W1.Alive){
                        d += TD.DeltaTimeS;
                        if(d > 0.5f){ W1.Title = TD.FPS.ToString(); d = 0; }

                        W1.Render(W1.BackgroundColor, true, null, HDC => {
                            const int Size = 8;

                            const int t = 100;
                            
                            for(int i = 0; i < 10000; i++){
                                WL.System.HDC.Fill(HDC, (i - (int)Math.Floor((double)i / t) * t) * Size, (int)Math.Floor((double)i / t) * Size, Size, Size, ColorF.Random.ToRGBiA());
                            }
                        });
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