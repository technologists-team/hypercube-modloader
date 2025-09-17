using System.Collections.Immutable;
using JetBrains.Annotations;

namespace Hypercube.ModLoader;

/// <inheritdoc/>
public abstract class Mod : IMod
{
    /// <inheritdoc/>
    [UsedImplicitly(ImplicitUseKindFlags.Assign)]
    public IModLoaderClient ModLoader { get; private set; } = default!;

    /// <inheritdoc/>
    public abstract ModId Id { get; }
    
    /// <inheritdoc/>
    public abstract Version Version { get; }
    
    /// <inheritdoc/>
    public abstract Version RequiredLoaderVersion { get; }
    
    /// <inheritdoc/>
    public abstract ImmutableArray<ModDependency> Dependencies { get; }
    
    /// <inheritdoc/>
    public abstract ModMetaData MetaData { get; }
    
    /// <inheritdoc/>
    public abstract void OnLoad();
    
    /// <inheritdoc/>
    public abstract void OnUnload();
}
