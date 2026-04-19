namespace WLO;

/// <summary>
/// Поддерживаемые значения Transform
/// </summary>
[Flags]
public enum TransformType{
    /// <summary>
    /// Никакие
    /// </summary>
    None = 0,
    
    /// <summary>
    /// Только позиция
    /// </summary>
    Position = 1 << 0,
    /// <summary>
    /// Только размер
    /// </summary>
    Size     = 1 << 1,
    /// <summary>
    /// Только поворот
    /// </summary>
    Rotation = 1 << 2,
    
    /// <summary>
    /// Все значения
    /// </summary>
    All = Position | Size | Rotation
}