namespace Hypercube.ModLoader.Exceptions;

public abstract class ModLoaderException : Exception
{
    protected ModLoaderException(string? message) : base(message)
    {
    }
}
