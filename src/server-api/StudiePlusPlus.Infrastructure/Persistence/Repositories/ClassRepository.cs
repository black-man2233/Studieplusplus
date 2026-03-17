using System;
using Microsoft.Extensions.Logging;
using StudiePlusPlus.Application.Abstractions.Persistence;
using StudiePlusPlus.Domain.Academics;

namespace StudiePlusPlus.Infrastructure.Persistence.Repositories;

public class ClassRepository : Repository<Class, Guid>, IClassRepository
{
    public ClassRepository(AppDbContext db, ILoggerFactory loggerFactory) : base(db, loggerFactory)
    {
    }
}
