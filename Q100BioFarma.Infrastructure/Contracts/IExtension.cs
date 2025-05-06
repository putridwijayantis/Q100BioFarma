namespace Q100BioFarma.Infrastructur.Contracts;

/// <summary>
/// Extension modular
/// </summary>
public interface IExtension
{
    /// <summary>
    /// Gets name module
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets description module
    /// </summary>
    string Description { get; }

    /// <summary>
    /// Gets url module
    /// </summary>
    string Url { get; }

    /// <summary>
    /// Gets version module
    /// </summary>
    string Version { get; }

    /// <summary>
    /// Gets author module
    /// </summary>
    string Authors { get; }
}