using Q100BioFarma.Database.Abstract.Contracts;
using Q100BioFarma.Infrastructur.Dtos;
using Q100BioFarma.Modules.Common.Models.Datas;

namespace Q100BioFarma.Modules.Common.Models.Contracts;

public interface IRecipesRepository : IRepository
{
    /// <summary>
    /// Get all data
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task<List<Recipes>> GetAllData();
    
    /// <summary>
    /// Get By Id
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task<Recipes?> GetById(Guid id);
    
    /// <summary>
    /// Add or Update
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task AddOrUpdate(Recipes model);
    
    /// <summary>
    /// Delete
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task Delete(Recipes model);
}