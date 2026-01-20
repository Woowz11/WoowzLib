using System.Drawing;
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
            
            Button Test1 = new Button(default, 10, 10);
            Button Test2 = new Button("Кнопка", 10, 10 + 30);
            Button Test3 = new Button("AAAAAAAAAAAAAAAAAAAAA", 10, 10 + 30 + 30);
            Button Test4 = new Button("😁", 10, 10 + 30 + 30 + 30);

            Test1.OnClick += button => {
                Logger.Debug("CLICK");
            };
            
            Test2.OnClick += button => {
                Logger.Debug("CLICK 2");
            };
            
            Panel P = new Panel();
            P.Color = ColorF.Yellow;
            
            Panel P2 = new Panel();
            P2.Color = ColorF.Blue;
            
            W1.Add(Test1);

            W1.Add(P);
            W1.Add(P2);
            
            P.Add(Test2, Test3, Test4);

            RenderPanel RP2 = new RenderPanel(64, 64, 512, 512);
            W1.Add(RP2);

            RenderContext R2 = WL.Render.Connect(RP2);
            
            Window W2 = new Window();
            
            RenderPanel RP1 = new RenderPanel(Width: W2.Width, Height: W2.Height);
            W2.Add(RP1);
            
            RenderContext R1 = WL.Render.Connect(RP1);

            RenderPanel RP3 = new RenderPanel();
            W2.Add(RP3);
            
            RenderContext R3 = WL.Render.Connect(RP3); 
            
            W2.OnResize += (window, u, arg3) => {
                RP1.Width = u;
                RP1.Height = arg3;
            };

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

            double d = 2;
            while(W1.Alive || W2.Alive){
                WL.System.Tick.LimitFPS(1, 300, TD => {
                    if(W1.Alive){
                        W1.Title = TD.Tick.ToString();
                        
                        P.X = (int)(W1.Width / 2) + (int)(Math.Sin(TD.DeltaTick) * 200);
                        P2.X = (int)(W1.Width / 2);

                        //P.Color = ColorF.Random;
                        
                        Test1.Text = W1.Title;

                        Test2.Text = "Русский " + Test1.Text;

                        R2.Render(ColorF.Random, () => {
                            
                        });
                        
                        W1.UpdateRender();
                    }

                    if(W2.Alive){
                        d += TD.DeltaTimeS;
                        if(d > 0.5f){ W2.Title = TD.FPS.ToString(); d = 0; }

                        R1.Render(ColorF.Random, () => {
                            
                        });
                        
                        R3.Render(ColorF.Random, () => {
                            
                        });
                        
                        W2.UpdateRender();
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