using System;
using Microsoft.Extensions.Logging;
using StudiePlusPlus.Application.Abstractions.Persistence;
using StudiePlusPlus.Domain.Academics;

namespace StudiePlusPlus.Infrastructure.Persistence.Repositories;

public class SubjectRepository : Repository<Subject, Guid>, ISubjectRepository
{
    public SubjectRepository(AppDbContext db, ILoggerFactory loggerFactory) : base(db, loggerFactory)
    {
    }
}
