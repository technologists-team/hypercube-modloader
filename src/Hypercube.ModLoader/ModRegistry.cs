using System.Reflection;

namespace Hypercube.ModLoader;

public readonly struct ModRegistry<T> where T : IMod
{
    public readonly T Instance;
    public readonly Assembly Assembly;

    public ModRegistry(T instance, Assembly assembly)
    {
        Instance = instance;
        Assembly = assembly;
    }
}
