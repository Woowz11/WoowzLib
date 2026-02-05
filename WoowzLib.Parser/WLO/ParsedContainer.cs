using WL;

namespace WLO;

public abstract class ParsedContainer{
    public abstract FileFormat Format{ get; }

    public override string ToString() => Format + "()";
}