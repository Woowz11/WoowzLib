using System.Runtime.InteropServices;
using System.Text;

namespace WoowzLibTest.Tests;

/// <summary>
/// Тест C#
/// </summary>
public static class Test_CSharp{
    private class TestObject{
        public int Value;
    }
    
    private static WeakReference CreateObject(){
        var obj = new object();
        return new WeakReference(obj);
    }
    
    enum TestEnum { A = 1, B = 2 }
    
    public static void Run(){
        Test.Run("CSharp", () => {
            Logger.Info(RuntimeInformation.FrameworkDescription + "\n" + Environment.Version);
            
            Test.F("Арифметика", 30, () => {
                int a = 10;
                int b = 5;

                return (a + b) * 2;
            });
            
            Test.F("Ссылки", 20, () => {
                var obj1 = new TestObject { Value = 10 };
                var obj2 = obj1;

                obj2.Value = 20;

                return obj2.Value;
            });
            
            Test.F("Массив", 2, () => {
                int[] array = new int[3];

                array[0] = 1;
                array[1] = 2;
                array[2] = 3;

                return array[1];
            });
            
            Test.F("Строка", () => {
                string text = "Hello";

                string result = text + " World";

                Test.CheckResult(result, "Hello World", "Соединение строк сломано");

                byte[] bytes = Encoding.UTF8.GetBytes(text);

                Test.NotCheckResult(bytes.Length, 0, "Encoding сломан");
            });
            
            Test.F("Сборщик мусора", () => {
                WeakReference reference = CreateObject();

                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();

                if(reference.IsAlive){ throw new Exception("Сборщик мусора не собрал объект"); }
            });
            
            Test.F("Исключения", () => {
                bool caught = false;

                try{
                    throw new Exception("Test");
                }catch{
                    caught = true;
                }

                if(!caught){ throw new Exception("Catch исключения сломан!"); }
            });
            
            Test.F("Деление дробного числа", () => {
                double a = 0.1;
                double b = 0.2;

                double result = a + b;

                if(Math.Abs(result - 0.3) > 0.000001){ throw new Exception("Плавущая точка сломана!"); }
            });
            
            Test.F("Unsafe и память", () => {
                unsafe{
                    nint ptr = Marshal.AllocHGlobal(4);

                    try{
                        int* intPtr = (int*)ptr;
                        *intPtr = 123;

                        Test.CheckResult(*intPtr, 123, "Запись/чтение из памяти сломано!");
                    }finally{
                        Marshal.FreeHGlobal(ptr);
                    }
                }
            });
            
            Test.F("Поток", 42, () => {
                int value = 0;

                Thread thread = new Thread(() => {
                    value = 42;
                });

                thread.Start();
                thread.Join();

                return value;
            });
            
            Test.F("Лямбда", 10, () => {
                Func<int, int> func = x => x * 2;

                return func(5);
            });
            
            Test.F("Переполнение int", () => {
                try{
                    checked{
                        int x = int.MaxValue;
                        x = x + 1;
                    }

                    throw new Exception("Переполнение не было схвачено!");
                }catch (OverflowException){}
            });

            Test.F("List", 3, () => {
                var list = new List<int> { -1, 532 };
                list.Add(8881212);

                return list.Count;
            });
            
            Test.F("Dictionary", 2, () => {
                var dict = new Dictionary<string, int>();
                dict["a"] = 10;
                dict["b"] = 20;

                return dict.Count;
            });
            
            Test.F("Boxing", 10, () => {
                object obj = 10; // boxing
                int value = (int)obj; // unboxing

                return value;
            });
            
            Test.F("Nullable", true, () => {
                int? value = null;

                return value.HasValue == false;
            });
            
            Test.F("Enum", 1, () => {
                return (int)TestEnum.A;
            });
            
            Test.F("Ref", 20, () => {
                int x = 10;

                void Add(ref int value){
                    value += 10;
                }

                Add(ref x);

                return x;
            });
            
            Test.F("Out", 5, () => {
                int Parse(out int value){
                    value = 5;
                    return 1;
                }

                int result;
                Parse(out result);

                return result;
            });
            
            Test.F("Сравнение строк", true, () => {
                string a = "test";
                string b = "test";

                return object.ReferenceEquals(a, b);
            });
            
            Test.F("LINQ", 5, () => {
                var numbers = new[] { 1, 2, 3 };

                return numbers.Where(x => x > 1).Sum();
            });
            
            Test.F("Сравнение ссылок", true, () => {
                var obj = new object();

                return object.ReferenceEquals(obj, obj);
            });
            
            Test.F("Сравнение чисел", true, () => {
                int a = 10;
                int b = 10;

                return a.Equals(b);
            });
            
            Test.F("Decimal", 0.0000000003m, () => {
                return 0.0000000001m + 0.0000000002m;
            });
            
            Test.F("Race Condition (with lock)", 100000, () =>
            {
                int counter = 0;
                object locker = new object();

                Thread[] threads = new Thread[10];

                for (int i = 0; i < threads.Length; i++)
                {
                    threads[i] = new Thread(() =>
                    {
                        for (int j = 0; j < 10000; j++)
                        {
                            lock (locker)
                            {
                                counter++;
                            }
                        }
                    });

                    threads[i].Start();
                }

                foreach (var t in threads)
                    t.Join();

                return counter;
            });
            
            Test.F("GC allocation", () => {
                for (int i = 0; i < 10000; i++){
                    var obj = new byte[1024];
                }
            });
            
            Test.F("Unsafe математика", 15, () => {
                unsafe{
                    int value = 10;
                    int* ptr = &value;

                    *ptr += 5;

                    return *ptr;
                }
            });
        });
    }
}