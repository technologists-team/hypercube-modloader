using System.Reflection;

namespace Hypercube.ModLoader;

public readonly struct ModRegistry
{
    public readonly IMod Instance;
    public readonly Assembly Assembly;

    public ModRegistry(IMod instance, Assembly assembly)
    {
        Instance = instance;
        Assembly = assembly;
    }
}