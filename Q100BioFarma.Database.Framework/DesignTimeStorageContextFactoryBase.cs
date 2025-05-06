using Microsoft.EntityFrameworkCore.Design;
using Q100BioFarma.Database.Abstract.Contracts;

namespace Q100BioFarma.Database.Framework;

public abstract class DesignTimeStorageContextFactoryBase<T> : IDesignTimeDbContextFactory<T>
    where T : StorageContextBase
{
    public static T StorageContext { get; set; }

    public static void Initialize(IServiceProvider serviceProvider)
    {
        StorageContext = serviceProvider.GetService<IStorageContext>() as T;
    }

    public T CreateDbContext(string[] args)
    {
        return StorageContext;
    }
}