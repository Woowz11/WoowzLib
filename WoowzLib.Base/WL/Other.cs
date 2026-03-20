using System.Text;

namespace WL;

public static partial class __Base{
    public static class Other{
        /// <summary>
        /// Превращает в строку
        /// </summary>
        public static string ToString(object? Obj = null){
            if(Obj == null){ return "null"; }
            return Obj.ToString() ?? "null";
        }

        /// <summary>
        /// Объединяет объекты в одну строку
        /// </summary>
        public static string JoinString(object[] Objects){
            if(Objects.Length == 0){ return string.Empty; }

            StringBuilder SB = new StringBuilder();

            for(int i = 0; i < Objects.Length; i++){
                if(i > 0){ SB.Append(", "); }

                SB.Append(ToString(Objects[i]));
            }

            return SB.ToString();
        }
    }
}