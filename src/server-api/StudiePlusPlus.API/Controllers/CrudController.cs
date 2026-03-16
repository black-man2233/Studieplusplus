using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudiePlusPlus.Application.Common.Handlers;

namespace StudiePlusPlus.API.Controllers;

[Authorize]
public abstract class CrudController<TEntity, TKey, TDto, TCreateRequest, TUpdateRequest> : ControllerBase
    where TEntity : class
{
    protected readonly ReadHandler<TEntity, TKey, TDto> _read;
    protected readonly WriteHandler<TEntity, TKey, TCreateRequest, TUpdateRequest, TDto> _write;
    protected readonly ILogger _logger;

    private static readonly string _entityName = typeof(TEntity).Name;

    protected CrudController(
        ReadHandler<TEntity, TKey, TDto> read,
        WriteHandler<TEntity, TKey, TCreateRequest, TUpdateRequest, TDto> write,
        ILoggerFactory loggerFactory)
    {
        _read = read;
        _write = write;
        _logger = loggerFactory.CreateLogger(GetType());
    }

    private string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "anonymous";

    [HttpGet]
    public virtual async Task<ActionResult<IReadOnlyList<TDto>>> GetAll(CancellationToken ct)
    {
        _logger.LogInformation("User={User} GET all {Entity}", UserId, _entityName);
        var result = await _read.Handle(new GetAllQuery(), ct);
        _logger.LogInformation("User={User} GET all {Entity} returned {Count} items", UserId, _entityName, result.Count);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public virtual async Task<ActionResult<TDto>> GetById([FromRoute] TKey id, CancellationToken ct)
    {
        _logger.LogInformation("User={User} GET {Entity} id={Id}", UserId, _entityName, id);
        var result = await _read.Handle(new GetByIdQuery<TKey>(id), ct);
        if (result is null)
        {
            _logger.LogWarning("User={User} {Entity} id={Id} not found", UserId, _entityName, id);
            return NotFound();
        }
        return Ok(result);
    }

    [HttpPost]
    public virtual async Task<ActionResult<TDto>> Create([FromBody] TCreateRequest request, CancellationToken ct)
    {
        _logger.LogInformation("User={User} POST create {Entity}", UserId, _entityName);
        try
        {
            var created = await _write.Handle(new CreateCommand<TCreateRequest>(request), ct);
            _logger.LogInformation("User={User} {Entity} created with id={Id}", UserId, _entityName, GetEntityId(created));
            return CreatedAtAction(nameof(GetById), new { id = GetEntityId(created) }, created);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "User={User} failed to create {Entity}", UserId, _entityName);
            throw;
        }
    }

    [HttpPut("{id}")]
    public virtual async Task<ActionResult<TDto>> Update([FromRoute] TKey id, [FromBody] TUpdateRequest request, CancellationToken ct)
    {
        _logger.LogInformation("User={User} PUT update {Entity} id={Id}", UserId, _entityName, id);
        var updated = await _write.Handle(new UpdateCommand<TKey, TUpdateRequest>(id, request), ct);
        if (updated is null)
        {
            _logger.LogWarning("User={User} {Entity} id={Id} not found for update", UserId, _entityName, id);
            return NotFound();
        }
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> Delete([FromRoute] TKey id, CancellationToken ct)
    {
        _logger.LogInformation("User={User} DELETE {Entity} id={Id}", UserId, _entityName, id);
        var deleted = await _write.Handle(new DeleteCommand<TKey>(id), ct);
        if (!deleted)
        {
            _logger.LogWarning("User={User} {Entity} id={Id} not found for delete", UserId, _entityName, id);
            return NotFound();
        }
        return NoContent();
    }

    protected virtual object GetEntityId(TDto dto)
    {
        var prop = typeof(TDto).GetProperty("Id");
        return prop?.GetValue(dto);
    }
}
