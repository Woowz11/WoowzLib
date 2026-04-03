using WLO.Attribute;

namespace WLO;

[WoowzLibHint(Information.WorkInProgress)]
public class SceneAlgorithm<T>{
    // Переменная, указывающая на сколько возможен по глубине иерархия


    public SceneAlgorithm(){
        ID = TotalID++;
    }
    
    private readonly long ID;
    private static   long TotalID;
    
    // добавляет новый объект
    public SceneNode<T> Add(T NewChild) => Add(new SceneNode<T>(NewChild));

    // добавляет существующий объект
    public SceneNode<T> Add(SceneNode<T> Child){
        if(Child.Scene == this){ throw new Exception("Объект уже добавлен на сцену!"); }
        
        Child.Scene = this;
        
        return Child;
    }

    // удаляет существующий объект
    public void Remove(SceneNode<T> Child){
        if(Child.Scene != this){ throw new Exception("Объект привязан к другой сцене, или вообще не привязан!"); }
        
        Child.Scene = null;
    }
    
    // ищет по значению
    public bool Contains(T Object) => false;
    
    // ищет объект
    public bool Contains(SceneNode<T> Object) => false;

    // всего объектов на сцене
    public int Count => Childrens.Count;

    // возвращает объекты на определённом слое
    public SceneNode<T>[] GetLayer(uint Layer){
        if(Layer == 0){
            return Layer0.ToArray();
        }else{
            return [];
        }
    }

    // все объекты на каждом слое
    public List<SceneNode<T>> Layer0 = [];

    // все объекты у сцены
    public List<SceneNode<T>> Childrens = [];

    internal void __Add(SceneNode<T> Child){
        
    }
    
    internal void __Remove(SceneNode<T> Child){
        
    }

    internal void __Update(){
        
    }
    
    // ----------------------------------------------------------------------

    public override string ToString() => "SceneAlg.(" + Layer0.Count + " (" + Count + "))";
    
    public override bool Equals(object? obj){
        if(obj is SceneAlgorithm<T> other){ return ID == other.ID; }
        return false;
    }

    public override int GetHashCode(){
        return ID.GetHashCode();
    }
}

[WoowzLibHint(Information.WorkInProgress)]
public class SceneNode<T>{
    public SceneNode(T Self){
        ID = TotalID++;
        this.Self = Self;
    }

    private readonly long ID;
    private static   long TotalID;
    
    // объект к которому привязан нод
    public readonly T Self;

    // сцена к которой привязан объект
    public SceneAlgorithm<T>? Scene{
        get => __Scene;
        set{
            if(__Scene == value){ return; }

            if(__Scene != null){
                __Scene.__Remove(this);
            }
            
            __Scene = value;
            Parent = null;
            
            if(__Scene != null){
                __Scene.__Add(this);
            }
        }
    }
    private SceneAlgorithm<T>? __Scene;

    // в памяти?
    public bool InMemory => Scene == null;
    
    // родитель
    public SceneNode<T>? Parent;
    
    
    
    // добавляет новый объект
    public void Add(T NewChild){
        Add(new SceneNode<T>(NewChild));
    }

    // добавляет существующий объект
    public void Add(SceneNode<T> Child){
        
    }

    // удаляет существующий объект
    public void Remove(SceneNode<T> Child){
        
    }
    
    // ищет по значению
    public bool Contains(T Object) => false;
    
    // ищет объект
    public bool Contains(SceneNode<T> Object) => false;
    
    // возвращает объекты на определённом слое
    public SceneNode<T>[] GetLayer(uint Layer){
        if(Layer == 0){
            return Layer0.ToArray();
        }else{
            return [];
        }
    }

    // все объекты на каждом слое
    public List<SceneNode<T>> Layer0 = [];

    // все объекты
    public List<SceneNode<T>> Childrens = [];
    
    // ----------------------------------------------------------------------

    public override string ToString() => "SN(" + Self + ", " + WL.__Base.Other.ToString(Parent) + " (" + (Scene == null ? "В памяти" : "На сцене") + "), " + Layer0.Count + " (" + Childrens.Count + "))";

    public override bool Equals(object? obj){
        if(obj is SceneNode<T> other){ return ID == other.ID; }
        return false;
    }

    public override int GetHashCode(){
        return ID.GetHashCode();
    }
}