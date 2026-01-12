using System;

public static class Generator{
    public static int Main(string[] Args){
        try{
            WL.WoowzLib.Start();

            string GeneratorFolder = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
            if(Path.GetFileName(GeneratorFolder) != "GeneratorWoowzLib"){ throw new Exception("Я тебя спас от страшной ошибки... GeneratorFolder [" + GeneratorFolder + "] неверный!"); }

            string BaseFolder = Path.GetFullPath(Path.Combine(GeneratorFolder, "..", "..", ".."));
            if(Path.GetFileName(BaseFolder) != "WoowzLib"){ throw new Exception("Я тебя спас от страшной ошибки... BaseFolder [" + GeneratorFolder + "] неверный!"); }
            
            string MathFolder = Path.GetFullPath(Path.Combine(BaseFolder, "WoowzLib.Math"));
            
            GenerateVector(MathFolder);
        }catch(Exception e){
            throw new Exception("Произошла ошибка во время генерации!", e);
        }

        return 0;
    }

    #region Vector

        private static void GenerateVector(string OutputFolder){
            try{
                Console.WriteLine("Генерация векторов в [" + OutputFolder + "]:");
                foreach(VectorType Type in Enum.GetValues(typeof(VectorType))){
                    for(int i = 2; i < 4 + 1; i++){
                        CreateVector(Type, i);
                    }
                }
                Console.WriteLine("Завершение генерации векторов");
            }catch(Exception e){
                throw new Exception("Произошла ошибка во время генерации векторов!", e);
            }
        }
        
        public enum VectorType{ Int, UInt, Float, Double }
        private static void CreateVector(VectorType Type, int N){
            try{
                Console.WriteLine("\tСоздание вектора [" + Type + ", " + N + "]");
                
                
            }catch(Exception e){
                throw new Exception("Произошла ошибка во время генерации вектора [" + Type + ", " + N + "]!", e);
            }
        }

    #endregion
}