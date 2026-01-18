using WL;
using WL.Windows;
using WLO;
using WLO.GLFW;
using WLO.Render;
using WLO.GL;
using WLO.GLFW;
using GL = WLO.Render.GL;
using Logger = WLO.Logger;

public static class Program{
    
    public static int Main(string[] Args){
        try{
            WL.WoowzLib.Start();
            
            WL.GL.Debug.LogBuffer = true;
            WL.GL.Debug.LogCreate = true;
            WL.GL.Debug.LogDestroy = true;
            WL.GL.Debug.LogMain = true;
            WL.GL.Debug.LogProgram = true;
            WL.GL.Debug.LogUse = false;
            WL.GL.Debug.LogVertexConfig = true;
            
            WL.GLFW.Start();
            
            Window W1 = new Window();
            Window W2 = new Window();

            GL GL1 = new GL();
            
            Uniform_Float U_Time = null;
            WLO.GL.Program Prog = null;
            VertexConfig VC = null;
            
            GL1.Context(() => {
                Shader VShader = new Shader(ShaderType.Vertex, """
                                                                   layout(location = 0) in vec3 InPosition;

                                                                   void main(){
                                                                       gl_Position = vec4(InPosition, 1.0);
                                                                   }
                                                                   """);

                Shader FShader = new Shader(ShaderType.Fragment, """
                                                                     uniform float Time;

                                                                     out vec4 OutColor;

                                                                     void main(){
                                                                         OutColor = vec4(
                                                                            0.5 + sin(Time / 10000000)/2,
                                                                            0,
                                                                            0,
                                                                            1
                                                                        );
                                                                     }
                                                                     """);
                
                Prog = new WLO.GL.Program(VShader, FShader );

                U_Time = new Uniform_Float(Prog, "Time");

                VShader.Destroy();
                FShader.Destroy();

                FloatBuffer VBuffer = new FloatBuffer(new MassiveF([
                    0   ,  0.5f, 0,
                    0.5f, -0.5f, 0,
                    -0.5f, -0.5f, 0
                ]));

                VC = new VertexConfig();

                VC.Connect(VBuffer, 0, DataCount.Three, 3, 0, false);
            });
            
            while(!W1.ShouldDestroy || !W2.ShouldDestroy){
                WL.WoowzLib.Tick.LimitFPS(1, 120, TD => {
                    if(!W1.ShouldDestroy){
                        GL1.Context(W1, () => {
                            U_Time.Value = WL.Math.Time.ProgramLifeTick;

                            GL1.Viewport = new RectI(W1.Size);
                        
                            GL1.BackgroundColor = ColorF.Random;
                            GL1.Clear();

                            VC.Render(Prog, RenderMode.Triangles, 9);
                        
                            W1.StopRender();
                        });
                        
                        W1.Title = TD.FPS.ToString();
                    }

                    if(!W2.ShouldDestroy){
                        GL1.Context(W2, () => {
                            U_Time.Value = WL.Math.Time.ProgramLifeTick * 10;

                            GL1.Viewport = new RectI(W2.Size);
                        
                            GL1.BackgroundColor = ColorF.Random;
                            GL1.Clear();

                            VC.Render(Prog, RenderMode.Triangles, 9);
                        
                            W2.StopRender();
                        });
                    }
                });
                
                WL.GLFW.Tick();
            }
            
            WL.GLFW.Stop();

        }catch(Exception e){
            Logger.Fatal("ОШИБКА ВНУТРИ ПРИЛОЖЕНИЯ", e);
            return 1;
        }
        
        return 0;
    }
}