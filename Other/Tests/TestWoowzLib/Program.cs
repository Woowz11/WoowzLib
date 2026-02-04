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
            P3.Anchor_X = 0;
            P3.Anchor_Y = 0;
            
            P.Add(P3);

            Panel P4 = new Panel(Color: ColorF.Red);
            Panel P5 = new Panel(Color: ColorF.Blue);
            
            P3.Add(P4).Add(P5);

            P4.Anchor_X = -1;
            P5.Anchor_X = 1;
            
            Panel P6 = new Panel(Color: ColorF.Green);
            Panel P7 = new Panel(Color: ColorF.Yellow);
            
            P3.Add(P6).Add(P7);

            P6.Anchor_Y = 1;
            P7.Anchor_Y = 1;
            
            P6.Anchor_X = -1;
            P7.Anchor_X = 1;
            
            Panel P8 = new Panel(Color: ColorF.Magenta);
            Panel P9 = new Panel(Color: ColorF.Orange);
            
            P3.Add(P8).Add(P9);

            P8.Anchor_Y = -1;
            P9.Anchor_Y = 1;
            
            P8.Anchor_X = 0;
            P9.Anchor_X = 0;
            
            Panel P10 = new Panel(Color: ColorF.Pink);
            Panel P11 = new Panel(Color: ColorF.DarkYellow);
            
            P3.Add(P10).Add(P11);

            P10.Anchor_Y = 0;
            P11.Anchor_Y = 0;
            
            P10.Anchor_X = -1;
            P11.Anchor_X = 1;
            
            Panel P12 = new Panel(Color: ColorF.DarkRed);
            
            P3.Add(P12);

            P12.Anchor_X = 0;
            P12.Anchor_Y = 0;
            
            W1.BackgroundColor = ColorF.Brown;

            Panel P13 = new Panel(Color: ColorF.DarkAqua, Width: 16);
            P13.Anchor_Size = ElementAnchorSize.Vertical;
            P3.Add(P13);
            
            Panel P14 = new Panel(Color: ColorF.DarkAqua, Width: 16);
            P14.Anchor_Size = ElementAnchorSize.Vertical;
            P14.Anchor_X = 1;
            P3.Add(P14);
            
            Panel P15 = new Panel(Color: ColorF.DarkPink, Height: 16);
            P15.Anchor_Size = ElementAnchorSize.Horizon;
            P3.Add(P15);
            
            Panel P16 = new Panel(Color: ColorF.DarkPink, Height: 16);
            P16.Anchor_Size = ElementAnchorSize.Horizon;
            P16.Anchor_Y = 1;
            P3.Add(P16);

            P.Anchor_X = -1;
            P.Anchor_Y = -1;
            
            double d = 2;
            string FPS = "";
            while(W1.Alive){
                WL.System.Tick.LimitFPS(1, 300, TD => {
                    if(W1.Alive){
                        d += TD.DeltaTimeS;
                        if(d > 0.5f){ FPS = TD.FPS.ToString(); d = 0; }

                        W1.Title = FPS + " | " + W1.CursorInside;

                        P.X = W1.CursorPosition.X - (int)(P.Width  / 2);
                        P.Y = W1.CursorPosition.Y - (int)(P.Height / 2);
                        
                        P3.Width  = (uint)((0.75f + Math.Sin(TD.DeltaTick * 2) / 4) * 512);
                        P3.Height = (uint)((0.75f + Math.Sin(TD.DeltaTick * 2) / 4) * 512);

                        P3.Anchor_X = (float)Math.Sin(TD.DeltaTick);
                        P3.Anchor_Y = -(float)Math.Cos(TD.DeltaTick);
                        
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