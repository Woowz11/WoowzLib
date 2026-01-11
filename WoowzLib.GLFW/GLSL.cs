using File = WLO.File;

namespace WL{
    [WoowzLibModule(30)]
    public static class GLSL{
        static GLSL(){
            AppDomain.CurrentDomain.ProcessExit        += (_, _) => Destroy(true);
            AppDomain.CurrentDomain.UnhandledException += (_, _) => Destroy(true);
        }
        
        /// <summary>
        /// Текущий glfw3.dll
        /// </summary>
        private static File? DLL;
        
        /// <summary>
        /// Запуск GLFW
        /// </summary>
        public static void Start(){
            try{
                if(DLL != null){ throw new Exception("GLFW уже был загружен!"); }

                DLL = WL.Explorer.Resources.Load("WoowzLib.GLFW.glfw3.dll", typeof(WL.GLSL).Assembly);
                WL.Native.Load(DLL);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при запуске GLFW!", e);
            }
        }

        /// <summary>
        /// Остановка GLFW
        /// </summary>
        public static void Stop(){
            try{
                if(DLL == null){ throw new Exception("GLFW и не был загружен!"); }
                Destroy(false);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при остановке GLFW!", e);
            }
        }

        private static void Destroy(bool Warn){
            try{
                if(DLL == null){ return; }
                WL.Native.Unload(DLL);
                DLL = null;
                if(Warn){ Console.WriteLine("Автоостановка GLFW!"); }
            }catch(Exception e){
                throw new Exception("Произошла ошибка при базовой остановке GLFW!", e);
            }
        }
    }
}