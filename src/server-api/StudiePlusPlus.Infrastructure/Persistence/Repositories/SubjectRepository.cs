using System;
using Microsoft.EntityFrameworkCore;
using StudiePlusPlus.Application.Abstractions.Persistence;
using StudiePlusPlus.Domain.Academics;
using StudiePlusPlus.Domain.Teachers;

namespace StudiePlusPlus.Infrastructure.Persistence.Repositories;

public class SubjectRepository : Repository<Subject, Guid>, ISubjectRepository
{
    public SubjectRepository(AppDbContext db) : base(db)
    {
    }
}