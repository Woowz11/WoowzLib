using System.Reflection;
using System.Runtime.CompilerServices;

namespace WL{
    [WoowzLibModule(int.MinValue)]
    public static class WoowzLib{
        public static void Start(){
            try{
                Console.WriteLine("Установка WL:");

                string RunFolder = AppContext.BaseDirectory;
                foreach(string DLL in Directory.GetFiles(RunFolder, "WoowzLib.*.dll")){
                    Assembly.LoadFrom(DLL);
                }

                var Modules = AppDomain.CurrentDomain.GetAssemblies()
                       .Where(A => A.FullName != null && A.FullName.Contains("WoowzLib"))
                       .SelectMany(A => A.GetTypes().Select(T => new{
                           Type = T,
                           Attribute = T.GetCustomAttribute<WoowzLibModule>()
                       }))
                       .Where(A => A.Attribute != null)
                       .ToList().OrderBy(A => A.Attribute!.Order);

                foreach(var Module in Modules){
                    Console.WriteLine("Загружен WL модуль: " + Module.Type.Name);
                    RuntimeHelpers.RunClassConstructor(Module.Type.TypeHandle);
                }
            
                Console.WriteLine("Установка WL завершена!");
            }catch(Exception e){
                throw new Exception("Произошла ошибка при запуске WoowzLib!");
            }
        }
    }
}