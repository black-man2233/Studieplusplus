using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudiePlusPlus.Application.Abstractions.Persistence;
using StudiePlusPlus.Domain.Students;

namespace StudiePlusPlus.Infrastructure.Persistence.Repositories;

public sealed class StudentRepository : Repository<Student, Guid>, IStudentRepository
{
    private readonly AppDbContext _db;

    public StudentRepository(AppDbContext db, ILoggerFactory loggerFactory) : base(db, loggerFactory)
    {
        _db = db;
    }

    public Task<Student> GetByUserIdAsync(Guid id, CancellationToken ct = default)
    {
        Logger.LogDebug("DB GetByUserId Student id={Id}", id);
        return _db.Students.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id, ct);
    }
}
