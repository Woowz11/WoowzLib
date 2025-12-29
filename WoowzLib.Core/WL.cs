using System.Dynamic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace WoowzLib.Core;

public static class WL{

    public static void Install(){
        Console.WriteLine("WL INSTALL");

        var path = AppContext.BaseDirectory;
        foreach(var file in Directory.GetFiles(path, "WoowzLib.*.dll")){
            Console.WriteLine(file);
            Assembly.LoadFrom(file);
        }
        
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        foreach(var VARIABLE in assemblies){
            Console.WriteLine(VARIABLE);
        }
    }
}