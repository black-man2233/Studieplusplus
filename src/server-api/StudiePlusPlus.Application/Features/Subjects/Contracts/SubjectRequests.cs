using System;

namespace StudiePlusPlus.Application.Features.Subjects.Contracts;

public sealed record CreateSubjectRequest(string Name);

public sealed record UpdateSubjectRequest(Guid id, UpdateSubject updateObject);

public class UpdateSubject
{
    public string Name { get; set; }
}