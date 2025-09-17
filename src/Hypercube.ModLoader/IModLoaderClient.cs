using System.Diagnostics.CodeAnalysis;
using Hypercube.Utilities.Dependencies;

namespace Hypercube.ModLoader;

/// <summary>
/// Represents the base functionality of a loader client.
/// </summary>
public interface IModLoaderClient
{
    /// <summary>
    /// Gets the container that manages all dependencies available to the loader.
    /// </summary>
    IDependenciesContainer DependenciesContainer { get; }
    
    /// <summary>
    /// Gets the version of the loader.
    /// </summary>
    Version Version { get; }
}

/// <summary>
/// Represents a generic client-side loader interface with extension management functions.
/// </summary>
/// <typeparam name="TClient">The type of client-side extensions managed by the loader.</typeparam>
public interface IModLoaderClient<TClient> : IModLoaderClient where TClient : IModClient
{
    /// <summary>
    /// Determines whether an extension with the specified identifier is loaded.
    /// </summary>
    /// <param name="id">The extension identifier.</param>
    /// <returns><c>true</c> if the extension is loaded; otherwise <c>false</c>.</returns>
    bool HasMod(ModId id);
    
    /// <summary>
    /// Gets a loaded extension by its identifier.
    /// </summary>
    /// <param name="id">The extension identifier.</param>
    /// <returns>The loaded extension.</returns>
    TClient GetMod(ModId id);
    
    /// <summary>
    /// Attempts to retrieve an extension by its identifier.
    /// </summary>
    /// <param name="id">The extension identifier.</param>
    /// <param name="mod">The loaded extension if found.</param>
    /// <returns><c>true</c> if the extension was found; otherwise <c>false</c>.</returns>
    bool TryGetMod(ModId id, [NotNullWhen(true)] out TClient? mod);
}
