using System;

namespace StudiePlusPlus.Application.Features.Subjects.Contracts;

public sealed record CreateSubjectRequest(string Name);
public sealed record UpdateSubjectRequest(string Name);
