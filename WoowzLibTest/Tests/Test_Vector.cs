using WLO.Vector;

namespace WoowzLibTest.Tests;

/// <summary>
/// Тест векторов
/// </summary>
public static class Test_Vector{
    public static void Run(){
        Test.Run("Vector", () => {
            Test.F("Создание", () => {
                Vector2I v = new Vector2I(123, -321);
                if(v.X != 123 || v.Y != -321){ throw new Exception("Сломано создание вектора! 1"); }
                
                v = new Vector2I(123);
                if(v.X != 123 || v.Y != 123){ throw new Exception("Сломано создание вектора! 2"); }
                
                v = new Vector2I();
                if(v.X != 0 || v.Y != 0){ throw new Exception("Сломано создание вектора! 3"); }
            });
            
            Test.F("Свойства W/H", () => {
                Vector2I v = new Vector2I(512, 256);
                if(v.W != 512 || v.H != 256){ throw new Exception("W/H сломаны!"); }

                v.W = 22;
                v.H = 77;

                if(v.W != 22 || v.H != 77){ throw new Exception("W/H сеттеры сломаны!"); }
            });

            Test.F("Операторы", () => {
                var A = new Vector2I(3, 1);
                var B = new Vector2I(3, 1);
                var C = new Vector2I(1, 3);

                if(!(A == B)){ throw new Exception("Сломан: == 1"); }
                if(A == C){ throw new Exception("Сломан: == 2"); }
                if(A != B){ throw new Exception("Сломан: != 1"); }
                if(!(A != C)){ throw new Exception("Сломан: != 2"); }
            });
        });
    }
}