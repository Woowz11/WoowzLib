using System.Drawing;
using WL;
using WLO;
using File = WLO.File;
using Math = WL.Math;
using Window = WLO.Window;

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

            /*W1.OnKeyboardDown += (window, key, arg3) => {
                Logger.Info(arg3, key);
            };
            
            W1.OnKeyboardUp += (window, key, arg3) => {
                Logger.Info("UP", arg3, key);
            };*/

            WL.Input.Keyboard.OnDown += (key, i) => {
                Logger.Info(key, i);
                return false;
            };
            WL.Input.Keyboard.OnUp += (key, i) => {
                Logger.Info("UP", key, i);
            };
            
            Panel P = new Panel(Color: ColorB.Gray);
            W1.Add(P);
            
            P.OnMouseInside += (element, b) => {
                P.Color = b ? ColorB.Green : ColorB.Gray;
            };

            P.Anchor_X = 0;
            P.Anchor_Y = 0;
            
            P.Anchor_Width  = 0.9f;
            P.Anchor_Height = 0.9f;

            /*RenderPanel RP = new RenderPanel();
            P.Add(RP);
            
            RP.Anchor_X = 0;
            RP.Anchor_Y = 0;
            
            RP.Anchor_Width  = 0.9f;
            RP.Anchor_Height = 0.9f;*/

            Panel IMAGEPANEL = new Panel();
            IMAGEPANEL.Image = new Image(512, ColorB.Red);
            P.Add(IMAGEPANEL);

            IMAGEPANEL.Anchor_X = 0;
            IMAGEPANEL.Anchor_Y = 0;
            IMAGEPANEL.Anchor_Width  = 0.9f;
            IMAGEPANEL.Anchor_Height = 0.9f;
            
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

                        IMAGEPANEL.Image.Change(C => {
                            C.Fill(ColorB.Random);
                            
                            C.For((X, Y, W, H) => {
                                C[X, Y] = new ColorB((byte)(((float)X / W) * 255), (byte)(((float)Y / H) * 255), (byte)(WL.Math.DSin((float)TD.DeltaTick) * 255));
                                
                                float fx = (float)X / W * 5;
                                float fy = (float)Y / H * 5;
                                float t = (float)TD.DeltaTick * 10;

                                byte r = (byte)((WL.Math.DSin(WL.Math.DCos(fx + t) * 50 + fy)) * 255);
                                byte g = (byte)((WL.Math.DCos(WL.Math.Tan(fy + t)) + fx) * 255);
                                byte b = (byte)((WL.Math.DSin((fx * fy) * 5 + t)) * 255);

                                C[X, Y] -= new ColorB(g, b, r);
                            });
                        });

                        W1.Title = FPS + " | " + W1.KeyboardKeyPressed(Key.Up);
                        
                        W1.Render();
                    }
                });
                
                WL.WoowzLib.Update();
            }
        }catch(Exception e){
            Logger.Fatal("ОШИБКА ВНУТРИ ПРИЛОЖЕНИЯ", e);
            return 1;
        }
        
        return 0;
    }
}