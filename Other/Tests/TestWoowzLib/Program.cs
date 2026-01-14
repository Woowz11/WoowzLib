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
            
            WL.GLFW.Start();

            Window<GL> AAA = new Window<GL>(Title: "привет hello");

            Shader TestShader = new Shader(AAA.Render, ShaderType.Vertex, "");
            
            while(!AAA.ShouldDestroy){
                AAA.Render.BackgroundColor = ColorF.Red;
                
                AAA.Render.Clear();
                
                AAA.FinishRender();

                WL.GLFW.Tick();
            }
            
            WL.GLFW.Stop();
        }catch(Exception e){
            throw new Exception("ОШИБКА ВНУТРИ ПРИЛОЖЕНИЯ!", e);
        }
        
        return 0;
    }
}