using System.Xml.Linq;

public static class Run{
    public static int Main(string[] Args){
        try{
            if(Args.Length == 0){
                Console.WriteLine("Использования: VersionUpdater <Путь до .csproj>");
                return 0;
            }

            string ProjPath = Args[0];

            if(!File.Exists(ProjPath)){ throw new Exception("Проект [\"" + ProjPath + "\"] не найден!"); }

            XDocument Document = XDocument.Load(ProjPath);

            XElement? VersionElement = Document.Descendants("Version").FirstOrDefault();

            if(VersionElement == null){ throw new Exception("<Version> в проекте не найден!"); }

            string Version = VersionElement.Value.Trim();

            string[] Parts = Version.Split('.');

            if(!Parts.All(P => int.TryParse(P, out int _))){
                throw new Exception("Неверный формат версии [" + Version + "]!");
            }

            int LastIndex = Parts.Length - 1;
            int LastValue = int.Parse(Parts[LastIndex]);
            Parts[LastIndex] = (LastValue + 1).ToString();

            string NewVersion = string.Join(".", Parts);
            
            Console.WriteLine(NewVersion);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при обновлении версии у проекта!", e);
        }
        return 0;
    }    
}