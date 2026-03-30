using System.ComponentModel;
using System.Text;

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
                
                if(c == ' ' && SB.Length > 0 && "{};/".Contains(SB[^1])){ continue; }
                
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
    public static string Beautify(string Code, string Indent = "\t"){
        if(WL.String.IsWhiteSpace(Code)){ return WL.String.Empty; }

        Code = Inline(Code);
        
        StringBuilder SB = new StringBuilder();
        int I = 0;
        bool InString = false;
        bool InComment = false;
        
        void ApplyIndent(){ for(int i = 0; i < I; i++){ SB.Append(Indent); } }
        void AddC(char C = '\n'){ SB.Append(C); }
        void AddS(string S     ){ SB.Append(S); }
        
        char Get(int Index){
            if(Index < 0 || Index >= Code.Length){ return '\0'; }
            return Code[Index];
        }
        
        for(int i = 0; i < Code.Length; i++){
            char C  = Code[i];
            char CP = Get(i - 1);
            char CN = Get(i + 1);

            if(!InString && !InComment && WL.String.AtLeft(Code.Substring(i), "@LINE@")){
                AddC();
                ApplyIndent();
                AddS("// ----------------------------------------------------------------------");
                AddC();
                ApplyIndent();
                AddC();
                ApplyIndent();
                
                i += 5;
                continue;
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
            
            switch(C){
                case '{': {
                    AddS("{\n");
                    I++;
                    ApplyIndent();
                    
                    break;
                }
                
                case '}': {
                    AddC();
                    I--;
                    ApplyIndent();
                    AddC('}');

                    if(CN != ';' && CN != '}' && CN != ')'){
                        AddC();
                        ApplyIndent();
                    }
                    
                    break;
                }
                
                case ';': {
                    AddS(";");
                    if(CN != '}'){
                        AddC();
                        ApplyIndent();
                    }
                    
                    break;
                }
                
                default: {
                    SB.Append(C);
                    break;
                }
            }
        }

        if(SB.Length > 0 && SB[^1] == '\n'){ SB.Length--; }

        string Result = SB.ToString();
        
        return Result;
    }
    
    // ----------------------------------------------------------------------

    public static string Generate_GeneratorComment(string Class) => "/* Сгенерировано с помощью " + WL.Core.Metadata.Project.Name + " " + WL.Core.Metadata.Project.Version + ", внутри класса \"" + Class + ".cs\" */";
    public static string Generate_Namespace(string Name) => "namespace " + Name + ";";
    public static string Generate_PublicStaticClass(string Name, string? Parent = null) => "public static class " + Name + (Parent != null ? " : " + Parent : "");
    public static string Generate_Line() => "@LINE@";
}