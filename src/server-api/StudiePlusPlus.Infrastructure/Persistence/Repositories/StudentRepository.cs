using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudiePlusPlus.Application.Abstractions.Persistence;
using StudiePlusPlus.Domain.Students;

namespace StudiePlusPlus.Infrastructure.Persistence.Repositories;

public sealed class StudentRepository : Repository<Student, Guid>, IStudentRepository
{
    private readonly AppDbContext _db;

    public StudentRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }

    public Task<Student> GetByUserIdAsync(Guid id, CancellationToken ct = default) =>
        _db.Students.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id, ct);
}