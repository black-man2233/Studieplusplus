using System;
using StudiePlusPlus.Application.Common.Mapping;
using StudiePlusPlus.Application.Features.Grades.Contracts;
using StudiePlusPlus.Application.Features.Grades.Dtos;
using StudiePlusPlus.Domain.Students;

namespace StudiePlusPlus.Application.Features.Grades.Mapping;

public sealed class GradeDtoMapper : BaseMapper<Grade, GradeDto>
{
    public override GradeDto Map(Grade source) => new(source.Id, source.StudentId, source.SubjectId, source.Score, source.Label);
    public override void Update(Grade source, GradeDto destination) { }
}

public sealed class CreateGradeRequestMapper : BaseMapper<CreateGradeRequest, Grade>
{
    public override Grade Map(CreateGradeRequest source) => new(Guid.NewGuid(), source.StudentId, source.SubjectId, source.Score, source.Label);
    public override void Update(CreateGradeRequest source, Grade destination) { }
}

public sealed class UpdateGradeRequestMapper : BaseMapper<UpdateGradeRequest, Grade>
{
    public override Grade Map(UpdateGradeRequest source) => new(Guid.NewGuid(), source.StudentId, source.SubjectId, source.Score, source.Label);
    public override void Update(UpdateGradeRequest source, Grade destination)
    {
        destination.Update(source.StudentId, source.SubjectId, source.Score, source.Label);
    }
}
