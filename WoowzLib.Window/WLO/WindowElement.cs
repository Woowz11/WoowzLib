namespace WLO;

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
    public Window? Window{
        get => __Window;
        set{
            try{
                if(__Window == value){ return; }
                
                if(value == null){
                    __Window?.__Children.Remove(this);
                    __Window = null;
                    return;
                }
                
                if(!value.Alive){ throw new Exception("Окно не живое!"); }

                __Window?.__Children.Remove(this);
            
                if(Parent != null && Parent.Window != value){
                    Parent = null;
                }
            
                value.__Children.Add(this);
                
                __Window = value;
            }catch(Exception e){
                throw new Exception("Произошла ошибка при привязке окна [" + value + "] элементу [" + this + "]!", e);
            }
        }
    }
    private Window? __Window;
    
    /// <summary>
    /// Выносит элемент обратно в память, считайте что удаляет
    /// </summary>
    public WindowElement ToMemory(){
        try{
            if(InMemory){ return this; }
            
            Parent = null;
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
    /// В памяти? С учётом родителей
    /// </summary>
    public bool InMemory_Final => Parent?.InMemory_Final ?? InMemory;
    
    /// <summary>
    /// Родитель элемента
    /// </summary>
    public WindowElement? Parent{
        get => __Parent;
        set{
            try{
                if(__Parent == value){ return; }

                if(value == null){
                    __Parent?.__Children.Remove(this);
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
        /// Вызывается когда курсор входит или выходиз из элемента [Элемент, Входит?]
        /// </summary>
        public event Action<WindowElement, bool>? OnCursorInside;
        public void __OnCursorInsideInvoke(bool B){ OnCursorInside?.Invoke(this, B); }

        #endregion

    #region Рендер

        public void BaseRender(IntPtr HDC){
            try{
                if(!InMemory_Final && Visible && Active){
                    Render(HDC);
                }
            }catch(Exception e){
                throw new Exception("Произошла ошибка при базовом рендере элемента [" + this + "]!\nHDC: " + HDC, e);
            }
        }
        
        public virtual void Render(IntPtr HDC){
            try{
                if(InMemory_Final || !VisibleChild){ return; }
                
                int ClipResult = 0;

                if(ClipChild){
                    ClipResult = WL.System.HDC.Clip(HDC, X_Final, Y_Final, Width_Final, Height_Final);
                }

                foreach(WindowElement Child in __Children){
                    Child.Render(HDC);
                }

                if(ClipChild){
                    WL.System.HDC.Unclip(HDC, ClipResult);
                }
            }catch(Exception e){
                throw new Exception("Произошла ошибка при рендере детей [" + this + "]!\nHDC: " + HDC, e);
            }
        }

    #endregion

    /// <summary>
    /// Название элемента
    /// </summary>
    public string Name = "Новый элемент";
    
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

    #region Трансформация

        #region Позиция

            /// <summary>
            /// Относительно чего обрабатывается позиция?
            /// </summary>
            public ElementLocation Location = ElementLocation.InParent;
        
            /// <summary>
            /// Относительно какой горизонтали располагать элемент? (-1: Лево, 0: Центр, 1: Право)
            /// </summary>
            public float Anchor_X = -1;
            /// <summary>
            /// Относительно какой вертикали располагать элемент? (-1: Вверх, 0: Центр, 1: Низ)
            /// </summary>
            public float Anchor_Y = -1;
        
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
            public int X_Anchor => (int)(((Anchor_X + 1) / 2f) * ((int)(Parent != null && Location == ElementLocation.InParent ? Parent.Width_Final : Window?.Width ?? 0) - (int)Width_Final));

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
            public int Y_Anchor => (int)(((Anchor_Y + 1) / 2f) * ((int)(Parent != null && Location == ElementLocation.InParent ? Parent.Height_Final : Window?.Height ?? 0) - (int)Height_Final));

            /// <summary>
            /// Позиция по Y элемента с учётом всего
            /// </summary>
            public int Y_Final => Y + Y_Location + Y_Anchor;

            /// <summary>
            /// Позиция элемента
            /// </summary>
            public Vector2I Position{
                get => new Vector2I(X, Y);
                set{
                    X = value.X;
                    Y = value.Y;
                }
            }
            
            /// <summary>
            /// Позиция элемента с учётом всего
            /// </summary>
            public Vector2I Position_Final => new Vector2I(X_Final, Y_Final);
            
        #endregion
    
        #region Размер

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
            public uint Width_Final => Anchor_Width > 0 ? (uint)((Parent != null && Location == ElementLocation.InParent ? Parent.Width_Final : Window?.Width ?? 0) * Anchor_Width) : Width;

            /// <summary>
            /// Высота элемента
            /// </summary>
            public uint Height;

            /// <summary>
            /// Высота элемента с учётом растягивания
            /// </summary>
            public uint Height_Final => Anchor_Height > 0 ? (uint)((Parent != null && Location == ElementLocation.InParent ? Parent.Height_Final : Window?.Height ?? 0) * Anchor_Height) : Height;
        
            /// <summary>
            /// Размер элемента
            /// </summary>
            public Vector2U Size{
                get => new Vector2U(Width, Height);
                set{
                    Width  = value.X;
                    Height = value.Y;
                }
            }
        
            /// <summary>
            /// Размер элемента с учётом растягивания
            /// </summary>
            public Vector2U Size_Final => new Vector2U(Width_Final, Height_Final);

        #endregion

        /// <summary>
        /// Позиция и размер элемента
        /// </summary>
        public RectI Rect{
            get => new RectI(X, Y, (int)Width, (int)Height);
            set{
                X = value.X;
                Y = value.Y;
                Width  = (uint)value.Width;
                Height = (uint)value.Height;
            }
        }
        
        /// <summary>
        /// Позиция и размер элемента с учётом всего
        /// </summary>
        public RectI Rect_Final => new RectI(X_Final, Y_Final, (int)Width_Final, (int)Height_Final);
        
    #endregion

    /// <summary>
    /// Указанная позиция, находится внутри элемента? Без учёта родителей, детей, Clip
    /// </summary>
    public bool InsideRect(Vector2I ClientVector){
        try{
            return Rect_Final.Inside(ClientVector);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при определении, находится ли точка внутри элемента [" + this + "] (без учёта всего)!\nC. Вектор: " + ClientVector, e);
        }
    }
    
    /// <summary>
    /// Указанная позиция, находится внутри элемента?
    /// </summary>
    public bool Inside(Vector2I ClientVector){
        try{
            return Hit(ClientVector) != null;
        }catch(Exception e){
            throw new Exception("Произошла ошибка при определении, находится ли точка внутри элемента [" + this + "]!\nC. Вектор: " + ClientVector, e);
        }
    }

    /// <summary>
    /// Возвращает самый верхний элемент под позицией
    /// </summary>
    public WindowElement? Hit(Vector2I ClientVector){
        try{
            if(InMemory_Final || !Active){ return null; }

            if(Parent != null && Parent.ClipChild){
                if(!Parent.InsideRect(ClientVector)){ return null; }
            }
            
            for(int i = __Children.Count - 1; i >= 0; i--){
                WindowElement Child = __Children[i];

                WindowElement? R = Child.Hit(ClientVector);
                if(R != null){ return R; }
            }

            return InsideRect(ClientVector) ? this : null;
        }catch(Exception e){
            throw new Exception("Произошла ошибка при определении, самого верхнего элемента под позицией, у элемента [" + this + "]!\nКоордината: " + ClientVector, e);
        }
    }
}