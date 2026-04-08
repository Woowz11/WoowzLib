using File = WLO.File;

namespace WoowzLibGenerator.Generator;

public static class Vector{

    public static readonly Info.ValueType[] Info_Vector_Types = [Info.ValueType.Int, Info.ValueType.UInt, Info.ValueType.Float, Info.ValueType.Double/*, Info.ValueType.Decimal, Info.ValueType.Short, Info.ValueType.UShort, Info.ValueType.Long, Info.ValueType.ULong, Info.ValueType.Byte, Info.ValueType.SByte*/];
    
    public static readonly char  [] Info_Vector_Axis      = ['X', 'Y', 'Z', 'W'];
    public static readonly char  [] Info_Vector_Size      = ['W', 'H', 'D'];
    public static readonly string[] Info_Vector_Direction = ["L", "T", "R", "B"];

    /*
     * 0 - Это нулевое значение
     * 1 - Это максимальное значение (1)
     * 5 - Это 1 / 2
     * - - Это отрицательный 1
     * 2 - Это 1 * 2
     * 3 - Это 1 / 4
     * m - Это максимальное число
     */
    public static readonly Info_VectorConst[] Info_Vector_Constants = [
        new Info_VectorConst{ Name = "Zero", Values = ['0', '0'] },
        new Info_VectorConst{ Name = "One", Values = ['1', '1'], Other = '1' },
        new Info_VectorConst{ Name = "NOne", Values = ['-', '-'], Other = '-' },
        new Info_VectorConst{ Name = "Half", Values = ['5', '5'], Other = '5' },
        new Info_VectorConst{ Name = "Max", Values = ['m', 'm'], Other = 'm' },
        
        new Info_VectorConst{ Name = "Right", Values = ['1', '0'] },
        new Info_VectorConst{ Name = "Left", Values = ['-', '0'] },
        new Info_VectorConst{ Name = "Up", Values = ['0', '1'] },
        
        new Info_VectorConst{ Name = "RightTop", Values = ['1', '1'] },
        new Info_VectorConst{ Name = "RightBottom", Values = ['1', '-'] },
        new Info_VectorConst{ Name = "LeftTop", Values = ['-', '1'] },
        new Info_VectorConst{ Name = "LeftBottom", Values = ['-', '-'] },
        
        new Info_VectorConst{ Name = "Down", Values = ['0', '-'] },
        new Info_VectorConst{ Name = "Front", Values = ['0', '0', '1'] },
        new Info_VectorConst{ Name = "Back", Values = ['0', '0', '-'] },
        new Info_VectorConst{ Name = "Ana", Values = ['0', '0', '0', '1'] },
        new Info_VectorConst{ Name = "Kata", Values = ['0', '0', '0', '-'] },
        new Info_VectorConst{ Name = "AxisX", Values = ['1', '0'] },
        new Info_VectorConst{ Name = "AxisY", Values = ['0', '1'] },
        new Info_VectorConst{ Name = "AxisZ", Values = ['0', '0', '1'] },
        new Info_VectorConst{ Name = "AxisW", Values = ['0', '0', '0', '1'] },
        new Info_VectorConst{ Name = "Double", Values = ['2', '2'], Other = '2' },
        new Info_VectorConst{ Name = "Quarter", Values = ['3', '3'], Other = '3' },
    ];
    
    public struct Info_Vector{
        public Info.ValueType      Type;
        public int                 AxisCount;
        public char[]              Axis;
        public char[]              Sizes;
        public string[]            Directions;
        public bool                SupportSizes;
        public string              Name;
        public string              Parent;
        public string              Primitive;
        public string              AllAxis;
        public string              Zero;
        public bool                SupportNegative;
        public bool                SupportFraction;
        public Info_VectorConst2[] Consts;
    }

    public struct Info_VectorConst{
        public string Name;
        public char[] Values;
        public char?  Other;
    }
    
    public struct Info_VectorConst2{
        public string   Name;
        public string[] Values;
    }
    
    // ----------------------------------------------------------------------

    private static string OutFolder      = null!;
    private static string OutFolderDebug = null!;
    public static void Generate(string OutFolder__, string OutFolderDebug__){
        try{
            OutFolder = OutFolder__; 
            WL.Explorer.Folder.GetOrCreate(OutFolder);

            OutFolderDebug = OutFolderDebug__;
            WL.Explorer.Folder.GetOrCreate(OutFolderDebug);

            foreach(Info.ValueType VT in Info_Vector_Types){
                bool SupportNegative = Info.ValueType_SupportNegative(VT);
                bool SupportFraction = Info.ValueType_SupportFraction(VT);
                string Primitive = Info.ValueType_Primitive(VT);
                
                string Zero = Info.ValueType_Default(VT);
                string One  = Info.ValueType_One    (VT);
                string Half = Info.ValueType_Half   (VT);
                string Quar = Info.ValueType_Quarter(VT);
                string Duab = Info.ValueType_Double (VT);
                string Max  = Info.ValueType_Max    (VT);
                
                for(int i = 2; i <= Info_Vector_Axis.Length; i++){
                    string Name = "Vector" + i + Info.ValueType_Name(VT);
                    char[] Axis = Info_Vector_Axis.Take(i).ToArray();

                    List<Info_VectorConst2> VectorConsts = [];

                    foreach(Info_VectorConst VC in Info_Vector_Constants){
                        if(i < VC.Values.Length){ continue; }
                        
                        bool Add = true;
                        
                        List<string> Values = [];

                        string Convert(char c) => c switch{
                            '0' => Zero,
                            '1' => One,
                            '-' => "-" + One,
                            '5' => Half,
                            '3' => Quar,
                            '2' => Duab,
                            'm' => Max
                        };
                        
                        for(int j = 0; j < i; j++){
                            char Value = VC.Values.Length > j ? VC.Values[j] : VC.Other ?? '0';

                            if(!SupportNegative && Value == '-'){ Add = false; break; }
                            if(!SupportFraction && Value is '5' or '3'){ Add = false; break; }

                            string Value__ = Convert(Value);
                            if(Value__ == "~"){ Add = false; break; }
                            
                            Values.Add(Value__);
                        }
                        
                        if(Add){
                            VectorConsts.Add(new Info_VectorConst2{
                                Name = VC.Name,
                                Values = Values.ToArray()
                            });
                        }
                    }
                    
                    CreateVector(new Info_Vector{
                        Type = VT,
                        AxisCount = i,
                        Axis = Axis,
                        Name = Name,
                        Parent = "IEquatable<" + Name + ">",
                        Primitive = Primitive,
                        AllAxis = new string(Axis),
                        Zero = Zero,
                        Sizes = Info_Vector_Size.Take(i).ToArray(),
                        SupportSizes = i <= Info_Vector_Size.Length,
                        SupportNegative = SupportNegative,
                        SupportFraction = SupportFraction,
                        Consts = VectorConsts.ToArray(),
                        Directions = Info_Vector_Direction.Take(i).ToArray()
                    });
                }
            }

        }catch(Exception e){
            throw new Exception("Произошла ошибка при генерации [Vector]!", e);
        }
    }
    
    // ----------------------------------------------------------------------

    private static string Result = "";
    public static void CreateVector(Info_Vector I){
        try{
            Logger.Info("Создание вектора " + I.Name + "");
            Result = "";
            Tests.Info_Vectors.Add(I);

            Result += "/* ReSharper disable NonReadonlyMemberInGetHashCode */";
            
            Result += Other.Generate_Namespace("WLO.Vector");

            Result += Other.Generate_PublicStruct(I.Name, I.Parent);
            Result += "{";

            VectorContent(I);
            
            Result += "}";
            
            Result = Other.Generate_Using("System.Runtime.CompilerServices") + Result;
            
            Result = Other.Generate_GeneratorComment("Vector") + Result;
            
            File FileR = WL.Explorer.File.GetOrCreate(WL.String.Path.Add(OutFolder     , I.Name + ".cs"));
            File FileD = WL.Explorer.File.GetOrCreate(WL.String.Path.Add(OutFolderDebug, I.Name + ".cs"));

            string R__ = Other.Inline(Result);
            FileD.Content = R__;
            FileR.Content = Other.Beautify(R__, AutoInline: false);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при генерации вектора [" + I.Name + "]!", e);
        }
    }

    public static string RFEA(char[] Chars, string Code, string Between = "", char[]? SecondChars = null){
        string R = "";
        for(int i = 0; i < Chars.Length; i++){
            char C = Chars[i];

            if(i != 0){ R += Between; }

            string R__ = Code;
                
            if(SecondChars != null){
                R__ = WL.String.Replace(R__, "@2", SecondChars[i].ToString());
            }
                
            R += WL.String.Replace(WL.String.Replace(R__, "@I", i.ToString()), "@", C.ToString());
        }

        return R;
    }
    
    public static string RFEA2(char[] Chars, string Code, string Between = "", string[]? SecondStrings = null){
        string R = "";
        for(int i = 0; i < Chars.Length; i++){
            char C = Chars[i];

            if(i != 0){ R += Between; }

            string R__ = Code;
                
            if(SecondStrings != null){
                R__ = WL.String.Replace(R__, "@2", SecondStrings[i]);
            }
                
            R += WL.String.Replace(WL.String.Replace(R__, "@I", i.ToString()), "@", C.ToString());
        }

        return R;
    }
        
    public static string RFEAS(string[] Strings, string Code, string Between = ""){
        string R = "";
        for(int i = 0; i < Strings.Length; i++){
            string S = Strings[i];

            if(i != 0){ R += Between; }
            
            R += WL.String.Replace(WL.String.Replace(Code, "@I", i.ToString()), "@", S);
        }

        return R;
    }
    
    public static void VectorContent(Info_Vector I){
        string RFEA(string Code, string Between = "", char[]? Chars = null, char[]? SecondChars = null) => Vector.RFEA(Chars ?? I.Axis, Code, Between, SecondChars);
        string RFEA2(string Code, string Between = "", char[]? Chars = null, string[]? SecondStrings = null) => Vector.RFEA2(Chars ?? I.Axis, Code, Between, SecondStrings);
        string RFEAS(string[] Strings, string Code, string Between = "") => Vector.RFEAS(Strings, Code, Between);

        void Generate_Constructors(){
            Result += Other.Generate_Constructor(I.Name, RFEA(I.Primitive + " @", ", "), RFEA("this.@ = @;"));
            Result += Other.Generate_Constructor(I.Name, I.Primitive + " " + I.AllAxis, "", RFEA(I.AllAxis, ", "));
            Result += Other.Generate_Constructor(I.Name, "", "");
        }
        Generate_Constructors();

        Result += Other.Generate_Line();
        
        void Generate_Values(){
            Result += RFEA("public " + I.Primitive + " @;");

            if(I.SupportSizes){
                Result += Other.Generate_NextLine();
                Result += RFEA("public " + I.Primitive + " @{ get => @2; set => @2 = value; }", "", I.Sizes, I.Axis);
            }
            
            Result += Other.Generate_NextLine();
            Result += RFEA2("public " + I.Primitive + " @2{ get => @; set => @ = value; }", "", I.Axis, I.Directions);
        }
        Generate_Values();
        
        Result += Other.Generate_Line();

        void Generate_Consts(){
            void Generate_Const(Info_VectorConst2 VC2){
                Result += "public static readonly " + I.Name + " " + VC2.Name + " = new " + I.Name + "(" + RFEAS(VC2.Values, "@", ", ") + ");";
            }

            foreach(Info_VectorConst2 VC2 in I.Consts){ Generate_Const(VC2); }
        }
        Generate_Consts();
        
        Result += Other.Generate_Line();

        void Generate_Simples(){
            void Generate_Simple(string Name, string Params, string Inside){
                Result += Other.Generate_AggressiveInlining() + "public " + I.Name + " " + Name + "(" + Params + ") => " + Inside + ";";
            }

            void Generate_Simple_Pack(string Name, string Operator){
                Generate_Simple(Name, RFEA(I.Primitive + " @", ", "), "new " + I.Name + "(" + RFEA("this.@ " + Operator + " @", ", ") + ")");
                Generate_Simple(Name, I.Name + " Other", Name + "(" + RFEA("Other.@", ", ") + ")");
                Generate_Simple(Name, I.Primitive + " S", Name + "(" + RFEA("S", ", ") + ")");
            }
            Generate_Simple_Pack("Add", "+");
            Result += Other.Generate_NextLine();
            Generate_Simple_Pack("Sub", "-");
            Result += Other.Generate_NextLine();
            Generate_Simple_Pack("Mul", "*");
            Result += Other.Generate_NextLine();
            Generate_Simple_Pack("Div", "/");
        }
        Generate_Simples();
        
        Result += Other.Generate_Line();

        void Generate_Other(){
            Result += "public override string ToString() => \"" + I.Name + "(\" + ToShortString() + \")" + "\";";
            Result += "public string ToShortString() => " + RFEA("@", " + \", \" + ") + ";";
            
            if(I.SupportSizes){
                Result += "public string ToPositionString() => " + RFEA("@", " + \":\" + ") + ";";
                Result += "public string ToSizeString() => " + RFEA("@", " + \"x\" + ", I.Sizes) + ";";
            }

            Result += Other.Generate_NextLine();
            
            Result += "public bool Equals(" + I.Name + " Other) => " + RFEA("@ == Other.@", " && ") + ";";
            Result += "public override bool Equals(object? Object) => Object is " + I.Name + " Other && Equals(Other);";
            
            Result += Other.Generate_NextLine();

            Result += "public override int GetHashCode() => HashCode.Combine(" + RFEA("@", ", ") + ");";
        }
        Generate_Other();
        
        Result += Other.Generate_Line();

        void Generate_Operators(){
            Result += Other.Generate_Operator(I.Name, "==", I.Name + " L, " + I.Name + " R", "L.Equals(R)", "bool");
            Result += Other.Generate_Operator(I.Name, "!=", I.Name + " L, " + I.Name + " R", "!L.Equals(R)", "bool");

            Result += Other.Generate_NextLine();

            void Generate_Operator_Pack(string Operator, string Func){
                Result += Other.Generate_Operator(I.Name, Operator, I.Name + " L, " + I.Name + " R", "L." + Func + "(R)");
                Result += Other.Generate_Operator(I.Name, Operator, I.Name + " V, " + I.Primitive + " S", "V." + Func + "(S)");
                Result += Other.Generate_Operator(I.Name, Operator, I.Primitive + " S, " + I.Name + " V", "V " + Operator + " S");
            }
            
            Generate_Operator_Pack("+", "Add");
            Result += Other.Generate_NextLine();
            Generate_Operator_Pack("-", "Sub");
            Result += Other.Generate_NextLine();
            Generate_Operator_Pack("*", "Mul");
            Result += Other.Generate_NextLine();
            Generate_Operator_Pack("/", "Div");
        }
        Generate_Operators();
    }
}