using WLO;
using File = WLO.File;
using Logger = WLO.Logger;

public static class Program{
    public static int Main(string[] Args){
        try{
            WL.Render.Debug.LogMain = true;
            
            WL.WoowzLib.Start(new WoowzLibInfo(
                Name  : "Test WoowzLib",
                Author: "Woowz11"
            ));

            Image Img = new Image();

            const string FilesPath = "W:/Other/WoowzLib/Other/Tests/TestWoowzLib/FILES/img.";
            
            byte[] PNG = new File(FilesPath + "png").ReadByte();
            Logger.Info(WL.Parser.Detect.WhatFormat(PNG));
            
            byte[] JPEG = new File(FilesPath + "jpg").ReadByte();
            Logger.Info(WL.Parser.Detect.WhatFormat(JPEG));
            
            byte[] WEBP = new File(FilesPath + "webp").ReadByte();
            Logger.Info(WL.Parser.Detect.WhatFormat(WEBP));
            
            byte[] TIFF = new File(FilesPath + "tiff").ReadByte();
            Logger.Info(WL.Parser.Detect.WhatFormat(TIFF));
            
            byte[] BMP = new File(FilesPath + "bmp").ReadByte();
            Logger.Info(WL.Parser.Detect.WhatFormat(BMP));
            
            byte[] GIF = new File(FilesPath + "gif").ReadByte();
            Logger.Info(WL.Parser.Detect.WhatFormat(GIF));

            ParsedContainer_BMP PC_BMP = (ParsedContainer_BMP)WL.Parser.Parse(BMP);
            Logger.Info(PC_BMP);
            
            Window W1 = new Window();

            Panel P = new Panel(Color: ColorF.Gray);
            W1.Add(P);

            P.OnCursorInside += (element, b) => {
                P.Color = b ? ColorF.Green : ColorF.Gray;
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

            Panel P2 = new Panel(Color: ColorF.LightBlue);
            P2.Parent = RP;
            P2.Anchor_X = 0;
            P2.Anchor_Y = 0;
            
            P2.OnCursorInside += (element, b) => {
                P2.Size = new Vector2U(128, 128) * (b ? 2U : 1U);
            };
            
            double d = 2;
            string FPS = "";
            bool dodo = false;
            while(W1.Alive){
                WL.System.Tick.LimitFPS(1, 300, TD => {
                    if(W1.Alive){
                        d += TD.DeltaTimeS;
                        if(d > 0.5f){
                            FPS = TD.FPS.ToString(); d = 0;
                        }

                        W1.Title = FPS + " | " + W1.CursorInside;
                        
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