using File = WLO.File;

namespace WoowzLibGenerator.Generator;

public static class Rect{
    
    public static readonly Info.ValueType[] Info_Rect_Types   = [Info.ValueType.Int , Info.ValueType.Float, Info.ValueType.Double];
    public static readonly Info.ValueType[] Info_Rect_Types_S = [Info.ValueType.UInt, Info.ValueType.Float, Info.ValueType.Double];
    
    public static readonly char[] Info_Rect_Position = ['X', 'Y', 'Z'];
    public static readonly char[] Info_Rect_Size     = ['W', 'H', 'D'];
    
    public struct Info_Rect{
        public Info.ValueType Type;
        public Info.ValueType TypeSize;
        public string         Name;
        public string         Parent;
        public char[]         Position;
        public char[]         Size;
        public string         Primitive;
        public string         PrimitiveSize;
        public bool           SupportFraction;
        public bool           Three;
        public string         TypeSymbol;
        public string         VectorPosition;
        public string         VectorSize;
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

            for(int j = 0; j < Info_Rect_Types.Length; j++){
                Info.ValueType VT  = Info_Rect_Types  [j];
                Info.ValueType VTS = Info_Rect_Types_S[j];
                
                string Primitive  = Info.ValueType_Primitive(VT);
                string PrimitiveS = Info.ValueType_Primitive(VTS);
                
                bool SupportFraction = Info.ValueType_SupportFraction(VT);

                string TypeSymbol     = Info.ValueType_Name(VT);
                string TypeSizeSymbol = Info.ValueType_Name(VTS);
                
                for(int i = 2; i <= Info_Rect_Position.Length; i++){
                    string Name = "Rect" + i + TypeSymbol;
                    char[] Position = Info_Rect_Position.Take(i).ToArray();
                    char[] Size     = Info_Rect_Size    .Take(i).ToArray();
                    
                    CreateRect(new Info_Rect{
                        Type = VT,
                        TypeSize = VTS,
                        Name = Name,
                        Parent = "IEquatable<" + Name + ">",
                        Position = Position,
                        Size = Size,
                        Primitive = Primitive,
                        PrimitiveSize = PrimitiveS,
                        SupportFraction = SupportFraction,
                        Three = i >= 3,
                        TypeSymbol = TypeSymbol,
                        VectorPosition = "Vector" + i + TypeSymbol,
                        VectorSize = "Vector" + i + TypeSizeSymbol
                    });
                } 
            }
            
        }catch(Exception e){
            throw new Exception("Произошла ошибка при генерации [Rect]!", e);
        }
    }
    
    // ----------------------------------------------------------------------
    
    private static string Result = "";
    public static void CreateRect(Info_Rect I){
        try{
            Logger.Info("Создание прямоугольника " + I.Name + "");
            Result = "";
            
            Result += Other.Generate_Namespace("WLO.Rect");

            Result += Other.Generate_Summary("Прямоугольник, счёт позиции идёт с нижнего левого угла!");
            
            Result += Other.Generate_PublicStruct(I.Name, I.Parent);
            Result += "{";

            RectContent(I);
            
            Result += "}";
            
            Result = Other.Generate_Using("System.Runtime.CompilerServices") + Result;
            Result = Other.Generate_Using("WLO.Vector") + Result;
            
            Result = Other.Generate_GeneratorComment("Rect") + Result;
            
            File FileR = WL.Explorer.File.GetOrCreate(WL.String.Path.Add(OutFolder     , I.Name + ".cs"));
            File FileD = WL.Explorer.File.GetOrCreate(WL.String.Path.Add(OutFolderDebug, I.Name + ".cs"));

            string R__ = Other.Inline(Result);
            FileD.Content = R__;
            FileR.Content = Other.Beautify(R__, AutoInline: false);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при генерации прямоугольника [" + I.Name + "]!", e);
        }
    }

    public static string RFEAwithType(char[] Chars, string Type, string Code, string Between = "", char[]? SecondChars = null){
        string R = "";
        for(int i = 0; i < Chars.Length; i++){
            char C = Chars[i];

            if(i != 0){ R += Between; }

            string R__ = Code;
                
            if(SecondChars != null){
                R__ = WL.String.Replace(R__, "@2", SecondChars[i].ToString());
            }
            
            R += WL.String.Replace(WL.String.Replace(WL.String.Replace(R__, "#", Type), "@I", i.ToString()), "@", C.ToString());
        }

        return R;
    }
    
    public static void RectContent(Info_Rect I){
        string RFEA(bool Position, string Code, string Between = "", char[]? Chars = null, char[]? SecondChars = null) => Rect.RFEAwithType(Chars ?? (Position ? I.Position : I.Size), Position ? I.Primitive : I.PrimitiveSize, Code, Between, SecondChars);

        void Generate_Constructors(){
            string Constructor = "public " + I.Name;

            void Generate_Constructor(string Params, string Inside, string? Base = null) => Result += Constructor + "(" + Params + ")" + (Base != null ? " : this(" + Base + ")" : "") + "{" + Inside + "}";
            
            Generate_Constructor(I.Primitive + " " + I.Position[0] + ", " + I.Primitive + " " + I.Position[1] + (I.Three ? ", " + I.Primitive + " " + I.Position[2] : "") + ", " + I.PrimitiveSize + " " + I.Size[0] + ", " + I.PrimitiveSize + " " + I.Size[1] + (I.Three ? ", " + I.PrimitiveSize + " " + I.Size[2] : ""), "this." + I.Position[0] + " = " + I.Position[0] + ";" + "this." + I.Position[1] + " = " + I.Position[1] + ";" + (I.Three ? "this." + I.Position[2] + " = " + I.Position[2] + ";" : "") + "this." + I.Size[0] + " = " + I.Size[0] + ";" + "this." + I.Size[1] + " = " + I.Size[1] + ";" + (I.Three ? "this." + I.Size[2] + " = " + I.Size[2] + ";" : ""));
            Generate_Constructor(I.VectorPosition + " Position, " + I.VectorSize + " Size", "this.Position = Position; this.Size = Size;");
            Generate_Constructor("", "");
        }
        Generate_Constructors();
        
        Result += Other.Generate_Line();
        
        void Generate_Values(){
            Result += RFEA(true , "public # @;");
            Result += RFEA(false, I.SupportFraction ? "private # __@; public # @{ get => __@; set{ if(value < 0){ throw new Exception(\"Значение @ не может быть < 0 в \" + this + \"!\"); } __@ = value; } }" : "public # @;");

            Result += Other.Generate_NextLine();

            void Generate_Value(string Name, string Position, string Size, bool Alt, string AltName = ""){
                string Convert     = (I.SupportFraction ? "" : "(" + I.Primitive + ")");
                string ConvertSize = (I.SupportFraction ? "" : "(" + I.PrimitiveSize + ")");
                Result += "public " + I.Primitive + " " + Name + "{ get => " + (Alt ? Position : Position + " + " + Convert + Size) + "; set{ " + (Alt ? I.Primitive + " Old" + AltName + " = " + AltName + "; " + Position + " = value; " + Size + " = " + ConvertSize + "WL.Math.Max" + I.TypeSymbol + "(0, Old" + AltName + " - " + Position + ");" : Size + " = " + ConvertSize + "WL.Math.Max" + I.TypeSymbol + "(0, value - " + Position + ");") + " } }";
            }

            void Generate_Value2(string Name, string Name2, string Position, string Size){
                Generate_Value(Name, Position, Size, true, Name2);
                Generate_Value(Name2, Position, Size, false);
            }

                         Generate_Value2("Left", "Right", "X", "W");
                         Generate_Value2("Bottom", "Top", "Y", "H");
            if(I.Three){ Generate_Value2("Back", "Front", "Z", "D"); }
            
            Result += Other.Generate_NextLine();

            void Generate_Value3(string VectorType, string Name, char[] Chars){
                Result += "public " + VectorType + " " + Name + "{ get => new " + VectorType + "(" + Chars[0] + ", " + Chars[1] + (I.Three ? ", " + Chars[2] : "") + "); set{ " + Chars[0] + " = value." + Chars[0] + "; " + Chars[1] + " = value." + Chars[1] + "; " + (I.Three ? Chars[2] + " = value." + Chars[2] + ";" : "") + "} }";
            }
            Generate_Value3(I.VectorPosition, "Position", I.Position);
            Generate_Value3(I.VectorSize, "Size", I.Size);
        }
        Generate_Values();

        Result += Other.Generate_Line();

        void Generate_Other(){
            Result += "public override string ToString() => \"" + I.Name + "(\" + ToShortString() + \")" + "\";";
            Result += "public string ToShortString() => Position.ToPositionString() + \", \" + Size.ToSizeString();";
            
            Result += Other.Generate_NextLine();
            
            Result += "public bool Equals(" + I.Name + " Other) => " + I.Position[0] + " == Other." + I.Position[0] + " && " + I.Position[1] + " == Other." + I.Position[1] + (I.Three ? " && " + I.Position[2] + " == Other." + I.Position[2] : "") + " && " + I.Size[0] + " == Other." + I.Size[0] + " && " + I.Size[1] + " == Other." + I.Size[1] + (I.Three ? " && " + I.Size[2] + " == Other." + I.Size[2] : "") + ";";
            Result += "public override bool Equals(object? Object) => Object is " + I.Name + " Other && Equals(Other);";
            
            Result += Other.Generate_NextLine();

            Result += "public override int GetHashCode() => HashCode.Combine(" + RFEA(true, "@", ", ") + ", " + RFEA(false, "@", ", ") + ");";
        }
        Generate_Other();
        
        Result += Other.Generate_Line();

        void Generate_Operators(){
            void Generate_Operator(string Operator, string Params, string Inside, string? Return = null, bool Lambda = true){
                Return ??= I.Name;
                Result += Other.Generate_AggressiveInlining() + "public static " + Return + " operator " + Operator + "(" + Params + ")" + (Lambda ? " => " : "{") + Inside + (Lambda ? ";" : "}");
            }
            
            Generate_Operator("==", I.Name + " L, " + I.Name + " R", "L.Equals(R)", "bool");
            Generate_Operator("!=", I.Name + " L, " + I.Name + " R", "!L.Equals(R)", "bool");
        }
        Generate_Operators();
    }
}