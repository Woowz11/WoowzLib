using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using WLO;
using WLO.Attribute;

namespace WL;

public static partial class String{
    static String(){
        DictionaryUpperCase = new BiDictionary<char, char>();

        for(char C = 'a'; C <= 'z'; C++){
            DictionaryUpperCase.Add(C, (char)(C - 32));
        }
        
        for(char C = 'а'; C <= 'я'; C++){
            DictionaryUpperCase.Add(C, (char)(C - 32));
        }
        
        DictionaryUpperCase.Add('ё', 'Ё');
    }

    /// <summary>
    /// Пустая строка
    /// </summary>
    public const string Empty = "";
    
    // ----------------------------------------------------------------------
    
    /// <summary>
    /// Строка null или пустая?
    /// </summary>
    public static bool IsEmpty(string? S) => string.IsNullOrEmpty(S);

    /// <summary>
    /// Строка null или полная пробелов (а так же \n, \t, \r, \v, \f)?
    /// </summary>
    public static bool IsWhiteSpace(string? S) => string.IsNullOrWhiteSpace(S);

    /// <summary>
    /// Превращает в строку
    /// </summary>
    public static string ToString(object? Object) => WL.__Base.Other.ToString(Object);
    
    /// <summary>
    /// Превращает в строку (если и так строка, добавляет кавычки)
    /// </summary>
    public static string ToBeautifulString(object? Object) => WL.__Base.Other.ToBeautifulString(Object);
    
    // ----------------------------------------------------------------------

    /// <summary>
    /// Заменяет строки в строке
    /// </summary>
    public static string Replace(string S, string Old, string? New){
        if(IsEmpty(S) || IsEmpty(Old) || Old == New){ return S; }
        return S.Replace(Old, New);
    }
    
    /// <summary>
    /// Заменяет символы в строке
    /// </summary>
    public static string Replace(string S, char Old, char? New){
        if(IsEmpty(S) || Old == New){ return S; }
        return New.HasValue ? S.Replace(Old, New.Value) : S.Replace(Old.ToString(), Empty);
    }
    
    /// <summary>
    /// Заменяет символы в строке
    /// </summary>
    public static string Replace(string S, string[] Old, string[] New){
        if(IsEmpty(S) || Old.Length == 0 || New.Length == 0){ return S; }
        int Count = int.Min(Old.Length, New.Length);
        for(int i = 0; i < Count; i++){
            string SO = Old[i];
            string SN = New[i];
            if(!IsEmpty(SO) && SO != SN){
                S = S.Replace(SO, SN);
            }   
        }
        return S;
    }
    
    /// <summary>
    /// Заменяет левые символы из CharSet, на правые (или наоборот если Reverse включен)
    /// </summary>
    /// <param name="S">Строка</param>
    /// <param name="CharSet">Заменяемые символы</param>
    /// <param name="Reverse">Обратить?</param>
    public static string ReplaceDictionary(string S, BiDictionary<char, char> CharSet, bool Reverse = false){
        try{
            if(IsEmpty(S) || CharSet.Count == 0){ return S; }

            StringBuilder SB = new StringBuilder(S.Length);

            foreach(char C in S){
                SB.Append(CharSet.TryGet(C, out char C__, Reverse) ? C__ : C);
            }

            return SB.ToString();
        }catch(Exception e){
            throw new Exception($"Произошла ошибка при замене символов с левых на правых!\nОбратно: {Reverse}\nСимволы: {CharSet}\nСтрока:\n{S}", e);
        }
    }

    /// <summary>
    /// Превращает коллекцию (List, HashSet, Dictionary) в строку
    /// </summary>
    /// <param name="Object">List, HashSet, Dictionary</param>
    /// <param name="Flat">Сделать строку плоской</param>
    /// <param name="Indent">Кол-во табуляции</param>
    [WoowzLibHint(Information.Brainstorming)]
    public static string ToTableString(object? Object, bool Flat = false, int Indent = 0){
        try{
            if(Object == null){ return "null"; }

            string Tab = Flat ? "" : new string('\t', Indent);

            if(Object is IDictionary D){
                StringBuilder SB = new StringBuilder();
                SB.Append(Flat ? "{" : "{\n");
                foreach(DictionaryEntry Entry in D){
                    string Key   = ToTableString(Entry.Key  , Flat, Indent + 1);
                    string Value = ToTableString(Entry.Value, Flat, Indent + 1);
                    SB.Append(Flat ? $"{Key}: {Value}," : Tab + $"\t{Key}: {Value},\n");
                }
                if(SB.Length > 0 && SB[^1] == ',' || SB[^1] == '\n'){ SB.Length--; }
                SB.Append(Flat ? "}" : Tab + "}");
                return SB.ToString();
            }

            if(Object is IEnumerable E and not string){
                List<object?> List = E.Cast<object?>().ToList();
                StringBuilder SB = new StringBuilder();
                SB.Append($"<{List.Count}>[");
                for(int i = 0; i < List.Count; i++){
                    SB.Append(ToTableString(List[i], true, 0));
                    if(i < List.Count - 1) SB.Append(", ");
                }
                SB.Append(']');
                return SB.ToString();
            }

            return ToBeautifulString(Object);
        }catch(Exception e){
            throw new Exception($"Произошла ошибка при превращении таблицы в строку!\nТаблица: {ToString(Object)}\nПлоская?: {Flat}\nТабуляция: {Indent}", e);
        }
    }
    
    // ----------------------------------------------------------------------

    /// <summary>
    /// CharSet для UpperCase
    /// </summary>
    public static readonly BiDictionary<char, char> DictionaryUpperCase;
    
    /// <summary>
    /// Сделать буквы заглавными
    /// </summary>
    public static string UpperCase(string S) => ReplaceDictionary(S, DictionaryUpperCase);
    
    /// <summary>
    /// Сделать буквы прописью
    /// </summary>
    public static string LowerCase(string S) => ReplaceDictionary(S, DictionaryUpperCase, true);

    // ----------------------------------------------------------------------
    
    /// <summary>
    /// Убирает пробелы с обеих сторон (а так же \n, \t, \r, \v, \f)
    /// </summary>
    public static string Trim(string S) => TrimRight(TrimLeft(S));
    
    /// <summary>
    /// Убирает пробелы слева (а так же \n, \t, \r, \v, \f)
    /// </summary>
    public static string TrimLeft(string S){
        if(IsEmpty(S)){ return S; }

        int i = 0;
        while(i < S.Length && char.IsWhiteSpace(S[i])){ i++; }

        return S[i..];
    }
    
    /// <summary>
    /// Убирает пробелы справа (а так же \n, \t, \r, \v, \f)
    /// </summary>
    public static string TrimRight(string S){
        if(IsEmpty(S)){ return S; }

        int i = S.Length - 1;
        while(i >= 0 && char.IsWhiteSpace(S[i])){ i--; }

        return S[..(i + 1)];
    }
    
    // ----------------------------------------------------------------------

    /// <summary>
    /// Заменяет по Regex
    /// </summary>
    [WoowzLibHint(Information.New)]
    public static string RegexReplace(string S, [StringSyntax(StringSyntaxAttribute.Regex)] string Regex, string? Replacement){
        if(IsEmpty(S) || IsEmpty(Regex)){ return S; }
        return System.Text.RegularExpressions.Regex.Replace(S, Regex, Replacement ?? Empty);
    }

    /// <summary>
    /// Находит совпадения по Regex
    /// </summary>
    [WoowzLibHint(Information.New)]
    public static bool RegexMatch(string S, [StringSyntax(StringSyntaxAttribute.Regex)] string Regex){
        if(IsEmpty(S) || IsEmpty(Regex)){ return false; }
        return System.Text.RegularExpressions.Regex.IsMatch(S, Regex);
    }
    
    // ----------------------------------------------------------------------

    /// <summary>
    /// Объединяет части в строку с помощью сепаратора (если получит null, не добавит в строку)
    /// </summary>
    /// <param name="Separator"></param>
    /// <param name="Parts"></param>
    /// <returns></returns>
    public static string Join(string Separator, params string?[] Parts){
        if(Parts.Length == 0){ return Empty; }

        List<string> Filtered = new List<string>(Parts.Length);
        
        Filtered.AddRange(from Part in Parts where !IsEmpty(Part) select Part!);

        return Filtered.Count == 0 ? Empty : string.Join(Separator, Filtered);
    }
    
    // ----------------------------------------------------------------------
    
    /// <summary>
    /// Строка содержит символ?
    /// </summary>
    /// <param name="S"></param>
    /// <param name="C"></param>
    /// <returns></returns>
    public static bool Contains(string S, char C) => S.Contains(C);

    /// <summary>
    /// Строка начинается на указанную?
    /// </summary>
    public static bool AtLeft(string S, string Target) => S.StartsWith(Target);
    /// <summary>
    /// Строка начинается на указанную?
    /// </summary>
    public static bool AtLeft(string S, char Target) => S.StartsWith(Target);
    
    /// <summary>
    /// Строка заканчивается на указанную?
    /// </summary>
    public static bool AtRight(string S, string Target) => S.EndsWith(Target);
    /// <summary>
    /// Строка заканчивается на указанную?
    /// </summary>
    public static bool AtRight(string S, char Target) => S.EndsWith(Target);

    /// <summary>
    /// Разъединяет строку
    /// </summary>
    public static string[] Split(string S, char Splitter) => S.Split(Splitter);
    /// <summary>
    /// Разъединяет строку (несколько вариантов)
    /// </summary>
    public static string[] Split(string S, params char[] Splitters) => S.Split(Splitters, StringSplitOptions.None);
    
    /// <summary>
    /// Ограничивает число указанным кол-во знаков после запятой
    /// </summary>
    /// <param name="Value">Число</param>
    /// <param name="Places">Кол-во знаков после запятой</param>
    public static string LimitF(double Value, int Places){ if(Places < 0){ Places = 0; } return Value.ToString($"F{Places}", CultureInfo.InvariantCulture); }

    /// <summary>
    /// Ограничивает число указанным кол-во знаков после запятой
    /// </summary>
    /// <param name="Value">Число</param>
    /// <param name="Places">Кол-во знаков после запятой</param>
    public static string LimitF(float Value, int Places) => LimitF((double)Value, Places);
}