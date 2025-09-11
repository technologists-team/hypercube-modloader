using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Hypercube.ModLoader.Exceptions;
using Hypercube.Utilities.Helpers;
using JetBrains.Annotations;

namespace Hypercube.ModLoader;

[PublicAPI]
public class ModLoader
{
    public virtual Version Version => new(1, 0, 0);

    private readonly Dictionary<ModId, ModRegistry> _mods = new();

    public void Load(byte[] rawAssembly)
    {
        var registry = CreateRegistry(rawAssembly);
        
        if (!_mods.TryAdd(registry.Instance.Id, registry))
            throw new Exception();
    }

    public void Unload(IMod mod)
    {
        
    }

    public void Unload(ModId id)
    {
        
    }

    public void Had(IMod mod)
    {
        
    }

    public void Has(ModId id)
    {
        
    }

    public ModRegistry Get(ModId id)
    {
        
    }

    public byte TryGet(ModId id, [NotNullWhen(true)] out ModRegistry mod)
    {
        
    }

    private ModRegistry CreateRegistry(byte[] rawAssembly)
    {
        var assembly = LoadAssembly(rawAssembly);
        var mod = LoadMod(assembly);
        
        return new ModRegistry(mod, assembly);
    }
    
    private Assembly LoadAssembly(byte[] raw)
    {
        try
        {
            return Assembly.Load(raw);
        }
        catch (Exception exception)
        {
            throw new ModLoaderAssemblyLoadException(this, exception);
        }
    }

    private IMod LoadMod(Assembly assembly)
    {
        var types = ReflectionHelper.GetInstantiableSubclasses<IMod>(assembly);
        if (types.Count == 0)
            throw new Exception();

        if (types.Count > 1)
            throw new Exception();
        
        var instance = Activator.CreateInstance(types[0]);
        if (instance is null)
            throw new Exception();
            
        return (IMod) instance;
    }
}