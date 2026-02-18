using System;
using StudiePlusPlus.Domain.Students;

namespace StudiePlusPlus.Application.Abstractions.Persistence;

public interface IEnrollmentRepository : IRepository<Enrollment, Guid>
{
    
}