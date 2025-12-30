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
        
        var Modules = AppDomain.CurrentDomain.GetAssemblies()
                                                   .Where(A => A.FullName != null && A.FullName.Contains("WoowzLib"))
                                                   .SelectMany(A => A.GetTypes().Select(T => new{
                                                       Type = T,
                                                       Attribute = T.GetCustomAttribute<WLModule>()
                                                   }))
                                                   .Where(A => A.Attribute != null)
                                                   .ToList().OrderBy(A => A.Attribute!.Order);

        foreach(var Module in Modules){
            Console.WriteLine("Модуль: " + Module);
            RuntimeHelpers.RunClassConstructor(Module.Type.TypeHandle);
        }
    }
}