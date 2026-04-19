using System.Text;

namespace WL;

public static partial class __Base{
    public static class Other{
        /// <summary>
        /// Превращает в строку
        /// </summary>
        public static string ToString(object? Object = null){
            if(Object == null){ return "null"; }
            return Object.ToString() ?? "null";
        }

        /// <summary>
        /// Превращает в красивую строку (если объект строка, то делает в кавычках)
        /// </summary>
        public static string ToBeautifulString(object? Object = null){
            return Object switch{
                null             => "null",
                string String    => '"' + String + '"',
                StringBuilder SB => '"' + SB.ToString() + '"',
                var _            => Object.ToString() ?? "null"
            };
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

        /// <summary>
        /// Очень чётко сравнивает объекты
        /// </summary>
        public static bool EqualsNice<T>(T A, T B) => EqualityComparer<T>.Default.Equals(A, B);
    }
}