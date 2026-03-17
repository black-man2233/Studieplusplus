using System;
using Microsoft.Extensions.Logging;
using StudiePlusPlus.Application.Abstractions.Persistence;
using StudiePlusPlus.Domain.Scheduling;

namespace StudiePlusPlus.Infrastructure.Persistence.Repositories;

public class WeeklyScheduleRepository : Repository<WeeklySchedule, Guid>, IWeeklyScheduleRepository
{
    public WeeklyScheduleRepository(AppDbContext db, ILoggerFactory loggerFactory) : base(db, loggerFactory)
    {
    }
}
