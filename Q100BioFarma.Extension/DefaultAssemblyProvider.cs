using System.Reflection;
using System.Runtime.Loader;
using Microsoft.Extensions.DependencyModel;
using Q100BioFarma.Extension.Contracts;

namespace Q100BioFarma.Extension;

public class DefaultAssemblyProvider : IAssemblyProvider
{
    protected ILogger _iLogger;

    public DefaultAssemblyProvider(IServiceProvider serviceProvider)
    {
        _iLogger = serviceProvider.GetService<ILoggerFactory>().CreateLogger("Q100BioFarma.Extension");
        IsCandidateAssembly = assembly =>
            !assembly.FullName.StartsWith("System", StringComparison.OrdinalIgnoreCase) &&
            !assembly.FullName.StartsWith("Microsoft", StringComparison.OrdinalIgnoreCase);

        IsCandidateCompilationLibrary = library =>
            !library.Name.StartsWith("mscorlib", StringComparison.OrdinalIgnoreCase) &&
            !library.Name.StartsWith("netstandard", StringComparison.OrdinalIgnoreCase) &&
            !library.Name.StartsWith("System", StringComparison.OrdinalIgnoreCase) &&
            !library.Name.StartsWith("Microsoft", StringComparison.OrdinalIgnoreCase) &&
            !library.Name.StartsWith("WindowsBase", StringComparison.OrdinalIgnoreCase);
    }

    public Func<Assembly, bool> IsCandidateAssembly { get; set; }

    public Func<Library, bool> IsCandidateCompilationLibrary { get; set; }

    public IEnumerable<Assembly> GetAssemblies(string path, bool includingSubpaths)
    {
        var assemblies = new List<Assembly>();

        GetAssembliesFromDependencyContext(assemblies);
        GetAssembliesFromPath(assemblies, path, includingSubpaths);
        return assemblies;
    }

    private void GetAssembliesFromDependencyContext(List<Assembly> assemblies)
    {
        _iLogger.LogInformation("Discovering and loading assemblies from DependencyContext");

        foreach (var compilationLibrary in DependencyContext.Default.CompileLibraries)
        {
            if (IsCandidateCompilationLibrary(compilationLibrary))
            {
                Assembly assembly = null;

                try
                {
                    assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(
                        new AssemblyName(compilationLibrary.Name));

                    if (!assemblies.Any(a =>
                            string.Equals(a.FullName, assembly.FullName, StringComparison.OrdinalIgnoreCase)))
                    {
                        assemblies.Add(assembly);
                        _iLogger.LogInformation("Assembly '{0}' is discovered and loaded", assembly.FullName);
                    }
                }
                catch (Exception e)
                {
                    _iLogger.LogWarning("Error loading assembly '{0}'", compilationLibrary.Name);
                    _iLogger.LogWarning(e.ToString());
                }
            }
        }
    }

    private void GetAssembliesFromPath(List<Assembly> assemblies, string path, bool includingSubpaths)
    {
        if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
        {
            _iLogger.LogInformation("Discovering and loading assemblies from path '{0}'", path);

            foreach (var extensionPath in Directory.EnumerateFiles(path, "*.dll"))
            {
                Assembly assembly = null;

                try
                {
                    assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(extensionPath);

                    if (IsCandidateAssembly(assembly) && !assemblies.Any(a =>
                            string.Equals(a.FullName, assembly.FullName, StringComparison.OrdinalIgnoreCase)))
                    {
                        assemblies.Add(assembly);
                        _iLogger.LogInformation("Assembly '{0}' is discovered and loaded", assembly.FullName);
                    }
                }
                catch (Exception e)
                {
                    _iLogger.LogWarning("Error loading assembly '{0}'", extensionPath);
                    _iLogger.LogWarning(e.ToString());
                }
            }

            if (includingSubpaths)
            {
                foreach (var subpath in Directory.GetDirectories(path))
                    GetAssembliesFromPath(assemblies, subpath, true);
            }
        }
        else
        {
            if (string.IsNullOrEmpty(path))
                _iLogger.LogWarning("Discovering and loading assemblies from path skipped: path not provided");
            else
                _iLogger.LogWarning("Discovering and loading assemblies from path '{0}' skipped: path not found", path);
        }
    }
}