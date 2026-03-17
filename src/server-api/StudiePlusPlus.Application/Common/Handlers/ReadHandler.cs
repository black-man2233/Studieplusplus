using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StudiePlusPlus.Application.Abstractions.Mapping;
using StudiePlusPlus.Application.Abstractions.Persistence;

namespace StudiePlusPlus.Application.Common.Handlers;

public record GetByIdQuery<TKey>(TKey Id);
public record GetAllQuery();

public class ReadHandler<TEntity, TKey, TDto> where TEntity : class
{
    private readonly IRepository<TEntity, TKey> _repository;
    private readonly IMapper<TEntity, TDto> _mapper;
    private readonly ILogger<ReadHandler<TEntity, TKey, TDto>> _logger;

    private static readonly string _entityName = typeof(TEntity).Name;

    public ReadHandler(
        IRepository<TEntity, TKey> repository,
        IMapper<TEntity, TDto> mapper,
        ILogger<ReadHandler<TEntity, TKey, TDto>> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IReadOnlyList<TDto>> Handle(GetAllQuery query, CancellationToken ct = default)
    {
        _logger.LogDebug("Querying all {Entity} records", _entityName);
        var entities = await _repository.GetAllAsync(ct);
        var results = _mapper.Map(entities).ToList();
        _logger.LogDebug("Returned {Count} {Entity} records", results.Count, _entityName);
        return results;
    }

    public async Task<TDto> Handle(GetByIdQuery<TKey> query, CancellationToken ct = default)
    {
        _logger.LogDebug("Querying {Entity} id={Id}", _entityName, query.Id);
        var entity = await _repository.GetByIdAsync(query.Id, ct);

        if (entity == null)
        {
            _logger.LogWarning("{Entity} id={Id} not found", _entityName, query.Id);
            return default;
        }

        return _mapper.Map(entity);
    }
}
