using File = WLO.File;

namespace WoowzLibGenerator.Generator;

public static class Math{
    
    // ----------------------------------------------------------------------
    
    private static string OutFolder      = null!;
    private static string OutFolderDebug = null!;
    public static void Generate(string OutFolder__, string OutFolderDebug__){
        try{
            OutFolder = OutFolder__; 
            WL.Explorer.Folder.GetOrCreate(OutFolder);

            OutFolderDebug = OutFolderDebug__;
            WL.Explorer.Folder.GetOrCreate(OutFolderDebug);

            CreateMath();
        }catch(Exception e){
            throw new Exception("Произошла ошибка при генерации [Math]!", e);
        }
    }
    
    // ----------------------------------------------------------------------

    private static string Result = "";
    public static void CreateMath(){
        try{
            Logger.Info("Создание математики");
            Result = "";

            Result = Other.Generate_GeneratorComment("Math") + Result;
            Result += Other.Generate_Line(false);

            MathContent();
            
            Result += Other.Generate_Line();
            
            File FileR = WL.Explorer.File.GetOrCreate(WL.String.Path.Add(OutFolder     , "Math.cs"));
            File FileD = WL.Explorer.File.GetOrCreate(WL.String.Path.Add(OutFolderDebug, "Math.cs"));

            string R__ = Other.Inline(Result);
            FileD.Content = R__;

            R__ = Other.Beautify(R__, AutoInline: false, StartIndent: 1) + "\n\t/* Конец генератора */";

            string MathContent__ = new File("W:/Other/WoowzLib/WoowzLib.Math/WL/Math.cs").Content;

            int StartIndex = MathContent__.IndexOf("\t/* Сгенерировано с помощью WoowzLibGenerator", StringComparison.Ordinal);
            int EndIndex = MathContent__.IndexOf("/* Конец генератора */", StringComparison.Ordinal);

            if(StartIndex == -1 || EndIndex == -1 && EndIndex > StartIndex){
                throw new Exception("Не найдено куда вставлять Math!");
            }

            R__ = MathContent__[..StartIndex] + R__ + MathContent__[(EndIndex + 22)..];
            
            FileR.Content = R__;
        }catch(Exception e){
            throw new Exception("Произошла ошибка при генерации математики!", e);
        }
    }

    public static void MathContent(){
        Info.ValueType[] Numbers_SupportNegative = [Info.ValueType.Float, Info.ValueType.Double, Info.ValueType.Int, Info.ValueType.Short, Info.ValueType.Long, Info.ValueType.SByte, Info.ValueType.Decimal];
        Info.ValueType[] Numbers_FloatDouble = [Info.ValueType.Float, Info.ValueType.Double];
        Info.ValueType[] Numbers_NoByteShort = [Info.ValueType.Float, Info.ValueType.Double, Info.ValueType.Int, Info.ValueType.UInt, Info.ValueType.Long, Info.ValueType.ULong, Info.ValueType.Decimal];
        
        string RFM(string Code, Info.ValueType[]? VTS = null){
            VTS ??= Info.Numbers;
            
            return VTS.Aggregate("", (current, VT) => current +
                                      WL.String.Replace(Code,
                                          ["@", "#", "0.5!", "0.25!", "0!", "1!"],
                                          [
                                              Info.ValueType_Primitive(VT),
                                              Info.ValueType_Name(VT),
                                              Info.ValueType_Half(VT),
                                              Info.ValueType_Quarter(VT),
                                              Info.ValueType_Zero(VT),
                                              Info.ValueType_One(VT)
                                          ]));
        }

        void Generate_Abs(){
            Result += RFM(Other.Generate_Summary("Делает число не отрицательным (0! -> 0!, 1! -> 1!, -1! -> 1!)") + "public static @ Abs#(@ A) => @.Abs(A);", Numbers_SupportNegative);
        }
        Generate_Abs();

        Result += Other.Generate_Line();
        
        void Generate_MinMax(){
            void Generate_MinMax2(string Comment, string Func){
                Result += RFM(Other.Generate_Summary("Выбирает " + Comment + " число из указанных") + "public static @ " + Func + "#(@ A, @ B) => @." + Func + "(A, B);");

                Result += RFM(Other.Generate_Summary("Выбирает " + Comment + " число из указанных") + "public static @ " + Func + "#(params @[] A){ if(A.Length == 0){ return 0!; } @ M = A[0]; for(int i = 1; i < A.Length; i++){ M = WL.Math." + Func + "#(M, A[i]); } return M; }");
            }
            Generate_MinMax2("минимальное", "Min");
            Result += Other.Generate_NextLine();
            Generate_MinMax2("максимальное", "Max");
        }
        Generate_MinMax();

        Result += Other.Generate_Line();
        
        void Generate_Trigonometry(){
            void Generate_Trigonometry2(string Comment, string Func, string? RealFunc__ = null){
                string RealFunc = RealFunc__ ?? Func;
                Result += RFM(Other.Generate_Summary(Comment) + "public static @ " + Func + "#(@ A) => @." + RealFunc + "(A);", Numbers_FloatDouble);
            }
            Generate_Trigonometry2("Синус числа (0 -> 0, π/2 -> 1, π -> 0)", "Sin");
            Generate_Trigonometry2("Косинус числа (0 -> 1, π/2 -> 0, π -> -1)", "Cos");
            Generate_Trigonometry2("Тангенс числа (0 -> 0, π/2 -> ∞, π -> 0)", "Tan");
            Result += RFM(Other.Generate_Summary("Котангенс (0 -> ∞, π/2 -> 0, π -> ∞)") + "public static @ Cot#(@ A) => 1! / WL.Math.Tan#(A);", Numbers_FloatDouble);
            Generate_Trigonometry2("Арксинус числа [-1, 1] (0 -> 0, 1 -> π/2, -1 -> -π/2)", "ASin", "Asin");
            Generate_Trigonometry2("Арккосинус числа [-1, 1] (0 -> π/2, 1 -> 0, -1 -> π)", "ACos", "Acos");
            Generate_Trigonometry2("Арктангенс числа ((1, 1) -> π/4, (0, 1) -> 0, (0, -1) -> π, (1, -1) -> 3π/4)", "ATan", "Atan");
            
            Result += RFM(Other.Generate_Summary("Арктангенс по двум координатам") + "public static @ ATan2#(@ A, @ B) => @.Atan2(A, B);", Numbers_FloatDouble);
        }
        Generate_Trigonometry();

        Result += Other.Generate_Line();
        
        void Generate_Exp(){
            Result += RFM(Other.Generate_Summary("Возвращает экспоненту числа eˣ (0 -> 1, 1 -> 2.718, 2 -> 7.389)") + "public static @ Exp#(@ A) => @.Exp(A);", Numbers_FloatDouble);
        }
        Generate_Exp();
        
        Result += Other.Generate_Line();
        
        void Generate_Log(){
            Result += RFM(Other.Generate_Summary("Натуральный логарифм (1 -> 0, e -> 1, 7.389 -> 2)") + "public static @ Log#(@ A) => @.Log(A);", Numbers_FloatDouble);
            Result += RFM(Other.Generate_Summary("Логарифм, где B основание, Bʳᵉᵗᵘʳⁿ = A ((8, 2) -> 3, (100, 10) -> 2, (27, 3) -> 3)") + "public static @ Log#(@ A, @ B) => @.Log(A, B);", Numbers_FloatDouble);
            Result += RFM(Other.Generate_Summary("Логарифм с основанием 10 (10 -> 1, 100 -> 2, 1000 -> 3)") + "public static @ Log10#(@ A) => @.Log10(A);", Numbers_FloatDouble);
        }
        Generate_Log();
        
        Result += Other.Generate_Line();

        void Generate_Pow(){
            Result += RFM(Other.Generate_Summary("Возводит в степень Aᴮ (A^B)") + "public static @ Pow#(@ A, @ B) => @.Pow(A, B);", Numbers_FloatDouble);
            Result += RFM(Other.Generate_Summary("Квадратный корень") + "public static @ Sqrt#(@ A) => @.Sqrt(A);", Numbers_FloatDouble);
            Result += RFM(Other.Generate_Summary("Кубический корень") + "public static @ Cbrt#(@ A) => @.Cbrt(A);", Numbers_FloatDouble);
            Result += RFM(Other.Generate_Summary("Возводит число в квадрат") + "public static @ Sqr#(@ A) => A * A;", Numbers_NoByteShort);
            Result += RFM(Other.Generate_Summary("Возводит число в куб") + "public static @ Cube#(@ A) => A * A * A;", Numbers_NoByteShort);
        }
        Generate_Pow();
    }
}