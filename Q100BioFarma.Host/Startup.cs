using System.Reflection;
using Q100BioFarma.Database.Framework;
using Q100BioFarma.Extension.Extensions;
using Q100BioFarma.Host.Migrations;
using Q100BioFarma.Infrastructur;
using Q100BioFarma.Infrastructur.Middleware;
using Q100BioFarma.Modules.Common.Services.Contracts;
using Q100BioFarma.Modules.Common.Services.Repositories;

namespace Q100BioFarma.Host;

public class Startup
{
    private readonly string _extensionsPath;
    private readonly IWebHostEnvironment _hostingEnvironment;
    private readonly string swaggerBasePath = "sys/be";

    /// <summary>
    ///     Initializes a new instance of the <see cref="Startup" /> class.
    /// </summary>
    /// <param name="configuration"> Get Configuration. </param>
    /// <param name="webHostEnvironment"> Get WebHostEnvironment. </param>
    public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
        Configuration = configuration;
        _extensionsPath = webHostEnvironment.ContentRootPath + configuration["Extensions:Path"];
        _hostingEnvironment = webHostEnvironment;
    }

    public IConfiguration Configuration { get; }

    /// <summary>
    ///     Configuration Global Service and DI.
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices(IServiceCollection services)
    {
        GlobalConfiguration.WebRootPath = _hostingEnvironment.WebRootPath;
        GlobalConfiguration.ContentRootPath = _hostingEnvironment.ContentRootPath;
        GlobalConfiguration.Environment = Configuration.GetValue<string>("Environment");

        services.AddCore(_extensionsPath, true);

        services.Configure<StorageContextOptions>(options =>
        {
            options.ConnectionString = Configuration.GetConnectionString("Connection");
            options.MigrationsAssembly = typeof(DesignTimeStorageContextFactory).GetTypeInfo().Assembly.FullName;
        });

        services.AddMemoryCache();
        
        // Dependency Injection for services
        services.AddScoped<IRecipesService, RecipesService>();
        services.AddScoped<IStepsServices, StepsService>();

        services.AddHttpContextAccessor();

        if (services != null)
        {
            DesignTimeStorageContextFactory.Initialize(services.BuildServiceProvider());
        }
    }

    /// <summary>
    ///     Configure App and Environment.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseMiddleware<ErrorHandlerMiddleware>();
        app.UseStatusCodePages();
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        if (!env.IsProduction())
        {
            app.UseSwagger(options =>
            {
                options.RouteTemplate = swaggerBasePath + "/swagger/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint($"/{swaggerBasePath}/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = $"{swaggerBasePath}/swagger";
            });
        }

        app.UseCookiePolicy();
        app.UseRouting();
        app.UseCore();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}