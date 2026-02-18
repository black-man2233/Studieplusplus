using System;
using StudiePlusPlus.Domain.Common;

namespace StudiePlusPlus.Domain.Students;

public sealed class Enrollment : Entity<Guid>
{
    public Guid StudentId { get; private set; }
    public Guid ClassId { get; private set; }

    private Enrollment() { } 

    public Enrollment(Guid id, Guid studentId, Guid classId)
    {
        Id = id;
        StudentId = studentId;
        ClassId = classId;
    }

    public void Update(Guid studentId, Guid classId)
    {
        StudentId = studentId;
        ClassId = classId;
    }
}