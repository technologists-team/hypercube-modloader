namespace Hypercube.ModLoader;

public interface IMod
{
    ModId Id { get; }
    Version Version { get; }
    Version RequiredLoaderVersion { get; }
    ModDependency[] Dependencies { get; }
    ModMetaData MetaData { get; }
    void OnLoad() {}
    void OnUnload() {}
}