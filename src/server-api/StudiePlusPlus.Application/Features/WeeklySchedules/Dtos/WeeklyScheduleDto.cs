using System;
using StudiePlusPlus.Domain.Common.Enums;

namespace StudiePlusPlus.Application.Features.WeeklySchedules.Dtos;

public sealed record WeeklyScheduleDto(
    Guid Id,
    Guid ClassGroupId,
    Guid StudentId,
    Guid TeacherId,
    DayOfTheWeek DayOfTheWeek,
    DateTime StartTime,
    DateTime EndTime
);
