using System.Collections.Immutable;
using JetBrains.Annotations;

namespace Hypercube.ModLoader;

public abstract class Mod : IMod
{
    [UsedImplicitly(ImplicitUseKindFlags.Assign)]
    public IModLoaderClient ModLoader { get; private set; } = default!;

    public abstract ModId Id { get; }
    public abstract Version Version { get; }
    public abstract Version RequiredLoaderVersion { get; }
    public abstract ImmutableArray<ModDependency> Dependencies { get; }
    public abstract ModMetaData MetaData { get; }
    public abstract void OnLoad();
    public abstract void OnUnload();
}
