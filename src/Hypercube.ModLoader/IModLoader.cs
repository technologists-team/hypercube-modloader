namespace Hypercube.ModLoader;

public interface IModLoader : IModLoaderClient
{
    ModRegistry GetRegistry(ModId id);
    bool TryGetRegistry(ModId id, out ModRegistry registry);
    
    ModRegistry Load(byte[] assembly);
    void Unload(IMod mod);
    void Unload(ModId id);
}