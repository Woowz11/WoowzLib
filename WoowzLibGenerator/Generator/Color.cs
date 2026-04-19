using WLO.Attribute;
using File = WLO.File;

namespace WoowzLibGenerator.Generator;

public static class Color{
    
    public static readonly Info.ValueType[] Info_Color_Types = [Info.ValueType.Float, Info.ValueType.Double, Info.ValueType.Byte];
    
    public static readonly char[] Info_Color_Channel = ['R', 'G', 'B', 'A'];
    
    /*
     * 0 - Это 0   (0  %)
     * 1 - Это 1   (100%)
     * 5 - Это 1/2 (50 %)
     * 2 - Это 1/4 (25 %)
     * 7 - Это 3/4 (75 %)
     */
    public static readonly Info_ColorConst[] Info_Color_Constants = [
        new Info_ColorConst{ Name = "Red", Values = ['1', '0', '0', '1'] },
        new Info_ColorConst{ Name = "Orange", Values = ['1', '5', '0', '1'] },
        new Info_ColorConst{ Name = "Yellow", Values = ['1', '1', '0', '1'] },
        new Info_ColorConst{ Name = "Lime", Values = ['5', '1', '0', '1'] },
        new Info_ColorConst{ Name = "Green", Values = ['0', '1', '0', '1'] },
        new Info_ColorConst{ Name = "Aqua", Values = ['0', '1', '1', '1'] },
        new Info_ColorConst{ Name = "Water", Values = ['0', '5', '1', '1'] },
        new Info_ColorConst{ Name = "Blue", Values = ['0', '0', '1', '1'] },
        new Info_ColorConst{ Name = "Purple", Values = ['5', '0', '1', '1'] },
        new Info_ColorConst{ Name = "Magenta", Values = ['1', '0', '1', '1'] },
        new Info_ColorConst{ Name = "Brown", Values = ['5', '2', '0', '1'] },
        new Info_ColorConst{ Name = "DarkRed", Values = ['5', '0', '0', '1'] },
        new Info_ColorConst{ Name = "DarkYellow", Values = ['5', '5', '0', '1'] },
        new Info_ColorConst{ Name = "DarkGreen", Values = ['0', '5', '0', '1'] },
        new Info_ColorConst{ Name = "DarkAqua", Values = ['0', '5', '5', '1'] },
        new Info_ColorConst{ Name = "DarkBlue", Values = ['0', '0', '5', '1'] },
        new Info_ColorConst{ Name = "DarkPurple", Values = ['2', '0', '5', '1'] },
        new Info_ColorConst{ Name = "DarkMagenta", Values = ['5', '0', '5', '1'] },
        new Info_ColorConst{ Name = "Pink", Values = ['1', '5', '1', '1'] },
        new Info_ColorConst{ Name = "White", Values = ['1', '1', '1', '1'] },
        new Info_ColorConst{ Name = "Silver", Values = ['7', '7', '7', '1'] },
        new Info_ColorConst{ Name = "Gray", Values = ['5', '5', '5', '1'] },
        new Info_ColorConst{ Name = "Charcoal", Values = ['2', '2', '2', '1'] },
        new Info_ColorConst{ Name = "Black", Values = ['0', '0', '0', '1'] },
        new Info_ColorConst{ Name = "Transparent", Values = ['0', '0', '0', '0'] }
    ];
    
    public struct Info_Color{
        public Info.ValueType     Type;
        public string             Name;
        public string             Parent;
        public char[]             Channel;
        public bool               SupportFraction;
        public string             Primitive;
        public string             One;
        public string             Zero;
        public bool               HasAlpha;
        public Info_ColorConst2[] Consts;
    }
    
    public struct Info_ColorConst{
        public string Name;
        public char[] Values;
    }
    
    public struct Info_ColorConst2{
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

            for(int j = 0; j < Info_Color_Types.Length; j++){
                Info.ValueType VT = Info_Color_Types[j];
                
                string Primitive = Info.ValueType_Primitive(VT);

                string TypeSymbol = Info.ValueType_Name(VT);
                
                bool SupportFraction = VT != Info.ValueType.Byte;

                string Zero = Info.ValueType_Zero(VT);
                string One = Info.ValueType_One_Detail(VT);
                string Half = Info.ValueType_Half(VT);
                string Quar = Info.ValueType_Quarter(VT);
                string TQua = Info.ValueType_ThreeQuarter(VT);
                
                for(int i = 3; i <= Info_Color_Channel.Length; i++){
                    string Name = "Color" + i + TypeSymbol;
                    char[] Channel = Info_Color_Channel.Take(i).ToArray();
                    
                    List<Info_ColorConst2> ColorConsts = [];
                    
                    foreach(Info_ColorConst VC in Info_Color_Constants){
                        if(i == 3 && VC.Values[3] != '1'){ continue; }
                        
                        bool Add = true;
                        
                        List<string> Values = [];

                        string Convert(char c) => c switch{
                            '0' => Zero,
                            '1' => One,
                            '5' => Half,
                            '2' => Quar,
                            '7' => TQua
                        };
                        
                        for(int k = 0; k < 4; k++){
                            char Value = VC.Values[k];

                            if(k < i){
                                string Value__ = Convert(Value);

                                Values.Add(Value__);
                            }
                        }
                        
                        if(Add){
                            ColorConsts.Add(new Info_ColorConst2{
                                Name = VC.Name,
                                Values = Values.ToArray()
                            });
                        }
                    }
                    
                    CreateColor(new Info_Color{
                        Type = VT,
                        Name = Name,
                        Parent = "IEquatable<" + Name + ">",
                        Channel = Channel,
                        SupportFraction = SupportFraction,
                        Primitive = Primitive,
                        Zero = Zero,
                        One = One,
                        HasAlpha = i == 4,
                        Consts = ColorConsts.ToArray()
                    });
                } 
            }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при генерации [Color]!", e);
        }
    }
    
    // ----------------------------------------------------------------------
    
    private static string Result = "";
    public static void CreateColor(Info_Color I){
        try{
            Logger.Info("Создание цвета " + I.Name + "");
            Result = "";
            
            Result += Other.Generate_Namespace("WLO.Color");

            Result += Other.Generate_WLTag(Information.New);
            
            Result += Other.Generate_PublicStruct(I.Name, I.Parent);
            Result += "{";

            ColorContent(I);
            
            Result += "}";
            
            Result = Other.Generate_Using("System.Runtime.CompilerServices") + Result;
            Result = Other.Generate_Using("WLO.Attribute") + Result;
            
            Result = Other.Generate_GeneratorComment("Color") + Result;
            
            File FileR = WL.Explorer.File.GetOrCreate(WL.String.Path.Add(OutFolder     , I.Name + ".cs"));
            File FileD = WL.Explorer.File.GetOrCreate(WL.String.Path.Add(OutFolderDebug, I.Name + ".cs"));

            string R__ = Other.Inline(Result);
            FileD.Content = R__;
            FileR.Content = Other.Beautify(R__, AutoInline: false);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при генерации цвета [" + I.Name + "]!", e);
        }
    }

    public static void ColorContent(Info_Color I){
        string RFEA(string Code, string Between = "", char[]? Chars = null, string[]? SecondStrings = null) => Vector.RFEA2(Chars ?? I.Channel, Code, Between, SecondStrings);
        string RFEAS(string[] Strings, string Code, string Between = "") => Vector.RFEAS(Strings, Code, Between);
        
        void Generate_Constructors(){
            Result += Other.Generate_Constructor(I.Name, RFEA(I.Primitive + " @ = @2", ", ", SecondStrings: [I.Zero, I.Zero, I.Zero, I.One]), RFEA("this.@ = @;"));
        }
        Generate_Constructors();
        
        Result += Other.Generate_Line();

        void Generate_Values(){
            Result += RFEA(I.SupportFraction ? "private " + I.Primitive + " __@; public " + I.Primitive + " @{ get => __@; set{ if(value is < 0 or > 1){ throw new Exception($\"Цвет @ выходит за пределы [0, 1] у [{this}]!\\nЗначение: {value}\"); } __@ = value; } }" : "public " + I.Primitive + " @;");

            if(I.Type == Info.ValueType.Byte){
                Result += Other.Generate_NextLine();
                
                void Generate_ByteConvert(params int[] Order){
                    string Name = string.Concat(Order.Where(i => i < I.Channel.Length).Select(i => I.Channel[i]));

                    string Expr = "";
                    int[] ValidIndices = Order.Where(i => i < I.Channel.Length).ToArray();

                    for(int i = 0; i < ValidIndices.Length; i++){
                        int IDX = ValidIndices[i];
                        int Shift = 8 * (ValidIndices.Length - 1 - i);
                        Expr += I.Channel[IDX] + (Shift > 0 ? " << " + Shift : "");
                        if(i < ValidIndices.Length - 1){ Expr += " | "; }
                    }
                    
                    string SetExpr = "";
                    for(int i = 0; i < ValidIndices.Length; i++){
                        int IDX = ValidIndices[i];
                        int Shift = 8 * (ValidIndices.Length - 1 - i);
    
                        if(Shift > 0){
                            SetExpr += I.Channel[IDX] + " = (byte)((value >> " + Shift + ") & 0xFF);";
                        }else{
                            SetExpr += I.Channel[IDX] + " = (byte)(value & 0xFF);";
                        }

                        if(i < ValidIndices.Length - 1) SetExpr += " ";
                    }
                    
                    Result += "public uint " + Name + "{ get => (uint)(" + Expr + "); set{" + SetExpr + "} }";
                }
                Generate_ByteConvert(0, 1, 2, 3);
                Generate_ByteConvert(3, 2, 1, 0);

                if(I.HasAlpha){
                    Result += Other.Generate_Summary("Подходит для WINAPI") + "public uint AiBGR{get => (uint)((255 - A) << 24 | B << 16 | G << 8 | R);set{A = (byte)(255 - ((value >> 24) & 0xFF));B = (byte)((value >> 16) & 0xFF);G = (byte)((value >> 8) & 0xFF);R = (byte)(value & 0xFF);}}";
                }
            }
        }
        Generate_Values();
        
        Result += Other.Generate_Line();
        
        void Generate_Consts(){
            void Generate_Const(Info_ColorConst2 VC2){
                Result += "public static readonly " + I.Name + " " + VC2.Name + " = new " + I.Name + "(" + RFEAS(VC2.Values, "@", ", ") + ");";
            }

            foreach(Info_ColorConst2 VC2 in I.Consts){ Generate_Const(VC2); }
        }
        Generate_Consts();
        
        Result += Other.Generate_Line();

        void Generate_Extra(){
            if(I is{ Type: Info.ValueType.Byte, HasAlpha: false }){
                Result += "public string ToANSI(bool Background = false) => ANSI.ToColorANSI(Background ? ANSI.Code.Custom_BG : ANSI.Code.Custom, R, G, B);";
            }
        }
        Generate_Extra();
        
        Result += Other.Generate_Line();
        
        void Generate_Other(){
            Result += "public override string ToString() => $\"" + I.Name + "({ToShortString()})" + "\";";
            Result += "public string ToShortString() => $\"{" + RFEA("@", "}, {") + "}\";";

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
        }
        Generate_Operators();
    }
}