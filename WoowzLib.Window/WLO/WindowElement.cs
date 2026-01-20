using System.Runtime.InteropServices;
using WLO;

namespace WL.WLO;

public abstract class WindowElement : WindowContext{
    public void __SetParent(Window Parent){
        try{
                 Window = Parent;
            this.Parent = Parent;
            
            if(!this.Parent.Alive){ throw new Exception("Окно не живое!"); }
            if(Created){ throw new Exception("Элемент уже созданный!"); }
            
            __CreateElement(this);
            __CreateAndChild();
        }catch(Exception e){
            throw new Exception("Произошла ошибка при установке элементу [" + this + "] родителя (окно) [" + Parent + "]!", e);
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
    public WindowContext? Parent{ get; private set; }
    
    /// <summary>
    /// Дети элемента
    /// </summary>
    public readonly List<WindowElement> Children = [];

    private void __CreateElement(WindowElement Element){
        try{
            Element.__Create();
            Element.Created = true;
            if(Handle == IntPtr.Zero){ throw new Exception("Элемент не создался!"); }

            __Events__ = System.Native.ConnectEventsToWindow(Element.Handle, __Events);

            System.Native.Windows.SendMessage(Handle, System.Native.Windows.WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при создании элемента [" + Element + "] у [" + this + "]!", e);
        }
    }
    // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
    private System.Native.Windows.WndProcDelegate __Events__;
    
    private void __CreateAndChild(){
        try{
            foreach(WindowElement Child in Children){
                __CreateElement(Child);
                Child.__CreateAndChild();
                Child.__UpdateZOrder();
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

    public abstract IntPtr __Destroy();
    
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
                Handle = __Destroy();
                if(Handle != IntPtr.Zero){ throw new Exception("Неверно удалился элемент!"); }
            }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при уничтожении [" + this + "]!", e);
        }
    }
    
    public override void Add(WindowElement Element){
        try{
            if(Created){ CheckDestroyed(); }
                
            if(Element.Parent != null){ throw new Exception("Этот элемент уже привязан к какому-то окну! Ссылка на окно: " + Element.Parent); }
                
            Element.__SetParent(this);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при добавлении элемента [" + Element + "] окну [" + this + "]!", e);
        }
    }

    /// <summary>
    /// Обновляет позиции у элементов по Z
    /// </summary>
    public void __UpdateZOrder(){ __UpdateZOrder(Children); }

    public void __UpdateRender(IntPtr HDC){ __UpdateRender(HDC, Children); }

    public virtual bool __NeedUpdateRender(){ return true; }

    #region Ивенты

        /// <summary>
        /// Вызывается при уничтожении
        /// </summary>
        public event Action? OnDestroy;

    #endregion
    
    public new uint Width{
        get => __Width;
        set{
            try{
                if(Created){ CheckDestroyed(); }

                if(__Width == value){ return; }
                __Width = value;

                if(Created){ __UpdateSize(); }
            }catch(Exception e){
                throw new Exception("Произошла ошибка при изменении ширины у пародии окна [" + this + "]!\nШирина: " + value, e);
            }
        }
    }
    
    public new uint Height{
        get => __Height;
        set{
            try{
                if(Created){ CheckDestroyed(); }
                
                if(__Height == value){ return; }
                __Height = value;

                if(Created){ __UpdateSize(); }
            }catch(Exception e){
                throw new Exception("Произошла ошибка при изменении высоты у пародии окна [" + this + "]!\nВысота: " + value, e);
            }
        }
    }

    public new int X{
        get => __X;
        set{
            try{
                if(Created){ CheckDestroyed(); }

                if(__X == value){ return; }
                __X = value;

                if(Created){ __UpdatePosition(); }
            }catch(Exception e){
                throw new Exception("Произошла ошибка при изменении позиции по X у пародии окна [" + this + "]!\nX: " + value, e);
            }
        }
    }
    
    public new int Y{
        get => __Y;
        set{
            try{
                if(Created){ CheckDestroyed(); }

                if(__Y == value){ return; }
                __Y = value;

                if(Created){ __UpdatePosition(); }
            }catch(Exception e){
                throw new Exception("Произошла ошибка при изменении позиции по Y у пародии окна [" + this + "]!\nY: " + value, e);
            }
        }
    }

    public double Z{
        get => __Z;
        set{
            try{
                if(Created){ CheckDestroyed(); }

                if(__Z == value){ return; }
                __Z = value;

                if(Created){ __UpdateZOrder(); }
            }catch(Exception e){
                throw new Exception("Произошла ошибка при изменении позиции по Z у пародии окна [" + this + "]!\nZ: " + value, e);
            }
        }
    }
    private double __Z;
}