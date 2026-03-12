using System;
using StudiePlusPlus.Application.Common.Handlers;
using StudiePlusPlus.Application.Features.Students.Contracts;
using StudiePlusPlus.Application.Features.Students.Dtos;
using StudiePlusPlus.Application.Features.Teachers.Contracts;
using StudiePlusPlus.Application.Features.Teachers.Dtos;
using StudiePlusPlus.Domain.Teachers;

namespace StudiePlusPlus.API.Controllers;

public class TeachersController : CrudController<Teacher, Guid, TeacherDto, CreateTeacherRequest, UpdateTeacherRequest>
{
    public TeachersController(ReadHandler<Teacher, Guid, TeacherDto> read,
        WriteHandler<Teacher, Guid, CreateTeacherRequest, UpdateTeacherRequest, TeacherDto> write) : base(read, write)
    {
    }
}