using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using Hypercube.ModLoader.Exceptions;
using Hypercube.Utilities.Extensions;
using Hypercube.Utilities.Helpers;
using JetBrains.Annotations;

namespace Hypercube.ModLoader;

[PublicAPI]
public class ModLoader : IModLoader
{
    public virtual Version Version => new(1, 0, 0);
    
    private readonly Dictionary<ModId, ModRegistry> _mods = [];
    
    public bool HasMod(ModId id)
    {
        return _mods.ContainsKey(id);
    }

    IModClient IModLoaderClient.GetMod(ModId id)
    {
        return _mods[id].Instance;
    }

    ModRegistry IModLoader.GetRegistry(ModId id)
    {
        return _mods[id];
    }

    public bool TryGetMod(ModId id, [NotNullWhen(true)] out IModClient? mod)
    {
        return TryGetRegistry(id, out var registry) 
           ? (mod = registry.Instance) is not null 
           : (mod = null) is not null;
    }

    public bool TryGetRegistry(ModId id, out ModRegistry registry)
    {
        return _mods.TryGetValue(id, out registry);
    }

    public ModRegistry Load(byte[] assembly)
    {
        var registry = CreateRegistry(assembly);
        _mods.Add(registry.Instance.Id, registry);
        registry.Instance.OnLoad();
        return registry;
    }

    public void Unload(IMod mod)
    {
        Unload(mod.Id);
    }

    public void Unload(ModId id)
    {
        _mods.Remove(id, out var registry);
        registry.Instance.OnUnload();
    }

    private ModRegistry CreateRegistry(byte[] raw)
    {
        var assembly = LoadAssembly(raw);
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

        var type = types[0];
        var instance = RuntimeHelpers.GetUninitializedObject(type);

        foreach (var info in type.GetClassHierarchy().SelectMany(t => t.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance)))
        {
            if (info is { Name: nameof(IMod.ModLoader), CanWrite: true })
                info.SetValue(instance, this);
        }
        
        type.GetConstructors()
            .First(info => info.GetParameters().Length == 0)
            .Invoke(instance, null);
        
        if (instance is null)
            throw new Exception();
            
        return (IMod) instance;
    }
}