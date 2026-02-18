using System;
using StudiePlusPlus.Application.Common.Mapping;
using StudiePlusPlus.Application.Features.Subjects.Contracts;
using StudiePlusPlus.Application.Features.Subjects.Dtos;
using StudiePlusPlus.Domain.Academics;

namespace StudiePlusPlus.Application.Features.Subjects.Mapping;

public sealed class SubjectDtoMapper : BaseMapper<Subject, SubjectDto>
{
    public override SubjectDto Map(Subject source) => new(source.Id, source.Name);
    public override void Update(Subject source, SubjectDto destination) { }
}

public sealed class CreateSubjectRequestMapper : BaseMapper<CreateSubjectRequest, Subject>
{
    public override Subject Map(CreateSubjectRequest source) => new(Guid.NewGuid(), source.Name);
    public override void Update(CreateSubjectRequest source, Subject destination) { }
}

public sealed class UpdateSubjectRequestMapper : BaseMapper<UpdateSubjectRequest, Subject>
{
    public override Subject Map(UpdateSubjectRequest source) => new(Guid.NewGuid(), source.Name);
    public override void Update(UpdateSubjectRequest source, Subject destination)
    {
        destination.Name = source.Name;
    }
}
