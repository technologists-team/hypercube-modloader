namespace Hypercube.ModLoader;

/// <summary>
/// Represents an extension that can be loaded and unloaded by the loader.
/// </summary>
public interface IMod : IModClient
{
    /// <summary>
    /// Called when the extension is loaded.
    /// </summary>
    void OnLoad();
    
    /// <summary>
    /// Called when the extension is unloaded.
    /// </summary>
    void OnUnload();
}
