using WLO.GLFW;

 public static class Program{
    public static int Main(string[] Args){
        try{
            WL.WoowzLib.Start();
        
            WL.GLFW.Start();

            Window W1 = new Window();

            WL.GLFW.Native.glfwMakeContextCurrent(W1.Handle);

            W1.OnResize += (a, b, c) => {
                WL.GL.Native.glClearColor(WL.Math.Random.Fast_0_1(), WL.Math.Random.Fast_0_1(), WL.Math.Random.Fast_0_1(), 1);
                WL.GL.Native.glClear(WL.GL.Native.GL_COLOR_BUFFER_BIT);

                WL.GLFW.Native.glfwSwapBuffers(W1.Handle);
            };
            
            while(!W1.ShouldDestroy){
                W1.Title = DateTime.Now.ToString("T");
                
                
                
                WL.GLFW.Tick();
            }
            
            WL.GLFW.Stop();
        }catch(Exception e){
            throw new Exception("ОШИБКА ВНУТРИ ПРИЛОЖЕНИЯ!", e);
        }
        
        return 0;
    }
}