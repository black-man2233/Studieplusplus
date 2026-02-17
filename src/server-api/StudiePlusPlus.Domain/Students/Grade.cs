using System;
using StudiePlusPlus.Domain.Common;

namespace StudiePlusPlus.Domain.Students;

public class Grade : Entity<Guid>
{
    public Guid StudentId { get; private set; }

    public Guid SubjectId { get; private set; }

    public decimal Score { get; set; }

    public string Label { get; set; }

    public Grade()
    {
    }

    public Grade(Guid studentId, Guid subjectId)
    {
        StudentId = studentId;
        SubjectId = subjectId;
    }
}