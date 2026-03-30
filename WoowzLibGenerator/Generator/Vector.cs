using File = WLO.File;

namespace WoowzLibGenerator.Generator;

public static class Vector{

    public static readonly Info.ValueType[] Info_Vector_Types = [Info.ValueType.Int, Info.ValueType.UInt, Info.ValueType.Float, Info.ValueType.Double];
    
    public static readonly char[]           Info_Vector_Axis  = ['X', 'Y', 'Z', 'W'];
    
    public struct Info_Vector{
        public Info.ValueType Type;
        public int            AxisCount;
        public char[]         Axis;
        public string         Name;
    }
    
    // ----------------------------------------------------------------------

    private static string OutFolder;
    public static void Generate(string OutFolder__){
        try{
            OutFolder = OutFolder__; 
            WL.Explorer.Folder.GetOrCreate(OutFolder);

            foreach(Info.ValueType VT in Info_Vector_Types){
                for(int i = 2; i <= Info_Vector_Axis.Length; i++){
                    CreateVector(new Info_Vector{
                        Type = VT,
                        AxisCount = i,
                        Axis = Info_Vector_Axis.Take(i).ToArray(),
                        Name = "Vector" + i + Info.ValueType_Name(VT),
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
            Result = """"
                     using File = WLO.File;
                     
                     namespace WoowzLibGenerator.Generator;
                     
                     public static class Vector{
                     
                         public static readonly Info.ValueType[] Info_Vector_Types = [Info.ValueType.Int, Info.ValueType.UInt, Info.ValueType.Float, Info.ValueType.Double];
                         
                         public static readonly char[] Info_Vector_Axis = ['X', 'Y', 'Z', 'W'];
                         
                         public struct Info_Vector{
                             public Info.ValueType Type;
                             public int            AxisCount;
                             public char[]         Axis;
                             public string         Name;
                         }
                     
                     
                         private static string OutFolder;
                         public static void Generate(string OutFolder__){
                             try{
                                 OutFolder = OutFolder__; 
                                 WL.Explorer.Folder.GetOrCreate(OutFolder);
                     
                                 foreach(Info.ValueType VT in Info_Vector_Types){
                                     for(int i = 2; i <= Info_Vector_Axis.Length; i++){
                                         CreateVector(new Info_Vector{
                                             Type = VT,
                                             AxisCount = i,
                                             Axis = Info_Vector_Axis.Take(i).ToArray(),
                                             Name = "Vector" + i + Info.ValueType_Name(VT),
                                         });
                                     }
                                 }
                     
                                // example test
                     
                             }catch(Exception e){
                                 throw new Exception("Произошла ошибка при генерации [Vector]!", e);
                             }
                         }
                         
                         // commend
                         
                         /* test */
                     
                         private static string Result = "";
                         public static void CreateVector(Info_Vector I){
                             try{
                                 Logger.Info("Создание вектора " + I.Name + "");
                                 Result = """
                     \t\r\n
                     \"
                     {;}
                     
                                          """;
                     
                     
                     
                     
                     
                                 File File = WL.Explorer.File.GetOrCreate(WL.String.Path.Add(OutFolder, I.Name + ".cs"));
                                 File.Content = Other.Beautify(Result);
                             }catch(Exception e){
                                 throw new Exception("Произошла ошибка при генерации вектора [" + I.Name + "]!", e);
                             }
                         }
                     }
                     """";





            File File = WL.Explorer.File.GetOrCreate(WL.String.Path.Add(OutFolder, I.Name + ".cs"));
            File.Content = Other.Beautify(Result);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при генерации вектора [" + I.Name + "]!", e);
        }
    }
}