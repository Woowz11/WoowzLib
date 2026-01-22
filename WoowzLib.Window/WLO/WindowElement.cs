using System.Runtime.InteropServices;
using WLO;

namespace WL.WLO;

public abstract class WindowElement{
    public void __SetParent(Window Window){
        try{
            this.Window = Window;
            
            if(!Window.Alive){ throw new Exception("Окно не живое!"); }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при установке элементу [" + this + "] родителя (окно) [" + Window + "]!", e);
        }
    }
    
    public void __SetParent(WindowElement Parent){
        try{
                 Window = Parent.Window;
            this.Parent = Parent;
            
            Parent.Children.Add(this);
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
    /// Дети элемента
    /// </summary>
    public readonly List<WindowElement> Children = [];
    
    [Obsolete("пока-что не работает", true)]
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
            }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при уничтожении [" + this + "]!", e);
        }
    }
    
    public void Add(WindowElement Element){
        try{
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

    #region Рендер

    public void BaseRender(IntPtr HDC){
        if(Visible && Active){
            Render(HDC);
        }
    }
    
    public virtual void Render(IntPtr HDC){
        if(!VisibleChild){ return; }

        int ClipResult = 0;

        if(ClipChild){
            ClipResult = System.HDC.Clip(HDC, X, Y, Width, Height);
        }
        
        foreach(WindowElement Child in Children){
            Child.Render(HDC);
        }

        if(ClipChild){
            System.HDC.Unclip(HDC, ClipResult);
        }
    }

    #endregion

    /// <summary>
    /// Делает элемент невидимым (но активным!)
    /// </summary>
    public bool Visible = true;

    /// <summary>
    /// Делает элемент не активным и невидимым!
    /// </summary>
    public bool Active = true;

    /// <summary>
    /// Обрезать детей внутри элемента?
    /// </summary>
    public bool ClipChild = true;

    /// <summary>
    /// Делает детей внутри элемента невидимыми (но активными!)
    /// </summary>
    public bool VisibleChild = true;

    /// <summary>
    /// Ширина элемента
    /// </summary>
    public uint Width;

    /// <summary>
    /// Высота элемента
    /// </summary>
    public uint Height;

    /// <summary>
    /// Позиция по X элемента
    /// </summary>
    public int X;
    
    /// <summary>
    /// Позиция по Y элемента
    /// </summary>
    public int Y;
    
    /// <summary>
    /// Расположение по Z элемента (слои)
    /// </summary>
    public double Z;
}