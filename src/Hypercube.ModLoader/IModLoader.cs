namespace Hypercube.ModLoader;

/// <summary>
/// Represents a loader that can manage extensions, their registries, and lifecycle.
/// </summary>
/// <typeparam name="TMod">The type of extension being managed.</typeparam>
/// <typeparam name="TClient">The type of client representation of an extension.</typeparam>
public interface IModLoader<TMod, TClient> : IModLoaderClient<TClient>
    where TMod : IMod, IModClient
    where TClient : IModClient
{
    /// <summary>
    /// Gets the registry associated with a given extension identifier.
    /// </summary>
    /// <param name="id">The extension identifier.</param>
    /// <returns>The registry for the specified identifier.</returns>
    ModRegistry<TMod> GetRegistry(ModId id);
    
    /// <summary>
    /// Attempts to get the registry associated with a given extension identifier.
    /// </summary>
    /// <param name="id">The extension identifier.</param>
    /// <param name="registry">The registry if found.</param>
    /// <returns><c>true</c> if the registry was found; otherwise <c>false</c>.</returns>
    bool TryGetRegistry(ModId id, out ModRegistry<TMod> registry);
    
    /// <summary>
    /// Loads an extension from its compiled assembly.
    /// </summary>
    /// <param name="assembly">The compiled assembly as a byte array.</param>
    /// <returns>The loaded extension registry.</returns>
    ModRegistry<TMod> Load(byte[] assembly);
    
    /// <summary>
    /// Unloads a specific extension instance.
    /// </summary>
    /// <param name="mod">The extension to unload.</param>
    void Unload(TMod mod);
    
    
    /// <summary>
    /// Unloads an extension by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the extension to unload.</param>
    void Unload(ModId id);
}
