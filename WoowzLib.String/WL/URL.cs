namespace WL;

public static partial class String{
    public static class URL{
        static URL(){
            InvalidChars = [' ', '<', '>', '|', '\\', '^', '`', '{', '}'];
        }
        
        /// <summary>
        /// Пустая ссылка
        /// </summary>
        public const string Blank = "about:blank";

        /// <summary>
        /// Строка LocalHost
        /// </summary>
        public const string LocalHost = "localhost";

        /// <summary>
        /// Запрещённые ссылки в ссылке
        /// </summary>
        public static readonly char[] InvalidChars;
        
        // ----------------------------------------------------------------------
        
        /// <summary>
        /// Проверяет, корректная ли ссылка, иначе выдаёт исключения
        /// </summary>
        public static void Validate(string URL){
            try{
                URL = Trim(URL);

                if(IsEmpty(URL)){ throw new Exception("Ссылка пустая!"); }
                
                if(URL == Blank){ return; }

                bool HasProtocol = Contains(URL, "://");

                if(!HasProtocol){
                    if(!AtLeft(URL, LocalHost) && !Contains(URL, '.', ':')){
                        throw new Exception("Если нет протокола, неверный домен!");
                    }

                    if(Contains(URL, ' ')){ throw new Exception("Если нет протокола, не может содержать пробелы!"); }
                }else{
                    string Protocol = Sub(URL, 0, IndexOf(URL, "://"));
                    if(IsEmpty(Protocol)){ throw new Exception("Если есть протокол, протокол не найден!"); }

                    if(!ValidateProtocol(Protocol)){ throw new Exception("Если есть протокол, неверный протокол!"); }

                    string AfterProtocol = Sub(URL, IndexOf(URL, "://") + 3);
                    if(IsEmpty(AfterProtocol)){ throw new Exception("Если есть протокол, после протокола ничего не найдено!"); }

                    if(AtLeft(AfterProtocol, '/')){ throw new Exception("Если есть протокол, после протокола должен быть хост, а не слеш!"); }
                }

                if(Contains(URL, InvalidChars)){ throw new Exception("Содержит не поддерживаемые символы!"); }
            }catch(Exception e){
                throw new Exception($"Произошла ошибка при проверке ссылки [{URL}]!", e);
            }
        }

        /// <summary>
        /// Проверяет, корректная ли ссылка
        /// </summary>
        public static bool IsValid(string URL){
            try{
                Validate(URL);
                return true;
            }catch{
                return false;
            }
        }

        /// <summary>
        /// Проверяет, корректный протокол ссылки или нет
        /// </summary>
        public static bool ValidateProtocol(string Protocol) => Protocol is "http" or "https" or "file" or "ftp" or "ws" or "wss";
    }
}