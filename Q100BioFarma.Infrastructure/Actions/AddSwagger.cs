using Microsoft.OpenApi.Models;
using Q100BioFarma.Infrastructur.Contracts;

namespace Q100BioFarma.Infrastructur.Actions;

public class AddSwagger : IConfigureServicesAction
    {
        public int Priority => 1000;

        public void Execute(IServiceCollection services, IServiceProvider serviceProvider)
        {
            services.AddSwaggerGen(options =>
            {
                var envName = new List<string> { "dev", "uat" };
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Q100BioFarma API",
                    Description = "Q100BioFarma Api skeleton with .net 8 with Clean Architecture" + (envName.Contains(GlobalConfiguration.Environment.ToLower()) ? " - " + GlobalConfiguration.Environment : string.Empty),
                });
            });
        }
    }