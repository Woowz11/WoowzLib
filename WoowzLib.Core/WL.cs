using System.Dynamic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace WoowzLib.Core;

public static class WL{
    private static readonly dynamic Modules = new ExpandoObject();
    public static dynamic M => Modules;

    public static void Register(int Order, string Name, object Module){
        Modules[Name] = Module;
    }

    public static void Install(){
        Console.WriteLine("WL INSTALL");
        
        var Ass = AppDomain.CurrentDomain.GetAssemblies();
        foreach(var asm in Ass){
            var types = asm.GetTypes().Where(t => t.GetCustomAttribute<WLModule>() != null).OrderBy(t => t.GetCustomAttribute<WLModule>().Order);

            foreach(var type in types){
                RuntimeHelpers.RunClassConstructor(type.TypeHandle);
            }
        }
    }
}