using System.Diagnostics.CodeAnalysis;

namespace Hypercube.ModLoader;

public interface IModLoaderClient
{
    Version Version { get; }

    bool HasMod(ModId id);
    IModClient GetMod(ModId id);
    bool TryGetMod(ModId id, [NotNullWhen(true)] out IModClient? mod);
}