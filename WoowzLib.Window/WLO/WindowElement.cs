using System.Runtime.InteropServices;
using WLO;

namespace WL.WLO;

public abstract class WindowElement : WindowContext{
    public WindowContext? Parent{ get; private set; }
    public readonly List<WindowElement> Children = [];
    
    public void __SetParent(Window Parent){
        try{
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
            this.Parent = Parent;
            if(Parent.Created && !Parent.Alive){ throw new Exception("Родительский элемент уничтоженный!"); }

            Parent.Children.Add(this);

            if(Parent.Created){ __CreateElement(this); }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при установке элементу [" + this + "] родителя [" + Parent + "]!", e);
        }
    }

    private void __CreateElement(WindowElement Element){
        try{
            Element.__Create();
            Element.Created = true;
            if(Handle == IntPtr.Zero){ throw new Exception("Элемент не создался!"); }

            __Events__ = System.Native.ConnectEventsToWindow(Element.Handle, __Events);
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
}