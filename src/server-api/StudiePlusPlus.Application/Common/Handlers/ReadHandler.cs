using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using StudiePlusPlus.Application.Abstractions.Mapping;
using StudiePlusPlus.Application.Abstractions.Persistence;

namespace StudiePlusPlus.Application.Common.Handlers;

public record GetByIdQuery<TKey>(TKey Id);
public record GetAllQuery();


public class ReadHandler<TEntity, TKey, TDto> where TEntity : class
{
    private readonly IRepository<TEntity, TKey> _repository;
    private readonly IMapper<TEntity, TDto> _mapper;

    public ReadHandler(IRepository<TEntity, TKey> repository, IMapper<TEntity, TDto> mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<TDto>> Handle(GetAllQuery query, CancellationToken ct = default)
    {
        var entities = await _repository.GetAllAsync(ct);
        return _mapper.Map(entities).ToList();
    }
    
    public async Task<TDto> Handle(GetByIdQuery<TKey> query, CancellationToken ct = default)
    {
        var entity = await _repository.GetByIdAsync(query.Id, ct);
        return entity == null ? default : _mapper.Map(entity);
    }
}