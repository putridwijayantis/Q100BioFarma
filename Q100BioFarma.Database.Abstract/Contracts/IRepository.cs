namespace Q100BioFarma.Database.Abstract.Contracts;

public interface IRepository
{
    /// <summary>
    ///     Storage Context on every Repository.
    /// </summary>
    /// <param name="storageContext"></param>
    void SetStorageContext(IStorageContext storageContext);
}