using System;
using WLO;
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

            GlobalInfo = $$"""
                           /// Сгенерировано через GeneratorWoowzLib!
                           /// Сгенерирован: {{WL.Math.Time.Format("g")}}
                           """;
            
            string MathFolder = Path.GetFullPath(Path.Combine(BaseFolder, "WoowzLib.Math"));
            string ByteFolder = Path.GetFullPath(Path.Combine(BaseFolder, "WoowzLib.Byte"));
            
            GenerateVector (Path.GetFullPath(Path.Combine(MathFolder, "WLO", "Vector" )));
            GenerateColor  (Path.GetFullPath(Path.Combine(MathFolder, "WLO", "Color"  )));
            GenerateRect   (Path.GetFullPath(Path.Combine(MathFolder, "WLO", "Rect"   )));
            GenerateMassive(Path.GetFullPath(Path.Combine(ByteFolder, "WLO", "Massive")));
        }catch(Exception e){
            throw new Exception("Произошла ошибка во время генерации!", e);
        }

        return 0;
    }

    private static string GlobalInfo;

    private static string PreComment(){
        return $"""
               /// <summary>
               {GlobalInfo}
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
                Logger.Info("Генерация векторов в [" + OutputFolder + "]:");

                if(!WL.Explorer.Folder.Exist(OutputFolder)){ throw new Exception("Не найдена Output папка!"); }

                WL.Explorer.Folder.Clear(OutputFolder);

                foreach(VectorType Type in Enum.GetValues(typeof(VectorType))){
                    for(int i = 2; i <= 4; i++){
                        CreateVector(OutputFolder, Type, i);
                    }
                }
                Logger.Info("Завершение генерации векторов");
            }catch(Exception e){
                throw new Exception("Произошла ошибка во время генерации векторов!", e);
            }
        }
        
        private enum VectorType{ Int, UInt, Float, Double }
        private static readonly char[] VectorComponents = ['X', 'Y', 'Z', 'W'];
        private static void CreateVector(string OutputFolder, VectorType VectorType, int N){
            try{
                Logger.Info("\tСоздание вектора [" + VectorType + ", " + N + "]");

                // VectorComponents но сокращённый под N
                object[] Components = VectorComponents.Take(N).Cast<object>().ToArray();
                
                // Первая буква типа (I, F, D)
                string TypeChar = VectorType.ToString()[0].ToString();

                // Название (Vector3F, Vector2U)
                string Name = "Vector" + N + TypeChar;

                // Тип (int, float, double)
                string Type = VectorType.ToString().ToLower();
                
                bool NoMinus = VectorType is VectorType.UInt;
                
                const string V_0 =  "0";
                const string V_1 =  "1";
                const string VM1 = "-1";

                Dictionary<string, string[]> Constants2 = new Dictionary<string, string[]>{
                    {"Zero"       , [ V_0, V_0, V_0, V_0 ]},
                    {"One"        , [ V_1, V_1, V_1, V_1 ]},
                    {"MOne"       , [ VM1, VM1, VM1, VM1 ]},
                    {"Right"      , [ V_1, V_0, V_0, V_0 ]},
                    {"Left"       , [ VM1, V_0, V_0, V_0 ]},
                    {"Up"         , [ V_0, V_1, V_0, V_0 ]},
                    {"Down"       , [ V_0, VM1, V_0, V_0 ]}
                };
                Dictionary<string, string[]> Constants3 = new Dictionary<string, string[]>{
                    {"Front"       , [ V_0, V_0, V_1, V_0 ]},
                    {"Back"        , [ V_0, V_0, VM1, V_0 ]}
                };
                Dictionary<string, string[]> Constants4 = new Dictionary<string, string[]>{
                    {"Ana"       , [ V_0, V_0, V_0, V_1 ]},
                    {"Kata"      , [ V_0, V_0, V_0, VM1 ]}
                };

                Dictionary<string, string[]> AllConstants = new[]{ Constants2, Constants3, Constants4 }
                                                            .Take(Math.Max(0, N - 1))
                                                            .SelectMany(D => D)
                                                            .Where(P => !NoMinus || !P.Value.Contains(VM1))
                                                            .ToDictionary(P => P.Key, P => P.Value.Take(N).ToArray());
                
                string Result = Pre() + "\n";

                Result += "public struct " + Name + "{\n";

                Result += $$"""
                                public static readonly int  Numbers = {{N}};
                                public static readonly Type Type····= typeof({{Type}});
                            
                                public {{Name}}({{WL.String.Join(Type + " $0 = 0, ", Type + " $0 = 0", Components)}}){
                                    {{WL.String.Join("this.$0 = $0; ", Components)}}
                                }
                            
                            {{WL.String.Join("\tpublic " + Type + " $0;\n", Components)}}
                                public {{Name}} Set({{WL.String.Join(Type + " $0, ", Type + " $0", Components)}}){ {{WL.String.Join("this.$0 = $0; ", Components)}}return this; }
                                    
                            {{WL.String.Join((i, Obj, Last) => {
                                return "\tpublic " + Name + " To" + Obj.Key + "(){ return Set(" + WL.String.Join(Obj.Value) + "); }\n" +
                                       "\tpublic static " + Name + " " + Obj.Key + " => new " + Name + "().To" + Obj.Key + "();\n";
                            }, AllConstants)}}
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

                File F = new File(Path.Combine(OutputFolder, Name + ".cs")).WriteString(Result.Replace("    ", "\t").Replace('·',' '));
            }catch(Exception e){
                throw new Exception("Произошла ошибка во время генерации вектора [" + VectorType + ", " + N + "]!", e);
            }
        }

    #endregion
    
    #region Color

        private static void GenerateColor(string OutputFolder){
            try{
                Logger.Info("Генерация цветов в [" + OutputFolder + "]:");

                if(!WL.Explorer.Folder.Exist(OutputFolder)){ throw new Exception("Не найдена Output папка!"); }

                WL.Explorer.Folder.Clear(OutputFolder);

                foreach(ColorType Type in Enum.GetValues(typeof(ColorType))){
                    CreateColor(OutputFolder, Type);
                }
                Logger.Info("Завершение генерации цветов");
            }catch(Exception e){
                throw new Exception("Произошла ошибка во время генерации цветов!", e);
            }
        }
        
        private enum ColorType{ Int, Byte, Float, Double }
        private static readonly char[] ColorComponents = ['R', 'G', 'B', 'A'];
        private static void CreateColor(string OutputFolder, ColorType ColorType){
            try{
                Logger.Info("\tСоздание цвета [" + ColorType + "]");

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

                Result += "public struct " + Name + "{\n";

                Result += $$"""
                                public static readonly Type Type = typeof({{Type}});
                            
                                public {{Name}}({{WL.String.Join(Type + " $0 = 0, ", Type + " $0 = " + V_1, Components)}}){
                                    {{WL.String.Join("this.$0 = $0; ", Components)}}
                                }
                            
                            {{WL.String.Join("\tpublic " + Type + " $0;\n", Components)}}
                                public {{Name}} Set({{WL.String.Join(Type + " $0, ", Type + " $0", Components)}}){ {{WL.String.Join("this.$0 = $0; ", Components)}}return this; }
                                
                            {{WL.String.Join((i, Obj, Last) => {
                                return "\tpublic " + Name + " To" + Obj.Key + "(){ return Set(" + WL.String.Join(Obj.Value) + "); }\n" +
                                       "\tpublic static " + Name + " " + Obj.Key + " => new " + Name + "().To" + Obj.Key + "();\n";
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
    
    #region Rect

        private static void GenerateRect(string OutputFolder){
            try{
                Logger.Info("Генерация Rect в [" + OutputFolder + "]:");

                if(!WL.Explorer.Folder.Exist(OutputFolder)){ throw new Exception("Не найдена Output папка!"); }

                WL.Explorer.Folder.Clear(OutputFolder);

                foreach(RectType Type in Enum.GetValues(typeof(RectType))){
                    CreateRect(OutputFolder, Type);
                }
                Logger.Info("Завершение генерации Rect");
            }catch(Exception e){
                throw new Exception("Произошла ошибка во время генерации Rect!", e);
            }
        }
        
        private enum RectType{ Int, Float, Double }
        private static void CreateRect(string OutputFolder, RectType RectType){
            try{
                Logger.Info("\tСоздание Rect [" + RectType + "]");
                
                // Первая буква типа (I, F, D)
                string TypeChar = RectType.ToString()[0].ToString();

                // Название (RectF, RectI)
                string Name = "Rect" + TypeChar;

                // Тип (int, float, double)
                string Type = RectType.ToString().ToLower();
                
                string Result = Pre() + "\n";

                Result += "public struct " + Name + "{\n";

                Result += $$"""
                                public static readonly Type Type = typeof({{Type}});
                            
                                public {{Name}}({{Type}} X, {{Type}} Y, {{Type}} Width, {{Type}} Height){
                                    this.X = X; this.Y = Y; this.Width = Width; this.Height = Height;
                                }
                                public {{Name}}({{Type}} Width, {{Type}} Height){
                                    this.Width = Width; this.Height = Height;
                                }
                                public {{Name}}(){
                                    Width = 128; Height = 128;
                                }
                            
                                public {{Type}} X;
                                public {{Type}} Y;
                                
                                public {{Type}} Width {
                                    get => __Width;
                                    set{
                                        if(__Width == value){ return; }
                                    
                                        if(value <= 0){ throw new Exception("Ширина не может быть <= 0 у [" + this + "]!"); }
                                        __Width = value;
                                    }
                                }
                                private {{Type}} __Width;
                                
                                public {{Type}} Height {
                                    get => __Height;
                                    set{
                                        if(__Height == value){ return; }
                                    
                                        if(value <= 0){ throw new Exception("Высота не может быть <= 0 у [" + this + "]!"); }
                                        __Height = value;
                                    }
                                }
                                private {{Type}} __Height;
                            
                                #region Override
                            
                                    public override string ToString(){
                                        return "{{Name}}(" + X + ":" + Y + ", " + Width + "x" + Height + ")";
                                    }
                                    
                                    public override bool Equals(object? obj){
                                        if(obj is not {{Name}} other){ return false; }
                                        return X == other.X && Y == other.Y && Width == other.Width && Height == other.Height;
                                    }
                                    
                                    public override int GetHashCode(){
                                        return HashCode.Combine(X, Y, Width, Height);
                                    }
                                    
                                    public static bool operator ==({{Name}} A, {{Name}} B){
                                        return A.X == B.X && A.Y == B.Y && A.Width == B.Width && A.Height == B.Height;
                                    }
                                    
                                    public static bool operator !=({{Name}} A, {{Name}} B){
                                        return !(A == B);
                                    }
                                #endregion
                            """;
                
                Result += "\n}";

                File F = new File(Path.Combine(OutputFolder, Name + ".cs")).WriteString(Result.Replace("    ", "\t").Replace('·',' '));
            }catch(Exception e){
                throw new Exception("Произошла ошибка во время генерации Rect [" + RectType + "]!", e);
            }
        }

    #endregion
    
    #region Massive

        private static void GenerateMassive(string OutputFolder){
            try{
                Logger.Info("Генерация массивов в [" + OutputFolder + "]:");

                if(!WL.Explorer.Folder.Exist(OutputFolder)){ throw new Exception("Не найдена Output папка!"); }

                WL.Explorer.Folder.Clear(OutputFolder);

                foreach(MassiveType Type in Enum.GetValues(typeof(MassiveType))){
                    CreateMassive(OutputFolder, Type);
                }
                Logger.Info("Завершение генерации массивов");
            }catch(Exception e){
                throw new Exception("Произошла ошибка во время генерации массивов!", e);
            }
        }
        
        private enum MassiveType{ Byte, Short, UShort, Int, UInt, Long, ULong, Float, Double, Char, T }
        private static void CreateMassive(string OutputFolder, MassiveType MassiveType){
            try{
                Logger.Info("\tСоздание массива [" + MassiveType + "]");
                
                // Первая буква типа (I, F, D)
                string TypeChar = MassiveType switch{
                    MassiveType.UShort => "US",
                    MassiveType.ULong  => "UL",
                                     _ => MassiveType.ToString()[0].ToString()
                };

                bool Custom = MassiveType is MassiveType.T;
                
                // Название (MassiveF, MassiveU)
                string Name = "Massive" + (Custom ? "<T>" : TypeChar);

                string NameWithoutT = Custom ? "Massive" : Name;
                
                // Тип (int, float, double)
                string Type = Custom ? "T" : MassiveType.ToString().ToLower();
                
                string Result = Pre() + "\n";

                Result += "public struct " + Name + " : ByteObject" + (Custom ? " where T : unmanaged" : "") + "{\n";

                Result += $$"""
                                // надо добавить sha256...
                            
                                public {{NameWithoutT}}(){
                                    Data = [];
                                    AutoSize = true;
                                }
                            
                                public {{NameWithoutT}}(int Size, bool AutoSize = true){
                                    if(Size < 0){ throw new Exception("Размер не может быть < 0!"); }
                                    Data = new {{Type}}[Size];
                                    this.AutoSize = AutoSize;
                                }
                                
                                public {{NameWithoutT}}({{Type}}[] Data, bool AutoSize = true){
                                    this.Data = Data ?? throw new Exception("Задан пустой массив!");
                                    this.AutoSize = AutoSize;
                                }
                            
                                public {{Type}}[] Data;
                                
                                public int Size => Data.Length;
                                
                                public bool AutoSize;
                                
                                public ref {{Type}} this[int Index]{
                                    get{
                                        if(Index < 0){ throw new Exception("Индекс < 0!"); }
                                        if(Index >= Size){
                                            if(AutoSize){
                                                EnsureSize(Index);
                                            }else{
                                                throw new Exception("Индекс выходит за пределы у таблицы [" + this + "]! Индекс: " + Index);
                                            }
                                        }
                                        return ref Data[Index];
                                    }
                                }
                                
                                public {{Name}} Set({{Type}}[] Data){
                                    try{
                                        this.Data = new {{Type}}[Data.Length];
                                        Array.Copy(Data, this.Data, Data.Length);
                                        
                                        return this;
                                    }catch(Exception e){
                                        throw new Exception("Произошла ошибка при установке значений в массив [" + this + "]!\nЗначения: " + Data, e);
                                    }
                                }
                                
                                public {{Name}} SetSlice(int Index, {{Type}}[] Data){
                                    try{
                                        if(Index < 0){ throw new Exception("Индекс < 0!"); }
                            
                                        int EndIndex = Index + Data.Length - 1;
                                        
                                        if(EndIndex >= Size){
                                            if(AutoSize){
                                                EnsureSize(EndIndex);
                                            }else{
                                                throw new Exception("Индекс выходит за пределы!");
                                            }
                                        }
                                        
                                        Array.Copy(Data, 0, this.Data, Index, Data.Length);
                                        return this;
                                    }catch(Exception e){
                                        throw new Exception("Произошла ошибка при установке части значений в массив [" + this + "]!\nИндекс: " + Index + "\nЗначения: " + Data, e);
                                    }
                                }
                                
                                public {{Type}}[] GetSlice(int Index, int EndIndex){
                                    try{
                                        if(Index < 0 || EndIndex < Index){ throw new Exception("Неверный диапазон! Index < 0 || EndIndex < Index"); }
                                        if(EndIndex >= Size){
                                            if(AutoSize){
                                                EnsureSize(EndIndex);
                                            }else{
                                                throw new Exception("Диапазон выходит за границы! EndIndex >= Size");
                                            }
                                        }
                                        
                                        int L = EndIndex - Index + 1;
                                        {{Type}}[] Slice = new {{Type}}[L];
                                        Array.Copy(Data, Index, Slice, 0, L);
                                        return Slice;
                                    }catch(Exception e){
                                        throw new Exception("Произошла ошибка при получении части от массива [" + this + "]!\nДиапазон: " + Index + "-" + EndIndex, e);
                                    }
                                }
                                
                                public {{Name}} Resize(int NewSize){
                                    try{
                                        Array.Resize(ref Data, NewSize);
                                    }catch(Exception e){
                                        throw new Exception("Произошла ошибка при изменении размера у массива [" + this + "]!\nНовый размер: " + NewSize, e);
                                    }
                                    
                                    return this;
                                }
                                
                                /// <summary>
                                /// Увеличивает размер массива, если индекс выходит за края (в указанное кол-во раз)
                                /// </summary>
                                public {{Name}} EnsureSize(int Index, int HowMuch = 2){
                                    try{
                                        int Required = Index + 1;
                                        Resize(Size == 0 ? Required : Math.Max(Size * HowMuch, Required));
                                    }catch(Exception e){
                                        throw new Exception("Произошла ошибка при увеличении размера у массива [" + this + "]!\nИндекс: " + Index + "\nНа сколько?: " + HowMuch, e);
                                    }
                                    
                                    return this;
                                }
                            
                                public Span<{{Type}}> AsSpan{
                                    get => Data;
                                    set{
                                        if(value.Length != Size){
                                            if(AutoSize){
                                                Resize(value.Length);
                                            }else{
                                                throw new Exception("Размеры Span и массива различаются!");
                                            }
                                        }
                                        
                                        value.CopyTo(Data);
                                    }
                                }
                            
                                #region Override
                            
                                   public override string ToString(){
                                       return "{{Name}}(0-" + (Size - 1) + ", " + AutoSize + ")";
                                   }
                                   
                                   public int ByteSize(){
                                       return Size * {{WL.WoowzLib.Condition(Custom, "WL.Byte.Size(typeof(T))", "sizeof(" + Type + ")")}}; 
                                   }
                            
                                #endregion
                            """;
                
                Result += "\n}";

                File F = new File(Path.Combine(OutputFolder, NameWithoutT + ".cs")).WriteString(Result.Replace("    ", "\t").Replace('·',' '));
            }catch(Exception e){
                throw new Exception("Произошла ошибка во время генерации массива [" + MassiveType + "]!", e);
            }
        }

    #endregion
}