namespace WLO;

/// <summary>
/// Поле на которое можно привязать ивент
/// </summary>
public class ReactiveProperty<T> : Metadata{
    public ReactiveProperty(string Name = "?", object? Parent = null, T Initial = default!) : base(Name, Parent){
        __Value = Initial;
    }
    
    /// <summary>
    /// Значение
    /// </summary>
    private T __Value;

    /// <summary>
    /// Значение
    /// </summary>
    public T Value{
        get{
            try{
                T Value = __Value;

                if(OnGet != null){
                    foreach(Delegate Delegate in OnGet.GetInvocationList()){
                        try{
                            Value = ((Func<T, T>)Delegate)(Value);
                        }catch(Exception e){
                            throw new Exception($"Произошла ошибка при вызове ивента OnGet у [{this}]!", e);
                        }
                    }
                }

                return Value;
            }catch(Exception e){
                throw new Exception($"Произошла ошибка при получении значения у ReactiveProperty [{this}]!", e);
            }
        }
        set{
            try{
                T Old = __Value;
                T New = value;

                if(OnApply != null){
                    foreach(Delegate Delegate in OnApply.GetInvocationList()){
                        try{
                            T? Result = ((Func<T, T, T?>)Delegate)(Old, New);
                            if(Result == null){ return; }
                            New = Result;
                        }catch(Exception e){
                            throw new Exception($"Произошла ошибка при вызове ивента OnApply у [{this}]!\nСтарое значение: {Old}\nНовое значение: {New}", e);
                        }
                    }
                }
                
                if(WL.__Base.Other.EqualsNice(Old, New)){ return; }

                try{
                    OnChanged?.Invoke(Old, New);
                }catch(Exception e){
                    throw new Exception($"Произошла ошибка при вызове ивента OnChanged у [{this}]!\nСтарое значение: {Old}\nНовое значение: {New}", e);
                }

                __Value = New;
            }catch(Exception e){
                throw new Exception($"Произошла ошибка при применении нового значения у ReactiveProperty [{this}]!\nНовое значение: {WL.__Base.Other.ToString(value)}", e);
            }
        }
    }

    /// <summary>
    /// Вызывается при изменении значения (Старое значение, новое значение) [Может вызывать исключение!]
    /// </summary>
    public event Action<T, T>? OnChanged;
    
    /// <summary>
    /// Вызывается при применении значения (всегда) (Старое значение, новое значение) => (Изменённое новое значение (null для отмены)) [Может вызывать исключение!]
    /// </summary>
    public event Func<T, T, T?>? OnApply;

    /// <summary>
    /// Вызывается при получении значения (Старое значение) => (Изменённое новое значение) [Может вызывать исключение!]
    /// </summary>
    public event Func<T, T>? OnGet;
    
    // ----------------------------------------------------------------------

    public override string ToString() => $"RP<{typeof(T).Name}>({ToMetadataString()})";
}