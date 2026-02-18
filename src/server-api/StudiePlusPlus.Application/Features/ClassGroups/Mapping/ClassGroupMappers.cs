using System;
using StudiePlusPlus.Application.Common.Mapping;
using StudiePlusPlus.Application.Features.ClassGroups.Contracts;
using StudiePlusPlus.Application.Features.ClassGroups.Dtos;
using StudiePlusPlus.Domain.Academics;

namespace StudiePlusPlus.Application.Features.ClassGroups.Mapping;

public sealed class ClassGroupDtoMapper : BaseMapper<ClassGroup, ClassGroupDto>
{
    public override ClassGroupDto Map(ClassGroup source) => new(source.Id, source.Name);
    public override void Update(ClassGroup source, ClassGroupDto destination) { }
}

public sealed class CreateClassGroupRequestMapper : BaseMapper<CreateClassGroupRequest, ClassGroup>
{
    public override ClassGroup Map(CreateClassGroupRequest source) => new(Guid.NewGuid(), source.Name);
    public override void Update(CreateClassGroupRequest source, ClassGroup destination) { }
}

public sealed class UpdateClassGroupRequestMapper : BaseMapper<UpdateClassGroupRequest, ClassGroup>
{
    public override ClassGroup Map(UpdateClassGroupRequest source) => new(Guid.NewGuid(), source.Name);
    public override void Update(UpdateClassGroupRequest source, ClassGroup destination)
    {
        destination.Name = source.Name;
    }
}
