using System;
using Microsoft.EntityFrameworkCore;
using StudiePlusPlus.Application.Abstractions.Persistence;
using StudiePlusPlus.Domain.Academics;

namespace StudiePlusPlus.Infrastructure.Persistence.Repositories;

public class ClassRepository : Repository<Class, Guid>, IClassRepository
{
    public ClassRepository(AppDbContext db) : base(db)
    {
    }
}