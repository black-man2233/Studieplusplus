using System;
using StudiePlusPlus.Application.Common.Mapping;
using StudiePlusPlus.Application.Features.WeeklySchedules.Contracts;
using StudiePlusPlus.Application.Features.WeeklySchedules.Dtos;
using StudiePlusPlus.Domain.Scheduling;

namespace StudiePlusPlus.Application.Features.WeeklySchedules.Mapping;

public sealed class WeeklyScheduleDtoMapper : BaseMapper<WeeklySchedule, WeeklyScheduleDto>
{
    public override WeeklyScheduleDto Map(WeeklySchedule source) => new(
        source.Id,
        // source.ClassId,
        source.StudentId,
        source.TeacherId,
        source.DayOfTheWeek,
        source.StartTime,
        source.EndTime);

    public override void Update(WeeklySchedule source, WeeklyScheduleDto destination) { }
}

public sealed class CreateWeeklyScheduleRequestMapper : BaseMapper<CreateWeeklyScheduleRequest, WeeklySchedule>
{
    public override WeeklySchedule Map(CreateWeeklyScheduleRequest source) => new(
        // source.ClassId,
        source.StudentId,
        source.TeacherId,
        source.DayOfTheWeek,
        source.StartTime,
        source.EndTime);

    public override void Update(CreateWeeklyScheduleRequest source, WeeklySchedule destination) { }
}

public sealed class UpdateWeeklyScheduleRequestMapper : BaseMapper<UpdateWeeklyScheduleRequest, WeeklySchedule>
{
    public override WeeklySchedule Map(UpdateWeeklyScheduleRequest source) => new(
        // source.ClassId,
        source.StudentId,
        source.TeacherId,
        source.DayOfTheWeek,
        source.StartTime,
        source.EndTime);

    public override void Update(UpdateWeeklyScheduleRequest source, WeeklySchedule destination)
    {
        // destination.ClassId = source.ClassId;
        destination.StudentId = source.StudentId;
        destination.TeacherId = source.TeacherId;
        destination.DayOfTheWeek = source.DayOfTheWeek;
        destination.StartTime = source.StartTime;
        destination.EndTime = source.EndTime;
    }
}
