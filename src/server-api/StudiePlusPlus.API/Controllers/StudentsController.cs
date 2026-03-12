using System;
using Microsoft.AspNetCore.Mvc;
using StudiePlusPlus.Application.Common.Handlers;
using StudiePlusPlus.Application.Features.Students.Contracts;
using StudiePlusPlus.Application.Features.Students.Dtos;
using StudiePlusPlus.Domain.Students;

namespace StudiePlusPlus.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : CrudController<Student, Guid, StudentDto, CreateStudentRequest, UpdateStudentRequest>
{
    public StudentsController(
        ReadHandler<Student, Guid, StudentDto> read,
        WriteHandler<Student, Guid, CreateStudentRequest, UpdateStudentRequest, StudentDto> write)
        : base(read, write)
    {
    }
}