using System;
using StudiePlusPlus.Domain.Teachers;

namespace StudiePlusPlus.Application.Abstractions.Persistence;

public interface ITeacherRepository : IRepository<Teacher, Guid>
{
    
}