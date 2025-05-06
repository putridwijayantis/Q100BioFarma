using Microsoft.EntityFrameworkCore;
using Q100BioFarma.Database.Abstract.Contracts;

namespace Q100BioFarma.Database.Framework;

public abstract class RepositoryBase<TEntity> : IRepository
    where TEntity : class, IEntity
{
    protected DbSet<TEntity> dbSet;
    protected DbContext storageContext;

    public void SetStorageContext(IStorageContext sContext)
    {
        this.storageContext = sContext as DbContext;
        dbSet = this.storageContext.Set<TEntity>();
    }
}