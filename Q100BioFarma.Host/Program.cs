using Microsoft.AspNetCore;
using Serilog;

namespace Q100BioFarma.Host;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IWebHostBuilder CreateHostBuilder(string[] args)
    {
        return WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .ConfigureAppConfiguration(SetupConfiguration)
            .ConfigureLogging(SetupLogging);
    }

    private static void SetupConfiguration(IConfigurationBuilder configBuilder)
    {
        var configuration = configBuilder.Build();
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
    }

    private static void SetupLogging(WebHostBuilderContext hostingContext, ILoggingBuilder loggingBuilder)
    {
        loggingBuilder.AddSerilog();
    }
}