using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Q100BioFarma.Database.Abstract.Contracts;
using Q100BioFarma.Infrastructur;
using Q100BioFarma.Infrastructur.Constants;

namespace Q100BioFarma.Database.Framework;

public class Storage : IStorage
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public Storage(IStorageContext storageContext, IHttpContextAccessor httpContextAccessor)
    {
        if (!(storageContext is DbContext))
        {
            throw new ArgumentException("The storageContext object must be an instance of the " +
                                        "Microsoft.EntityFrameworkCore.DbContext class.");
        }

        StorageContext = storageContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public IStorageContext StorageContext { get; }

    public TRepository GetRepository<TRepository>()
        where TRepository : IRepository
    {
        var repository = ExtensionManager.GetInstance<TRepository>();

        if (repository != null) repository.SetStorageContext(StorageContext);

        return repository;
    }

    public void Save()
    {
        var username = GetAuthenticatedUserName();

        UpdateSoftDeleteStatuses();
        var strategy = (StorageContext as DbContext).Database.CreateExecutionStrategy();
        strategy.Execute(() =>
        {
            using var transact = (StorageContext as DbContext).Database.BeginTransaction();
            try
            {
                (StorageContext as DbContext).SaveChanges();
                transact.Commit();
            }
            catch (Exception ex)
            {
                transact.Rollback();
                throw Error.CustomError(ex.Message);
            }
        });
    }

    public async Task SaveAsync()
    {
        var username = GetAuthenticatedUserName();

        UpdateSoftDeleteStatuses();
        (StorageContext as DbContext).Database.CreateExecutionStrategy();
        //new AuditLogHelper(this).AddAuditLogs(username);
        var strategy = (StorageContext as DbContext).Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            await using var transact = await (StorageContext as DbContext).Database.BeginTransactionAsync();
            try
            {
                await (StorageContext as DbContext).SaveChangesAsync();
                await transact.CommitAsync();
            }
            catch (Exception ex)
            {
                await transact.RollbackAsync();
                throw Error.CustomError(ex.Message);
            }
        });
    }

    private void UpdateSoftDeleteStatuses()
    {
        try
        {
            var user = GetAuthenticatedUserId();
            foreach (var entry in (StorageContext as DbContext).ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Detached ||
                    entry.State == EntityState.Unchanged)
                    continue;

                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["CreatedAt"] = DateTime.Now;
                        entry.CurrentValues["CreatedBy"] = user;
                        entry.CurrentValues["DeletedAt"] = null;
                        entry.CurrentValues["UpdatedAt"] = null;
                        entry.CurrentValues["UpdatedBy"] = user;
                        break;
                    case EntityState.Modified:
                        entry.CurrentValues["UpdatedAt"] = DateTime.Now;
                        entry.CurrentValues["UpdatedBy"] = user;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["DeletedAt"] = DateTime.Now;
                        entry.CurrentValues["DeletedBy"] = user;
                        break;
                }
            }
        }
        catch { }
    }

    private Guid GetAuthenticatedUserId()
    {
        var id = _httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
        var userId = Guid.Parse("81314787-537b-474f-999a-9152c9703bbb");
        if (id != null) userId = Guid.Parse(id.Value);

        return userId;
    }

    private string GetAuthenticatedUserName()
    {
        return "systemadmin";
    }
}
