using System.Drawing;
using WLO;
using File = WLO.File;
using Logger = WLO.Logger;
using Math = WL.Math;

public static class Program{
    public static int Main(string[] Args){
        try{
            WL.Render.Debug.LogMain = true;
            
            WL.WoowzLib.Start(new WoowzLibInfo(
                Name  : "Test WoowzLib",
                Author: "Woowz11"
            ));

            const string FilesPath = "W:/Other/WoowzLib/Other/Tests/TestWoowzLib/FILES/";
            
            Window W1 = new Window();

            Panel P = new Panel(Color: ColorB.Gray);
            W1.Add(P);

            P.OnCursorInside += (element, b) => {
                P.Color = b ? ColorB.Green : ColorB.Gray;
            };

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

            Panel P2 = new Panel();
            P2.Parent = RP;
            P2.Anchor_X = 0;
            P2.Anchor_Y = 0;

            P2.Image = WL.Parser.ParseBMP(new File(FilesPath + "img24.bmp").ReadByte()).ToImage();
            
            Panel P3 = new Panel();
            P3.Parent = RP;
            P3.Anchor_X = -0.5f;
            P3.Anchor_Y = 0;

            P3.Image = WL.Parser.ParseBMP(new File(FilesPath + "img8.bmp").ReadByte()).ToImage();
            
            Panel P4 = new Panel();
            P4.Parent = RP;
            P4.Anchor_X = 0.5f;
            P4.Anchor_Y = 0;

            P4.Image = WL.Parser.ParseBMP(new File(FilesPath + "img1.bmp").ReadByte()).ToImage();
            
            Panel P5 = new Panel();
            P5.Parent = RP;
            P5.Anchor_X = 0;
            P5.Anchor_Y = -0.75f;

            P5.Image = WL.Parser.ParseBMP(new File(FilesPath + "img32.bmp").ReadByte()).ToImage();
            
            double d = 2;
            string FPS = "";
            bool dodo = false;
            Vector2F t2 = new Vector2F(P2.Width, P2.Height);
            Vector2F t3 = new Vector2F(P3.Width, P3.Height);
            Vector2F t4 = new Vector2F(P4.Width, P4.Height);
            Vector2F t5 = new Vector2F(P5.Width, P5.Height);
            while(W1.Alive){
                WL.System.Tick.LimitFPS(1, 300, TD => {
                    if(W1.Alive){
                        d += TD.DeltaTimeS;
                        if(d > 0.5f){
                            FPS = TD.FPS.ToString(); d = 0;
                        }

                        W1.Title = FPS + " | " + W1.CursorInside;

                        //P2.Color = ColorB.Lerp(P2.Color, DO ? ColorB.Black : ColorB.White, (float)TD.DeltaTimeS * 2);
                        t2 = Vector2F.Lerp(t2, new Vector2F(128, 128) * (W1.CursorElement == P2 ? 5 : 1), (float)TD.DeltaTimeS * 2);
                        P2.Size = new Vector2U((uint)t2.X, (uint)t2.Y);
                        
                        t3 = Vector2F.Lerp(t3, new Vector2F(128, 128) * (W1.CursorElement == P3 ? 5 : 1), (float)TD.DeltaTimeS * 2);
                        P3.Size = new Vector2U((uint)t3.X, (uint)t3.Y);
                        
                        t4 = Vector2F.Lerp(t4, new Vector2F(128, 128) * (W1.CursorElement == P4 ? 5 : 1), (float)TD.DeltaTimeS * 2);
                        P4.Size = new Vector2U((uint)t4.X, (uint)t4.Y);
                        
                        t5 = Vector2F.Lerp(t5, new Vector2F(128, 128) * (W1.CursorElement == P5 ? 5 : 1), (float)TD.DeltaTimeS * 2);
                        P5.Size = new Vector2U((uint)t5.X, (uint)t5.Y);
                        
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