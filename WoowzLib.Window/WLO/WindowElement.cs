using System.Runtime.InteropServices;
using WLO;

namespace WL.WLO;

public enum ElementLocation{
    /// <summary>
    /// Двигать вместе с окном
    /// </summary>
    InWindow,
    /// <summary>
    /// Двигать вместе с родителем
    /// </summary>
    InParent,
    /// <summary>
    /// Не двигать
    /// </summary>
    InWorld
}

public enum ElementAnchorX{
    None,
    Left,
    Center,
    Right
}

public enum ElementAnchorY{
    None,
    Top,
    Center,
    Bottom
}

public enum ElementAnchorSize{
    None,
    Horizon,
    Vertical,
    Both
}

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
    
    public WindowElement Add(WindowElement Element){
        try{
            if(Element.Parent != null){ throw new Exception("Этот элемент уже привязан к какому-то окну! Ссылка на окно: " + Element.Parent); }
                
            Element.__SetParent(this);

            return this;
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
                ClipResult = System.HDC.Clip(HDC, X_Final, Y_Final, Width_Final, Height_Final);
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
    /// Относительно чего обрабатывается позиция?
    /// </summary>
    public ElementLocation Location = ElementLocation.InParent;
    
    /// <summary>
    /// Позиция по X элемента
    /// </summary>
    public int X;

    /// <summary>
    /// Позиция по X элемента с учётом локации
    /// </summary>
    public int X_Location => Location switch{
        ElementLocation.InWindow => X,
        ElementLocation.InParent => X + (Parent?.X_Final ?? 0),
        ElementLocation.InWorld  => X - (Window?.X ?? 0),
        var _ => X
    };
    
    public int X_Anchor {
        get{
            if(Parent == null){ return X; }

            int ParentW = (int)Parent.Width_Final;
            int Result = X;

            switch(Anchor_X){
                case ElementAnchorX.Left  : Result = 0                                 ; break;
                case ElementAnchorX.Center: Result = (ParentW - (int)Width_Final)/2 + X; break;
                case ElementAnchorX.Right : Result =  ParentW - (int)Width_Final    + X; break;
            }
            
            return Result;
        }
    }

    public int X_Final => X_Location + X_Anchor;
    
    /// <summary>
    /// Позиция по Y элемента
    /// </summary>
    public int Y;
    
    /// <summary>
    /// Позиция по Y элемента с учётом локации
    /// </summary>
    public int Y_Location => Location switch{
        ElementLocation.InWindow => Y,
        ElementLocation.InParent => Y + (Parent?.Y_Final ?? 0),
        ElementLocation.InWorld  => Y - (Window?.Y ?? 0),
        var _ => Y
    };
    
    public int Y_Anchor {
        get{
            if(Parent == null){ return Y; }

            int ParentH = (int)Parent.Height_Final;
            int Result = Y;

            switch(Anchor_Y){
                case ElementAnchorY.Top   : Result = 0                              ; break;
                case ElementAnchorY.Center: Result = (ParentH - (int)Height_Final)/2; break;
                case ElementAnchorY.Bottom: Result =  ParentH - (int)Height_Final   ; break;
            }
            
            return Result;
        }
    }

    public int Y_Final => Y_Location + Y_Anchor;

    public ElementAnchorX Anchor_X = ElementAnchorX.None;
    public ElementAnchorY Anchor_Y = ElementAnchorY.None;
    public ElementAnchorSize Anchor_Size = ElementAnchorSize.None;
    
    /// <summary>
    /// Ширина элемента
    /// </summary>
    public uint Width;

    /// <summary>
    /// Ширина элемент с учётом растягивания
    /// </summary>
    public uint Width_Final{
        get{
            return Width;
        }
    }

    /// <summary>
    /// Высота элемента
    /// </summary>
    public uint Height;

    /// <summary>
    /// Высота элемент с учётом растягивания
    /// </summary>
    public uint Height_Final{
        get{
            return Height;
        }
    }
    
    /// <summary>
    /// Расположение по Z элемента (слои)
    /// </summary>
    public double Z;
}