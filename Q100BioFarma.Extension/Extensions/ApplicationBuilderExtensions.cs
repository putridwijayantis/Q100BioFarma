using Microsoft.AspNetCore.Builder;
using Q100BioFarma.Infrastructur;
using Q100BioFarma.Infrastructur.Contracts;

namespace Q100BioFarma.Extension.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void UseCore(this IApplicationBuilder applicationBuilder)
    {
        var logger = applicationBuilder.ApplicationServices.GetService<ILoggerFactory>()
            .CreateLogger("Q100BioFarma.Extension");

        foreach (var action in ExtensionManager.GetInstances<IConfigureAction>().OrderBy(a => a.Priority))
        {
            logger.LogInformation("Executing Configure action '{0}'", action.GetType().FullName);
            action.Execute(applicationBuilder, applicationBuilder.ApplicationServices);
        }
    }
}