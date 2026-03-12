using System;
using StudiePlusPlus.Domain.Common.Enums;

namespace StudiePlusPlus.Application.Features.WeeklySchedules.Contracts;

public sealed record CreateWeeklyScheduleRequest(
    Guid ClassId,
    Guid StudentId,
    Guid TeacherId,
    DayOfTheWeek DayOfTheWeek,
    DateTime StartTime,
    DateTime EndTime
);

public sealed record UpdateWeeklyScheduleRequest(
    Guid ClassId,
    Guid StudentId,
    Guid TeacherId,
    DayOfTheWeek DayOfTheWeek,
    DateTime StartTime,
    DateTime EndTime
);
