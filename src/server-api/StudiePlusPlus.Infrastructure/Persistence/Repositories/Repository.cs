using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudiePlusPlus.Application.Abstractions.Persistence;

namespace StudiePlusPlus.Infrastructure.Persistence.Repositories;

public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
{
    protected readonly AppDbContext Db;
    protected readonly DbSet<TEntity> Set;
    protected readonly ILogger Logger;

    private static readonly string _entityName = typeof(TEntity).Name;

    public Repository(AppDbContext db, ILoggerFactory loggerFactory)
    {
        Db = db;
        Set = db.Set<TEntity>();
        // Use the concrete subclass type as the logger name so Seq shows e.g. "MessageRepository"
        Logger = loggerFactory.CreateLogger(GetType());
    }

#nullable enable
    public virtual Task<TEntity?> GetByIdAsync(TKey id, CancellationToken ct = default)
    {
        Logger.LogDebug("DB GetById {Entity} id={Id}", _entityName, id);
        return Set.FindAsync([id], ct).AsTask();
    }
#nullable disable

    public Task<bool> ExistsAsync(TKey id, CancellationToken ct = default)
    {
        return Set.Where(entity => EF.Property<TKey>(entity, "Id").Equals(id)).AnyAsync(ct);
    }

    public virtual async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default)
    {
        Logger.LogDebug("DB GetAll {Entity}", _entityName);
        var result = await Set.AsNoTracking().ToListAsync(ct);
        Logger.LogDebug("DB GetAll {Entity} — {Count} rows", _entityName, result.Count);
        return result;
    }

    public virtual async Task AddAsync(TEntity entity, CancellationToken ct = default)
    {
        Logger.LogDebug("DB Insert {Entity}", _entityName);
        await Set.AddAsync(entity, ct);
        await Db.SaveChangesAsync(ct);
        Logger.LogDebug("DB Insert {Entity} committed", _entityName);
    }

    public virtual async Task UpdateAsync(TEntity entity, CancellationToken ct = default)
    {
        Logger.LogDebug("DB Update {Entity}", _entityName);
        Set.Update(entity);
        await Db.SaveChangesAsync(ct);
        Logger.LogDebug("DB Update {Entity} committed", _entityName);
    }

    public virtual async Task RemoveAsync(TEntity entity, CancellationToken ct = default)
    {
        Logger.LogDebug("DB Delete {Entity}", _entityName);
        Set.Remove(entity);
        await Db.SaveChangesAsync(ct);
        Logger.LogDebug("DB Delete {Entity} committed", _entityName);
    }
}
