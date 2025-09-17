using System.Text;
using JetBrains.Annotations;

namespace Hypercube.ModLoader;

/// <summary>
/// Represents metadata information associated with an extension (plugin, add-on, or module).
/// </summary>
/// <remarks>
/// This structure contains human-readable information such as name, description, authors, license,
/// source, and tags that can be used for discovery, diagnostics, or user display.
/// </remarks>
[PublicAPI]
public readonly struct ModMetaData
{
    /// <summary>
    /// Gets the display name of the extension.
    /// </summary>
    public readonly string Name;

    /// <summary>
    /// Gets the description of the extension.
    /// </summary>
    public readonly string Description;

    /// <summary>
    /// Gets the list of authors of the extension.
    /// </summary>
    public readonly string[] Authors;

    /// <summary>
    /// Gets the license under which the extension is distributed.
    /// </summary>
    public readonly string License;

    /// <summary>
    /// Gets the source location of the extension (e.g. repository URL).
    /// </summary>
    public readonly string Source;

    /// <summary>
    /// Gets the list of tags associated with the extension.
    /// </summary>
    public readonly string[] Tags;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ModMetaData"/> struct.
    /// </summary>
    /// <param name="name">The display name of the extension.</param>
    /// <param name="description">The description of the extension.</param>
    /// <param name="authors">The list of authors.</param>
    /// <param name="license">The license under which the extension is distributed.</param>
    /// <param name="source">The source location (e.g. repository URL).</param>
    /// <param name="tags">The list of tags associated with the extension.</param>
    public ModMetaData(string name, string description, string[] authors, string license, string source, string[] tags)
    {
        Name = name;
        Description = description;
        Authors = authors;
        Source = source;
        Tags = tags;
        License = license;
    }
    
    /// <summary>
    /// Returns a formatted string containing the metadata in a human-readable format.
    /// </summary>
    /// <returns>A string representation of the metadata.</returns>
    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.AppendLine($"[{Name}]");
        
        if (!string.IsNullOrWhiteSpace(Description))
            sb.AppendLine($"  Description : {Description}");

        if (Authors is { Length: > 0 })
            sb.AppendLine($"  Authors     : {string.Join(", ", Authors)}");

        if (!string.IsNullOrWhiteSpace(License))
            sb.AppendLine($"  License     : {License}");

        if (!string.IsNullOrWhiteSpace(Source))
            sb.AppendLine($"  Source      : {Source}");

        if (Tags is { Length: > 0 })
            sb.AppendLine($"  Tags        : {string.Join(", ", Tags)}");

        return sb.ToString().TrimEnd();
    }
}
