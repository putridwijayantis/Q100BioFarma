using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Q100BioFarma.Infrastructur.Contracts;

namespace Q100BioFarma.Infrastructur.Actions;

public class AddMvcAction : IConfigureServicesAction
{
    public int Priority => 10000;

    public void Execute(IServiceCollection services, IServiceProvider serviceProvider)
    {
        var mvcBuilder = services.AddMvc();

        foreach (var assembly in ExtensionManager.Assemblies)
        {
            mvcBuilder.AddApplicationPart(assembly);
        }

        foreach (var action in ExtensionManager.GetInstances<IAddMvcAction>().OrderBy(a => a.Priority))
        {
            var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger("Q100BioFarma.Infrastructure");

            logger.LogInformation("Executing AddMvc action '{0}'", action.GetType().FullName);
            action.Execute(mvcBuilder, serviceProvider);
        }

        // Configuring endpoint routing for MVC
        services.AddMvc(option =>
        {
            option.EnableEndpointRouting = false;
            option.MaxIAsyncEnumerableBufferLimit = 100000;
        }).AddJsonOptions(jsonOptions =>
        {
            // IgnoreNullValues is obsolete.
            // jsonOptions.JsonSerializerOptions.IgnoreNullValues = true;
            jsonOptions.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        }).AddNewtonsoftJson(opt =>
        {
            opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            opt.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            opt.SerializerSettings.DateFormatString = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
        });
    }
}