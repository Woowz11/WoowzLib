namespace WLO;

/// <summary>
/// Dictionary состоящий по логике из двух пар ключей (обе пары должны быть уникальными)
/// </summary>
public class BiDictionary<T> where T : notnull{
    private readonly Dictionary<T, T> K = new Dictionary<T, T>();
    private readonly Dictionary<T, T> V = new Dictionary<T, T>();
    
    public void Add(T Key, T Value, bool Reverse = false){
        if(Reverse){
            V[Key  ] = Value;
            K[Value] = Key  ;   
        }else{
            K[Key  ] = Value;
            V[Value] = Key  ;   
        }
    }

    public T this[T Key, bool Reverse = false]{
        get => Reverse ? V[Key] : K[Key];
        set => Add(Key, value, Reverse);
    }

    public int Count => V.Count;
    
    public bool TryGet(T Key, out T? Value, bool Reverse = false) => Reverse ? V.TryGetValue(Key, out Value) : K.TryGetValue(Key, out Value);

    public bool ContainsKey  (T Key  ) => K.ContainsKey(Key);
    public bool ContainsValue(T Value) => V.ContainsKey(Value);
}