using File = WLO.File;

namespace WoowzLibGenerator.Generator;

public static class Transform{
    
    public static readonly Info.ValueType[] Info_Transform_Types = [Info.ValueType.Int , Info.ValueType.Float, Info.ValueType.Double];
    
    public struct Info_Transform{
        public Info.ValueType Type;
        public int            Axis;
        public string         Name;
        public string         Primitive;
        public bool           SupportFraction;
        public string         TypeSymbol;
        public string         Vector;
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

            for(int j = 0; j < Info_Transform_Types.Length; j++){
                Info.ValueType VT = Info_Transform_Types[j];
                
                string Primitive  = Info.ValueType_Primitive(VT);
                
                bool SupportFraction = Info.ValueType_SupportFraction(VT);

                string TypeSymbol = Info.ValueType_Name(VT);
                
                for(int i = 2; i <= 3; i++){ // не хочу пока-что 4D, мб в будущем добавлю
                    string Name = "Transform" + i + TypeSymbol;
                    
                    CreateTransform(new Info_Transform{
                        Type = VT,
                        Axis = i,
                        Name = Name,
                        Primitive = Primitive,
                        SupportFraction = SupportFraction,
                        TypeSymbol = TypeSymbol,
                        Vector = "Vector" + i + TypeSymbol
                    });
                } 
            }
            
        }catch(Exception e){
            throw new Exception("Произошла ошибка при генерации [Transform]!", e);
        }
    }
    
    // ----------------------------------------------------------------------
    
    private static string Result = "";
    public static void CreateTransform(Info_Transform I){
        try{
            Logger.Info("Создание трансформации " + I.Name + "");
            Result = "";
            
            Result += Other.Generate_Namespace("WLO.Transform");
            
            Result += Other.Generate_PublicClass(I.Name);
            Result += "{";

            TransformContent(I);
            
            Result += "}";
            
            Result = Other.Generate_Using("WLO.Vector") + Result;
            
            Result = Other.Generate_GeneratorComment("Transform") + Result;
            
            File FileR = WL.Explorer.File.GetOrCreate(WL.String.Path.Add(OutFolder     , I.Name + ".cs"));
            File FileD = WL.Explorer.File.GetOrCreate(WL.String.Path.Add(OutFolderDebug, I.Name + ".cs"));

            string R__ = Other.Inline(Result);
            FileD.Content = R__;
            FileR.Content = Other.Beautify(R__, AutoInline: false);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при генерации трансформации [" + I.Name + "]!", e);
        }
    }

    public static void TransformContent(Info_Transform I){
        void Generate_Constructors(){
            
        }
        Generate_Constructors();
        
        Result += Other.Generate_Line();

        void Generate_Values(){
            Result += $"public readonly ReactiveProperty<{I.Vector}> Position = new ReactiveProperty<{I.Vector}>();";
            Result += $"public readonly ReactiveProperty<{I.Vector}> Size = new ReactiveProperty<{I.Vector}>();";
        }
        Generate_Values();

        Result += Other.Generate_Line();

        void Generate_Settings(){
            Result += "public TransformType Type = TransformType.All;";

            Result += Other.Generate_NextLine();

            Result += "public bool SupportPosition => (Type & TransformType.Position) != 0;";
            Result += "public bool SupportSize···· => (Type & TransformType.Size····) != 0;";
            Result += "public bool SupportRotation => (Type & TransformType.Rotation) != 0;";
        }
        Generate_Settings();
    }
}