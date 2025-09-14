namespace Hypercube.ModLoader;

public interface IMod : IModClient
{
    void OnLoad();
    void OnUnload();
}