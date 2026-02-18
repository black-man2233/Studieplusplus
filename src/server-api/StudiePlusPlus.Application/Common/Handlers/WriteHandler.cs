using System.Threading;
using System.Threading.Tasks;
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

    public WriteHandler(
        IRepository<TEntity, TKey> repository, 
        IMapper<TCreateRequest, TEntity> createMapper, 
        IMapper<TUpdateRequest, TEntity> updateMapper,
        IMapper<TEntity, TDto> responseMapper)
    {
        _repository = repository;
        _createMapper = createMapper;
        _updateMapper = updateMapper;
        _responseMapper = responseMapper;
    }

    public async Task<TDto> Handle(CreateCommand<TCreateRequest> command, CancellationToken ct = default)
    {
        var entity = _createMapper.Map(command.Request);
        await _repository.AddAsync(entity, ct);
        return _responseMapper.Map(entity);
    }

    public async Task<TDto> Handle(UpdateCommand<TKey, TUpdateRequest> command, CancellationToken ct = default)
    {
        var entity = await _repository.GetByIdAsync(command.Id, ct);
        if (entity == null) return default;

        _updateMapper.Update(command.Request, entity);

        await _repository.UpdateAsync(entity, ct);
        return _responseMapper.Map(entity);
    }

    public async Task<bool> Handle(DeleteCommand<TKey> command, CancellationToken ct = default)
    {
        var entity = await _repository.GetByIdAsync(command.Id, ct);
        if (entity == null) return false;

        await _repository.RemoveAsync(entity, ct);
        return true;
    }
}
