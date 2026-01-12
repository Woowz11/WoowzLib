using WLO.GLFW;

 public static class Program{
    public static int Main(string[] Args){
        try{
            WL.WoowzLib.Start();
        
            WL.GLFW.Start();

            Window W1 = new Window();

            WL.GLFW.Native.glfwMakeContextCurrent(W1.Handle);

            W1.OnResize += (window, width, height) => UPD(W1);
            W1.OnMove += (window, i, i1) => UPD(W1); 
            
            while(!W1.ShouldDestroy){
                W1.Title = DateTime.Now.ToString("T");
                
                UPD(W1);
                
                WL.GLFW.Tick();
            }
            
            WL.GLFW.Stop();
        }catch(Exception e){
            throw new Exception("ОШИБКА ВНУТРИ ПРИЛОЖЕНИЯ!", e);
        }
        
        return 0;
    }

    private static void UPD(Window W1){
        WL.GL.Native.glClearColor(WL.Math.Random.Fast_0_1((uint)W1.X), WL.Math.Random.Fast_0_1((uint)W1.Y), WL.Math.Random.Fast_0_1((uint)(W1.Width * W1.Height)), 1);
        WL.GL.Native.glClear(WL.GL.Native.GL_COLOR_BUFFER_BIT);

        WL.GLFW.Native.glfwSwapBuffers(W1.Handle);
    }
}