using Microsoft.AspNetCore.Builder;
using Q100BioFarma.Infrastructur.Contracts;

namespace Q100BioFarma.Infrastructur.Actions;

public class UseEndpointsAction : IConfigureAction
{
    public int Priority => 11000;

    public void Execute(IApplicationBuilder applicationBuilder, IServiceProvider serviceProvider)
    {
        applicationBuilder.UseEndpoints(
            endpointRouteBuilder =>
            {
                foreach (var action in ExtensionManager.GetInstances<IUseEndpointsAction>().OrderBy(a => a.Priority))
                {
                    var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger("Q100BioFarma.Infrastructure");

                    logger.LogInformation("Executing UseEndpoints action '{0}'", action.GetType().FullName);
                    action.Execute(endpointRouteBuilder, serviceProvider);
                }
            });
    }
}