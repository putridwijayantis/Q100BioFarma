using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Q100BioFarma.Database.Abstract.Contracts;
using Q100BioFarma.Database.Framework.Extensions;

namespace Q100BioFarma.Database.Framework;

public abstract class StorageContextBase : DbContext, IStorageContext
{
    public StorageContextBase(DbContextOptions<StorageContextBase> options)
        : base(options)
    {
    }

    public StorageContextBase(IOptions<StorageContextOptions> options)
    {
        ConnectionString = options.Value.ConnectionString;
        MigrationsAssembly = options.Value.MigrationsAssembly;
    }

    public string ConnectionString { get; }

    public string MigrationsAssembly { get; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        this.RegisterEntities(modelBuilder);
    }
}