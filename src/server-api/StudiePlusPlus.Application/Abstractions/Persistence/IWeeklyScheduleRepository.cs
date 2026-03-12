using System;
using StudiePlusPlus.Domain.Scheduling;

namespace StudiePlusPlus.Application.Abstractions.Persistence;

public interface IWeeklyScheduleRepository: IRepository<WeeklySchedule, Guid>
{
    
}