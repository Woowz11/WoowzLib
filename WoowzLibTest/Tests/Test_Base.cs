using WLO;

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
            
            Test.F("ReactiveProperty", () => {
                // Базовое поведение

                var rp = new ReactiveProperty<int>("Test", null, 10);
                Test.CheckResult(rp.Value, 10, "Initial value неверен!");

                rp.Value = 20;
                Test.CheckResult(rp.Value, 20, "Set/Get не работает!");

                // ----------------------------------------------------------------------
                // OnChanged

                bool changedCalled = false;
                int oldV = 0, newV = 0;

                rp = new ReactiveProperty<int>(Initial: 0);
                rp.OnChanged += (o, n) => {
                    changedCalled = true;
                    oldV = o;
                    newV = n;
                };

                rp.Value = 5;

                Test.CheckResult(changedCalled, true, "OnChanged не вызвался!");
                Test.CheckResult(oldV, 0, "Old неверен!");
                Test.CheckResult(newV, 5, "New неверен!");

                // ----------------------------------------------------------------------
                // OnApply изменение

                rp = new ReactiveProperty<int>(Initial: 0);
                rp.OnApply += (oldVal, newVal) => Cancellable<int>.Continue(newVal * 2);

                rp.Value = 5;
                Test.CheckResult(rp.Value, 10, "OnApply не изменил значение!");

                // ----------------------------------------------------------------------
                // OnApply отмена

                bool cancelChanged = false;

                rp = new ReactiveProperty<int>(Initial: 10);
                rp.OnChanged += (_, _) => cancelChanged = true;
                rp.OnApply += (_, _) => Cancellable<int>.Cancelled();

                rp.Value = 20;

                Test.CheckResult(rp.Value, 10, "Отмена не сработала!");
                Test.CheckResult(cancelChanged, false, "OnChanged не должен вызываться!");

                // ----------------------------------------------------------------------
                // OnApply цепочка

                rp = new ReactiveProperty<int>(Initial: 1);
                rp.OnApply += (o, n) => Cancellable<int>.Continue(n + 1); // 5 -> 6
                rp.OnApply += (o, n) => Cancellable<int>.Continue(n * 2); // 6 -> 12

                rp.Value = 5;

                Test.CheckResult(rp.Value, 12, "Цепочка OnApply не работает!");

                // ----------------------------------------------------------------------
                // OnGet

                rp = new ReactiveProperty<int>(Initial: 10);
                rp.OnGet += v => v + 5;

                Test.CheckResult(rp.Value, 15, "OnGet не применяется!");

                // ----------------------------------------------------------------------
                // OnGet цепочка

                rp = new ReactiveProperty<int>(Initial: 10);
                rp.OnGet += v => v + 2;  // 12
                rp.OnGet += v => v * 3;  // 36

                Test.CheckResult(rp.Value, 36, "Цепочка OnGet не работает!");

                // ----------------------------------------------------------------------
                // Без изменений → OnChanged не вызывается

                bool noChangeCalled = false;

                rp = new ReactiveProperty<int>(Initial: 10);
                rp.OnChanged += (_, _) => noChangeCalled = true;

                rp.Value = 10;

                Test.CheckResult(noChangeCalled, false, "OnChanged не должен вызываться!");

                // ----------------------------------------------------------------------
                // Порядок выполнения OnChanged (ДО изменения)

                int observedInside = -1;

                rp = new ReactiveProperty<int>(Initial: 1);
                rp.OnChanged += (_, _) => {
                    observedInside = rp.Value;
                };

                rp.Value = 5;

                Test.CheckResult(observedInside, 1, "OnChanged порядок выполнения изменился!");
            });
            
            Test.F("Cancellable", () => {
                // Continue

                var c1 = Cancellable<int>.Continue(5);

                Test.CheckResult(c1.Cancel, false, "Continue должен быть без отмены!");
                Test.CheckResult(c1.Value, 5, "Continue значение неверное!");

                // ----------------------------------------------------------------------
                // Cancelled

                var c2 = Cancellable<int>.Cancelled();

                Test.CheckResult(c2.Cancel, true, "Cancelled должен быть с отменой!");
                Test.CheckResult(c2.Value, default(int), "Cancelled значение должно быть default!");

                // ----------------------------------------------------------------------
                // Equals (одинаковые значения)

                var c3 = Cancellable<int>.Continue(10);
                var c4 = Cancellable<int>.Continue(10);

                Test.CheckResult(c3 == c4, true, "Equals одинаковых значений сломан!");
                Test.CheckResult(c3.Equals(c4), true, "Equals метод сломан!");

                // ----------------------------------------------------------------------
                // Equals (разные значения)

                var c5 = Cancellable<int>.Continue(10);
                var c6 = Cancellable<int>.Continue(20);

                Test.CheckResult(c5 == c6, false, "Equals разных значений сломан!");

                // ----------------------------------------------------------------------
                // Equals (Cancel влияет)

                var c7 = Cancellable<int>.Continue(0);
                var c8 = Cancellable<int>.Cancelled();

                Test.CheckResult(c7 == c8, false, "Cancel должен влиять на Equals!");

                // ----------------------------------------------------------------------
                // HashCode (одинаковые)

                var c9 = Cancellable<int>.Continue(42);
                var c10 = Cancellable<int>.Continue(42);

                Test.CheckResult(c9.GetHashCode() == c10.GetHashCode(), true, "HashCode одинаковых значений сломан!");

                // ----------------------------------------------------------------------
                // HashCode (разные)

                var c11 = Cancellable<int>.Continue(1);
                var c12 = Cancellable<int>.Continue(2);

                Test.CheckResult(c11.GetHashCode() != c12.GetHashCode(), true, "HashCode разных значений не отличается!");

                // ----------------------------------------------------------------------
                // ToString / ToShortString (базовая проверка)

                string s1 = c1.ToString();
                string s2 = c1.ToShortString();

                Test.CheckResult(string.IsNullOrEmpty(s1), false, "ToString вернул пустоту!");
                Test.CheckResult(string.IsNullOrEmpty(s2), false, "ToShortString вернул пустоту!");

                // ----------------------------------------------------------------------
                // Сравнение с object

                object obj = Cancellable<int>.Continue(5);

                Test.CheckResult(c1.Equals(obj), true, "Equals(object) сломан!");

                // ----------------------------------------------------------------------
                // Проверка !=

                Test.CheckResult((c1 != c2), true, "Оператор != сломан!");

                // ----------------------------------------------------------------------
                // Структурное копирование (важно для struct)

                var c13 = c1;
                c13.Value = 999;

                Test.CheckResult(c1.Value, 5, "Struct копирование сломано (Value изменился у оригинала)!");
            });
        });
    }
}