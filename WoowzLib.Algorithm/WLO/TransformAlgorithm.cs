using WLO.Attribute;
using WLO.Rect;
using WLO.Vector;

namespace WLO;

public class TransformAlgorithm{
    public TransformAlgorithm(int X = 0, int Y = 0, uint W = 100, uint H = 100){ this.X = X; this.Y = Y; this.W = W; this.H = H; }
    public TransformAlgorithm(Vector2I Position, Vector2UI Size) : this(Position.X, Position.Y, Size.W, Size.H){}
    public TransformAlgorithm(Rect2I Rect) : this(Rect.X, Rect.Y, Rect.W, Rect.H){}
    
    /// <summary>
    /// Позиция по X
    /// </summary>
    public int X{
        get => __X;
        set{
            try{
                if(!CallAnyway && __X == value){ return; }
                __X = value;

                __InvokeOnPosition("Произошла ошибка при вызове ивента OnPosition у [" + this + "]!\nПозиция по X: " + value);

                __InvokeOnRect("Произошла ошибка при вызове ивента OnRect у [" + this + "]!\nПозиция по X: " + value);
                
            }catch(Exception e){
                throw new Exception("Произошла ошибка при изменении позиции по X у [" + this + "]!\nПозиция по X: " + value, e);
            }
        }
    }
    private int __X;
    
    /// <summary>
    /// Позиция по Y
    /// </summary>
    public int Y{
        get => __Y;
        set{
            try{
                if(!CallAnyway && __Y == value){ return; }
                __Y = value;
                
                __InvokeOnPosition("Произошла ошибка при вызове ивента OnPosition у [" + this + "]!\nПозиция по Y: " + value);
                
                __InvokeOnRect("Произошла ошибка при вызове ивента OnRect у [" + this + "]!\nПозиция по Y: " + value);
                
            }catch(Exception e){
                throw new Exception("Произошла ошибка при изменении позиции по Y у [" + this + "]!\nПозиция по Y: " + value, e);
            }
        }
    }
    private int __Y;
    
    /// <summary>
    /// Ширина
    /// </summary>
    public uint W{
        get => __W;
        set{
            try{
                if(!CallAnyway && __W == value){ return; }
                __W = value;
                
                __InvokeOnSize("Произошла ошибка при вызове ивента OnSize у [" + this + "]!\nШирина: " + value);
                
                __InvokeOnRect("Произошла ошибка при вызове ивента OnRect у [" + this + "]!\nШирина: " + value);
                
            }catch(Exception e){
                throw new Exception("Произошла ошибка при изменении ширины у [" + this + "]!\nШирина: " + value, e);
            }
        }
    }
    private uint __W;
    
    /// <summary>
    /// Высота
    /// </summary>
    public uint H{
        get => __H;
        set{
            try{
                if(!CallAnyway && __H == value){ return; }
                __H = value;
                
                __InvokeOnSize("Произошла ошибка при вызове ивента OnSize у [" + this + "]!\nВысота: " + value);
                
                __InvokeOnRect("Произошла ошибка при вызове ивента OnRect у [" + this + "]!\nВысота: " + value);
                
            }catch(Exception e){
                throw new Exception("Произошла ошибка при изменении высоты у [" + this + "]!\nВысота: " + value, e);
            }
        }
    }
    private uint __H;
    
    /// <summary>
    /// Позиция
    /// </summary>
    public Vector2I Position{
        get => new Vector2I(X, Y);
        set{
            try{
                if(!CallAnyway && __X == value.X && __Y == value.Y){ return; }
                __X = value.X;
                __Y = value.Y;
                
                __InvokeOnPosition("Произошла ошибка при вызове ивента OnPosition у [" + this + "]!\nПозиция: " + value);
                
                __InvokeOnRect("Произошла ошибка при вызове ивента OnRect у [" + this + "]!\nПозиция: " + value);
                
            }catch(Exception e){
                throw new Exception("Произошла ошибка при изменении позиции у [" + this + "]!\nПозиция: " + value, e);
            }
        }
    }
    
    /// <summary>
    /// Размер
    /// </summary>
    public Vector2UI Size{
        get => new Vector2UI(W, H);
        set{
            try{
                if(!CallAnyway && __W == value.W && __H == value.H){ return; }
                __W = value.W;
                __H = value.H;
                
                __InvokeOnSize("Произошла ошибка при вызове ивента OnSize у [" + this + "]!\nРазмер: " + value);
                
                __InvokeOnRect("Произошла ошибка при вызове ивента OnRect у [" + this + "]!\nРазмер: " + value);
                
            }catch(Exception e){
                throw new Exception("Произошла ошибка при изменении размера у [" + this + "]!\nРазмер: " + value, e);
            }
        }
    }

    /// <summary>
    /// Позиция и размер
    /// </summary>
    public Rect2I Rect{
        get => new Rect2I(X, Y, W, H);
        set{
            try{
                if(!CallAnyway && __X == value.X && __Y == value.Y && __W == value.W && __H == value.H){ return; }
                __X = value.X;
                __Y = value.Y;
                __W = value.W;
                __H = value.H;
                
                __InvokeOnPosition("Произошла ошибка при вызове ивента OnPosition у [" + this + "]!\nПозиция и размер: " + value);
                
                __InvokeOnSize("Произошла ошибка при вызове ивента OnSize у [" + this + "]!\nПозиция и размер: " + value);
                
                __InvokeOnRect("Произошла ошибка при вызове ивента OnRect у [" + this + "]!\nПозиция и размер: " + value);
                
            }catch(Exception e){
                throw new Exception("Произошла ошибка при изменении позиции и размера у [" + this + "]!\nПозиция и размер: " + value, e);
            }
        }
    }

    /// <summary>
    /// Если true, то ивенты вызываются в любом случае, даже если значения совпадают
    /// </summary>
    public bool CallAnyway = false;
    
    // ----------------------------------------------------------------------

    /// <summary>
    /// Вызывается при изменении позиции (Трансформ, Новая позиция) => (Изменённая позиция)
    /// </summary>
    public event Func<TransformAlgorithm, Vector2I, Vector2I>? OnPosition;
    
    /// <summary>
    /// Вызывается при изменении размера (Трансформ, Новый размер) => (Изменённый размер)
    /// </summary>
    public event Func<TransformAlgorithm, Vector2UI, Vector2UI>? OnSize;
    
    /// <summary>
    /// Вызывается при изменении позиции и размера (Трансформ, Новая позиция и размер) => (Изменённая позиция и размер)
    /// </summary>
    public event Func<TransformAlgorithm, Rect2I, Rect2I>? OnRect;
    
    // ----------------------------------------------------------------------

    private void __InvokeOnPosition(string Exception){
        try{
            if(OnPosition != null){
                foreach(Delegate D in OnPosition.GetInvocationList()){
                    Func<TransformAlgorithm, Vector2I, Vector2I> F = (Func<TransformAlgorithm, Vector2I, Vector2I>)D;
                    Vector2I Position__ = F(this, Position);
                    __X = Position__.X;
                    __Y = Position__.Y;
                }
            }   
        }catch(Exception e){
            Logger.Error(Exception, e);
        }
    }
    
    private void __InvokeOnSize(string Exception){
        try{
            if(OnSize != null){
                foreach(Delegate D in OnSize.GetInvocationList()){
                    Func<TransformAlgorithm, Vector2UI, Vector2UI> F = (Func<TransformAlgorithm, Vector2UI, Vector2UI>)D;
                    Vector2UI Size__ = F(this, Size);
                    __W = Size__.W;
                    __H = Size__.H;
                }
            }   
        }catch(Exception e){
            Logger.Error(Exception, e);
        }
    }
    
    private void __InvokeOnRect(string Exception){
        try{
            if(OnRect != null){
                foreach(Delegate D in OnRect.GetInvocationList()){
                    Func<TransformAlgorithm, Rect2I, Rect2I> F = (Func<TransformAlgorithm, Rect2I, Rect2I>)D;
                    Rect2I Rect__ = F(this, Rect);
                    __X = Rect__.X;
                    __Y = Rect__.Y;
                    __W = Rect__.W;
                    __H = Rect__.H;
                }
            }   
        }catch(Exception e){
            Logger.Error(Exception, e);
        }
    }
    
    // ----------------------------------------------------------------------

    public override string ToString() => "TransformAlg.(" + ToShortString() + ")";
    
    public string ToShortString() => Rect.ToShortString() + ", " + CallAnyway;
    
    public string ToVeryShortString() => Rect.ToShortString();
}

public interface ITransform{
    public void __UpdateTransform(object? Data = null){}
}

public class WorldTransformAlgorithm<T> where T : SceneObject<T>, ITransform{
    public WorldTransformAlgorithm(T Self){
        this.Self = Self;

        bool Sync = false;
        
        Local.OnRect += (Transform, Rect) => {
            if(Sync){ return Rect; }
            
            Rect2I Result = LocalToWorld(Rect);
            
            Sync = true; World.Rect = Result; Sync = false;

            __UpdateChildren(Self.Node, true);
            
            return Result;
        };

        World.OnRect += (Transform, Rect) => {
            if(Sync){ return Rect; }

            Rect2I Result = WorldToLocal(Rect);
            
            Sync = true; Local.Rect = Result; Sync = false;

            __UpdateChildren(Self.Node, false);
            
            return Result;
        };
    }

    public readonly T Self;

    /// <summary>
    /// Локальная трансформация, явялется основной
    /// </summary>
    public readonly TransformAlgorithm Local = new TransformAlgorithm();
    
    /// <summary>
    /// Мировая трансформация, только счёт
    /// </summary>
    public readonly TransformAlgorithm World = new TransformAlgorithm();

    /// <summary>
    /// Если true, то ивенты вызываются в любом случае, даже если значения совпадают
    /// </summary>
    public bool CallAnyway{
        get => Local.CallAnyway;
        set{
            Local.CallAnyway = value;
            World.CallAnyway = value;
        }
    }
    
    // ----------------------------------------------------------------------

    public Func<SceneAlgorithm<T>, WorldTransformAlgorithm<T>, Rect2I, Rect2I>? OnSceneTransform;
    
    public Func<SceneAlgorithm<T>, WorldTransformAlgorithm<T>, Rect2I, Rect2I>? OnSceneTransformReverse;

    public Func<SceneNode<T>, WorldTransformAlgorithm<T>, Rect2I, Rect2I>? OnParentTransform;
    
    public Func<SceneNode<T>, WorldTransformAlgorithm<T>, Rect2I, Rect2I>? OnParentTransformReverse;
    
    // ----------------------------------------------------------------------

    private Rect2I LocalToWorld(Rect2I Rect){
        Rect = Self.Node.Parents().Aggregate(Rect, (Current, Parent) => OnParentTransform?.Invoke(Parent, this, Current) ?? Current);
        
        if(!Self.Node.InMemory){
            Rect = OnSceneTransform?.Invoke(Self.Node.Scene!, this, Rect) ?? Rect;
        }

        return Rect;
    }

    private Rect2I WorldToLocal(Rect2I Rect){
        if(!Self.Node.InMemory){
            Rect = OnSceneTransformReverse?.Invoke(Self.Node.Scene!, this, Rect) ?? Rect;
        }
        
        Rect = Self.Node.Parents().Reverse().Aggregate(Rect, (Current, Parent) => OnParentTransformReverse?.Invoke(Parent, this, Current) ?? Current);

        return Rect;
    }
    
    /// <summary>
    /// Обновляет Local/World
    /// </summary>
    /// <param name="LocalToWorld">Обновить World?</param>
    public void Recalculate(bool LocalToWorld){
        if(LocalToWorld){
            World.Rect = this.LocalToWorld(Local.Rect);
        }else{
            Local.Rect = this.WorldToLocal(World.Rect);
        }
        
        __UpdateChildren(Self.Node, LocalToWorld);
    }

    private void __UpdateChildren(SceneNode<T> Node, bool LocalToWorld){
        foreach(SceneNode<T> Child in Node.Level0){
            Child.Self.__UpdateTransform(LocalToWorld);
            __UpdateChildren(Child, LocalToWorld);
        }
    }
    
    // ----------------------------------------------------------------------

    public override string ToString() => "WorldTransformAlg.(" + ToShortString() + ")";
    
    public string ToShortString() => Local.Rect.ToShortString() + ", " + Local.CallAnyway;
    
    public string ToVeryShortString() => Local.Rect.ToShortString();
}