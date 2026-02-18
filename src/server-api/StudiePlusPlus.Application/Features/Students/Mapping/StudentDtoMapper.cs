using StudiePlusPlus.Application.Common.Mapping;
using StudiePlusPlus.Application.Features.Students.Dtos;
using StudiePlusPlus.Domain.Students;

namespace StudiePlusPlus.Application.Features.Students.Mapping;

public sealed class StudentDtoMapper : BaseMapper<Student, StudentDto>
{
    public override StudentDto Map(Student source)
    {
        return new StudentDto(
            source.Id,
            source.FirstName,
            source.LastName,
            source.Email.Value);
    }

    public override void Update(Student source, StudentDto destination)
    {
        // Not typically needed for DTOs
    }
}
