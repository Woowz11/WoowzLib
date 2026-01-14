using System.Text;
using System.Text.RegularExpressions;

namespace WL{
    /// <summary>
    /// Работа со строками
    /// </summary>
    [WLModule(-5000)]
    public static class String{
        private static readonly Regex Regex1 = new Regex(@"\$(\d+)", RegexOptions.Compiled);
        
        /// <summary>
        /// Форматирование строки, заменяет '$0', '$1', '$2', ... на элементы из массива
        /// <br />Если передан 1 элемент, заменяет всё на этот элемент
        /// </summary>
        /// <param name="S">Строка</param>
        /// <param name="Values">Элементы</param>
        public static string Format(string S, params object[] Values){
            try{
                if(Values.Length == 0){ return S; }

                return Regex1.Replace(S,
                M => {
                    int i = int.Parse(M.Groups[1].Value);
                    if(Values.Length == 1){
                        return Values[0]?.ToString() ?? "";
                    }
                    
                    if(i < Values.Length){
                        return Values[i]?.ToString() ?? "";
                    }
                    
                    return M.Value;
                    
                });
            }catch(Exception e){
                throw new Exception("Произошла ошибка при форматировании строки!\nСтрока: \"" + S + "\"\nЭлементы: (" + string.Join(", ", Values) + ")", e);
            }
        }

        /// <summary>
        /// Объединяет массив объектов в строку с пользовательской функцией
        /// </summary>
        /// <param name="Func">Функция (Индекс текущего элемента в массиве, Текущий элемент в массиве, Это последний элемент?, Возвращаемый результат)</param>
        /// <param name="Values">Элементы</param>
        public static string Join(Func<int, object, bool, string> Func, object[] Values){
            try{
                switch(Values.Length){
                    case 0:
                        return Func(0, "", true);
                    case 1:
                        return Func(0, Values[0], true);
                }

                StringBuilder SB = new StringBuilder();
                int LastIndex = Values.Length - 1;

                for(int i = 0; i < LastIndex; i++){
                    SB.Append(Func(i, Values[i], false));
                }

                SB.Append(Func(LastIndex, Values[LastIndex], true));
                return SB.ToString();
            }catch(Exception e){
                throw new Exception("Произошла ошибка при объединении объектов в строку!\nФункция: " + Func+ "\nЭлементы: (" + Join(Values) + ")");
            }
        }

        /// <summary>
        /// Объединяет массив объектов в строку
        /// </summary>
        /// <param name="MiddleFormat">Перед последним [<c>"$0, "</c>]</param>
        /// <param name="LastFormat">Последний [<c>"$0"</c>]</param>
        /// <param name="Values">Элементы [<c>new object[]{"A", "B", "C"}</c>]</param>
        /// <returns>[<c>"A, B, C"</c>]</returns>
        public static string Join(string MiddleFormat, string LastFormat, object[] Values){
            return Join((I, O, Last) => Last ? Format(LastFormat, O) : Format(MiddleFormat, O), Values);
        }
        
        /// <summary>
        /// Объединяет массив объектов в строку
        /// </summary>
        /// <param name="Format">Формат [<c>"this.$0; "</c>]</param>
        /// <param name="Values">Элементы [<c>new object[]{"A", "B", "C"}</c>]</param>
        /// <returns>[<c>"this.A; this.B; this.C; "</c>]</returns>
        public static string Join(string Format, object[] Values){
            return Join(Format, Format, Values);
        }
        
        /// <summary>
        /// Объединяет массив объектов в строку
        /// </summary>
        /// <param name="Values">Элементы [<c>new object[]{"A", "B", "C"}</c>]</param>
        /// <returns>[<c>"A, B, C"</c>]</returns>
        public static string Join(object[] Values){
            return string.Join(", ", Values);
        }
        
        /// <summary>
        /// Объединяет массив объектов с ключами в строку с пользовательской функцией
        /// </summary>
        /// <param name="Func">Функция (Индекс текущего элемента в массиве, Текущий элемент в массиве с ключом, Это последний элемент?, Возвращаемый результат)</param>
        /// <param name="Values">Элементы с ключами</param>
        public static string Join<TKey, TValue>(Func<int, KeyValuePair<TKey, TValue>, bool, string> Func, Dictionary<TKey, TValue> Values){
            try{
                if(Values.Count == 0){ return Func(0, default, true); }

                StringBuilder SB = new StringBuilder();
                int Index = 0;
                int LastIndex = Values.Count - 1;

                foreach(KeyValuePair<TKey, TValue> Pair in Values){
                    SB.Append(Func(Index, Pair, Index == LastIndex));
                    Index++;
                }

                return SB.ToString();
            }catch(Exception e){
                throw new Exception("Произошла ошибка при объединении объектов с ключами в строку!\nФункция: " + Func+ "\nЭлементы: " + Values + "");
            }
        }
    }
}