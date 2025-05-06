using Microsoft.EntityFrameworkCore;
using Q100BioFarma.Database.Framework;
using Q100BioFarma.Modules.Common.Models.Contracts;
using Q100BioFarma.Modules.Common.Models.Datas;

namespace Q100BioFarma.Modules.Common.Models.Repositories;

public class RecipesRepository : RepositoryBase<Recipes>, IRecipesRepository
{
    public async Task<List<Recipes>> GetAllData()
    {
        var data = await dbSet.ToListAsync();
        return data;
    }
}