using System;

namespace StudiePlusPlus.Application.Features.ClassGroups.Contracts;

public sealed record CreateClassGroupRequest(string Name);
public sealed record UpdateClassGroupRequest(string Name);
