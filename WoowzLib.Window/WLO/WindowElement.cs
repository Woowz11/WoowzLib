namespace WL.WLO;

public abstract class WindowElement : IWindow{
    public IntPtr Parent{ get; private set; } = IntPtr.Zero;
    public readonly List<WindowElement> Children = [];
    
    public void __SetParent(Window Parent){
        try{
            this.Parent = Parent.Handle;
            if(this.Parent == IntPtr.Zero){ throw new Exception("Указан пустой родитель!"); }
            __Create();
            if(Handle == IntPtr.Zero){ throw new Exception("Элемент не создался!"); }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при установке элементу [" + this + "] родителя (окно) [" + Parent + "]!", e);
        }
    }
    
    public void __SetParent(WindowElement Parent){
        try{
            this.Parent = Parent.Handle;
            if(this.Parent == IntPtr.Zero){ throw new Exception("Указан пустой родитель!"); }
            Parent.Children.Add(this);
            __Create();
            if(Handle == IntPtr.Zero){ throw new Exception("Элемент не создался!"); }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при установке элементу [" + this + "] родителя [" + Parent + "]!", e);
        }
    }

    public abstract void __Create();

    public abstract void __Destroy();
    
    public void Destroy(){
        foreach(WindowElement Child in Children){
            Child.Destroy();
        }
        Children.Clear();

        if(Alive){
            __Destroy();
            //System.Native.Windows.DestroyWindow(Handle);
            Handle = IntPtr.Zero;
        }
    }
}