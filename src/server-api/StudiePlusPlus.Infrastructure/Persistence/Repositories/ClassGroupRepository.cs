using System;
using Microsoft.EntityFrameworkCore;
using StudiePlusPlus.Application.Abstractions.Persistence;
using StudiePlusPlus.Domain.Academics;

namespace StudiePlusPlus.Infrastructure.Persistence.Repositories;

public class ClassGroupRepository : Repository<ClassGroup, Guid>, IClassGroupRepository
{
    public ClassGroupRepository(AppDbContext db) : base(db)
    {
    }
}