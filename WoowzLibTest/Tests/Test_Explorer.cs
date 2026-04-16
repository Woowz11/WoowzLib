using WLO;
using File = WLO.File;

namespace WoowzLibTest.Tests;

/// <summary>
/// Тест работы с Explorer
/// </summary>
public static class Test_Explorer{
    public static void Run(){
        Test.Run("Explorer", () => {
            Test.F("Пути", () => {
                string S = WL.String.Path.Disk("D:/Test/File/Example.txt", out char? Disk, out bool Error);
                Test.CheckResult(S, "Test/File/Example.txt", "Disk не работает!");
                Test.CheckResult(Disk, 'D', "Disk 2 не работает!");
                Test.CheckResult(Error, false, "Disk 3 не работает!");
                
                WL.String.Path.Disk("!:/Test/File/Example.txt", out Disk, out Error);
                Test.CheckResult(Disk, null, "Disk 4 не работает!");
                Test.CheckResult(Error, true, "Disk 5 не работает!");
                
                WL.String.Path.Disk("C:Test/File/Example.txt", out Disk, out Error);
                Test.CheckResult(Error, true, "Disk 6 не работает!");
                
                
                Test.CheckResult(WL.String.Path.Split("test1/test2\\test3//test4/"), ["test1", "test2", "test3", "", "test4"], "Split не работает!");
                
                
                Test.CheckResult(WL.String.Path.Normalize("    "), "", "Normalize не работает!");
                Test.CheckResult(WL.String.Path.Normalize("file"), "file", "Normalize 2 не работает!");
                Test.CheckResult(WL.String.Path.Normalize("folder/file  "), "folder/file", "Normalize 3 не работает!");
                Test.CheckResult(WL.String.Path.Normalize("folder\\file"), "folder/file", "Normalize 4 не работает!");
                Test.CheckResult(WL.String.Path.Normalize("folder/folder\\"), "folder/folder", "Normalize 5 не работает!");
                Test.CheckResult(WL.String.Path.Normalize("folder\\FoLder\n\t\v\r\f "), "folder/FoLder", "Normalize 6 не работает!");
                
                
                Test.CheckResult(WL.String.Path.IsCorrect(""), true, "IsCorrect не работает!");
                Test.CheckResult(WL.String.Path.IsCorrect("example"), true, "IsCorrect 2 не работает!");
                Test.CheckResult(WL.String.Path.IsCorrect("folder/file"), true, "IsCorrect 3 не работает!");
                Test.CheckResult(WL.String.Path.IsCorrect("folder/file\\file.txt"), true, "IsCorrect 4 не работает!");
                Test.CheckResult(WL.String.Path.IsCorrect("D:/test"), true, "IsCorrect 5 не работает!");
                Test.CheckResult(WL.String.Path.IsCorrect("a/a/a/a/a/a/a/a/a/a/"), true, "IsCorrect 6 не работает!");
                Test.CheckResult(WL.String.Path.IsCorrect("test//file"), false, "IsCorrect 7 не работает!");
                Test.CheckResult(WL.String.Path.IsCorrect("E:test/file"), false, "IsCorrect 8 не работает!");
                Test.CheckResult(WL.String.Path.IsCorrect("C:/test/file/file:test"), false, "IsCorrect 9 не работает!");
                Test.CheckResult(WL.String.Path.IsCorrect("f?ile"), false, "IsCorrect 10 не работает!");
                Test.CheckResult(WL.String.Path.IsCorrect("file!!!!!"), true, "IsCorrect 11 не работает!");
            });
            
            Test.F("Файлы/Папки", () => {
                string PATH = "W:/Other/WoowzLib/WoowzLibTest/TEST_EXPLORER/";
                
                Test.CheckResult(WL.Explorer.Folder.Exist(PATH), true, "PATH для теста не найден!");
                
                WL.Explorer.Folder.Clear(PATH);
                
                Test.CheckResult(WL.Explorer.Folder.Files(PATH).Length, 0, "Clear не сработал для файлов!");
                Test.CheckResult(WL.Explorer.Folder.Folders(PATH).Length, 0, "Clear не сработал для папок!");

                // ----------------------------------------------------------------------
                
                string Content = "Hi, welcome here!";
                
                File File = WL.Explorer.File.GetOrCreate(PATH + "FILE.txt", Content);
                
                Test.CheckResult(WL.Explorer.File.Exist(PATH + "FILE.txt"), true, "File.Exist не работает!");
                Test.CheckResult(WL.Explorer.Folder.Files(PATH).Length, 1, "Files/File.GetOrCreate не работает!");
                
                Test.CheckResult(File.Content, Content, "File.Content неверное содержимое!");
                
                Test.CheckResult(File.Type, FileType.File, "File.Type не работает!");

                Content = "Okay, now bye!";
                File.Content = Content;
                
                Test.CheckResult(File.Content, Content, "File.Content 2 неверное содержимое!");
                
                // ----------------------------------------------------------------------
                
                File Folder = WL.Explorer.Folder.GetOrCreate(PATH + "FOLDER");
                
                Test.CheckResult(WL.Explorer.Folder.Exist(PATH + "FOLDER"), true, "Folder.Exist не работает!");
                Test.CheckResult(WL.Explorer.Folder.Folders(PATH).Length, 1, "Folders/Folder.GetOrCreate не работает!");
                
                Test.CheckResult(Folder.Type, FileType.Folder, "Folder.Type не работает!");
                
                // ----------------------------------------------------------------------
                
                File.Delete();
                Test.CheckResult(File.Exist, false, "File.Delete не работает!");
                
                Folder.Delete();
                Test.CheckResult(Folder.Exist, false, "Folder.Delete не работает!");
            });
        });
    }
}