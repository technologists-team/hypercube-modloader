using System.Text;
using JetBrains.Annotations;

namespace Hypercube.ModLoader;

[PublicAPI]
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
    
    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.AppendLine($"[{Name}]");
        sb.AppendLine($"- Description: {Description}");
        sb.AppendLine($"- Authors: {(Authors is { Length: > 0 } ? string.Join(", ", Authors) : "Unknown")}");
        sb.AppendLine($"- License: {License}");
        sb.AppendLine($"- Source: {Source}");
        sb.Append($"- Tags: {(Tags is { Length: > 0 } ? string.Join(", ", Tags) : "None")}");
        
        return sb.ToString();
    }
}