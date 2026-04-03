using WLO.Attribute;

namespace WLO;

[WoowzLibHint(Information.WorkInProgress)]
public class SceneAlgorithm<T>{
    // Переменная, указывающая на сколько возможен по глубине иерархия
    
    
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
}

[WoowzLibHint(Information.WorkInProgress)]
public class SceneNode<T>{
    public SceneNode(T Self){
        this.Self = Self;
    }

    // объект к которому привязан нод
    public readonly T Self;

    // сцена к которой привязан объект
    public SceneAlgorithm<T>? Scene;

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
}