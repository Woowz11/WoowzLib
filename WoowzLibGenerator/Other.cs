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
        
        void ApplyIndent(){ for(int i = 0; i < I; i++){ SB.Append(Indent); } }

        char Get(int Index){
            if(Index < 0 || Index >= Code.Length){ return '\0'; }
            return Code[Index];
        }
        
        for(int i = 0; i < Code.Length; i++){
            char C  = Code[i];
            char CP = Get(i - 1);
            char CN = Get(i + 1);

            switch(C){
                case '"':{
                    SB.Append('"');

                    bool Escaped = Get(i - 1) == '\\';
                    if(!Escaped){ InString = !InString; }
                    break;
                }
                
                case '{': {
                    SB.Append('{');
                    if(!InString){
                        SB.Append('\n');
                        I++;
                        ApplyIndent();
                    }

                    break;
                }
                
                case '}': {
                    if(!InString){
                        I--;
                        ApplyIndent();
                    }
                    SB.Append('}');
                    if(!InString){
                        SB.Append('\n');
                        ApplyIndent();
                    }

                    break;
                }

                case '*':{
                    SB.Append('*');
                    
                    break;  
                }
                
                case ';': {
                    SB.Append(';');
                    if(!InString){
                        SB.Append('\n');
                        if(Get(i + 1) != '}'){
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
        
        return SB.ToString();
    }
}