using System;
using StudiePlusPlus.Application.Common.Mapping;
using StudiePlusPlus.Application.Features.Teachers.Contracts;
using StudiePlusPlus.Application.Features.Teachers.Dtos;
using StudiePlusPlus.Domain.Teachers;
using StudiePlusPlus.Domain.ValueObjects;

namespace StudiePlusPlus.Application.Features.Teachers.Mapping;

public sealed class TeacherDtoMapper : BaseMapper<Teacher, TeacherDto>
{
    public override TeacherDto Map(Teacher source)
    {
        return new TeacherDto(
            source.Id,
            source.FirstName,
            source.LastName,
            source.Email.Value,
            source.Specializations);
    }

    public override void Update(Teacher source, TeacherDto destination)
    {
    }
}

public sealed class CreateTeacherRequestMapper : BaseMapper<CreateTeacherRequest, Teacher>
{
    public override Teacher Map(CreateTeacherRequest source)
    {
        return new Teacher(
            Guid.NewGuid(),
            source.FirstName,
            source.LastName,
            new Email(source.Email),
            source.LoginId,
            source.Specializations);
    }

    public override void Update(CreateTeacherRequest source, Teacher destination)
    {
    }
}

public sealed class UpdateTeacherRequestMapper : BaseMapper<UpdateTeacherRequest, Teacher>
{
    public override Teacher Map(UpdateTeacherRequest source)
    {
        return new Teacher(
            Guid.NewGuid(),
            source.FirstName,
            source.LastName,
            new Email(source.Email),
            source.LoginId,
            source.Specializations);
    }

    public override void Update(UpdateTeacherRequest source, Teacher destination)
    {
        destination.Update(
            source.FirstName,
            source.LastName,
            new Email(source.Email),
            source.LoginId,
            source.Specializations);
    }
}
