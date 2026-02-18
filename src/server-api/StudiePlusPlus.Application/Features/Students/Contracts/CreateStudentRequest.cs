using System;

namespace StudiePlusPlus.Application.Features.Students.Contracts;

public sealed record CreateStudentRequest(
    string FirstName,
    string LastName,
    string Email,
    Guid LoginId
);
