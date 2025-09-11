namespace Hypercube.ModLoader.Exceptions;

public sealed class ModLoaderVersionMismatchException : ModLoaderException
{
    public ModLoaderVersionMismatchException(ModLoader modLoader, IMod mod) : base($"Incompatible loader version. {mod.Id} requires loader version {mod.RequiredLoaderVersion}, current loader version is {modLoader.Version}.")
    {
    }
}
