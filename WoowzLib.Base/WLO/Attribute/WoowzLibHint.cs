namespace WLO.Attribute{

    [Flags]
    public enum Information{
        /// <summary>
        /// Не выбрана информация
        /// </summary>
        Unknown,
        /// <summary>
        /// Новое, не протестированное (только создано, лень было протестировать)
        /// </summary>
        New,
        /// <summary>
        /// Нужен глобальный тест, возможно переписать всё надо
        /// </summary>
        Global,
        /// <summary>
        /// В разработке
        /// </summary>
        WorkInProgress,
        /// <summary>
        /// Нужно переделать
        /// </summary>
        NeedRemake,
        /// <summary>
        /// Нужно обдумать
        /// </summary>
        Brainstorming,
        /// <summary>
        /// Нужно тестить и смотреть
        /// </summary>
        Testing,
        /// <summary>
        /// Заброшен, нужно доделать в будущем
        /// </summary>
        Abandoned
    }
    
    /// <summary>
    /// Подсказка для разработчика WoowzLib
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    public sealed class WoowzLibHint : System.Attribute{
        public WoowzLibHint(Information Information = Information.Unknown, string? Message = null){ this.Information = Information; this.Message = Message; }
        
        public readonly Information Information;
        public readonly string?     Message;
    }
}