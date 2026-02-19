using System;
using StudiePlusPlus.Domain.Academics;

namespace StudiePlusPlus.Application.Abstractions.Persistence;

public interface IClassRepository : IRepository<Class, Guid>
{
    
}