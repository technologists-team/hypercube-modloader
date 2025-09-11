namespace Hypercube.ModLoader;

public abstract class Mod : IMod
{
    public abstract ModId Id { get; }
    public abstract Version Version { get; }
    public abstract Version RequiredLoaderVersion { get; }
    public abstract ModDependency[] Dependencies { get; }
    public abstract ModMetaData MetaData { get; }
}
