using WLO.Attribute;
using File = WLO.File;

namespace WoowzLibGenerator.Generator;

public static class WorldTransform{
    
    public static readonly Info.ValueType[] Info_WorldTransform_Types = [Info.ValueType.Int , Info.ValueType.Float, Info.ValueType.Double];
    
    public struct Info_Transform{
        public Info.ValueType Type;
        public int            Axis;
        public string         Name;
        public string         Primitive;
        public bool           SupportFraction;
        public string         TypeSymbol;
        public string         Vector;
        public string         Transform;
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

            for(int j = 0; j < Info_WorldTransform_Types.Length; j++){
                Info.ValueType VT = Info_WorldTransform_Types[j];
                
                string Primitive  = Info.ValueType_Primitive(VT);
                
                bool SupportFraction = Info.ValueType_SupportFraction(VT);

                string TypeSymbol = Info.ValueType_Name(VT);
                
                for(int i = 2; i <= 3; i++){ // не хочу пока-что 4D, мб в будущем добавлю
                    string Transform = "Transform" + i + TypeSymbol;
                    string Name = "World" + Transform;

                    string Vector = "Vector" + i + TypeSymbol;
                    
                    CreateWorldTransform(new Info_Transform{
                        Type = VT,
                        Axis = i,
                        Name = Name,
                        Primitive = Primitive,
                        SupportFraction = SupportFraction,
                        TypeSymbol = TypeSymbol,
                        Vector = Vector,
                        Transform = Transform
                    });
                } 
            }
            
        }catch(Exception e){
            throw new Exception("Произошла ошибка при генерации [WorldTransform]!", e);
        }
    }
    
    // ----------------------------------------------------------------------
    
    private static string Result = "";
    public static void CreateWorldTransform(Info_Transform I){
        try{
            Logger.Info("Создание мировой трансформации " + I.Name + "");
            Result = "";
            
            Result += Other.Generate_Namespace("WLO.Transform");
            
            Result += Other.Generate_PublicClass(I.Name, "Metadata");
            Result += "{";

            WorldTransformContent(I);
            
            Result += "}";
            
            Result = Other.Generate_GeneratorComment("WorldTransform") + Result;
            
            File FileR = WL.Explorer.File.GetOrCreate(WL.String.Path.Add(OutFolder     , I.Name + ".cs"));
            File FileD = WL.Explorer.File.GetOrCreate(WL.String.Path.Add(OutFolderDebug, I.Name + ".cs"));

            string R__ = Other.Inline(Result);
            FileD.Content = R__;
            FileR.Content = Other.Beautify(R__, AutoInline: false);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при генерации мировой трансформации [" + I.Name + "]!", e);
        }
    }

    public static void WorldTransformContent(Info_Transform I){
        void Generate_Constructors(){
            Result += "public " + I.Name + $"(SceneNode<ITransform<{I.Name}>> Node, string Name = \"?\", object? Parent = null) : base(Name, Parent){{";

            Result += "__Node = Node;";
            
            Result += $"Local = new {I.Transform}(Name, Parent);";
            
            Result += "}";
        }
        Generate_Constructors();
        
        Result += Other.Generate_Line();

        void Generate_Values(){
            Result += $"public {I.Transform} Local{{ get; }}";

            Result += $"private SceneNode<ITransform<{I.Name}>>? __Node;";
        }
        Generate_Values();

        Result += Other.Generate_Line();

        void Generate_Settings(){
            Result += "public TransformType Type{get => Local.Type; set => Local.Type = value;}";

            Result += Other.Generate_NextLine();

            Result += "public bool SupportPosition => Local.SupportPosition;";
            Result += "public bool SupportSize···· => Local.SupportSize;";
            Result += "public bool SupportRotation => Local.SupportRotation;";
        }
        Generate_Settings();
        
        Result += Other.Generate_Line();

        void Generate_Events(){
           
        }
        Generate_Events();
        
        Result += Other.Generate_Line();

        void Generate_Private(){
            
        }
        Generate_Private();
        
        Result += Other.Generate_Line();

        void Generate_Other(){
            
        }
        Generate_Other();
    }
}