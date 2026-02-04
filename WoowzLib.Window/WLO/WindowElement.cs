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

public abstract class WindowElement{
    /// <summary>
    /// Окно к которому привязан элемент
    /// </summary>
    public Window? Window{ get; private set; }

    /// <summary>
    /// Привязать элемент к этому окну
    /// </summary>
    public WindowElement ToWindow(Window Window){
        try{
            if(!Window.Alive){ throw new Exception("Окно не живое!"); }
            if(this.Window == Window){ return this; }

            if(this.Window != null){ this.Window.__Children.Remove(this); }
            
            if(Parent != null && Parent.Window != Window){
                Parent = null;
            }
            
            Window.__Children.Add(this);
            
            this.Window = Window;
            
            return this;
        }catch(Exception e){
            throw new Exception("Произошла ошибка при привязке окна [" + Window + "] элементу [" + this + "]!", e);
        }
    }
    
    /// <summary>
    /// Выносит элемент обратно в память, считайте что удаляет
    /// </summary>
    public WindowElement ToMemory(){
        try{
            if(InMemory){ return this; }
            foreach(WindowElement Child in __Children.ToArray()){
                Child.ToMemory();
            }
            
            Parent = null;
            Window!.__Children.Remove(this);
            Window = null;
            return this;
        }catch(Exception e){
            throw new Exception("Не получилось вынести элемент [" + this + "] в память!", e);
        }
    }
    
    /// <summary>
    /// В памяти?
    /// </summary>
    public bool InMemory => Window == null;
    
    /// <summary>
    /// Родитель элемента
    /// </summary>
    public WindowElement? Parent{
        get => __Parent;
        set{
            try{
                if(__Parent == value){ return; }

                if(value == null){
                    if(__Parent != null){
                        __Parent.__Children.Remove(this);
                    }
                }else{
                    Window = value.Window;
                    if(__Parent == null){
                        value.__Children.Add(this);
                    }
                }
                
                __Parent = value;
            }catch(Exception e){
                throw new Exception("Произошла ошибка при установке родителя элементу [" + this + "]!\nРодитель: " + value, e);
            }
        }
    }
    private WindowElement? __Parent;

    /// <summary>
    /// Дети элемента
    /// </summary>
    private readonly List<WindowElement> __Children = [];
    
    /// <summary>
    /// Добавить ребёнка элементу
    /// </summary>
    public WindowElement Add(WindowElement Element) => Element.Parent = this; 
    
    #region Ивенты

        /// <summary>
        /// Вызывается при уничтожении
        /// </summary>
        //public event Action? OnDestroy;

    #endregion

    #region Рендер

        public void BaseRender(IntPtr HDC){
            try{
                if(Visible && Active){
                    Render(HDC);
                }
            }catch(Exception e){
                throw new Exception("Произошла ошибка при базовом рендере элемента [" + this + "]!\nHDC: " + HDC, e);
            }
        }
        
        public virtual void Render(IntPtr HDC){
            if(!VisibleChild){ return; }

            int ClipResult = 0;

            if(ClipChild){
                ClipResult = System.HDC.Clip(HDC, X_Final, Y_Final, Width_Final, Height_Final);
            }
            
            foreach(WindowElement Child in __Children){
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
        ElementLocation.InWindow => 0,
        ElementLocation.InParent =>  (Parent?.X_Final ?? 0),
        ElementLocation.InWorld  => -(Window?.X ?? 0)
    };
    
    /// <summary>
    /// Позиция по X элемента с учётом точки привязки
    /// </summary>
    public int X_Anchor => (int)(((Anchor_X + 1) / 2f) * ((int)(Parent?.Width_Final ?? Window.Width) - (int)Width_Final));

    /// <summary>
    /// Позиция по X элемента с учётом всего
    /// </summary>
    public int X_Final => X + X_Location + X_Anchor;
    
    /// <summary>
    /// Позиция по Y элемента
    /// </summary>
    public int Y;
    
    /// <summary>
    /// Позиция по Y элемента с учётом локации
    /// </summary>
    public int Y_Location => Location switch{
        ElementLocation.InWindow => 0,
        ElementLocation.InParent =>  (Parent?.Y_Final ?? 0),
        ElementLocation.InWorld  => -(Window?.Y ?? 0)
    };

    /// <summary>
    /// Позиция по Y элемента с учётом точки привязки
    /// </summary>
    public int Y_Anchor => (int)(((Anchor_Y + 1) / 2f) * ((int)(Parent?.Height_Final ?? Window.Height) - (int)Height_Final));

    /// <summary>
    /// Позиция по Y элемента с учётом всего
    /// </summary>
    public int Y_Final => Y + Y_Location + Y_Anchor;

    /// <summary>
    /// Относительно какой горизонтали располагать элемент? (-1: Лево, 0: Центр, 1: Право)
    /// </summary>
    public float Anchor_X = -1;
    /// <summary>
    /// Относительно какой вертикали располагать элемент? (-1: Вверх, 0: Центр, 1: Низ)
    /// </summary>
    public float Anchor_Y = -1;

    public float Anchor_Width{
        get => __Anchor_Width;
        set => __Anchor_Width = WL.Math.Max(value, 0);
    }
    private float __Anchor_Width = 0;
    
    public float Anchor_Height{
        get => __Anchor_Height;
        set => __Anchor_Height = WL.Math.Max(value, 0);
    }
    private float __Anchor_Height = 0;
    
    /// <summary>
    /// Ширина элемента
    /// </summary>
    public uint Width;

    /// <summary>
    /// Ширина элемента с учётом растягивания
    /// </summary>
    public uint Width_Final => Anchor_Width > 0 ? (uint)((Parent?.Width_Final ?? Window.Width) * Anchor_Width) : Width;

    /// <summary>
    /// Высота элемента
    /// </summary>
    public uint Height;

    /// <summary>
    /// Высота элемента с учётом растягивания
    /// </summary>
    public uint Height_Final => Anchor_Height > 0 ? (uint)((Parent?.Height_Final ?? Window.Height) * Anchor_Height) : Height;
}