using Microsoft.AspNetCore.Mvc;
using StudiePlusPlus.Application;
using StudiePlusPlus.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudiePlusPlus.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _repo;
    public ProductsController(IProductRepository repo) => _repo = repo;


    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> Get() => Ok(await _repo.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> Get(Guid id) => Ok(await _repo.GetByIdAsync(id));

}