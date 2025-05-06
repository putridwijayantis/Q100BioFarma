namespace Q100BioFarma.Database.Abstract.Contracts;

/// <summary>
///     IStorage.
/// </summary>
public interface IStorage
{
    /// <summary>
    ///     Gets Storage Context.
    /// </summary>
    IStorageContext StorageContext { get; }

    /// <summary>
    ///     GetRepository where T is table.
    /// </summary>
    /// <returns>Returning repository></returns>
    T GetRepository<T>()
        where T : IRepository;

    /// <summary>
    ///     Save transact.
    /// </summary>
    void Save();

    /// <summary>
    ///     Save transact async.
    /// </summary>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    Task SaveAsync();
}