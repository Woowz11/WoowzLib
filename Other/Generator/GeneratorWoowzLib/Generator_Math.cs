using System;
using Microsoft.CodeAnalysis;

[Generator]
public sealed class Generator_Math : ISourceGenerator{
    private static readonly DiagnosticDescriptor Info =
        new(
            id: "WZGEN001",
            title: "Generator",
            messageFormat: "{0}",
            category: "Generator",
            defaultSeverity: DiagnosticSeverity.Warning,
            isEnabledByDefault: true,
            helpLinkUri: "https://woowz.dev/generator"
        );
    
    private static void Log(GeneratorExecutionContext context, string message)
    {
        context.ReportDiagnostic(
            Diagnostic.Create(Info, Location.None, message)
        );
    }
    
    
    public void Initialize(GeneratorInitializationContext context){}
    
    public void Execute(GeneratorExecutionContext context){
        foreach(VectorType Type in Enum.GetValues(typeof(VectorType))){
            for(int i = 2; i <= 4 + 1; i++){
                CreateVector(context, Type, i);
            }
        }
    }

    public enum VectorType{ Float, Double, Int, UInt }
    private static void CreateVector(GeneratorExecutionContext c, VectorType Type, int N){
        Log(c, "Создание вектора [" + Type + ", " + N + "]");
        
        c.AddSource(
            $"Vector_{Type}_{N}.g.cs",
            $"// generated {DateTime.Now.Ticks}"
        );
    }
}