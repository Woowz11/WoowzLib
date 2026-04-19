using System.Text;
using WLO.Attribute;

namespace WoowzLibGenerator;

public static class Other{
    /// <summary>
    /// Делает код в линию
    /// </summary>
    public static string Inline(string Code){
        if(WL.String.IsWhiteSpace(Code)){ return WL.String.Empty; }

        StringBuilder SB = new StringBuilder();

        bool InString = false;
        bool LastWasSpace = false;
        
        for(int i = 0; i < Code.Length; i++){
            char c = Code[i];
            
            if(c == '"'){
                bool Escaped = i > 0 && Code[i - 1] == '\\';
                if(!Escaped){ InString = !InString; }
                SB.Append(c);
                LastWasSpace = false;
                continue;
            }

            if(!InString){
                if(c == '/' && i + 1 < Code.Length && Code[i + 1] == '/'){
                    SB.Append("/*");

                    i += 2;
                    while(i < Code.Length && Code[i] != '\n' && Code[i] != '\r'){
                        SB.Append(Code[i]);
                        i++;
                    }

                    SB.Append(" */");
                    LastWasSpace = false;
                    continue;
                }
                
                if(c == ' ' && SB.Length > 0 && "{};".Contains(SB[^1])){ continue; }
                
                switch(c){
                    case '\n' or '\t' or '\r':
                    case ' ' when LastWasSpace:
                        continue;
                    
                    case ' ':
                        LastWasSpace = true;
                        break;
                    
                    default:
                        LastWasSpace = false;
                        break;
                }
            }
            
            SB.Append(c);
        }
        
        return SB.ToString();
    }
    
    /// <summary>
    /// Делает код красивым
    /// </summary>
    public static string Beautify(string Code, string Indent = "\t", bool AutoInline = true, int StartIndent = 0){
        if(WL.String.IsWhiteSpace(Code)){ return WL.String.Empty; }

        if(AutoInline){ Code = Inline(Code); }

        StringBuilder SB = new StringBuilder();
        int I = StartIndent;
        bool InString = false;
        bool InComment = false;
        bool InForeach = false;
        
        void ApplyIndent(){ for(int i = 0; i < I; i++){ SB.Append(Indent); } }
        void AddC(char C = '\n'){ SB.Append(C); }
        void AddS(string S     ){ SB.Append(S); }
        
        char Get(int Index){
            if(Index < 0 || Index >= Code.Length){ return '\0'; }
            return Code[Index];
        }

        string Line = "// ----------------------------------------------------------------------";
        
        for(int i = 0; i < Code.Length; i++){
            char C  = Code[i];
            char CP = Get(i - 1);
            char CN = Get(i + 1);

            if(C == '·'){ C = ' '; }
            
            if(!InString && !InComment){
                string __Substring = Code.Substring(i);

                if(WL.String.AtLeft(__Substring, "for(") || WL.String.AtLeft(__Substring, "foreach(")){
                    InForeach = true;
                }
                
                if(WL.String.AtLeft(__Substring, SpecialSymbol + "LINE" + SpecialSymbol)){
                    AddC();
                    ApplyIndent();
                    AddS(Line);
                    AddC();
                    ApplyIndent();
                    AddC();
                    ApplyIndent();
                
                    i += 5;
                    continue;
                }
                
                if(WL.String.AtLeft(__Substring, SpecialSymbol + "LINE2" + SpecialSymbol)){
                    AddS(Line);
                    AddC();
                    ApplyIndent();
                    AddC();
                    ApplyIndent();
                
                    i += 6;
                    continue;
                }
            
                if(WL.String.AtLeft(__Substring, SpecialSymbol + "NEXTLINE" + SpecialSymbol)){
                    AddC();
                    ApplyIndent();
                
                    i += 9;
                    continue;
                }
                
                if(WL.String.AtLeft(__Substring, SpecialSymbol + "IMPL_AIL" + SpecialSymbol)){
                    AddS("[MethodImpl(MethodImplOptions.AggressiveInlining)]");
                    AddC();
                    ApplyIndent();
                
                    i += 9;
                    continue;
                }

                if(WL.String.AtLeft(__Substring, SpecialSymbol + "SUM<|")){
                    int Start = i + 6;
                    int End = Code.IndexOf("|>" + SpecialSymbol, Start, StringComparison.Ordinal);

                    if(End != -1){
                        string Content = Code.Substring(Start, End - Start);

                        AddS("/// <summary>");
                        AddC();
                        ApplyIndent();
                        
                        AddS("/// " + Content);
                        AddC();
                        ApplyIndent();
                        
                        AddS("/// </summary>");
                        AddC();
                        ApplyIndent();
                        
                        i = End + 2;
                        continue;
                    }
                }
                
                if(WL.String.AtLeft(__Substring, SpecialSymbol + "WLHINT<|")){
                    int Start = i + (SpecialSymbol + "WLHINT<|").Length;
                    
                    int Middle = Code.IndexOf("|" + SpecialSymbol + "|", Start, StringComparison.Ordinal);
                    int End = Code.IndexOf("|>" + SpecialSymbol, Start, StringComparison.Ordinal);
                    
                    if(End != -1){
                        string Hint;
                        string? Message = null;

                        if(Middle != -1 && Middle < End){
                            Hint = Code.Substring(Start, Middle - Start);
                            Message = Code.Substring(Middle + 3, End - (Middle + 3));
                        }else{
                            Hint = Code.Substring(Start, End - Start);
                        }
                        
                        AddS("[WoowzLibHint(Information." + Hint + (Message != null ? ", \"" + Message + "\"" : "") + ")]");
                        AddC();
                        ApplyIndent();

                        i = End + ("|>" + SpecialSymbol).Length - 1;
                        continue;
                    }
                }
            }
           
            if(C == '"'){
                bool Escaped = CP == '\\';
                if(!Escaped){ InString = !InString; }

                AddC(C);
                continue;
            }

            if(!InString && !InComment && C == '/' && CN == '*'){
                InComment = true;
                
                ApplyIndent();
                
                AddS("/*");
                i++;
                continue;
            }

            if(InComment && C == '*' && CN == '/'){
                InComment = false;
                
                AddS("*/");
                i++;
                
                AddC();
                ApplyIndent();
                
                continue;
            }
            
            if(InString || InComment){ AddC(C); continue; }

            if(InForeach && C == ')'){ InForeach = false; }

            switch(C){
                case '{': {
                    AddC('{');
                    if(CN != '}'){
                        AddC();
                        I++;
                        ApplyIndent();
                    }

                    break;
                }
                
                case '}': {
                    if(CP != '{'){
                        AddC();
                        I--;
                        ApplyIndent();
                    }

                    AddC('}');

                    if(CN != ';' && CN != '}' && CN != ')'){
                        AddC();
                        ApplyIndent();
                    }
                    
                    break;
                }
                
                case ';': {
                    AddS(";");
                    if(InForeach){
                        AddC(' ');
                    }else{
                        if(CN != '}'){
                            AddC();
                            ApplyIndent();
                        }
                    }

                    break;
                }
                
                default: {
                    SB.Append(C);
                    break;
                }
            }
        }

        string Result = WL.String.TrimRight(SB.ToString());
        
        return Result;
    }
    
    // ----------------------------------------------------------------------

    public const string SpecialSymbol = "\u2623";
    public static string Generate_Line(bool PrevNextLine = true) => Generate_SpecialTag("LINE" + (PrevNextLine ? "" : "2"));
    public static string Generate_NextLine() => Generate_SpecialTag("NEXTLINE");
    public static string Generate_AggressiveInlining() => Generate_SpecialTag("IMPL_AIL");
    public static string Generate_Summary(string Comment) => Generate_SpecialTag("SUM<|" + Comment + "|>");
    public static string Generate_SpecialTag(string Content) => SpecialSymbol + Content + SpecialSymbol;
    public static string Generate_WLTag(Information Hint, string? Message = null) => Generate_SpecialTag("WLHINT<|" + Hint + (Message != null ? "|" + SpecialSymbol + "|" + Message : "") + "|>");
    
    public static string Generate_GeneratorComment(string Class) => "/* Сгенерировано с помощью " + WL.Core.Metadata.Project.Name + " " + WL.Core.Metadata.Project.Version + ", внутри класса \"" + Class + ".cs\" */";
    public static string Generate_Namespace(string Name) => "namespace " + Name + ";";
    public static string Generate_PublicClass(string Name, string? Parent = null, bool Static = false) => "public " + (Static ? "static " : "") + "class " + Name + (Parent != null ? " : " + Parent : "");
    public static string Generate_PublicStruct(string Name, string? Parent = null, bool Static = false) => "public " + (Static ? "static " : "") + "struct " + Name + (Parent != null ? " : " + Parent : "");
    public static string Generate_Using(string Name) => "using " + Name + ";";
    public static string Generate_Constructor(string Name, string Params, string Inside, string? Base = null) => "public " + Name + "(" + Params + ")" + (Base != null ? " : this(" + Base + ")" : "") + "{" + Inside + "}";
    public static string Generate_Operator(string Name, string Operator, string Params, string Inside, string? Return = null, bool Lambda = true) => Other.Generate_AggressiveInlining() + "public static " + (Return ?? Name) + " operator " + Operator + "(" + Params + ")" + (Lambda ? " => " : "{") + Inside + (Lambda ? ";" : "}");
}