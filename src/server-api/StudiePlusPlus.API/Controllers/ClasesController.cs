using System;
using StudiePlusPlus.Application.Common.Handlers;
using StudiePlusPlus.Application.Features.Class.Contracts;
using StudiePlusPlus.Application.Features.Class.Dtos;
using StudiePlusPlus.Domain.Academics;

namespace StudiePlusPlus.API.Controllers;

public class ClasesController : CrudController<Class, Guid, ClassDto, CreateClassRequest, UpdateClassRequest>
{
    public ClasesController(ReadHandler<Class, Guid, ClassDto> read, WriteHandler<Class, Guid, CreateClassRequest, UpdateClassRequest, ClassDto> write) : base(read, write)
    {
    }
}