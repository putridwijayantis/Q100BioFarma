using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Q100BioFarma.Database.Framework;

namespace Q100BioFarma.Database.PostgreSQL;

public class StorageContext : StorageContextBase
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="StorageContext" /> class.
    /// </summary>
    /// <param name="options">Based on Options</param>
    public StorageContext(IOptions<StorageContextOptions> options)
        : base(options)
    {
    }

    public StorageContext(DbContextOptions<StorageContextBase> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        if (string.IsNullOrEmpty(MigrationsAssembly))
        {
            optionsBuilder.UseNpgsql(ConnectionString, options => options.EnableRetryOnFailure());
        }
        else
        {
            optionsBuilder.UseNpgsql(ConnectionString,
                options =>
                {
                    options.MigrationsAssembly(MigrationsAssembly);
                    options.EnableRetryOnFailure();
                });
        }
    }
}