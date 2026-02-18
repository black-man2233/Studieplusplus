using System;
using StudiePlusPlus.Application.Common.Mapping;
using StudiePlusPlus.Application.Features.Students.Contracts;
using StudiePlusPlus.Domain.Students;
using StudiePlusPlus.Domain.ValueObjects;

namespace StudiePlusPlus.Application.Features.Students.Mapping;

public sealed class CreateStudentRequestMapper : BaseMapper<CreateStudentRequest, Student>
{
    public override Student Map(CreateStudentRequest source)
    {
        return new Student(
            Guid.Empty,
            source.FirstName,
            source.LastName,
            new Email(source.Email));
    }

    public override void Update(CreateStudentRequest source, Student destination)
    {
    }
}

public sealed class UpdateStudentRequestMapper : BaseMapper<UpdateStudentRequest, Student>
{
    public override Student Map(UpdateStudentRequest source)
    {
        return new Student(
            Guid.Empty,
            source.FirstName,
            source.LastName,
            new Email(source.Email));
    }

    public override void Update(UpdateStudentRequest source, Student destination)
    {
        destination.Update(
            source.FirstName,
            source.LastName,
            new Email(source.Email),
            source.LoginId);
    }
}
