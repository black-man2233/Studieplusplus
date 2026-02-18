using System;
using Microsoft.EntityFrameworkCore;
using StudiePlusPlus.Application.Abstractions.Persistence;
using StudiePlusPlus.Domain.Scheduling;
using StudiePlusPlus.Domain.Teachers;

namespace StudiePlusPlus.Infrastructure.Persistence.Repositories;

public class WeeklyScheduleRepository : Repository<WeeklySchedule, Guid>, IWeeklyScheduleRepository
{
    public WeeklyScheduleRepository(AppDbContext db) : base(db)
    {
    }
}