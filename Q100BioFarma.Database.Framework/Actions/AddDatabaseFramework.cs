using System.Reflection;
using Q100BioFarma.Database.Abstract.Contracts;
using Q100BioFarma.Infrastructur;
using Q100BioFarma.Infrastructur.Contracts;

namespace Q100BioFarma.Database.Framework.Actions;

public class AddDatabaseFramework : IConfigureServicesAction
{
    public int Priority => 200;

    public void Execute(IServiceCollection services, IServiceProvider serviceProvider)
    {
        var type = ExtensionManager.GetImplementations<IStorageContext>()
            ?.FirstOrDefault(t => !t.GetTypeInfo().IsAbstract);

        if (type == null)
        {
            var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger("Q100BioFarma.Database.Framework");

            logger.LogError("Implementation of Q100BioFarma.Abstract.Contracts.IStorageContext not found");
            return;
        }

        services.AddScoped(typeof(IStorageContext), type);
    }
}