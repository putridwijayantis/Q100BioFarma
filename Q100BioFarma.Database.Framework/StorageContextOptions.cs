namespace Q100BioFarma.Database.Framework;

public class StorageContextOptions
{
    public string ConnectionString { get; set; }

    public string ConnectionStringBridging { get; set; }

    public string MigrationsAssembly { get; set; }
}