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

    public Grade(Guid id, Guid studentId, Guid subjectId, decimal score, string label)
    {
        Id = id;
        StudentId = studentId;
        SubjectId = subjectId;
        Score = score;
        Label = label;
    }

    public void Update(Guid studentId, Guid subjectId, decimal score, string label)
    {
        StudentId = studentId;
        SubjectId = subjectId;
        Score = score;
        Label = label;
    }
}