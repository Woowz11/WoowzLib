using WLO;
using WLO.Render;
using WLO.GL;
using WLO.GLFW;

public static class Program{
    public static int Main(string[] Args){
        try{
            WL.WoowzLib.Start();

            WL.GL.Debug.LogMain = true;
            WL.GL.Debug.LogCreate = true;
            WL.GL.Debug.LogDestroy = true;
            WL.GL.Debug.LogProgram = true;
            WL.GL.Debug.LogUse = true;
            
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
                                                                             OutColor = vec4(1.0, 0.0, 1.0, 1.0); // фиолетовый
                                                                         }
                                                                         """);

            WLO.GL.Program Prog = new WLO.GL.Program(AAA.Render, VShader, FShader);

            float[] VERTICES = [
                 0   ,  0.5f, 0,
                 0.5f, -0.5f, 0, 
                -0.5f, -0.5f, 0
            ];

            FloatBuffer VBuffer = new FloatBuffer(AAA.Render);

            MassiveF M = new MassiveF();

            M[5] = 125;
            Logger.Debug(M[5]);
            ref float test = ref M[5];
            test = -666;
            Logger.Debug(M[5]);
            
            Logger.Debug(M);
            
            while(!AAA.ShouldDestroy){
                AAA.Render.BackgroundColor = ColorF.Red;
                
                AAA.Render.Clear();
                
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