using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudiePlusPlus.Application.Abstractions.Persistence;

namespace StudiePlusPlus.Infrastructure.Persistence.Repositories;

public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
{
    protected readonly AppDbContext Db;
    protected readonly DbSet<TEntity> Set;

    public Repository(AppDbContext db)
    {
        Db = db;
        Set = db.Set<TEntity>();
    }

#nullable enable
    public virtual Task<TEntity?> GetByIdAsync(TKey id, CancellationToken ct = default)
    {
        return Set.FindAsync([id], ct).AsTask();
    }
#nullable disable

    public Task<bool> ExistsAsync(TKey id, CancellationToken ct = default)
    {
        return Set.Where(entity => EF.Property<TKey>(entity, "Id").Equals(id)).AnyAsync(ct);
    }

    public virtual async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default)
    {
        return await Set.AsNoTracking().ToListAsync(ct);
    }

    public virtual async Task AddAsync(TEntity entity, CancellationToken ct = default)
    {
        await Set.AddAsync(entity, ct);
        await Db.SaveChangesAsync(ct);
    }

    public virtual async Task UpdateAsync(TEntity entity, CancellationToken ct = default)
    {
        Set.Update(entity);
        await Db.SaveChangesAsync(ct);
    }

    public virtual async Task RemoveAsync(TEntity entity, CancellationToken ct = default)
    {
        Set.Remove(entity);
        await Db.SaveChangesAsync(ct);
    }
}