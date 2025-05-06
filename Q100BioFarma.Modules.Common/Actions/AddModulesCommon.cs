using Q100BioFarma.Infrastructur;
using Q100BioFarma.Infrastructur.Contracts;
using Q100BioFarma.Modules.Common.Services.Contracts;
using Q100BioFarma.Modules.Common.Services.Repositories;

namespace Q100BioFarma.Modules.Common.Actions;

public class AddModulesCommon : IConfigureServicesAction
{
    public IConfiguration Configuration { get; set; }

    public int Priority => 1000;

    public void Execute(IServiceCollection services, IServiceProvider serviceProvider)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(GlobalConfiguration.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        Configuration = builder.Build();

        // services.AddAutoMapper(typeof(CommonProfiles));
        services.AddScoped<IRecipesService, RecipesService>();
    }
}