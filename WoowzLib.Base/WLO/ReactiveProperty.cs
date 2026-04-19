namespace WLO;

/// <summary>
/// Поле на которое можно привязать ивент
/// </summary>
public class ReactiveProperty<T>{
    public ReactiveProperty(T Initial = default!){ __Value = Initial; }
    
    /// <summary>
    /// Значение
    /// </summary>
    private T __Value;

    /// <summary>
    /// Значение
    /// </summary>
    public T Value{
        get => __Value;
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
                            Logger.Error($"Произошла ошибка при вызове ивента OnApply у [{this}]!\nСтарое значение: {Old}\nНовое значение: {New}", e);
                        }
                    }
                }
                
                if(WL.__Base.Other.EqualsNice(Old, New)){ return; }

                try{
                    OnChanged?.Invoke(Old, New);
                }catch(Exception e){
                    Logger.Error($"Произошла ошибка при вызове ивента OnChanged у [{this}]!\nСтарое значение: {Old}\nНовое значение: {New}", e);
                }

                __Value = New;
            }catch(Exception e){
                throw new Exception($"Произошла ошибка при применении нового значения у ReactiveProperty [{this}]!\nНовое значение: {WL.__Base.Other.ToString(value)}", e);
            }
        }
    }

    /// <summary>
    /// Вызывается при изменении значения (Старое значение, новое значение)
    /// </summary>
    public event Action<T, T>? OnChanged;
    
    /// <summary>
    /// Вызывается при применении значения (всегда) (Старое значение, новое значение) => (Изменённое новое значение (null для отмены))
    /// </summary>
    public event Func<T, T, T?>? OnApply;
}