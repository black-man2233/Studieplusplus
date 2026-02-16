using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudiePlusPlus.Application;

namespace StudiePlusPlus.API.Controllers;

[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _repo;
    public ProductsController(IProductRepository repo) => _repo = repo;


    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id) => Ok(await _repo.GetByIdAsync(id));
}