using System;
using System.Collections.Generic;

namespace StudiePlusPlus.Application.Features.Teachers.Contracts;

public sealed record CreateTeacherRequest(
    string FirstName,
    string LastName,
    string Email,
    Guid LoginId,
    IEnumerable<string> Specializations
);

public sealed record UpdateTeacherRequest(
    string FirstName,
    string LastName,
    string Email,
    Guid LoginId,
    IEnumerable<string> Specializations
);
