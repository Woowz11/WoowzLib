 using System.Runtime.InteropServices;
 using WL;
 using WLO.GLFW;

 public static class Program{
    public static int Main(string[] Args){
        try{
            WL.WoowzLib.Start();
        
            WL.GLFW.Start();

            Window W1 = new Window();
            Window W2 = new Window(Title: "window 2");

            W2.OnResize += (w, W, H) => {
                w.Title = W + "x" + H + " | " + w.X + ":" + w.Y + " | " + w.Focused;
            };
            W2.OnPosition += (w, X, Y) => {
                w.Title = w.Width + "x" + w.Height + " | " + X + ":" + Y + " | " + w.Focused;
            };
            W2.OnFocus += (w, Focused) => {
                w.Title = w.Width + "x" + w.Height + " | " + w.X + ":" + w.Y + " | " + Focused;
            };
        
            while(!W1.ShouldDestroy || !W2.ShouldDestroy){
                if(!W1.Destroyed){ W1.Title = DateTime.Now.ToString("T"); }

                WL.GLFW.Tick();
            }
            
            WL.GLFW.Stop();
        }catch(Exception e){
            throw new Exception("ОШИБКА ВНУТРИ ПРИЛОЖЕНИЯ!", e);
        }
        
        return 0;
    }
}