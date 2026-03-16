using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudiePlusPlus.Application.Common.Handlers;
using StudiePlusPlus.Application.Features.WeeklySchedules.Contracts;
using StudiePlusPlus.Application.Features.WeeklySchedules.Dtos;
using StudiePlusPlus.Domain.Scheduling;

namespace StudiePlusPlus.API.Controllers;

[ApiController, Route("api/[controller]")]
public class WeeklyScheduleController : CrudController<WeeklySchedule, Guid, WeeklyScheduleDto, CreateWeeklyScheduleRequest, UpdateWeeklyScheduleRequest>
{
    public WeeklyScheduleController(
        ReadHandler<WeeklySchedule, Guid, WeeklyScheduleDto> read,
        WriteHandler<WeeklySchedule, Guid, CreateWeeklyScheduleRequest, UpdateWeeklyScheduleRequest, WeeklyScheduleDto> write,
        ILoggerFactory loggerFactory)
        : base(read, write, loggerFactory)
    {
    }
}
