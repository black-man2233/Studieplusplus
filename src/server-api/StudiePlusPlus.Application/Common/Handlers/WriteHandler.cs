using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StudiePlusPlus.Application.Abstractions.Mapping;
using StudiePlusPlus.Application.Abstractions.Persistence;

namespace StudiePlusPlus.Application.Common.Handlers;

public record CreateCommand<TRequest>(TRequest Request);
public record UpdateCommand<TKey, TRequest>(TKey Id, TRequest Request);
public record DeleteCommand<TKey>(TKey Id);

public class WriteHandler<TEntity, TKey, TCreateRequest, TUpdateRequest, TDto>
    where TEntity : class
{
    private readonly IRepository<TEntity, TKey> _repository;
    private readonly IMapper<TCreateRequest, TEntity> _createMapper;
    private readonly IMapper<TUpdateRequest, TEntity> _updateMapper;
    private readonly IMapper<TEntity, TDto> _responseMapper;
    private readonly ILogger<WriteHandler<TEntity, TKey, TCreateRequest, TUpdateRequest, TDto>> _logger;

    private static readonly string _entityName = typeof(TEntity).Name;

    public WriteHandler(
        IRepository<TEntity, TKey> repository,
        IMapper<TCreateRequest, TEntity> createMapper,
        IMapper<TUpdateRequest, TEntity> updateMapper,
        IMapper<TEntity, TDto> responseMapper,
        ILogger<WriteHandler<TEntity, TKey, TCreateRequest, TUpdateRequest, TDto>> logger)
    {
        _repository = repository;
        _createMapper = createMapper;
        _updateMapper = updateMapper;
        _responseMapper = responseMapper;
        _logger = logger;
    }

    public async Task<TDto> Handle(CreateCommand<TCreateRequest> command, CancellationToken ct = default)
    {
        _logger.LogInformation("Creating {Entity}", _entityName);
        var entity = _createMapper.Map(command.Request);
        await _repository.AddAsync(entity, ct);
        var dto = _responseMapper.Map(entity);
        _logger.LogInformation("{Entity} created successfully", _entityName);
        return dto;
    }

    public async Task<TDto> Handle(UpdateCommand<TKey, TUpdateRequest> command, CancellationToken ct = default)
    {
        _logger.LogInformation("Updating {Entity} id={Id}", _entityName, command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, ct);

        if (entity == null)
        {
            _logger.LogWarning("{Entity} id={Id} not found — update aborted", _entityName, command.Id);
            return default;
        }

        _updateMapper.Update(command.Request, entity);
        await _repository.UpdateAsync(entity, ct);
        _logger.LogInformation("{Entity} id={Id} updated successfully", _entityName, command.Id);
        return _responseMapper.Map(entity);
    }

    public async Task<bool> Handle(DeleteCommand<TKey> command, CancellationToken ct = default)
    {
        _logger.LogInformation("Deleting {Entity} id={Id}", _entityName, command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, ct);

        if (entity == null)
        {
            _logger.LogWarning("{Entity} id={Id} not found — delete aborted", _entityName, command.Id);
            return false;
        }

        await _repository.RemoveAsync(entity, ct);
        _logger.LogInformation("{Entity} id={Id} deleted successfully", _entityName, command.Id);
        return true;
    }
}
