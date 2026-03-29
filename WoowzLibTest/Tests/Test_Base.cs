namespace WoowzLibTest.Tests;

/// <summary>
/// Тест базовых функций
/// </summary>
public static class Test_Base{
    public static void Run(){
        Test.Run("Base", () => {
            Test.F("BiDictionary", () => {
                var dict = new WLO.BiDictionary<char>();

                // Добавляем пары
                dict.Add('a', 'A');
                dict.Add('b', 'B');
                dict.Add('c', 'C');

                // Проверяем Count
                Test.CheckResult(dict.Count, 3, "Count сломан!");

                // Прямой доступ через индексатор
                Test.CheckResult(dict['a'], 'A', "Indexer 1 прямой сломан!");
                Test.CheckResult(dict['b'], 'B', "Indexer 2 прямой сломан!");
                Test.CheckResult(dict['c'], 'C', "Indexer 3 прямой сломан!");

                // Обратный доступ через Reverse = true
                Test.CheckResult(dict['A', true], 'a', "Indexer 1 обратный сломан!");
                Test.CheckResult(dict['B', true], 'b', "Indexer 2 обратный сломан!");
                Test.CheckResult(dict['C', true], 'c', "Indexer 3 обратный сломан!");

                // TryGet прямой
                Test.CheckResult(dict.TryGet('a', out char v1), true, "TryGet прямой сломан!");
                Test.CheckResult(v1, 'A', "TryGet прямой значение сломано!");

                // TryGet обратный
                Test.CheckResult(dict.TryGet('A', out char k1, true), true, "TryGet обратный сломан!");
                Test.CheckResult(k1, 'a', "TryGet обратный значение сломано!");

                // ContainsKey / ContainsValue
                Test.CheckResult(dict.ContainsKey('b'), true, "ContainsKey сломан!");
                Test.CheckResult(dict.ContainsValue('C'), true, "ContainsValue сломан!");
                Test.CheckResult(dict.ContainsKey('z'), false, "ContainsKey false сломан!");
                Test.CheckResult(dict.ContainsValue('Z'), false, "ContainsValue false сломан!");

                // Перезапись значения
                dict['a'] = 'X';
                Test.CheckResult(dict['a'], 'X', "Перезапись индексатора сломана!");
                Test.CheckResult(dict['X', true], 'a', "Перезапись обратного доступа сломана!");
            });
        });
    }
}