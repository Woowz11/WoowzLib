namespace WLO;

/// <summary>
/// Таблица значений, где ключи - ключи, и значения - ключи
/// </summary>
/// <typeparam name="K">Ключи</typeparam>
/// <typeparam name="V">Значения</typeparam>
public class BiDictionary<K, V> where K : notnull where V : notnull{
    public BiDictionary(int Capacity){
        __K = new Dictionary<K, V>(Capacity);
        __V = new Dictionary<V, K>(Capacity);
    }
    public BiDictionary() : this(0){}

    /// <summary>
    /// Ключи
    /// </summary>
    private readonly Dictionary<K, V> __K;
    
    /// <summary>
    /// Значения
    /// </summary>
    private readonly Dictionary<V, K> __V;

    /// <summary>
    /// Добавляет значение к ключу, если уже есть, выдаст ошибку
    /// </summary>
    public void Add(K Key, V Value){
        try{
            if(__K.ContainsKey(Key  )){ throw new Exception("Ключ уже есть в таблице!"    ); }
            if(__V.ContainsKey(Value)){ throw new Exception("Значение уже есть в таблице!"); }

            __K[Key  ] = Value;
            __V[Value] = Key  ;
        }catch(Exception e){
            throw new Exception($"Произошла ошибка при добавлении ключа, значения в [{this}]!\nКлюч: {Key}\nЗначение: {Value}", e);
        }
    }

    /// <summary>
    /// Кол-во значени
    /// </summary>
    public int Count => __K.Count;

    /// <summary>
    /// Одинаковый тип K, V?
    /// </summary>
    public readonly bool ItSameType = typeof(K) == typeof(V);

    /// <summary>
    /// Пытается получить значения у ключа, или наоборот
    /// </summary>
    /// <param name="Reverse">Наоборот? т.е, получить ключ у значения</param>
    public bool TryGet(K Key, out K? Value, bool Reverse = false){
        try{
            if(!ItSameType){ throw new Exception("Работает только в ItSameType!"); }

            if(Reverse){
                if(TryGetKey((V)(object)Key, out K? Result)){
                    Value = Result!;
                    return true;
                }
            }else{
                if(TryGetValue(Key, out V? Result)){
                    Value = (K)(object)Result!;
                    return true;
                }
            }

            Value = default;
            return false;
        }catch(Exception e){
            throw new Exception($"Произошла ошибка при получении ключа/значения у [{this}]!\nКлюч/Значение: {Key}\nКлюч это значение?: {Reverse}", e);
        }
    }
    
    // ----------------------------------------------------------------------
    
    /// <summary>
    /// Будет работать только если K и V отличаются! Иначе используйте SetValue
    /// </summary>
    public V this[K Key]{
        get => GetValue(Key);
        set => SetValue(Key, value);
    }
    
    /// <summary>
    /// Устанавливает значение к ключу
    /// </summary>
    public void SetValue(K Key, V Value){
        if(__K.TryGetValue(Key, out V? OValue)){
            __V.Remove(OValue);
        }

        if(__V.TryGetValue(Value, out K? EKey) && !EqualityComparer<K>.Default.Equals(EKey, Key)){ throw new Exception("Значение уже существует для другого ключа!"); }

        __K[Key  ] = Value;
        __V[Value] = Key  ;
    }

    /// <summary>
    /// Получить значение
    /// </summary>
    public V GetValue(K Key) => __K[Key];

    /// <summary>
    /// Пытается получить значение
    /// </summary>
    public bool TryGetValue(K Key, out V? Value) => __K.TryGetValue(Key, out Value);

    /// <summary>
    /// Проверяет, есть ли такой ключ?
    /// </summary>
    public bool ContainsKey(K Key) => __K.ContainsKey(Key);
    
    // ----------------------------------------------------------------------

    /// <summary>
    /// Будет работать только если K и V отличаются! Иначе используйте SetKey
    /// </summary>
    public K this[V Value]{
        get => GetKey(Value);
        set => SetKey(Value, value);
    }
    
    /// <summary>
    /// Устанавливает ключ к значению
    /// </summary>
    public void SetKey(V Value, K Key){
        if(__V.TryGetValue(Value, out K? OKey)){
            __K.Remove(OKey);
        }

        if(__K.TryGetValue(Key, out V? EValue) && !EqualityComparer<V>.Default.Equals(EValue, Value)){ throw new Exception("Ключ уже существует для другого значения!"); }

        __V[Value] = Key  ;
        __K[Key  ] = Value;
    }

    /// <summary>
    /// Получить ключ
    /// </summary>
    public K GetKey(V Value) => __V[Value];

    /// <summary>
    /// Пытается получить ключ
    /// </summary>
    public bool TryGetKey(V Value, out K? Key) => __V.TryGetValue(Value, out Key);

    /// <summary>
    /// Проверяет, есть ли такое значение?
    /// </summary>
    public bool ContainsValue(V Value) => __V.ContainsKey(Value);
    
    // ----------------------------------------------------------------------

    public override string ToString() => $"BiDictionary<{typeof(K).Name}, {typeof(V).Name}>({Count})";
}