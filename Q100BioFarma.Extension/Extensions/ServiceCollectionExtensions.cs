using Q100BioFarma.Extension.Contracts;
using Q100BioFarma.Infrastructur;
using Q100BioFarma.Infrastructur.Contracts;

namespace Q100BioFarma.Extension.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddCore(this IServiceCollection services)
    {
        services.AddCore(null);
    }

    public static void AddCore(this IServiceCollection services, string extensionsPath)
    {
        services.AddCore(extensionsPath, false, new DefaultAssemblyProvider(services.BuildServiceProvider()));
    }

    public static void AddCore(this IServiceCollection services, string extensionsPath, bool includingSubpaths)
    {
        services.AddCore(extensionsPath, includingSubpaths, new DefaultAssemblyProvider(services.BuildServiceProvider()));
    }

    public static void AddCore(this IServiceCollection services, string extensionsPath, IAssemblyProvider assemblyProvider)
    {
        services.AddCore(extensionsPath, false, assemblyProvider);
    }

    public static void AddCore(this IServiceCollection services, string extensionsPath, bool includingSubpaths, IAssemblyProvider assemblyProvider)
    {
        DiscoverAssemblies(assemblyProvider, extensionsPath, includingSubpaths);

        IServiceProvider serviceProvider = services.BuildServiceProvider();
        var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger("Q100BioFarma.Extension");
        foreach (var action in ExtensionManager.GetInstances<IConfigureServicesAction>().OrderBy(a => a.Priority))
        {
            logger.LogInformation("Executing ConfigureServices action '{0}'", action.GetType().FullName);
            action.Execute(services, serviceProvider);
            serviceProvider = services.BuildServiceProvider();
        }
    }

    private static void DiscoverAssemblies(IAssemblyProvider assemblyProvider, string extensionsPath, bool includingSubpaths)
    {
        ExtensionManager.SetAssemblies(assemblyProvider.GetAssemblies(extensionsPath, includingSubpaths));
    }
}