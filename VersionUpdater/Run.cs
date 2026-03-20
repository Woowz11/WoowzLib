using System.Text.RegularExpressions;

public static partial class Run{
    public static int Main(string[] Args){
        try{
            if(Args.Length == 0){
                Console.WriteLine("Использования: VersionUpdater <Путь до .csproj>");
                return 0;
            }

            string ProjPath = Args[0];

            if(!File.Exists(ProjPath)){ throw new Exception("Проект [\"" + ProjPath + "\"] не найден!"); }

            string Content = File.ReadAllText(ProjPath);

            Match VersionMatch = Regex().Match(Content);

            if(!VersionMatch.Success){ throw new Exception("<Version> в проекте не найден!"); }

            string OldVersion = VersionMatch.Groups[1].Value.Trim();

            string[] Parts = OldVersion.Split('.');

            if(!Parts.All(P => int.TryParse(P, out int _))){
                throw new Exception("Неверный формат версии [" + OldVersion + "]!");
            }

            int LastIndex = Parts.Length - 1;
            Parts[LastIndex] = (int.Parse(Parts[LastIndex]) + 1).ToString();

            string NewVersion = string.Join(".", Parts);

            File.WriteAllText(ProjPath, Regex().Replace(Content, M => "<Version>" + NewVersion + "</Version>"));
            
            Console.WriteLine("Обновлена версия: " + OldVersion + " -> " + NewVersion);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при обновлении версии у проекта!", e);
        }
        return 0;
    }

    [GeneratedRegex(@"<Version>(.*?)</Version>")]
    private static partial Regex Regex();
}