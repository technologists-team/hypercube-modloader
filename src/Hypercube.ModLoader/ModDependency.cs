namespace Hypercube.ModLoader;

public readonly struct ModDependency
{
    public readonly string Id;
    public readonly Version Version;
    public readonly bool Optional;

    public ModDependency(string id, Version version, bool optional)
    {
        Id = id;
        Version = version;
        Optional = optional;
    }
}
