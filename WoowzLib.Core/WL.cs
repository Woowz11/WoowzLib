using System.Dynamic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace WoowzLib.Core;

public static class WL{

    public static void Install(){
        Console.WriteLine("WL INSTALL");

        string RunFolder = AppContext.BaseDirectory;
        foreach(string DLL in Directory.GetFiles(RunFolder, "WoowzLib.*.dll")){
            Console.WriteLine("Добавление: " + DLL);
            Assembly.LoadFrom(DLL);
        }
        
        IEnumerable<Assembly> WLModules = AppDomain.CurrentDomain.GetAssemblies().Where(A => A.FullName != null && A.FullName.Contains("WoowzLib"));

        foreach(Assembly WLModule in WLModules){
            Console.WriteLine("Нашёл: " + WLModule);

            IEnumerable<Type> Modules = WLModule.GetTypes().Where(M => M.GetCustomAttribute<WLModule>() != null);

            foreach(Type Module in Modules){
                Console.WriteLine("Модуль: " + Module);
            }
        }
    }
}