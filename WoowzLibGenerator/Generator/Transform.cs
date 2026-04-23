using WLO.Attribute;
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
        public string         PositionType;
        public string         SizeType;
        public string         RotationType;
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

                    string Vector = "Vector" + i + TypeSymbol;
                    
                    CreateTransform(new Info_Transform{
                        Type = VT,
                        Axis = i,
                        Name = Name,
                        Primitive = Primitive,
                        SupportFraction = SupportFraction,
                        TypeSymbol = TypeSymbol,
                        Vector = Vector,
                        
                        PositionType = Vector,
                        SizeType = Vector,
                        RotationType = "bool"
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
            Tests.Info_Transforms.Add(I);
            
            Result += Other.Generate_Namespace("WLO.Transform");
            
            Result += Other.Generate_PublicClass(I.Name, "Metadata");
            Result += "{";

            TransformContent(I);
            
            Result += "}";
            
            Result = Other.Generate_Using("WLO.Vector") + Result;
            Result = Other.Generate_Using("WLO.Attribute") + Result;
            
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
            Result += "public " + I.Name + "(string Name = \"?\", object? Parent = null) : base(Name, Parent){";

            void Generate_ConstructorValue(string Name, string BeautifulName, string Type, string? Default = null){
                Result += $"{Name} = new ReactiveProperty<{Type}>(\"{BeautifulName}\", this{(Default == null ? "" : $", {Default}")});";
            }
            
            Generate_ConstructorValue("Position", "Позиция", I.PositionType);
            Generate_ConstructorValue("Size"    , "Размер" , I.SizeType, I.SizeType + ".One");
            Generate_ConstructorValue("Rotation", "Поворот", I.RotationType, "false");

            void Generate_ConstructorEvents(string Name, string ErrorName, string Type){
                void F(string Event, string Values, string Return, bool OnChanged = false){
                    Result += $$"""{{Name}}.{{Event}} += ({{Values}}) => { {{(OnChanged ? $"__InvokeOnChanged(\"{Name}\");" : $"if(!Support{Name}){{ throw new Exception(\"Не поддерживает {ErrorName}!\"); }} return {Return};")}} };""";
                }
                
                F("OnApply"  , "_, V", $"Cancellable<{Type}>.Continue(V)");
                F("OnGet"    , "V", "V");
                F("OnChanged", "_, V", "", true);
            }
            
            Generate_ConstructorEvents("Position", "позицию", I.PositionType);
            Generate_ConstructorEvents("Size", "размер", I.SizeType);
            Generate_ConstructorEvents("Rotation", "поворот", I.RotationType);
            
            Result += "}";
        }
        Generate_Constructors();
        
        Result += Other.Generate_Line();

        void Generate_Values(){
            Result += $"public readonly ReactiveProperty<{I.PositionType}> Position;";
            Result += $"public readonly ReactiveProperty<{I.SizeType}> Size;";
            Result += Other.Generate_WLTag(Information.WorkInProgress);
            Result += $"public readonly ReactiveProperty<{I.RotationType}> Rotation;";

            Result += Other.Generate_NextLine();

            Result += "private bool __Dirty;";
        }
        Generate_Values();

        Result += Other.Generate_Line();

        void Generate_Settings(){
            Result += "public TransformType Type = TransformType.All;";

            Result += Other.Generate_NextLine();

            Result += "public bool SupportPosition => Flag.Contains(Type, TransformType.Position);";
            Result += "public bool SupportSize···· => Flag.Contains(Type, TransformType.Size····);";
            Result += "public bool SupportRotation => Flag.Contains(Type, TransformType.Rotation);";
        }
        Generate_Settings();
        
        Result += Other.Generate_Line();

        void Generate_Events(){
            Result += $"public event Action<{I.Vector}?, {I.Vector}?, bool?>? OnChanged;";
        }
        Generate_Events();
        
        Result += Other.Generate_Line();

        void Generate_Private(){
            Result += "private void __InvokeOnChanged(string Name){if(__Dirty){ return; } __Dirty = true; try{OnChanged?.Invoke(SupportPosition ? Position.Value : null, SupportSize ? Size.Value : null, SupportRotation ? Rotation.Value : null);}catch(Exception e){ throw new Exception($\"Произошла ошибка при вызове ивента OnChanged у {Name} [{this}]!\", e); }finally{ __Dirty = false; }}";
        }
        Generate_Private();
        
        Result += Other.Generate_Line();

        void Generate_Other(){
            Result += "public override string ToString() => $\"" + I.Name + "({ToShortString()})" + "\";";
            Result += "public string ToShortString() => !SupportPosition && !SupportSize && !SupportRotation ? \"Не поддерживает ничего\" : WL.String.Join(\", \", SupportPosition ? Position.Value.ToPositionString() : null, SupportSize ? Size.Value.ToSizeString() : null, SupportRotation ? Rotation.Value.ToString() : null);";

            Result += Other.Generate_NextLine();
            
            Result += "public bool Equals(" + I.Name + " Other){if(ReferenceEquals(this, Other)){ return true; } if(Type != Other.Type){ return false; } if(SupportPosition && !Position.Value.Equals(Other.Position.Value)){ return false; } if(SupportSize && !Size.Value.Equals(Other.Size.Value)){ return false; } if(SupportRotation && !Rotation.Value.Equals(Other.Rotation.Value)){ return false; } return true; }";
            Result += "public override bool Equals(object? Object) => Object is " + I.Name + " Other && Equals(Other);";
            
            Result += Other.Generate_NextLine();

            Result += "public override int GetHashCode(){ HashCode Hash = new HashCode(); Hash.Add(Type); if(SupportPosition){Hash.Add(Position.Value);} if(SupportSize){Hash.Add(Size.Value);} if(SupportRotation){Hash.Add(Rotation.Value);} return Hash.ToHashCode(); }";
        }
        Generate_Other();
    }
}