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
    }
}