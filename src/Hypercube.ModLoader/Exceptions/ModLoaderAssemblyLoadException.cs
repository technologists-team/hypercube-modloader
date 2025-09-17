namespace Hypercube.ModLoader.Exceptions;

public sealed class ModLoaderAssemblyLoadException : ModLoaderException
{
    public ModLoaderAssemblyLoadException(IModLoaderClient modLoader, Exception exception) : base($"Assembly loading error. Loader version: {modLoader.Version}. Exception: {exception}")
    {
    }
}
