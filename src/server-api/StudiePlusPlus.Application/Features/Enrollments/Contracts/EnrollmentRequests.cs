using System;

namespace StudiePlusPlus.Application.Features.Enrollments.Contracts;

public sealed record CreateEnrollmentRequest(Guid StudentId, Guid ClassId);
public sealed record UpdateEnrollmentRequest(Guid StudentId, Guid ClassId);
