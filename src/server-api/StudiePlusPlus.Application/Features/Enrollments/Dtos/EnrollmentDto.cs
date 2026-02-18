using System;

namespace StudiePlusPlus.Application.Features.Enrollments.Dtos;

public sealed record EnrollmentDto(Guid Id, Guid StudentId, Guid ClassId);
