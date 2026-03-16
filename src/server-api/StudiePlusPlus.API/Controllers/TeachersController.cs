using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudiePlusPlus.Application.Common.Handlers;
using StudiePlusPlus.Application.Features.Teachers.Contracts;
using StudiePlusPlus.Application.Features.Teachers.Dtos;
using StudiePlusPlus.Domain.Teachers;

namespace StudiePlusPlus.API.Controllers;

[ApiController, Route("api/[controller]")]
public class TeachersController : CrudController<Teacher, Guid, TeacherDto, CreateTeacherRequest, UpdateTeacherRequest>
{
    public TeachersController(
        ReadHandler<Teacher, Guid, TeacherDto> read,
        WriteHandler<Teacher, Guid, CreateTeacherRequest, UpdateTeacherRequest, TeacherDto> write,
        ILoggerFactory loggerFactory)
        : base(read, write, loggerFactory)
    {
    }
}
