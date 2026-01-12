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
            GenerateColor (Path.GetFullPath(Path.Combine(MathFolder, "WLO", "Color" )));
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

                foreach(VectorType Type in Enum.GetValues(typeof(VectorType))){
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

                // VectorComponents но сокращённый под N
                object[] Components = VectorComponents.Take(N).Cast<object>().ToArray();
                
                // Первая буква типа (I, F, U, D)
                string TypeChar = VectorType.ToString()[0].ToString();

                // Название (Vector3F, Vector2U)
                string Name = "Vector" + N + TypeChar;

                // Тип (int, float, uint, double)
                string Type = VectorType.ToString().ToLower();
                
                string Result = Pre() + "\n";

                Result += "public struct " + Name + "(" + WL.String.Join(Type + " $0 = 0, ", Type + " $0 = 0", Components) + "){\n";

                Result += $$"""
                                public readonly int  N = {{N}};
                                public readonly Type T = typeof({{Type}});
                            
                            {{WL.String.Join("\tpublic " + Type + " $0;\n", Components)}}
                                #region Override
                            
                                    public override string ToString(){
                                        return "{{Name}}(" + {{WL.String.Join("$0 + \", \" + ", "$0", Components)}} + ")";
                                    }
                                    
                                    public override bool Equals(object? obj){
                                        if(obj is not {{Name}} other){ return false; }
                                        return {{WL.String.Join("$0 == other.$0 && ", "$0 == other.$0", Components)}};
                                    }
                                    
                                    public override int GetHashCode(){
                                        return HashCode.Combine({{WL.String.Join(Components)}});
                                    }
                                    
                                    public static bool operator ==({{Name}} A, {{Name}} B){
                                        return {{WL.String.Join("A.$0 == B.$0 && ", "A.$0 == B.$0", Components)}};
                                    }
                                    
                                    public static bool operator !=({{Name}} A, {{Name}} B){
                                        return !(A == B);
                                    }
                                
                                    public static {{Name}} operator +({{Name}} A, {{Name}} B){
                                        return new {{Name}}({{WL.String.Join("A.$0 + B.$0, ", "A.$0 + B.$0", Components)}});
                                    }
                                    
                                    public static {{Name}} operator +({{Name}} A, {{Type}} B){
                                        return new {{Name}}({{WL.String.Join("A.$0 + B, ", "A.$0 + B", Components)}});
                                    }
                                    
                                    public static {{Name}} operator ++({{Name}} A){
                                        return A + 1;
                                    }
                                
                                    public static {{Name}} operator -({{Name}} A, {{Name}} B){
                                        return new {{Name}}({{WL.String.Join("A.$0 - B.$0, ", "A.$0 - B.$0", Components)}});
                                    }
                                    
                                    public static {{Name}} operator -({{Name}} A, {{Type}} B){
                                        return new {{Name}}({{WL.String.Join("A.$0 - B, ", "A.$0 - B", Components)}});
                                    }
                                    
                                    public static {{Name}} operator --({{Name}} A){
                                        return A - 1;
                                    }
                                    
                                    public static {{Name}} operator *({{Name}} A, {{Name}} B){
                                        return new {{Name}}({{WL.String.Join("A.$0 * B.$0, ", "A.$0 * B.$0", Components)}});
                                    }
                                    
                                    public static {{Name}} operator *({{Name}} A, {{Type}} B){
                                        return new {{Name}}({{WL.String.Join("A.$0 * B, ", "A.$0 * B", Components)}});
                                    }
                                    
                                    public static {{Name}} operator *({{Type}} A, {{Name}} B){
                                        return B * A;
                                    }
                                
                                #endregion
                            """;
                
                Result += "\n}";

                File F = new File(Path.Combine(OutputFolder, Name + ".cs")).WriteString(Result.Replace("    ", "\t"));
            }catch(Exception e){
                throw new Exception("Произошла ошибка во время генерации вектора [" + VectorType + ", " + N + "]!", e);
            }
        }

    #endregion
    
    #region Color

        private static void GenerateColor(string OutputFolder){
            try{
                Console.WriteLine("Генерация цветов в [" + OutputFolder + "]:");

                if(!WL.Explorer.Folder.Exist(OutputFolder)){ throw new Exception("Не найдена Output папка!"); }

                WL.Explorer.Folder.Clear(OutputFolder);

                foreach(ColorType Type in Enum.GetValues(typeof(ColorType))){
                    CreateColor(OutputFolder, Type);
                }
                Console.WriteLine("Завершение генерации цветов");
            }catch(Exception e){
                throw new Exception("Произошла ошибка во время генерации цветов!", e);
            }
        }
        
        private enum ColorType{ Int, Byte, Float, Double }
        private static readonly char[] ColorComponents = ['R', 'G', 'B', 'A'];
        private static void CreateColor(string OutputFolder, ColorType ColorType){
            try{
                Console.WriteLine("\tСоздание цвета [" + ColorType + "]");

                object[] Components = ColorComponents.Cast<object>().ToArray();
                
                // Первая буква типа (I, B, U, D)
                string TypeChar = ColorType.ToString()[0].ToString();

                // Название (ColorF, ColorB)
                string Name = "Color" + TypeChar;

                // Тип (int, float, byte, double)
                string Type = ColorType.ToString().ToLower();

                const string V_0 = "0";
                string       V05 = ColorType is ColorType.Byte or ColorType.Int ? "127" : ("0.5" + (ColorType is ColorType.Float ? "f" : ""));
                string       V_1  = ColorType is ColorType.Byte or ColorType.Int ? "255" : "1";

                Dictionary<string, string[]> Constants = new Dictionary<string, string[]>{
                    {"Red"        , [ V_1, V_0, V_0, V_1 ]},
                    {"Orange"     , [ V_1, V05, V_0, V_1 ]},
                    {"Yellow"     , [ V_1, V_1, V_0, V_1 ]},
                    {"Green"      , [ V_0, V_1, V_0, V_1 ]},
                    {"Aqua"       , [ V_0, V_1, V_1, V_1 ]},
                    {"Blue"       , [ V_0, V_0, V_1, V_1 ]},
                    {"Purple"     , [ V05, V_0, V_1, V_1 ]},
                    {"Magenta"    , [ V_1, V_0, V_1, V_1 ]},
                    {"Pink"       , [ V_1, V05, V_1, V_1 ]},
                    {"White"      , [ V_1, V_1, V_1, V_1 ]},
                    {"Gray"       , [ V05, V05, V05, V_1 ]},
                    {"Black"      , [ V_0, V_0, V_0, V_1 ]},
                    {"Transparent", [ V_0, V_0, V_0, V_0 ]}
                };
                
                string Result = Pre() + "\n";

                Result += "public struct " + Name + "(" + WL.String.Join(Type + " $0 = 0, ", Type + " $0 = " + V_1, Components) + "){\n";

                Result += $$"""
                                public readonly Type T = typeof({{Type}});
                            
                            {{WL.String.Join("\tpublic " + Type + " $0;\n", Components)}}
                                public {{Name}} Set({{WL.String.Join(Type + " $0, ", Type + " $0", Components)}}){ {{WL.String.Join("this.$0 = $0; ", Components)}}return this; }
                                
                            {{WL.String.Join((i, Obj, Last) => {
                                return "\tpublic " + Name + " To" + Obj.Key + "(){ return Set(" + WL.String.Join(Obj.Value) + "); }\n" +
                                       "\tpublic static readonly " + Name + " " + Obj.Key + " = new " + Name + "().To" + Obj.Key + "();\n";
                            }, Constants)}}
                            
                                #region Override
                            
                                    public override string ToString(){
                                        return "{{Name}}(" + {{WL.String.Join("$0 + \", \" + ", "($0 == " + V_1 + " ? \"\" : $0)", Components)}} + ")";
                                    }
                                    
                                    public override bool Equals(object? obj){
                                        if(obj is not {{Name}} other){ return false; }
                                        return {{WL.String.Join("$0 == other.$0 && ", "$0 == other.$0", Components)}};
                                    }
                                    
                                    public override int GetHashCode(){
                                        return HashCode.Combine({{WL.String.Join(Components)}});
                                    }
                                    
                                    public static bool operator ==({{Name}} A, {{Name}} B){
                                        return {{WL.String.Join("A.$0 == B.$0 && ", "A.$0 == B.$0", Components)}};
                                    }
                                    
                                    public static bool operator !=({{Name}} A, {{Name}} B){
                                        return !(A == B);
                                    }
                                
                                #endregion
                            """;
                
                Result += "\n}";

                File F = new File(Path.Combine(OutputFolder, Name + ".cs")).WriteString(Result.Replace("    ", "\t"));
            }catch(Exception e){
                throw new Exception("Произошла ошибка во время генерации цвета [" + ColorType + "]!", e);
            }
        }

    #endregion
}