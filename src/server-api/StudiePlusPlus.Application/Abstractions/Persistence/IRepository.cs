using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StudiePlusPlus.Application.Abstractions.Persistence;

public interface IRepository<TEntity, in TKey> where TEntity : class
{
    Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default);
#nullable enable
    Task<TEntity?> GetByIdAsync(TKey id, CancellationToken ct = default);
#nullable disable

    Task<bool> ExistsAsync(TKey id, CancellationToken ct = default);

    Task AddAsync(TEntity entity, CancellationToken ct = default);

    Task UpdateAsync(TEntity entity, CancellationToken ct = default);

    Task RemoveAsync(TEntity entity, CancellationToken ct = default);
}