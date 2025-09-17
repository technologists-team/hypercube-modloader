using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using Hypercube.ModLoader.Exceptions;
using Hypercube.Utilities.Dependencies;
using Hypercube.Utilities.Extensions;
using Hypercube.Utilities.Helpers;
using JetBrains.Annotations;

namespace Hypercube.ModLoader;

/// <summary>
/// Default concrete implementation of <see cref="ModLoader{TMod,TClient}"/> 
/// using <see cref="IMod"/> and <see cref="IModClient"/> as the base extension types.
/// </summary>
/// <remarks>
/// This class can be used directly when no specialization of <see cref="ModLoader{TMod,TClient}"/>
/// is required. It manages general-purpose extensions without additional constraints.
/// </remarks>
public class ModLoader : ModLoader<IMod, IModClient>;

/// <summary>
/// Base implementation of an <see cref="IModLoader{TMod,TClient}"/> that manages the lifecycle
/// of extensions (loading, unloading, registry access).
/// </summary>
/// <typeparam name="TMod">
/// The extension type. Must implement both <see cref="IMod"/> and <see cref="IModClient"/>.
/// </typeparam>
/// <typeparam name="TClient">
/// The client-facing representation of an extension.
/// </typeparam>
/// <remarks>
/// This class provides common functionality for managing dynamically loaded extensions or plugins,
/// including assembly loading, dependency management, and lifecycle invocation.
/// By default, it uses <see cref="DependenciesContainer"/> but allows injection of a custom
/// <see cref="IDependenciesContainer"/>.
/// </remarks>
[PublicAPI]
public abstract class ModLoader<TMod, TClient> : IModLoader<TMod, TClient> where TMod : IMod, TClient where TClient : IModClient
{
    private readonly Dictionary<ModId, ModRegistry<TMod>> _mods = [];

    /// <inheritdoc/>
    public IDependenciesContainer DependenciesContainer { get; }

    /// <inheritdoc/>
    public virtual Version Version => new(1, 0, 0);

    protected ModLoader()
    {
        DependenciesContainer = new DependenciesContainer();
    }
    
    protected ModLoader(IDependenciesContainer dependenciesContainer)
    {
        DependenciesContainer = dependenciesContainer;
    }

    /// <inheritdoc/>
    public bool HasMod(ModId id)
    {
        return _mods.ContainsKey(id);
    }

    /// <inheritdoc/>
    public TClient GetMod(ModId id)
    {
        return _mods[id].Instance;
    }

    /// <inheritdoc/>
    public ModRegistry<TMod> GetRegistry(ModId id)
    {
        return _mods[id];
    }

    /// <inheritdoc/>
    public bool TryGetMod(ModId id, [NotNullWhen(true)] out TClient? mod)
    {
        return TryGetRegistry(id, out var registry) 
           ? (mod = registry.Instance) is not null 
           : (mod = default) is not null;
    }

    /// <inheritdoc/>
    public bool TryGetRegistry(ModId id, out ModRegistry<TMod> registry)
    {
        return _mods.TryGetValue(id, out registry);
    }

    /// <inheritdoc/>
    public ModRegistry<TMod> Load(byte[] assembly)
    {
        var registry = CreateRegistry(assembly);
        _mods.Add(registry.Instance.Id, registry);
        registry.Instance.OnLoad();
        return registry;
    }

    /// <inheritdoc/>
    public void Unload(TMod mod)
    {
        Unload(mod.Id);
    }

    /// <inheritdoc/>
    public void Unload(ModId id)
    {
        _mods.Remove(id, out var registry);
        registry.Instance.OnUnload();
    }

    private ModRegistry<TMod> CreateRegistry(byte[] raw)
    {
        var assembly = LoadAssembly(raw);
        var mod = LoadMod(assembly);
        
        return new ModRegistry<TMod>(mod, assembly);
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

    private TMod LoadMod(Assembly assembly)
    {
        var types = ReflectionHelper.GetInstantiableSubclasses<TMod>(assembly);
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
            
        return (TMod) instance;
    }
}
