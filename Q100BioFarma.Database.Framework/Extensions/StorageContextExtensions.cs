using Microsoft.EntityFrameworkCore;
using Q100BioFarma.Database.Abstract.Contracts;
using Q100BioFarma.Infrastructur;

namespace Q100BioFarma.Database.Framework.Extensions;

public static class StorageContextExtensions
{
    public static void RegisterEntities(this IStorageContext storageContext, ModelBuilder modelBuilder)
    {
        foreach (var entityRegistrar in ExtensionManager.GetInstances<IEntityRegister>())
            entityRegistrar.RegisterEntities(modelBuilder);
    }
}