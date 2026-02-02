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
            
            Panel P = new Panel(Width: W1.Width, Height: 512, Color: ColorF.Black);

            W1.OnResize += (window, u, arg3) => {
                P.Width = u;
            };
            
            W1.Add(P);

            Panel P3 = new Panel(Width: 512, Height: 512);
            P3.Anchor_X = ElementAnchorX.Center;
            P3.Anchor_Y = ElementAnchorY.Center;
            
            P.Add(P3);

            Panel P4 = new Panel(Color: ColorF.Red);
            Panel P5 = new Panel(Color: ColorF.Blue);
            
            P3.Add(P4).Add(P5);

            P4.Anchor_X = ElementAnchorX.Left;
            P5.Anchor_X = ElementAnchorX.Right;
            
            Panel P6 = new Panel(Color: ColorF.Green);
            Panel P7 = new Panel(Color: ColorF.Yellow);
            
            P3.Add(P6).Add(P7);

            P6.Anchor_Y = ElementAnchorY.Bottom;
            P7.Anchor_Y = ElementAnchorY.Bottom;
            
            P6.Anchor_X = ElementAnchorX.Left;
            P7.Anchor_X = ElementAnchorX.Right;
            
            Panel P8 = new Panel(Color: ColorF.Magenta);
            Panel P9 = new Panel(Color: ColorF.Orange);
            
            P3.Add(P8).Add(P9);

            P8.Anchor_Y = ElementAnchorY.Top;
            P9.Anchor_Y = ElementAnchorY.Bottom;
            
            P8.Anchor_X = ElementAnchorX.Center;
            P9.Anchor_X = ElementAnchorX.Center;
            
            Panel P10 = new Panel(Color: ColorF.Pink);
            Panel P11 = new Panel(Color: ColorF.DarkYellow);
            
            P3.Add(P10).Add(P11);

            P10.Anchor_Y = ElementAnchorY.Center;
            P11.Anchor_Y = ElementAnchorY.Center;
            
            P10.Anchor_X = ElementAnchorX.Left;
            P11.Anchor_X = ElementAnchorX.Right;
            
            Panel P12 = new Panel(Color: ColorF.DarkRed);
            
            P3.Add(P12);

            P12.Anchor_X = ElementAnchorX.Center;
            P12.Anchor_Y = ElementAnchorY.Center;
            
            W1.BackgroundColor = ColorF.Red;
            
            double d = 2;
            string FPS = "";
            while(W1.Alive){
                WL.System.Tick.LimitFPS(1, 300, TD => {
                    if(W1.Alive){
                        d += TD.DeltaTimeS;
                        if(d > 0.5f){ FPS = TD.FPS.ToString(); d = 0; }

                        W1.Title = FPS + " | " + W1.CursorInside;

                        P.X = (int)((0.5f + Math.Sin(TD.DeltaTick) / 2) * 256);
                        
                        P3.Width  = (uint)((0.75f + Math.Sin(TD.DeltaTick * 2) / 4) * 512);
                        P3.Height = (uint)((0.75f + Math.Sin(TD.DeltaTick * 2) / 4) * 512);
                        
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