using Microsoft.EntityFrameworkCore;
using Q100BioFarma.Database.Framework;
using Q100BioFarma.Modules.Common.Models.Contracts;
using Q100BioFarma.Modules.Common.Models.Datas;

namespace Q100BioFarma.Modules.Common.Models.Repositories;

public class RecipesRepository : RepositoryBase<Recipes>, IRecipesRepository
{
    private IRecipesRepository _recipesRepositoryImplementation;

    public async Task<List<Recipes>> GetAllData()
    {
        var data = await dbSet.ToListAsync();
        return data;
    }

    public async Task<Recipes?> GetById(Guid id)
    {
        return await dbSet.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Recipes?> GetDetail(Guid id)
    {
        return await dbSet
            .Include(x => x.Steps)
            .ThenInclude(y => y.SubSteps)
            .Include(x => x.Steps)
            .ThenInclude(y => y.Parameters)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddOrUpdate(Recipes model)
    {
        var data = await dbSet.FirstOrDefaultAsync(x => x.Id == model.Id);
        if (data == null)
        {
            await dbSet.AddAsync(model);
        }
        else
        {
            data.Name = model.Name;
            data.Description = model.Description;
            dbSet.Update(data);
        }
    }

    public async Task Delete(Recipes model)
    {
        dbSet.Remove(model);
    }
}