namespace Hypercube.ModLoader;

public readonly struct ModId : IEquatable<ModId>
{
    public static readonly ModId Invalid = new(string.Empty);

    private readonly string _value;

    public ModId(string value)
    {
        _value = value;
    }

    public bool Equals(ModId other)
    {
        return _value == other._value;
    }

    public override bool Equals(object? obj)
    {
        return obj is ModId id && Equals(id);
    }

    public override string ToString()
    {
        return _value;
    }

    public override int GetHashCode()
    {
        return _value.GetHashCode();
    }

    public static bool operator ==(ModId a, ModId b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(ModId a, ModId b)
    {
        return !a.Equals(b);
    }

    public static implicit operator string(ModId id)
    {
        return id._value;
    }
}