using Microsoft.AspNetCore.Mvc;
using StudiePlusPlus.Application;
using StudiePlusPlus.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudiePlusPlus.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly IStudentRepository _repo;
    public StudentController(IStudentRepository repo) => _repo = repo;


    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> Get() => Ok(await _repo.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<Student>> Get(Guid id) => Ok(await _repo.GetByIdAsync(id));

}