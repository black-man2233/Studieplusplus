using System;
using Microsoft.EntityFrameworkCore;
using StudiePlusPlus.Application.Abstractions.Persistence;
using StudiePlusPlus.Domain.Students;
using StudiePlusPlus.Domain.Teachers;

namespace StudiePlusPlus.Infrastructure.Persistence.Repositories;

public class TeacherRepository : Repository<Teacher, Guid>, ITeacherRepository
{
    public TeacherRepository(AppDbContext db) : base(db)
    {
    }
}