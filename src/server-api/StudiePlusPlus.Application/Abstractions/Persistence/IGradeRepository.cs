using System;
using StudiePlusPlus.Domain.Students;

namespace StudiePlusPlus.Application.Abstractions.Persistence;

public interface IGradeRepository : IRepository<Grade, Guid>
{
    
}