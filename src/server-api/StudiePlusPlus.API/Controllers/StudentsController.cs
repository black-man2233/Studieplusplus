using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudiePlusPlus.Application.Common.Handlers;
using StudiePlusPlus.Application.Features.Students.Contracts;
using StudiePlusPlus.Application.Features.Students.Dtos;
using StudiePlusPlus.Domain.Students;

namespace StudiePlusPlus.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly ReadHandler<Student, Guid, StudentDto> _read;
    private readonly WriteHandler<Student, Guid, CreateStudentRequest, UpdateStudentRequest, StudentDto> _write;

    public StudentsController(ReadHandler<Student, Guid, StudentDto> read,
        WriteHandler<Student, Guid, CreateStudentRequest, UpdateStudentRequest, StudentDto> write)
    {
        _read = read;
        _write = write;
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult<IReadOnlyList<StudentDto>>> GetAll(CancellationToken ct)
    {
        var students = await _read.Handle(new GetAllQuery(), ct);
        return Ok(students);
    }

    [HttpGet("GetById/{id:guid}")]
    public async Task<ActionResult> GetById([FromBody]Guid id, CancellationToken ct)
    {
        var student = await _read.Handle(new GetByIdQuery<Guid>(id), ct);
        return student is null ? NotFound() : Ok(student);
    }

    [HttpPost("Create")]
    public async Task<ActionResult<StudentDto>> Create([FromBody] CreateStudentRequest request, CancellationToken ct)
    {
        var created = await _write.Handle(new CreateCommand<CreateStudentRequest>(request), ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateStudentRequest request,
        CancellationToken ct)
    {
        var updated = await _write.Handle(new UpdateCommand<Guid, UpdateStudentRequest>(id, request), ct);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var ok = await _write.Handle(new DeleteCommand<Guid>(id), ct);
        return ok ? NoContent() : NotFound();
    }
}