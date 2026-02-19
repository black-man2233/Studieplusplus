using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudiePlusPlus.Application.Common.Handlers;

namespace StudiePlusPlus.API.Controllers;

[ApiController, Route("api/[controller]/[action]")]
public abstract class CrudController<TEntity, TKey, TDto, TCreateRequest, TUpdateRequest> : ControllerBase
    where TEntity : class
{
    protected readonly ReadHandler<TEntity, TKey, TDto> _read;
    protected readonly WriteHandler<TEntity, TKey, TCreateRequest, TUpdateRequest, TDto> _write;

    protected CrudController(
        ReadHandler<TEntity, TKey, TDto> read,
        WriteHandler<TEntity, TKey, TCreateRequest, TUpdateRequest, TDto> write)
    {
        _read = read;
        _write = write;
    }

    [HttpGet]
    public virtual async Task<ActionResult<IReadOnlyList<TDto>>> GetAll(CancellationToken ct)
    {
        var result = await _read.Handle(new GetAllQuery(), ct);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public virtual async Task<ActionResult<TDto>> GetById([FromRoute] TKey id, CancellationToken ct)
    {
        var result = await _read.Handle(new GetByIdQuery<TKey>(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public virtual async Task<ActionResult<TDto>> Create([FromBody] TCreateRequest request, CancellationToken ct)
    {
        var created = await _write.Handle(new CreateCommand<TCreateRequest>(request), ct);

        return CreatedAtAction(nameof(GetById), new { id = GetEntityId(created) },
            created);
    }

    [HttpPut("{id}")]
    public virtual async Task<ActionResult<TDto>> Update([FromRoute] TKey id, [FromBody] TUpdateRequest request, CancellationToken ct)
    {
        var updated = await _write.Handle(new UpdateCommand<TKey, TUpdateRequest>(id, request), ct);

        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> Delete([FromRoute] TKey id, CancellationToken ct)
    {
        var deleted = await _write.Handle(new DeleteCommand<TKey>(id), ct);

        return deleted ? NoContent() : NotFound();
    }

    /// <summary>
    /// Override if your DTO does not expose Id directly.
    /// </summary>
    protected virtual object GetEntityId(TDto dto)
    {
        var prop = typeof(TDto).GetProperty("Id");
        return prop?.GetValue(dto);
    }
}
