using System.Runtime.CompilerServices;
using WL;
using WLO;
using WLO.GL;
using WLO.GLFW;
using GL = WLO.Render.GL;
using Logger = WLO.Logger;

public static class Program{
    public static int Main(string[] Args){
        try{
            WL.WoowzLib.Start();
            
            /*WL.GL.Debug.LogMain = true;
        
            WL.GL.Debug.LogDestroy = true;
            WL.GL.Debug.LogProgram = true;
            WL.GL.Debug.LogUse = true;
            WL.GL.Debug.LogBuffer = true;
            WL.GL.Debug.LogVertexConfig = true;*/
            
            WL.GLFW.Start();
            
            Window<GL> AAA = new Window<GL>();

            Shader VShader = new Shader(AAA.Render, ShaderType.Vertex, """
                                                                       layout(location = 0) in vec3 InPosition;
                                                                       
                                                                       void main(){
                                                                           gl_Position = vec4(InPosition, 1.0);
                                                                       }
                                                                       """);
            
            Shader FShader = new Shader(AAA.Render, ShaderType.Fragment, """
                                                                         out vec4 OutColor;
                                                                         
                                                                         void main(){
                                                                             OutColor = vec4(1, 0, 1, 1);
                                                                         }
                                                                         """);

            WLO.GL.Program Prog  = new WLO.GL.Program(AAA.Render, VShader, FShader );

            VShader.Destroy();
            FShader.Destroy();
            
            FloatBuffer VBuffer = new FloatBuffer(AAA.Render, new MassiveF([
                0   ,  0.5f, 0,
                0.5f, -0.5f, 0, 
                -0.5f, -0.5f, 0
            ]));

            VertexConfig VC = new VertexConfig(AAA.Render);

            VC.Connect(VBuffer, 0, DataCount.Three, 3, 0, false);

            TickData TD = new TickData();
            int i = int.MaxValue - 1;
            while(!AAA.ShouldDestroy){
                WL.WoowzLib.Tick.LimitFPS(1, 120, (TD__) => {
                    TD = TD__;
                    
                    AAA.Render.Viewport = new RectI(AAA.Size);
            
                    AAA.Render.BackgroundColor = ColorF.Red;
            
                    AAA.Render.Clear();
                    
                    VC.Render(Prog, RenderMode.Triangles, 9);

                    AAA.FinishRender();
                });

                WL.WoowzLib.Tick.Limit(2, 16, (TD__) => {
                    i++;
                    if(i > 16){
                        AAA.Title = "WoowzLib (" + TD.DeltaTime + " | " + TD.FPS + ") " + WL.Math.Time.ProgramLifeTick;
                        i = 0;
                    }

                    WL.GLFW.Tick();
                });
            }
            
            WL.GLFW.Stop();
        }catch(Exception e){
            Logger.Fatal("ОШИБКА ВНУТРИ ПРИЛОЖЕНИЯ", e);
        }
        
        return 0;
    }
}