using WLO.Attribute;

namespace WLO;

/// <summary>
/// Сцена с детьми
/// </summary>
[WoowzLibHint(Information.WorkInProgress)]
public class SceneAlgorithm<T>{
    private readonly List<SceneNode<T>> __Childrens    = [];
    private readonly List<SceneNode<T>> __AllChildrens = [];

    public IReadOnlyList<SceneNode<T>> Childrens => __Childrens;
    public IReadOnlyList<SceneNode<T>> AllChildrens => __AllChildrens;
    
    public bool Containts(T Child) => __Childrens.Any(N => EqualityComparer<T>.Default.Equals(N.Self, Child));
    public bool ContaintsToAll(T Child) => __AllChildrens.Any(N => EqualityComparer<T>.Default.Equals(N.Self, Child));
    
    public bool Containts(SceneNode<T> Child) => __Childrens.Any(N => EqualityComparer<SceneNode<T>>.Default.Equals(N, Child));
    public bool ContaintsToAll(SceneNode<T> Child) => __AllChildrens.Any(N => EqualityComparer<SceneNode<T>>.Default.Equals(N, Child));

    public void Add(T Child) => Add(new SceneNode<T>(Child));
    public void Add(SceneNode<T> Child){
        try{
            if(ContaintsToAll(Child)){ throw new Exception("Этот элемент уже есть на сцене!"); }

            Child.Scene = this;
        }catch(Exception e){
            throw new Exception();
        }
    }

    public void Remove(SceneNode<T> Child){
        try{
            
        }catch(Exception e){
            throw new Exception();
        }
    }
}

/// <summary>
/// Сценичный объект, который имеет детей и родителя
/// </summary>
[WoowzLibHint(Information.WorkInProgress)]
public class SceneNode<T>{
    public SceneNode(T Self){
        this.Self = Self;
    }

    public SceneAlgorithm<T>? Scene{
        get => __Scene;
        set{
            try{
                if(__Scene == value){ return; }
                
                if(__Scene != null){
                    
                }
                
                __Scene = value;
                
                if(__Scene != null){
                       
                }
            }catch(Exception e){
                throw new Exception();
            }
        }
    }
    private SceneAlgorithm<T>? __Scene;

    public bool InMemory => __Scene == null;
    
    public SceneNode<T>? Parent;
    public readonly List<SceneNode<T>> Childrens = [];

    public T Self;
}