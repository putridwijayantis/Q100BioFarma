using System.Reflection;

namespace Q100BioFarma.Extension.Contracts;

/// <summary>
/// Assembly Provider contract
/// </summary>
public interface IAssemblyProvider
{
    /// <summary>
    /// Populating assembly
    /// </summary>
    /// <param name="path"></param>
    /// <param name="includingSubpaths"></param>
    /// <returns>IEnumerable<Assembly />
    /// </returns>
    IEnumerable<Assembly> GetAssemblies(string path, bool includingSubpaths);
}