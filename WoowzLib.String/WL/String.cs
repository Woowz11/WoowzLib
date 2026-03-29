using System.Diagnostics.CodeAnalysis;
using System.Text;
using WLO;
using WLO.Attribute;

namespace WL;

public static partial class String{
    static String(){
        DictionaryUpperCase = new BiDictionary<char>();

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
    /// Заменяет левые символы из CharSet, на правые (или наоборот если Reverse включен)
    /// </summary>
    /// <param name="S">Строка</param>
    /// <param name="CharSet">Заменяемые символы</param>
    /// <param name="Reverse">Обратить?</param>
    public static string ReplaceDictionary(string S, BiDictionary<char> CharSet, bool Reverse = false){
        try{
            if(IsEmpty(S) || CharSet.Count == 0){ return S; }

            StringBuilder SB = new StringBuilder(S.Length);

            foreach(char C in S){ SB.Append(CharSet.TryGet(C, out char C__, Reverse) ? C__ : C); }

            return SB.ToString();
        }catch(Exception e){
            throw new Exception("Произошла ошибка при замене символов с левых на правых!\nОбратно: " + Reverse + "\nСимволы: " + CharSet + "\nСтрока:\n" + S, e);
        }
    }
    
    // ----------------------------------------------------------------------

    /// <summary>
    /// CharSet для UpperCase
    /// </summary>
    public static readonly BiDictionary<char> DictionaryUpperCase;
    
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
    /// Убирает пробелы справа (а так же \n, \t, \r, \v, \f)
    /// </summary>
    public static string TrimRight(string S){
        if(IsEmpty(S)){ return S; }

        int i = S.Length - 1;
        while(i >= 0 && char.IsWhiteSpace(S[i])){ i--; }

        return S[..(i + 1)];
    }
    
    /// <summary>
    /// Убирает пробелы слева (а так же \n, \t, \r, \v, \f)
    /// </summary>
    public static string TrimLeft(string S){
        if(IsEmpty(S)){ return S; }

        int i = 0;
        while(i < S.Length && char.IsWhiteSpace(S[i])){ i++; }

        return S[i..];
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
}