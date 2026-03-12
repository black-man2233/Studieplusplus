using System;
using System.Collections.Generic;

namespace StudiePlusPlus.Application.Features.Teachers.Dtos;

public sealed record TeacherDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    IEnumerable<string> Specializations
);
