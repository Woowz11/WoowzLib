using WLO.Vector;
using File = WLO.File;

namespace WoowzLibGenerator.Generator;

public static class Tests{

    public static readonly List<Vector.Info_Vector      > Info_Vectors    = [];
    public static readonly List<Transform.Info_Transform> Info_Transforms = [];

    public struct Info_Test{
        public string   Name;
        public Action   Inside;
        public string[] Usings;
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

            CreateTest(new Info_Test{
                Name = "Vector",
                Inside = () => {
                    foreach(Vector.Info_Vector Info_Vector in Info_Vectors){
                        CreateVectorTest(Info_Vector);
                    }
                },
                Usings = ["WLO.Vector"]
            });
            
            CreateTest(new Info_Test{
                Name = "Transform",
                Inside = () => {
                    foreach(Transform.Info_Transform Info_Transform in Info_Transforms){
                        CreateTransformTest(Info_Transform);
                    }
                },
                Usings = ["WLO", "WLO.Vector", "WLO.Transform"]
            });
            
        }catch(Exception e){
            throw new Exception("Произошла ошибка при генерации [Tests]!", e);
        }
    }
    
    // ----------------------------------------------------------------------
    
    private static string Result = "";
    public static void CreateTest(Info_Test I){
        try{
            Logger.Info("Создание теста " + I.Name + "");
            Result = "";
            
            string ClassName = "Test_" + I.Name;
            
            Result += Other.Generate_Namespace("WoowzLibTest.Tests");

            Result += Other.Generate_PublicClass(ClassName, Static: true);
            Result += "{ public static void Run(){";
            
            I.Inside();
            
            Result += "}}";

            foreach(string Using in I.Usings){
                Result = Other.Generate_Using(Using) + Result;
            }
            
            Result = Other.Generate_GeneratorComment("Tests") + Result;
            
            File FileR = WL.Explorer.File.GetOrCreate(WL.String.Path.Add(OutFolder     , ClassName + ".cs"));
            File FileD = WL.Explorer.File.GetOrCreate(WL.String.Path.Add(OutFolderDebug, ClassName + ".cs"));

            string R__ = Other.Inline(Result);
            FileD.Content = R__;
            FileR.Content = Other.Beautify(R__, AutoInline: false);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при генерации теста [" + I.Name + "]!", e);
        }
    }

    public static string Generate_TestRun(string Name, string Inside) => "Test.Run(\"" + Name + " (GENERATED)\", () => {" + Inside + "});";
    public static string Generate_TestF(string Name, string Inside) => "Test.F(\"" + Name + "\", () => {" + Inside + "});";
    public static string Generate_Check(string Got, string Need, string ErrorMessage, string? Type = null) => "Test.CheckResult" + (Type != null ? "<" + Type + ">" : "") + "(" + Got + ", " + Need + ", \"" + ErrorMessage + "\");";
    
    public static void CreateVectorTest(Vector.Info_Vector I){
        string Result = "";

        void Generate_Test_Construction(){
            string R = "";

            R += "var v = new " + I.Name + "();";
            R += Vector.RFEA(I.Axis, Generate_Check("v.@", I.Zero, "Создание @ не работает!", I.Primitive));
            
            R += "v = new " + I.Name + "(" + Vector.RFEA(I.Axis, "@I", ", ") + ");";
            R += Vector.RFEA(I.Axis, Generate_Check("v.@", "@I", "Создание 2 @ не работает!", I.Primitive));
            
            R += "v = new " + I.Name + "(555);";
            R += Vector.RFEA(I.Axis, Generate_Check("v.@", "555", "Создание 3 @ не работает!", I.Primitive));
            
            Result += Generate_TestF("Создание", R);
        }
        Generate_Test_Construction();

        void Generate_Test_Change(){
            string R = "";
            
            R += "var v = new " + I.Name + "();";
            R += Vector.RFEA(I.Axis, "v.@ = 32;");
            R += Vector.RFEA(I.Axis, Generate_Check("v.@", "32", "Изменение @ не работает!", I.Primitive));

            R += "v = v.Add(" + I.Name + ".One);";
            R += Vector.RFEA(I.Axis, Generate_Check("v.@", "33", "Изменение Add @ не работает!", I.Primitive));
            
            R += "v = v.Sub(" + I.Name + ".One);";
            R += Vector.RFEA(I.Axis, Generate_Check("v.@", "32", "Изменение Sub @ не работает!", I.Primitive));
            
            R += "v = v.Mul(" + I.Name + ".Double);";
            R += Vector.RFEA(I.Axis, Generate_Check("v.@", "64", "Изменение Mul @ не работает!", I.Primitive));
            
            R += "v = v.Div(" + I.Name + ".Double);";
            R += Vector.RFEA(I.Axis, Generate_Check("v.@", "32", "Изменение Div @ не работает!", I.Primitive));
            
            R += "v = v + " + I.Name + ".One;";
            R += Vector.RFEA(I.Axis, Generate_Check("v.@", "33", "Изменение Add 2 @ не работает!", I.Primitive));
            
            R += "v = v - " + I.Name + ".One;";
            R += Vector.RFEA(I.Axis, Generate_Check("v.@", "32", "Изменение Sub 2 @ не работает!", I.Primitive));
            
            R += "v = v * " + I.Name + ".Double;";
            R += Vector.RFEA(I.Axis, Generate_Check("v.@", "64", "Изменение Mul 2 @ не работает!", I.Primitive));
            
            R += "v = v / " + I.Name + ".Double;";
            R += Vector.RFEA(I.Axis, Generate_Check("v.@", "32", "Изменение Div 2 @ не работает!", I.Primitive));
            
            R += "v = v + 1;";
            R += Vector.RFEA(I.Axis, Generate_Check("v.@", "33", "Изменение Add 3 @ не работает!", I.Primitive));
            
            R += "v = v - 1;";
            R += Vector.RFEA(I.Axis, Generate_Check("v.@", "32", "Изменение Sub 3 @ не работает!", I.Primitive));
            
            R += "v = v * 2;";
            R += Vector.RFEA(I.Axis, Generate_Check("v.@", "64", "Изменение Mul 3 @ не работает!", I.Primitive));
            
            R += "v = v / 2;";
            R += Vector.RFEA(I.Axis, Generate_Check("v.@", "32", "Изменение Div 3 @ не работает!", I.Primitive));
            
            Result += Generate_TestF("Изменение", R);
        }
        Generate_Test_Change();

        void Generate_Test_Equals(){
            string R = "";

            R += "var v = " + I.Name + ".Right.Mul(" + I.Name + ".Double);";

            R += Generate_Check("v == null", "false", "Сравнение не работает!");
            R += Generate_Check("v != null", "true", "Сравнение 2 не работает!");
            R += Generate_Check("v == v", "true", "Сравнение 3 не работает!");
            R += Generate_Check("v != v", "false", "Сравнение 4 не работает!");
            R += Generate_Check("v == " + I.Name + ".Right.Mul(" + I.Name + ".Double)", "true", "Сравнение 5 не работает!");
            R += Generate_Check("v == " + I.Name + ".One", "false", "Сравнение 6 не работает!");
            
            Result += Generate_TestF("Сравнение", R);
        }
        Generate_Test_Equals();

        Tests.Result += Generate_TestRun(I.Name, Result);
    }

    public static void CreateTransformTest(Transform.Info_Transform I){
        string Result = "";

        void Generate_Test_Construction(){
            string R = "";

            R += $"var t = new {I.Name}();";

            R += Generate_Check("t.Position.Value", $"{I.PositionType}.Zero", "Position default неверный!");
            R += Generate_Check("t.Size.Value", $"{I.SizeType}.One", "Size default неверный!");
            R += Generate_Check("t.Rotation.Value", "false", "Rotation default неверный!");
            
            Result += Generate_TestF("Создание", R);
        }
        Generate_Test_Construction();

        void Generate_Test_Flags(){
            string R = "";
            
            R += $"var t = new {I.Name}();";

            R += "t.Type = TransformType.None;";
            
            R += Generate_Check("t.SupportPosition", "false", "SupportPosition не работает!");
            R += Generate_Check("t.SupportSize", "false", "SupportSize не работает!");
            R += Generate_Check("t.SupportRotation", "false", "SupportRotation не работает!");
            
            R += "t.Type = TransformType.All;";
            
            R += Generate_Check("t.SupportPosition", "true", "SupportPosition не работает! 2");
            R += Generate_Check("t.SupportSize", "true", "SupportSize не работает! 2");
            R += Generate_Check("t.SupportRotation", "true", "SupportRotation не работает! 2");
            
            Result += Generate_TestF("Flags", R);
        }
        Generate_Test_Flags();

        void Generate_Test_Set(){
            void F(string Param, string Value){
                string R = "";
            
                R += $"var t = new {I.Name}();";

                R += $"t.Type = TransformType.{Param};";

                R += $"t.{Param}.Value = {Value};";
                
                R += Generate_Check($"t.{Param}.Value", Value, $"{Param} установка не работает!");
            
                Result += Generate_TestF("Изменение " + Param, R);
            }
            
            F("Position", $"{I.PositionType}.Left");
            F("Size", $"{I.SizeType}.Left");
            F("Rotation", "true");
        }
        Generate_Test_Set();

        void Generate_Test_OnChanged(){
            string R = "";
            
            R += $"var t = new {I.Name}();";

            R += "bool Called = false;";

            R += "t.OnChanged += (_, _, _) => { Called = true; };";

            R += $"t.Position.Value = {I.PositionType}.Left;";
            
            R += Generate_Check("Called", "true", "OnChanged не работает!");
            
            Result += Generate_TestF("OnChanged событие", R);
        }
        Generate_Test_OnChanged();
        
        Tests.Result += Generate_TestRun(I.Name, Result);
    }
}