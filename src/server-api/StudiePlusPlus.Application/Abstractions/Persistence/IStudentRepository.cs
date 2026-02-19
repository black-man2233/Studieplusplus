using System;
using System.Threading;
using System.Threading.Tasks;
using StudiePlusPlus.Domain.Students;

namespace StudiePlusPlus.Application.Abstractions.Persistence;

public interface IStudentRepository : IRepository<Student, Guid>
{
    // Task<Student> GetByUserIdAsync(Guid userId, CancellationToken ct = default);
}