using System;
using StudiePlusPlus.Application.Common.Handlers;
using StudiePlusPlus.Application.Features.Students.Contracts;
using StudiePlusPlus.Application.Features.Students.Dtos;
using StudiePlusPlus.Application.Features.Subjects.Contracts;
using StudiePlusPlus.Application.Features.Subjects.Dtos;

namespace StudiePlusPlus.API.Controllers;

public class SubjectsController  : CrudController<SubjectsController, Guid, SubjectDto, CreateSubjectRequest, UpdateSubjectRequest>
{
    public SubjectsController(ReadHandler<SubjectsController, Guid, SubjectDto> read, WriteHandler<SubjectsController, Guid, CreateSubjectRequest, UpdateSubjectRequest, SubjectDto> write) : base(read, write)
    {
    }
}