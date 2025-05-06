using Q100BioFarma.Modules.Common.Models.Datas;

namespace Q100BioFarma.Modules.Common.Services.Contracts;

/// <summary>
///     Recipes Service Contracts
/// </summary>
public interface IRecipesService
{
    /// <summary>
    ///     Update App Setting By Id
    /// </summary>
    Task<List<Recipes>> GetAll();
}