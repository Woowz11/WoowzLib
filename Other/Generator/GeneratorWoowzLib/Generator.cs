using System;
using File = WLO.File;

/// <summary>
/// 
/// </summary>
public static class Generator{
    public static int Main(string[] Args){
        try{
            WL.WoowzLib.Start();

            string GeneratorFolder = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
            if(Path.GetFileName(GeneratorFolder) != "GeneratorWoowzLib"){ throw new Exception("Я тебя спас от страшной ошибки... GeneratorFolder [" + GeneratorFolder + "] неверный!"); }

            string BaseFolder = Path.GetFullPath(Path.Combine(GeneratorFolder, "..", "..", ".."));
            if(Path.GetFileName(BaseFolder) != "WoowzLib"){ throw new Exception("Я тебя спас от страшной ошибки... BaseFolder [" + GeneratorFolder + "] неверный!"); }

            GlobalInfo = "Сгенерировано через GeneratorWoowzLib!";
            
            string MathFolder = Path.GetFullPath(Path.Combine(BaseFolder, "WoowzLib.Math"));
            
            GenerateVector(Path.GetFullPath(Path.Combine(MathFolder, "WLO", "Vector")));
        }catch(Exception e){
            throw new Exception("Произошла ошибка во время генерации!", e);
        }

        return 0;
    }

    private static string GlobalInfo;

    private static string PreComment(){
        return $"""
               /// <summary>
               /// {GlobalInfo}
               /// </summary>
               """;
    }

    private static string Pre(){
        return $$"""
                 namespace WLO;
                 
                 {{PreComment()}}
                 """;
    }
    
    #region Vector

        private static void GenerateVector(string OutputFolder){
            try{
                Console.WriteLine("Генерация векторов в [" + OutputFolder + "]:");

                if(!WL.Explorer.Folder.Exist(OutputFolder)){ throw new Exception("Не найдена Output папка!"); }

                WL.Explorer.Folder.Clear(OutputFolder);
                
                
                File F__ = new File(Path.Combine(OutputFolder, "Vector.cs")).WriteString($$"""
                   {{Pre()}}
                   public interface Vector{
                        public int  N{ get; }
                        public Type T{ get; }
                   }
                   """);

                for(int i = 2; i <= 4; i++){
                    F__ = new File(Path.Combine(OutputFolder, "Vector" + i + ".cs")).WriteString($$"""
                        {{Pre()}}
                        public interface Vector{{i}} : Vector{
                            public int N => {{i}};
                        }
                        """);
                }

                foreach(VectorType Type in Enum.GetValues(typeof(VectorType))){
                    F__ = new File(Path.Combine(OutputFolder, "Vector" + Type.ToString()[0] + ".cs")).WriteString($$"""
                       {{Pre()}}
                       public interface Vector{{Type.ToString()[0]}} : Vector{
                            public new Type T => typeof({{Type.ToString().ToLower()}});
                       }
                       """);
                    
                    for(int i = 2; i <= 4; i++){
                        CreateVector(OutputFolder, Type, i);
                    }
                }
                Console.WriteLine("Завершение генерации векторов");
            }catch(Exception e){
                throw new Exception("Произошла ошибка во время генерации векторов!", e);
            }
        }
        
        private enum VectorType{ Int, UInt, Float, Double }
        private static readonly char[] VectorComponents = ['X', 'Y', 'Z', 'W'];
        private static void CreateVector(string OutputFolder, VectorType VectorType, int N){
            try{
                Console.WriteLine("\tСоздание вектора [" + VectorType + ", " + N + "]");

                // Первая буква типа (I, F, U, D)
                string TypeChar = VectorType.ToString()[0].ToString();

                // Название (Vector3F, Vector2U)
                string Name = "Vector" + N + TypeChar;

                // Тип (int, float, uint, double)
                string Type = VectorType.ToString().ToLower();
                
                string Result = Pre() + "\n";

                Result += "public class " + Name + " : Vector" + N + ", Vector" + TypeChar + "{\n";

                Result += $$"""
                                public int  N{ get; }
                                public Type T{ get; }
                            """;
                
                Result += "\n}";

                File F = new File(Path.Combine(OutputFolder, Name + ".cs")).WriteString(Result);
            }catch(Exception e){
                throw new Exception("Произошла ошибка во время генерации вектора [" + VectorType + ", " + N + "]!", e);
            }
        }

    #endregion
}