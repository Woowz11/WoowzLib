using File = WLO.File;

namespace WoowzLibGenerator.Generator;

public static class Vector{

    public static readonly Info.ValueType[] Info_Vector_Types = [Info.ValueType.Int, Info.ValueType.UInt, Info.ValueType.Float, Info.ValueType.Double];
    
    public static readonly char[] Info_Vector_Axis  = ['X', 'Y', 'Z', 'W'];
    
    public struct Info_Vector{
        public Info.ValueType Type;
        public int            AxisCount;
        public char[]         Axis;
        public string         Name;
        public string         Parent;
        public string         Primitive;
    }
    
    // ----------------------------------------------------------------------

    private static string OutFolder = null!;
    public static void Generate(string OutFolder__){
        try{
            OutFolder = OutFolder__; 
            WL.Explorer.Folder.GetOrCreate(OutFolder);

            foreach(Info.ValueType VT in Info_Vector_Types){
                for(int i = 2; i <= Info_Vector_Axis.Length; i++){
                    string Name = "Vector" + i + Info.ValueType_Name(VT);
                    
                    CreateVector(new Info_Vector{
                        Type = VT,
                        AxisCount = i,
                        Axis = Info_Vector_Axis.Take(i).ToArray(),
                        Name = Name,
                        Parent = "IEquatable<" + Name + ">",
                        Primitive = Info.ValueType_Primitive(VT)
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

            Result += Other.Generate_Namespace("WLO.Vector");

            Result += Other.Generate_PublicStaticClass(I.Name, I.Parent);
            Result += "{";

            VectorContent(I);
            
            Result += "}";

            Result = Other.Generate_GeneratorComment("Vector") + Result;
            
            File File = WL.Explorer.File.GetOrCreate(WL.String.Path.Add(OutFolder, I.Name + ".cs"));
            File.Content = Other.Beautify(Result);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при генерации вектора [" + I.Name + "]!", e);
        }
    }

    public static void VectorContent(Info_Vector I){
        void RepeatForEachAxis(string Code, string Between = ""){
            for(int i = 0; i < I.AxisCount; i++){
                char Axis = I.Axis[i];

                if(i != 0){ Result += Between; }
                
                Result += WL.String.Replace(Code, "@", Axis.ToString());
            }
        }

        void Generate_Constructors(){
            string Constructor = "public " + I.Name;

            Result += Constructor + "(";
            
            RepeatForEachAxis(I.Primitive + " @", ", ");

            Result += "){ ";

            RepeatForEachAxis("this.@ = @;");
            
            Result += "}";
        }
        Generate_Constructors();

        Result += Other.Generate_Line();
        
        void Generate_Values(){
            RepeatForEachAxis("public " + I.Primitive + " @ = 0;");
        }
        Generate_Values();
    }
}