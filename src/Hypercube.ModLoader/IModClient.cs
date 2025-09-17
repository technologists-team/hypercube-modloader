using System.Collections.Immutable;
using JetBrains.Annotations;

namespace Hypercube.ModLoader;

/// <summary>
/// Represents a client-side definition of an extension with metadata and loader information.
/// </summary>
public interface IModClient
{
    /// <summary>
    /// Gets the loader instance that manages this extension.
    /// </summary>
    [UsedImplicitly(ImplicitUseKindFlags.Assign)]
    IModLoaderClient ModLoader { get; }

    /// <summary>
    /// Gets the unique identifier of the extension.
    /// </summary>
    ModId Id { get; }

    /// <summary>
    /// Gets the version of the extension.
    /// </summary>
    Version Version { get; }

    /// <summary>
    /// Gets the required <see cref="IModLoaderClient.Version"/> to run this extension.
    /// </summary>
    Version RequiredLoaderVersion { get; }

    /// <summary>
    /// Gets the list of declared dependencies for this extension.
    /// </summary>
    ImmutableArray<ModDependency> Dependencies { get; }

    /// <summary>
    /// Gets the metadata associated with this extension.
    /// </summary>
    ModMetaData MetaData { get; }
}
