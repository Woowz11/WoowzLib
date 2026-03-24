using System.Runtime.CompilerServices;

namespace WLO;

/// <summary>
/// Система Child/Parent
/// </summary>
public class SceneNode<T>{
    /// <summary>
    /// Создаёт компонент с системой Child/Parent
    /// </summary>
    /// <param name="Target">Основа</param>
    /// <param name="ConstantParent">Можно изменять родителей?</param>
    /// <param name="StartingParent">Стартовый родитель</param>
    public SceneNode(T Target, bool ConstantParent = false, SceneNode<T>? StartingParent = null){
        Self = Target;

        if(StartingParent != null){ Parent = StartingParent; }
        
        this.ConstantParent = ConstantParent;
    }

    /// <summary>
    /// Привязанный компонент
    /// </summary>
    public readonly T Self;

    /// <summary>
    /// Нельзя изменить родителя?
    /// </summary>
    public readonly bool ConstantParent;

    /// <summary>
    /// Можно использовать? (Можно изменить переменную)
    /// </summary>
    public bool CanUse = true;
    
    // ----------------------------------------------------------------------

    private SceneNode<T>? __Parent;
    /// <summary>
    /// Родитель
    /// </summary>
    public SceneNode<T>? Parent{
        get => __Parent;
        set => __SetParent(value);
    }

    /// <summary>
    /// Есть родитель?
    /// </summary>
    public bool HasParent => Parent != null;
    
    /// <summary>
    /// Дети
    /// </summary>
    public IReadOnlyList<SceneNode<T>> Children => __Children;
    private readonly List<SceneNode<T>> __Children = [];

    /// <summary>
    /// Добавляет ребёнка
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add(SceneNode<T> Child) => Child.Parent = this;

    /// <summary>
    /// Удаляет ребёнка
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Remove(SceneNode<T> Child){
        if(Contains(Child)){ Child.Parent = null; }
    }

    /// <summary>
    /// Удаляет всех детей
    /// </summary>
    public void ClearAll(){
        try{
            if(!CanUse){ throw new Exception("Нельзя использовать!"); }

            try{
                OnChildRemoveAll?.Invoke();
            }
            catch(Exception e){
                Logger.Error("Произошла ошибка в ивенте OnChildRemoveAll в [" + this + "]!", e);
            }

            for(int i = Count - 1; i >= 0; i--){
                __Children[i].Parent = null;
            }
        }catch(Exception e){
            throw new Exception("Не получилось очистить всех детей у компонента [" + this + "]!", e);
        }
    }

    /// <summary>
    /// Получить компонент по индексу
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Get(int Index) => __Children[Index].Self;
    
    // ----------------------------------------------------------------------

    /// <summary>
    /// Этот компонент есть внутри?
    /// </summary>
    /// <param name="Child">Компонент</param>
    public bool IsDescendantOf(SceneNode<T> Child){
        try{
            if(!CanUse){ throw new Exception("Нельзя использовать!"); }

            SceneNode<T>? Current = this;

            while(Current != null){
                if(Current == Child){ return true; }
                Current = Current.Parent;
            }

            return false;
        }catch(Exception e){
            throw new Exception("Не получилось вызывать IsDescendantOf у [" + this + "]!\nКомпонент: " + Child, e);
        }
    }

    /// <summary>
    /// Этот компонент есть в детях у компонента?
    /// </summary>
    /// <param name="Child">Компонент</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(SceneNode<T> Child) => __Children.Contains(Child);

    /// <summary>
    /// Кол-во детей
    /// </summary>
    public int Count => __Children.Count;
    
    // ----------------------------------------------------------------------

    /// <summary>
    /// Вызывается когда меняется родитель (старый, новый)
    /// </summary>
    public event Action<SceneNode<T>?, SceneNode<T>?>? OnParent;
    
    /// <summary>
    /// Вызывается при добавлении ребёнка
    /// </summary>
    public event Action<SceneNode<T>>? OnChildAdd;
    
    /// <summary>
    /// Вызывается при удалении ребёнка
    /// </summary>
    public event Action<SceneNode<T>>? OnChildRemove;
    
    /// <summary>
    /// Вызывается при удалении всех детей (перед удалением)
    /// </summary>
    public event Action? OnChildRemoveAll;
    
    // ----------------------------------------------------------------------

    /// <summary>
    /// Устанавливает родителя
    /// </summary>
    /// <param name="NewParent"></param>
    private void __SetParent(SceneNode<T>? NewParent){
        try{
            if(!CanUse){ throw new Exception("Нельзя использовать!"); }
            
            if(ConstantParent){ throw new Exception("Нельзя изменить родителя у компонента!"); }
                
            if(__Parent == NewParent){ return; }

            if(NewParent != null && NewParent.IsDescendantOf(this)){ throw new Exception("Обнаружена цикличность! Этот компонент уже есть в компоненте!"); }
            
            SceneNode<T>? OldParent = __Parent;

            if(OldParent != null){
                OldParent.__Children.Remove(this);

                try{
                    OldParent.OnChildRemove?.Invoke(this);
                }catch(Exception e){
                    Logger.Error("Произошла ошибка в ивенте OnChildRemove в [" + OldParent + "]!", e);
                }
            }
            
            __Parent = NewParent;
            
            try{
                OnParent?.Invoke(OldParent, NewParent);
            }catch(Exception e){
                Logger.Error("Произошла ошибка в ивенте OnParent в [" + this + "]!", e);
            }

            if(NewParent != null && !NewParent.Contains(this)){
                NewParent.__Children.Add(this);

                try{
                    NewParent.OnChildAdd?.Invoke(this);
                }catch(Exception e){
                    Logger.Error("Произошла ошибка в ивенте OnChildAdd в [" + NewParent + "]!", e);
                }
            }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при установке родителя компоненту [" + this + "]!\nРодитель: " + WL.__Base.Other.ToString(NewParent), e);
        }
    }
    
    // ----------------------------------------------------------------------

    public override string ToString() => "SceneNode<" + typeof(T).Name + ">(" + Self + ", " + (!CanUse ? "Нельзя использовать" : (Count + ", " + (Parent != null ? Parent.ToShortString() : "null"))) + ")";

    public string ToShortString() => "SceneNode<" + typeof(T).Name + ">(" + Self + ", " + Count + ")";
    
    public T this[int Index] => Get(Index);
}