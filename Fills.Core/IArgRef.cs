namespace Fills;

public interface IArgRef<TArg>
{
    ref readonly TArg ArgRef { get; }

    TArg Arg { get; }
}
