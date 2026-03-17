using System;
using Microsoft.Extensions.Logging;
using StudiePlusPlus.Application.Abstractions.Persistence;
using StudiePlusPlus.Domain.Students;

namespace StudiePlusPlus.Infrastructure.Persistence.Repositories;

public class EnrollmentRepository : Repository<Enrollment, Guid>, IEnrollmentRepository
{
    public EnrollmentRepository(AppDbContext db, ILoggerFactory loggerFactory) : base(db, loggerFactory)
    {
    }
}
