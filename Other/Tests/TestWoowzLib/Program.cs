using WLO;
using WLO.Render;
using WLO.GL;
using WLO.GLFW;

public static class Program{
    public static int Main(string[] Args){
        try{
            WL.WoowzLib.Start();

            /*WL.GL.Debug.LogMain = true;
            WL.GL.Debug.LogCreate = true;
            WL.GL.Debug.LogDestroy = true;
            WL.GL.Debug.LogProgram = true;
            WL.GL.Debug.LogUse = true;
            WL.GL.Debug.LogBuffer = true;
            WL.GL.Debug.LogVertexConfig = true;*/
            
            WL.GLFW.Start();
            
            Window<GL> AAA = new Window<GL>(Title: "привет hello");

            Shader VShader = new Shader(AAA.Render, ShaderType.Vertex, """
                                                                       #version 330 core
                                                                       
                                                                       layout(location = 0) in vec3 InPosition;
                                                                       
                                                                       void main(){
                                                                           gl_Position = vec4(InPosition, 1.0);
                                                                       }
                                                                       """);
            
            Shader FShader = new Shader(AAA.Render, ShaderType.Fragment, """
                                                                         #version 330 core
                                                                         
                                                                         out vec4 OutColor;
                                                                         
                                                                         void main(){
                                                                             OutColor = vec4(1.0, 0.0, 1.0, 1.0);
                                                                         }
                                                                         """);
            
            Shader FShader2 = new Shader(AAA.Render, ShaderType.Fragment, """
                                                                         #version 330 core

                                                                         out vec4 OutColor;

                                                                         void main(){
                                                                             OutColor = vec4(1.0, 1.0, 0.0, 1.0);
                                                                         }
                                                                         """);

            WLO.GL.Program Prog  = new WLO.GL.Program(AAA.Render, VShader, FShader );
            WLO.GL.Program Prog2 = new WLO.GL.Program(AAA.Render, VShader, FShader2);

            VShader.Destroy();
            FShader.Destroy();
            FShader2.Destroy();
            
            FloatBuffer VBuffer = new FloatBuffer(AAA.Render, new MassiveF([
                0   ,  0.5f, 0,
                0.5f, -0.5f, 0, 
                -0.5f, -0.5f, 0
            ]));

            VertexConfig VC = new VertexConfig(AAA.Render);

            VC.Connect(VBuffer, 0, DataCount.Three, 3, 0, false);

            WLO.GL.Program P = Prog;
            while(!AAA.ShouldDestroy){
                AAA.Render.Viewport = new RectI(AAA.Size);
                
                AAA.Render.BackgroundColor = ColorF.Red;
                
                AAA.Render.Clear();

                VBuffer.Set(1, WL.Math.Random.Fast_0_1());
                
                P = P == Prog ? Prog2 : Prog;

                VC.Render(P, RenderMode.Triangles, 9);
                
                AAA.FinishRender();

                WL.GLFW.Tick();
            }
            
            WL.GLFW.Stop();
        }catch(Exception e){
            Logger.Fatal("ОШИБКА ВНУТРИ ПРИЛОЖЕНИЯ", e);
        }
        
        return 0;
    }
}