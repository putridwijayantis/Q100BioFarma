using Q100BioFarma.Database.Abstract.Contracts;
using Q100BioFarma.Modules.Common.Models.Datas;

namespace Q100BioFarma.Modules.Common.Models.Contracts;

public interface IStepRepository : IRepository
{
    /// <summary>
    /// Add or Update
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task AddOrUpdate(Steps model);
}