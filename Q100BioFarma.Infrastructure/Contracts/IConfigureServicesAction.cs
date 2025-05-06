namespace Q100BioFarma.Infrastructur.Contracts;

/// <summary>
/// Configure Services Action
/// </summary>
public interface IConfigureServicesAction
{
    /// <summary>
    /// Gets priority to executed
    /// </summary>
    int Priority { get; }

    /// <summary>
    /// Execute Services Collection
    /// </summary>
    /// <param name="services"></param>
    /// <param name="serviceProvider"></param>
    void Execute(IServiceCollection services, IServiceProvider serviceProvider);
}