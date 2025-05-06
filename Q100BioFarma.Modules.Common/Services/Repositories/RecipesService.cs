using Q100BioFarma.Database.Abstract.Contracts;
using Q100BioFarma.Modules.Common.Models.Contracts;
using Q100BioFarma.Modules.Common.Models.Datas;
using Q100BioFarma.Modules.Common.Services.Contracts;

namespace Q100BioFarma.Modules.Common.Services.Repositories;

public class RecipesService : IRecipesService
{
    
    private readonly IStorage _iStorage;

    public RecipesService(IStorage iStorage)
    {
        _iStorage = iStorage;
    }
    
    public async Task<List<Recipes>> GetAll()
    {
        return await _iStorage.GetRepository<IRecipesRepository>().GetAllData();
    }
}