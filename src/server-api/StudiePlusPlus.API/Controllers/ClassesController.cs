using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudiePlusPlus.Application.Common.Handlers;
using StudiePlusPlus.Application.Features.Class.Contracts;
using StudiePlusPlus.Application.Features.Class.Dtos;
using StudiePlusPlus.Domain.Academics;

namespace StudiePlusPlus.API.Controllers;

[ApiController, Route("api/[controller]")]
public class ClassesController : CrudController<Class, Guid, ClassDto, CreateClassRequest, UpdateClassRequest>
{
    public ClassesController(
        ReadHandler<Class, Guid, ClassDto> read,
        WriteHandler<Class, Guid, CreateClassRequest, UpdateClassRequest, ClassDto> write,
        ILoggerFactory loggerFactory)
        : base(read, write, loggerFactory)
    {
    }
}
