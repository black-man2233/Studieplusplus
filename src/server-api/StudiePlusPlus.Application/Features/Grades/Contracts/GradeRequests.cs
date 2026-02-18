using System;

namespace StudiePlusPlus.Application.Features.Grades.Contracts;

public sealed record CreateGradeRequest(Guid StudentId, Guid SubjectId, decimal Score, string Label);
public sealed record UpdateGradeRequest(Guid StudentId, Guid SubjectId, decimal Score, string Label);
