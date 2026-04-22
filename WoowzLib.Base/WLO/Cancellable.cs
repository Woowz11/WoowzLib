namespace WLO;

/// <summary>
/// Отменяемое значение 
/// </summary>
public struct Cancellable<T>{
    private Cancellable(T Value, bool Cancel){
        this.Value = Value;
        this.Cancel = Cancel;
    }
    
    /// <summary>
    /// Значение
    /// </summary>
    public T Value;
    
    /// <summary>
    /// Отменить?
    /// </summary>
    public bool Cancel;

    // ----------------------------------------------------------------------
    
    /// <summary>
    /// Продолжить
    /// </summary>
    public static Cancellable<T> Continue(T Value) => new Cancellable<T>(Value, false);

    /// <summary>
    /// Отменить
    /// </summary>
    public static Cancellable<T> Cancelled() => new Cancellable<T>(default!, true);
    
    // ----------------------------------------------------------------------

    public override string ToString() => $"Cancellable<{typeof(T).Name}>({ToShortString()})";

    public string ToShortString() => $"{WL.__Base.Other.ToBeautifulString(Value)}, Cancel: {Cancel}";

    public bool Equals(Cancellable<T> Other) => Cancel == Other.Cancel && WL.__Base.Other.EqualsNice(Value, Other.Value);

    public override bool Equals(object? Object) => Object is Cancellable<T> Other && Equals(Other);

    public override int GetHashCode() => HashCode.Combine(Cancel, WL.__Base.Other.HashCodeNice(Value));

    public static bool operator ==(Cancellable<T> Left, Cancellable<T> Right) =>  Left.Equals(Right);
    public static bool operator !=(Cancellable<T> Left, Cancellable<T> Right) => !Left.Equals(Right);
}