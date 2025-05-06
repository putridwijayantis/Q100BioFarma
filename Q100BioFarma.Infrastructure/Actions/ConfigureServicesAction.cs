using Q100BioFarma.Infrastructur.Contracts;

namespace Q100BioFarma.Infrastructur.Actions;

public class ConfigureServicesAction : IConfigureServicesAction
{
    public IConfiguration Configuration { get; set; }

    public int Priority => 1000;

    public void Execute(IServiceCollection services, IServiceProvider serviceProvider)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(GlobalConfiguration.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        Configuration = builder.Build();
    }
}