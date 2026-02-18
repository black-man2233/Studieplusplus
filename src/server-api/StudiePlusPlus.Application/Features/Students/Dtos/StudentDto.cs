using System;

namespace StudiePlusPlus.Application.Features.Students.Dtos;

public sealed record StudentDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Email
);