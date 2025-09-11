namespace Hypercube.ModLoader;

public readonly struct ModMetaData
{
    public readonly string Name;
    public readonly string Description;
    public readonly string[] Authors;
    public readonly string License;
    public readonly string Source;
    public readonly string[] Tags;
    
    public ModMetaData(string name, string description, string[] authors, string license, string source, string[] tags)
    {
        Name = name;
        Description = description;
        Authors = authors;
        Source = source;
        Tags = tags;
        License = license;
    }
}