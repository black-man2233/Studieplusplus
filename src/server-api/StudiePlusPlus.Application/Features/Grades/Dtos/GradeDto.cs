using System;

namespace StudiePlusPlus.Application.Features.Grades.Dtos;

public sealed record GradeDto(Guid Id, Guid StudentId, Guid SubjectId, decimal Score, string Label);
