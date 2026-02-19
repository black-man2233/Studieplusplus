using System;
using StudiePlusPlus.Application.Common.Handlers;
using StudiePlusPlus.Application.Features.Teachers.Contracts;
using StudiePlusPlus.Application.Features.Teachers.Dtos;
using StudiePlusPlus.Application.Features.WeeklySchedules.Contracts;
using StudiePlusPlus.Application.Features.WeeklySchedules.Dtos;
using StudiePlusPlus.Domain.Scheduling;
using StudiePlusPlus.Domain.Teachers;

namespace StudiePlusPlus.API.Controllers;

public class WeeklyScheduleController : CrudController<WeeklySchedule, Guid, WeeklyScheduleDto, CreateWeeklyScheduleRequest, UpdateWeeklyScheduleRequest>
{
    public WeeklyScheduleController(ReadHandler<WeeklySchedule, Guid, WeeklyScheduleDto> read, WriteHandler<WeeklySchedule, Guid, CreateWeeklyScheduleRequest, UpdateWeeklyScheduleRequest, WeeklyScheduleDto> write) : base(read, write)
    {
    }
}