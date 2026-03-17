using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudiePlusPlus.Application.Common.Handlers;
using StudiePlusPlus.Application.Features.Subjects.Contracts;
using StudiePlusPlus.Application.Features.Subjects.Dtos;
using StudiePlusPlus.Domain.Academics;

namespace StudiePlusPlus.API.Controllers;

[ApiController, Route("api/[controller]")]
public class SubjectsController : CrudController<Subject, Guid, SubjectDto, CreateSubjectRequest, UpdateSubjectRequest>
{
    public SubjectsController(
        ReadHandler<Subject, Guid, SubjectDto> read,
        WriteHandler<Subject, Guid, CreateSubjectRequest, UpdateSubjectRequest, SubjectDto> write,
        ILoggerFactory loggerFactory)
        : base(read, write, loggerFactory)
    {
    }
}
