using System;
using Microsoft.EntityFrameworkCore;
using StudiePlusPlus.Application.Abstractions.Persistence;
using StudiePlusPlus.Domain.Students;

namespace StudiePlusPlus.Infrastructure.Persistence.Repositories;

public class GradeRepository : Repository<Grade, Guid>, IGradeRepository
{
    public GradeRepository(AppDbContext db) : base(db)
    {
    }
}