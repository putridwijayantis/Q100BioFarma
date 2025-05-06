namespace Q100BioFarma.Infrastructur.Contracts;

/// <summary>
/// MVC Action
/// </summary>
public interface IAddMvcAction
{
    /// <summary>
    /// Gets priority to be executed
    /// </summary>
    int Priority { get; }

    /// <summary>
    /// Execute Service provider on modular
    /// </summary>
    /// <param name="mvcBuilder"></param>
    /// <param name="serviceProvider"></param>
    void Execute(IMvcBuilder mvcBuilder, IServiceProvider serviceProvider);
}