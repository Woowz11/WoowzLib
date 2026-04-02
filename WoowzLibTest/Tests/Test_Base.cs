namespace WoowzLibTest.Tests;

/// <summary>
/// Тест базовых функций
/// </summary>
public static class Test_Base{
    public static void Run(){
        Test.Run("Base", () => {
            Test.F("BiDictionary", () => {
                var dict = new WLO.BiDictionary<char, string>();

                // Добавляем пары
                dict.Add('a', "Apple");
                dict.Add('b', "Banana");
                dict.Add('c', "Cherry");

                // Проверяем Count
                Test.CheckResult(dict.Count, 3, "Count сломан!");

                // Прямой доступ через индексатор ключ -> значение
                Test.CheckResult(dict['a'], "Apple", "Indexer прямой сломан!");
                Test.CheckResult(dict['b'], "Banana", "Indexer прямой сломан!");
                Test.CheckResult(dict['c'], "Cherry", "Indexer прямой сломан!");

                // Обратный доступ через индексатор значение -> ключ
                Test.CheckResult(dict["Apple"], 'a', "Indexer обратный сломан!");
                Test.CheckResult(dict["Banana"], 'b', "Indexer обратный сломан!");
                Test.CheckResult(dict["Cherry"], 'c', "Indexer обратный сломан!");

                // TryGetValue прямой
                Test.CheckResult(dict.TryGetValue('a', out string v1), true, "TryGetValue прямой сломан!");
                Test.CheckResult(v1, "Apple", "TryGetValue прямой значение сломано!");

                // TryGetKey обратный
                Test.CheckResult(dict.TryGetKey("Apple", out char k1), true, "TryGetKey обратный сломан!");
                Test.CheckResult(k1, 'a', "TryGetKey обратный значение сломано!");

                // ContainsKey / ContainsValue
                Test.CheckResult(dict.ContainsKey('b'), true, "ContainsKey сломан!");
                Test.CheckResult(dict.ContainsValue("Cherry"), true, "ContainsValue сломан!");
                Test.CheckResult(dict.ContainsKey('z'), false, "ContainsKey false сломан!");
                Test.CheckResult(dict.ContainsValue("Zebra"), false, "ContainsValue false сломан!");

                // Перезапись значения по ключу
                dict['a'] = "Avocado";
                Test.CheckResult(dict['a'], "Avocado", "Перезапись индексатора ключ->значение сломана!");
                Test.CheckResult(dict["Avocado"], 'a', "Перезапись индексатора значение->ключ сломана!");

                // Перезапись ключа по значению
                dict["Avocado"] = 'X'; // меняем ключ 'a' -> 'X', значение остаётся "Avocado"
                Test.CheckResult(dict['X'], "Avocado", "Перезапись ключа сломана!");
                Test.CheckResult(dict["Avocado"], 'X', "Обратная связь ключа сломана!");
            });
        });
    }
}