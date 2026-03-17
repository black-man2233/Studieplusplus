using System;
using Microsoft.Extensions.Logging;
using StudiePlusPlus.Application.Abstractions.Persistence;
using StudiePlusPlus.Domain.Teachers;

namespace StudiePlusPlus.Infrastructure.Persistence.Repositories;

public class TeacherRepository : Repository<Teacher, Guid>, ITeacherRepository
{
    public TeacherRepository(AppDbContext db, ILoggerFactory loggerFactory) : base(db, loggerFactory)
    {
    }
}
