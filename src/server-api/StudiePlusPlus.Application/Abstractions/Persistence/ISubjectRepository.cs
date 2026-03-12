using System;
using StudiePlusPlus.Domain.Academics;

namespace StudiePlusPlus.Application.Abstractions.Persistence;

public interface ISubjectRepository : IRepository<Subject, Guid>
{
    
}