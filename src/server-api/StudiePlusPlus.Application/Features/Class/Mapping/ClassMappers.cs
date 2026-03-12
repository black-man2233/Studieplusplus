using System;
using StudiePlusPlus.Application.Common.Mapping;
using StudiePlusPlus.Application.Features.Class.Contracts;
using StudiePlusPlus.Application.Features.Class.Dtos;

namespace StudiePlusPlus.Application.Features.Class.Mapping;

public sealed class ClassDtoMapper : BaseMapper<Domain.Academics.Class, ClassDto>
{
    public override ClassDto Map(Domain.Academics.Class source) => new(source.Id, source.Name);
    public override void Update(Domain.Academics.Class source, ClassDto destination) { }
}

public sealed class CreateClassRequestMapper : BaseMapper<CreateClassRequest, Domain.Academics.Class>
{
    public override Domain.Academics.Class Map(CreateClassRequest source) => new(Guid.NewGuid(), source.Name);
    public override void Update(CreateClassRequest source, Domain.Academics.Class destination) { }
}

public sealed class UpdateClassRequestMapper : BaseMapper<UpdateClassRequest, Domain.Academics.Class>
{
    public override Domain.Academics.Class Map(UpdateClassRequest source) => new(Guid.NewGuid(), source.Name);
    public override void Update(UpdateClassRequest source, Domain.Academics.Class destination)
    {
        destination.Name = source.Name;
    }
}
