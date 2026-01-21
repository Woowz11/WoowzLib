using System.Runtime.InteropServices;
using WLO;

namespace WL.WLO;

public abstract class WindowElement{
    public void __SetParent(Window Window){
        try{
            this.Window = Window;
            
            if(!Window.Alive){ throw new Exception("Окно не живое!"); }
            if(Created){ throw new Exception("Элемент уже созданный!"); }
            
            __CreateElement(this);
            __CreateAndChild();
        }catch(Exception e){
            throw new Exception("Произошла ошибка при установке элементу [" + this + "] родителя (окно) [" + Window + "]!", e);
        }
    }
    
    public void __SetParent(WindowElement Parent){
        try{
                 Window = Parent.Window;
            this.Parent = Parent;
            
            if(Parent is{ Created: true, Alive: false }){ throw new Exception("Родительский элемент уничтоженный!"); }
            
            Parent.Children.Add(this);
            
            if(Parent.Created){ __CreateElement(this); }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при установке элементу [" + this + "] родителя [" + Parent + "]!", e);
        }
    }
    
    /// <summary>
    /// Окно к которому привязан элемент
    /// </summary>
    public Window Window{ get; private set; }
    
    /// <summary>
    /// Родитель элемента
    /// </summary>
    public WindowElement? Parent{ get; private set; }
    
    /// <summary>
    /// Окно живое?
    /// </summary>
    public bool Alive => Window.Alive;
    
    /// <summary>
    /// Делает проверку, уничтожено окно или нет?
    /// </summary>
    public void CheckDestroyed(){ if(!Alive){ throw new Exception("Пародия окна [" + this + "] уничтожена!"); } }
    
    /// <summary>
    /// Дети элемента
    /// </summary>
    public readonly List<WindowElement> Children = [];

    private void __CreateElement(WindowElement Element){
        try{
            Element.__Create();
            Element.Created = true;
        }catch(Exception e){
            throw new Exception("Произошла ошибка при создании элемента [" + Element + "] у [" + this + "]!", e);
        }
    }
    
    private void __CreateAndChild(){
        try{
            foreach(WindowElement Child in Children){
                __CreateElement(Child);
                Child.__CreateAndChild();
            }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при создании детей - детей у [" + this + "]!", e);
        }
    }

    /// <summary>
    /// Элемент созданный?
    /// </summary>
    public bool Created{ get; private set; }

    public abstract void __Create();

    public abstract void __Destroy();
    
    public void Destroy(){
        try{
            foreach(WindowElement Child in Children){
                Child.Destroy();
            }
            Children.Clear();

            if(Alive){
                try{
                    OnDestroy?.Invoke();   
                }catch(Exception e){
                    Logger.Error("Произошла ошибка при вызове ивента уничтожения элемента [" + this + "]!", e);
                }
                __Destroy();
            }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при уничтожении [" + this + "]!", e);
        }
    }
    
    public void Add(WindowElement Element){
        try{
            if(Created){ CheckDestroyed(); }
                
            if(Element.Parent != null){ throw new Exception("Этот элемент уже привязан к какому-то окну! Ссылка на окно: " + Element.Parent); }
                
            Element.__SetParent(this);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при добавлении элемента [" + Element + "] окну [" + this + "]!", e);
        }
    }

    #region Ивенты

        /// <summary>
        /// Вызывается при уничтожении
        /// </summary>
        public event Action? OnDestroy;

    #endregion
    
    public uint Width{
        get => __Width;
        set{
            try{
                if(Created){ CheckDestroyed(); }

                if(__Width == value){ return; }
                __Width = value;
            }catch(Exception e){
                throw new Exception("Произошла ошибка при изменении ширины у пародии окна [" + this + "]!\nШирина: " + value, e);
            }
        }
    }
    protected uint __Width;
    
    public uint Height{
        get => __Height;
        set{
            try{
                if(Created){ CheckDestroyed(); }
                
                if(__Height == value){ return; }
                __Height = value;
            }catch(Exception e){
                throw new Exception("Произошла ошибка при изменении высоты у пародии окна [" + this + "]!\nВысота: " + value, e);
            }
        }
    }
    protected uint __Height;

    public int X{
        get => __X;
        set{
            try{
                if(Created){ CheckDestroyed(); }

                if(__X == value){ return; }
                __X = value;
            }catch(Exception e){
                throw new Exception("Произошла ошибка при изменении позиции по X у пародии окна [" + this + "]!\nX: " + value, e);
            }
        }
    }
    protected int __X;
    
    public int Y{
        get => __Y;
        set{
            try{
                if(Created){ CheckDestroyed(); }

                if(__Y == value){ return; }
                __Y = value;
            }catch(Exception e){
                throw new Exception("Произошла ошибка при изменении позиции по Y у пародии окна [" + this + "]!\nY: " + value, e);
            }
        }
    }
    protected int __Y;

    public double Z{
        get => __Z;
        set{
            try{
                if(Created){ CheckDestroyed(); }

                if(__Z == value){ return; }
                __Z = value;
            }catch(Exception e){
                throw new Exception("Произошла ошибка при изменении позиции по Z у пародии окна [" + this + "]!\nZ: " + value, e);
            }
        }
    }
    private double __Z;
}