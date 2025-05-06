using System.Reflection;
using Q100BioFarma.Database.Abstract.Contracts;
using Q100BioFarma.Infrastructur;
using Q100BioFarma.Infrastructur.Contracts;

namespace Q100BioFarma.Database.Abstract.Abstract;

public class AddDatabaseAbstract : IConfigureServicesAction
{
    public int Priority => 200;

    public void Execute(IServiceCollection services, IServiceProvider serviceProvider)
    {
        var type = ExtensionManager.GetImplementations<IStorage>()?.FirstOrDefault(t => !t.GetTypeInfo().IsAbstract);

        if (type == null)
        {
            var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger("Q100BioFarma.Database.Abstract");

            logger.LogError("Q100BioFarma.Database.Abstract");
            return;
        }

        services.AddScoped(typeof(IStorage), type);
    }
}