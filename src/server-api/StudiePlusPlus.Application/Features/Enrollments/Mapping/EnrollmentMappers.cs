using System;
using StudiePlusPlus.Application.Common.Mapping;
using StudiePlusPlus.Application.Features.Enrollments.Contracts;
using StudiePlusPlus.Application.Features.Enrollments.Dtos;
using StudiePlusPlus.Domain.Students;

namespace StudiePlusPlus.Application.Features.Enrollments.Mapping;

public sealed class EnrollmentDtoMapper : BaseMapper<Enrollment, EnrollmentDto>
{
    public override EnrollmentDto Map(Enrollment source) => new(source.Id, source.StudentId, source.ClassId);
    public override void Update(Enrollment source, EnrollmentDto destination) { }
}

public sealed class CreateEnrollmentRequestMapper : BaseMapper<CreateEnrollmentRequest, Enrollment>
{
    public override Enrollment Map(CreateEnrollmentRequest source) => new(Guid.NewGuid(), source.StudentId, source.ClassId);
    public override void Update(CreateEnrollmentRequest source, Enrollment destination) { }
}

public sealed class UpdateEnrollmentRequestMapper : BaseMapper<UpdateEnrollmentRequest, Enrollment>
{
    public override Enrollment Map(UpdateEnrollmentRequest source) => new(Guid.NewGuid(), source.StudentId, source.ClassId);
    public override void Update(UpdateEnrollmentRequest source, Enrollment destination)
    {
        destination.Update(source.StudentId, source.ClassId);
    }
}
