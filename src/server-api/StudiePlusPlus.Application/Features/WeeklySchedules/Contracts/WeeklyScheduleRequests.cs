using System;
using StudiePlusPlus.Domain.Common.Enums;

namespace StudiePlusPlus.Application.Features.WeeklySchedules.Contracts;

public sealed record CreateWeeklyScheduleRequest(
    Guid ClassGroupId,
    Guid StudentId,
    Guid TeacherId,
    DayOfTheWeek DayOfTheWeek,
    DateTime StartTime,
    DateTime EndTime
);

public sealed record UpdateWeeklyScheduleRequest(
    Guid ClassGroupId,
    Guid StudentId,
    Guid TeacherId,
    DayOfTheWeek DayOfTheWeek,
    DateTime StartTime,
    DateTime EndTime
);
