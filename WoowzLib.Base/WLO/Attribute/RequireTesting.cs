namespace WLO.Attribute{

    /// <summary>
    /// Нужен тест, неизвестное поведение
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    public sealed class RequireTesting : System.Attribute{
        public TestingInformation Information{ get; }
        public string? Message{ get; }

        public RequireTesting(TestingInformation Information = TestingInformation.Unknown, string? Message = null){ this.Information = Information; this.Message = Message; }
    }
}

namespace WLO{
    public enum TestingInformation{
        /// <summary>
        /// Не выбрано
        /// </summary>
        Unknown,
        /// <summary>
        /// Новое, не протестированное (только создано, лень было протестировать)
        /// </summary>
        New,
        /// <summary>
        /// Нужен глобальный тест, возможно переписать всё надо
        /// </summary>
        Global
    }
}