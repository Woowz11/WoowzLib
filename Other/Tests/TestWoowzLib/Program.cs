using Microsoft.VisualBasic.Logging;
using WL;
using WLO;
using WLO.WinForm;
//using WLO.GL;
//using WLO.GLFW;
//using GL = WLO.Render.GL;
using Logger = WLO.Logger;

public static class Program{
    
    public static int Main(string[] Args){
        try{
            WL.WoowzLib.Start();




            Window W = new Window();
            Window W2 = new Window();

            while(!W.ShouldDestroy || !W2.ShouldDestroy){
                WL.Windows.Form.Tick();
            }


            /*WL.GL.Debug.LogMain = true;

            WL.GL.Debug.LogDestroy = true;
            WL.GL.Debug.LogProgram = true;
            WL.GL.Debug.LogUse = true;
            WL.GL.Debug.LogBuffer = true;
            WL.GL.Debug.LogVertexConfig = true;*/

            /*WL.GLFW.Start();

            Window<GL> AAA = new Window<GL>();

            Shader VShader = new Shader(AAA.Render, ShaderType.Vertex, """
                                                                       layout(location = 0) in vec3 InPosition;

                                                                       void main(){
                                                                           gl_Position = vec4(InPosition, 1.0);
                                                                       }
                                                                       """);

            Shader FShader = new Shader(AAA.Render, ShaderType.Fragment, """
                                                                         uniform float Time;
                                                                         uniform float Time2;

                                                                         out vec4 OutColor;

                                                                         void main(){
                                                                             OutColor = vec4(
                                                                                0.5 + sin(Time / 10000000)/2,
                                                                                0.5 + sin(Time / 20000000)/2,
                                                                                0.5 + sin(Time / 30000000)/2,
                                                                                1
                                                                            );
                                                                         }
                                                                         """);

            WLO.GL.Program Prog  = new WLO.GL.Program(AAA.Render, VShader, FShader );

            Uniform_Float U_Time = new Uniform_Float(Prog, "Time", true);
            Uniform_Float U_Time2 = new Uniform_Float(Prog, "Time2", true);

            VShader.Destroy();
            FShader.Destroy();

            FloatBuffer VBuffer = new FloatBuffer(AAA.Render, new MassiveF([
                0   ,  0.5f, 0,
                0.5f, -0.5f, 0,
                -0.5f, -0.5f, 0
            ]));

            VertexConfig VC = new VertexConfig(AAA.Render);

            VC.Connect(VBuffer, 0, DataCount.Three, 3, 0, false);

            const int TARGETFPS = 120;

            TickData TD = new TickData();
            int i = int.MaxValue - 1;
            while(!AAA.ShouldDestroy){
                WL.WoowzLib.Tick.LimitFPS(1, TARGETFPS, (TD__) => {
                    TD = TD__;

                    U_Time.Value = WL.Math.Time.ProgramLifeTick;

                    AAA.Render.Viewport = new RectI(AAA.Size);

                    AAA.Render.BackgroundColor = ColorF.Gray;

                    AAA.Render.Clear();

                    VC.Render(Prog, RenderMode.Triangles, 9);

                    AAA.FinishRender();
                });

                WL.WoowzLib.Tick.Limit(2, 16, (TD__) => {
                    i++;
                    if(i > 16){
                        AAA.Title = "WoowzLib (" + TD.DeltaTime + " | " + TD.FPS + ") [" + TD__.DeltaTime + "] {" + TD.DeltaFPS(TARGETFPS) + "} " + WL.Math.Time.ProgramLifeTick;
                        i = 0;
                    }

                    WL.GLFW.Tick();
                });
            }

            WL.GLFW.Stop();*/
        }catch(Exception e){
            Logger.Fatal("ОШИБКА ВНУТРИ ПРИЛОЖЕНИЯ", e);
        }
        
        return 0;
    }
}