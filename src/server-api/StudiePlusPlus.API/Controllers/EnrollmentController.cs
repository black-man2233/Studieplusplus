using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudiePlusPlus.Application.Common.Handlers;
using StudiePlusPlus.Application.Features.Enrollments.Contracts;
using StudiePlusPlus.Application.Features.Enrollments.Dtos;
using StudiePlusPlus.Domain.Academics;
using StudiePlusPlus.Domain.Students;
using System;

namespace StudiePlusPlus.API.Controllers;

[ApiController, Route("api/[controller]/[action]")]

public class EnrollmentController : CrudController<Enrollment, Guid, EnrollmentDto, CreateEnrollmentRequest, UpdateEnrollmentRequest>
{
    public EnrollmentController(ReadHandler<Enrollment, Guid, EnrollmentDto> read, WriteHandler<Enrollment, Guid, CreateEnrollmentRequest, UpdateEnrollmentRequest, EnrollmentDto> write, ILoggerFactory loggerFactory) : base(read, write, loggerFactory)
    {
    }
}