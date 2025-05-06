using Microsoft.EntityFrameworkCore;
using Q100BioFarma.Database.Framework;
using Q100BioFarma.Modules.Common.Models.Contracts;
using Q100BioFarma.Modules.Common.Models.Datas;

namespace Q100BioFarma.Modules.Common.Models.Repositories;

public class SubStepRepository : RepositoryBase<SubSteps>, ISubStepRepository
{
    public async Task AddOrUpdate(SubSteps model)
    {
        var data = await dbSet.FirstOrDefaultAsync(x => x.Id == model.Id);
        if (data == null)
        {
            await dbSet.AddAsync(model);
        }
        else
        {
            data.Name = model.Name;
            dbSet.Update(data);
        }
    }
}