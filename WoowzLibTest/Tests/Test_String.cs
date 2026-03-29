using WLO;
using WLO.Vector;

namespace WoowzLibTest.Tests;

/// <summary>
/// Тест работы со строками
/// </summary>
public static class Test_String{
    public static void Run(){
        Test.Run("String", () => {
            Test.F("Базовые операции", () => {
                string S1 = "string";
                string S2 = "test";
                string S3 = "LMAO";
                string S4 = "  1 2 3 4 5 % $ # @ !  ";
                string S5 = "йцукенгшщзхъфывапролджэячсмитьбю.УАЦПУЦЛУДЦУЩЦЫУЗАУЗЛЩТЬУЦ394ПКЫЦЦЦЦЦ934ЕОЫ4ШДЫУ44444У";
                string S6 = "";
                string S7 = "♱☭✞\t\nﷺ";

                Test.CheckResult(S1 + S2 + S3 + S4 + S5 + S6 + S7, "stringtestLMAO  1 2 3 4 5 % $ # @ !  йцукенгшщзхъфывапролджэячсмитьбю.УАЦПУЦЛУДЦУЩЦЫУЗАУЗЛЩТЬУЦ394ПКЫЦЦЦЦЦ934ЕОЫ4ШДЫУ44444У♱☭✞\t\nﷺ", "Объеденение строк сломано!");
                
                Test.CheckResult(WL.String.IsEmpty(null), true, "IsEmpty сломан!");
                Test.CheckResult(WL.String.IsEmpty(""), true, "IsEmpty 2 сломан!");
                Test.CheckResult(WL.String.IsEmpty(" "), false, "IsEmpty 3 сломан!");
                
                Test.CheckResult(WL.String.IsWhiteSpace(null), true, "IsWhiteSpace сломан!");
                Test.CheckResult(WL.String.IsWhiteSpace(""), true, "IsWhiteSpace 2 сломан!");
                Test.CheckResult(WL.String.IsWhiteSpace(" "), true, "IsWhiteSpace 3 сломан!");
                Test.CheckResult(WL.String.IsWhiteSpace("_"), false, "IsWhiteSpace 4 сломан!");
                Test.CheckResult(WL.String.IsWhiteSpace("\t\r\n   \v \f    "), true, "IsWhiteSpace 5 сломан!");
                
                Test.CheckResult(WL.String.Empty, "", "Empty сломан!");
            });
            
            Test.F("Trim", () => {
                string S1 = WL.String.TrimRight("  TEST                                     ");
                Test.CheckResult(S1, "  TEST", "TrimRight сломан!");
                
                S1 = WL.String.TrimLeft("            TEST  ");
                Test.CheckResult(S1, "TEST  ", "TrimLeft сломан!");
                
                S1 = WL.String.Trim("            TEST                                     ");
                Test.CheckResult(S1, "TEST", "Trim сломан!");
                
                S1 = WL.String.Trim("TEST");
                Test.CheckResult(S1, "TEST", "Trim 2 сломан!");
                
                S1 = WL.String.Trim("\t\t  \t \r \v \f TEST\t\n ");
                Test.CheckResult(S1, "TEST", "Trim 3 сломан!");
            });
            
            Test.F("ToString", () => {
                string S1 = WL.String.ToString(5);
                Test.CheckResult(S1, "5", "ToString сломан!");
                
                S1 = WL.String.ToString(null);
                Test.CheckResult(S1, "null", "ToString 2 сломан!");
                
                S1 = WL.String.ToString(new Vector2I(5, 2));
                Test.CheckResult(S1, "Vector2I(5, 2)", "ToString 3 сломан!");
                
                S1 = WL.String.ToString("HELLO WORLD");
                Test.CheckResult(S1, "HELLO WORLD", "ToString 4 сломан!");
                
                S1 = WL.String.ToBeautifulString("HELLO WORLD");
                Test.CheckResult(S1, "\"HELLO WORLD\"", "ToBeautifulString сломан!");
            });
            
            Test.F("ReplaceDictionary", () => {
                string S = WL.String.UpperCase("test");
                Test.CheckResult(S, "TEST", "UpperCase сломан!");
                
                S = WL.String.UpperCase("тест");
                Test.CheckResult(S, "ТЕСТ", "UpperCase 2 сломан!");
                
                S = WL.String.UpperCase("абвгд ё abc 123 321 _1 ВУВЗ");
                Test.CheckResult(S, "АБВГД Ё ABC 123 321 _1 ВУВЗ", "UpperCase 3 сломан!");
                
                S = WL.String.LowerCase("АБВГД Ё ABC 123 321 _1 ВУВЗ вувз");
                Test.CheckResult(S, "абвгд ё abc 123 321 _1 вувз вувз", "LowerCase сломан!");

                BiDictionary<char> CharSet = new BiDictionary<char>();

                CharSet['0'] = '1';
                CharSet['1'] = '!';
                
                S = WL.String.ReplaceDictionary("01234567890", CharSet);
                Test.CheckResult(S, "1!234567891", "ReplaceDictionary сломан!");
                
                S = WL.String.ReplaceDictionary("1!234567891", CharSet, true);
                Test.CheckResult(S, "01234567890", "ReplaceDictionary 2 сломан!");
            });
            
            Test.F("Replace", () => {
                Test.CheckResult(WL.String.Replace("test", "t", "HELLO"), "HELLOesHELLO", "Replace сломан!");
                Test.CheckResult(WL.String.Replace("test", "", "HELLO"), "test", "Replace 2 сломан!");
                Test.CheckResult(WL.String.Replace("test", "testt", "HELLO"), "test", "Replace 3 сломан!");
                Test.CheckResult(WL.String.Replace("test", "test", "testtest"), "testtest", "Replace 4 сломан!");
                Test.CheckResult(WL.String.Replace("рус килир 😁", "😁", "oh no..."), "рус килир oh no...", "Replace 5 сломан!");
            });
        });
    }
}