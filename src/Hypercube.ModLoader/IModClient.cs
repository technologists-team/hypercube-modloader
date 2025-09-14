using System.Collections.Immutable;

namespace Hypercube.ModLoader;

public interface IModClient
{
    IModLoaderClient ModLoader { get; }
    ModId Id { get; }
    Version Version { get; }
    Version RequiredLoaderVersion { get; }
    ImmutableArray<ModDependency> Dependencies { get; }
    ModMetaData MetaData { get; }
}