using WLO.Attribute;
using WLO.Vector;

namespace WLO.Rect;

[RequireTesting(TestingInformation.Global | TestingInformation.WorkInProgress, "человеческий фактор все дела")]
public struct RectI : IEquatable<RectI>{
    public RectI(int X, int Y, uint W, uint H){ this.X = X; this.Y = Y; this.W = W; this.H = H; }
    
    public int  X;
    public int  Y;
    public uint W;
    public uint H;

    public Vector2I Position{
        get => new Vector2I(X, Y);
        set{
            X = value.X;
            Y = value.Y;
        }
    }

    public Vector2UI Size{
        get => new Vector2UI(W, H);
        set{
            W = value.X;
            H = value.Y;
        }
    }

    /* доделайте пожалуйста умоляю просто боже
    public int Right{
        get => X + (int)W;
        set => W = (uint)(value - X);
    }
    public int Left{
        get => X;
        set{
            int O = Right;
            X = value;
            W = (uint)(O - X);
        }
    }
    public int Top{
        get => Y + (int)H;
        set{
            int O = Bottom;
            Y = value - (int)H;
        }
    }
    public int Bottom{
        
    }
    */
    
    // ----------------------------------------------------------------------

    public override string ToString() => "RectI(" + X + ":" + Y + ", " + W + "x" + H + ")";

    public bool Equals(RectI Other) => X == Other.X && Y == Other.Y && W == Other.W && H == Other.H;
    public override bool Equals(object? Object) => Object is RectI Other && Equals(Other);

    public override int GetHashCode() => HashCode.Combine(X, Y, W, H);
}