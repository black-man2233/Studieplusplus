using System;
using StudiePlusPlus.Domain.Common;

namespace StudiePlusPlus.Domain.Students;

public sealed class Student : Entity<Guid>
{
    public Guid UserId { get; private set; }

    private Student() { }

    public Student(Guid id, Guid userId)
    {
        Id = id;
        UserId = userId;
    }
}