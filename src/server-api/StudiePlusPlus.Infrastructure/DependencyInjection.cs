using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudiePlusPlus.Application.Abstractions.Mapping;
using StudiePlusPlus.Application.Abstractions.Persistence;
using StudiePlusPlus.Application.Common.Handlers;
using StudiePlusPlus.Application.Features.Class.Contracts;
using StudiePlusPlus.Application.Features.Class.Dtos;
using StudiePlusPlus.Application.Features.Class.Mapping;
using StudiePlusPlus.Application.Features.Enrollments.Contracts;
using StudiePlusPlus.Application.Features.Enrollments.Dtos;
using StudiePlusPlus.Application.Features.Enrollments.Mapping;
using StudiePlusPlus.Application.Features.Grades.Contracts;
using StudiePlusPlus.Application.Features.Grades.Dtos;
using StudiePlusPlus.Application.Features.Grades.Mapping;
using StudiePlusPlus.Application.Features.Students.Contracts;
using StudiePlusPlus.Application.Features.Students.Dtos;
using StudiePlusPlus.Application.Features.Students.Mapping;
using StudiePlusPlus.Application.Features.Subjects.Contracts;
using StudiePlusPlus.Application.Features.Subjects.Dtos;
using StudiePlusPlus.Application.Features.Subjects.Mapping;
using StudiePlusPlus.Application.Features.Teachers.Contracts;
using StudiePlusPlus.Application.Features.Teachers.Dtos;
using StudiePlusPlus.Application.Features.Teachers.Mapping;
using StudiePlusPlus.Application.Features.WeeklySchedules.Contracts;
using StudiePlusPlus.Application.Features.WeeklySchedules.Dtos;
using StudiePlusPlus.Application.Features.WeeklySchedules.Mapping;
using StudiePlusPlus.Domain.Academics;
using StudiePlusPlus.Domain.Scheduling;
using StudiePlusPlus.Domain.Students;
using StudiePlusPlus.Domain.Teachers;
using StudiePlusPlus.Infrastructure.Persistence;
using StudiePlusPlus.Infrastructure.Persistence.Repositories;

namespace StudiePlusPlus.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString("Default"),
                sqlOptions => sqlOptions.EnableRetryOnFailure());
        });

        // Generic repository
        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

        // Specific repositories
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<ITeacherRepository, TeacherRepository>();
        services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
        services.AddScoped<IGradeRepository, GradeRepository>();
        services.AddScoped<ISubjectRepository, SubjectRepository>();
        services.AddScoped<IWeeklyScheduleRepository, WeeklyScheduleRepository>();
        services.AddScoped<IClassRepository, ClassRepository>();
        
        // Mappers
        services.AddScoped<IMapper<Student, StudentDto>, StudentDtoMapper>();
        services.AddScoped<IMapper<CreateStudentRequest, Student>, CreateStudentRequestMapper>();
        services.AddScoped<IMapper<UpdateStudentRequest, Student>, UpdateStudentRequestMapper>();

        services.AddScoped<IMapper<Teacher, TeacherDto>, TeacherDtoMapper>();
        services.AddScoped<IMapper<CreateTeacherRequest, Teacher>, CreateTeacherRequestMapper>();
        services.AddScoped<IMapper<UpdateTeacherRequest, Teacher>, UpdateTeacherRequestMapper>();

        services.AddScoped<IMapper<Class, ClassDto>, ClassDtoMapper>();
        services.AddScoped<IMapper<CreateClassRequest, Class>, CreateClassRequestMapper>();
        services.AddScoped<IMapper<UpdateClassRequest, Class>, UpdateClassRequestMapper>();

        services.AddScoped<IMapper<Subject, SubjectDto>, SubjectDtoMapper>();
        services.AddScoped<IMapper<CreateSubjectRequest, Subject>, CreateSubjectRequestMapper>();
        services.AddScoped<IMapper<UpdateSubjectRequest, Subject>, UpdateSubjectRequestMapper>();

        services.AddScoped<IMapper<Enrollment, EnrollmentDto>, EnrollmentDtoMapper>();
        services.AddScoped<IMapper<CreateEnrollmentRequest, Enrollment>, CreateEnrollmentRequestMapper>();
        services.AddScoped<IMapper<UpdateEnrollmentRequest, Enrollment>, UpdateEnrollmentRequestMapper>();

        services.AddScoped<IMapper<Grade, GradeDto>, GradeDtoMapper>();
        services.AddScoped<IMapper<CreateGradeRequest, Grade>, CreateGradeRequestMapper>();
        services.AddScoped<IMapper<UpdateGradeRequest, Grade>, UpdateGradeRequestMapper>();

        services.AddScoped<IMapper<WeeklySchedule, WeeklyScheduleDto>, WeeklyScheduleDtoMapper>();
        services.AddScoped<IMapper<CreateWeeklyScheduleRequest, WeeklySchedule>, CreateWeeklyScheduleRequestMapper>();
        services.AddScoped<IMapper<UpdateWeeklyScheduleRequest, WeeklySchedule>, UpdateWeeklyScheduleRequestMapper>();

        // Generic Handlers
        services.AddScoped<ReadHandler<Student, Guid, StudentDto>>();
        services.AddScoped<WriteHandler<Student, Guid, CreateStudentRequest, UpdateStudentRequest, StudentDto>>();

        services.AddScoped<ReadHandler<Teacher, Guid, TeacherDto>>();
        services.AddScoped<WriteHandler<Teacher, Guid, CreateTeacherRequest, UpdateTeacherRequest, TeacherDto>>();

        services.AddScoped<ReadHandler<Class, Guid, ClassDto>>();
        services.AddScoped<WriteHandler<Class, Guid, CreateClassRequest, UpdateClassRequest, ClassDto>>();

        services.AddScoped<ReadHandler<Subject, Guid, SubjectDto>>();
        services.AddScoped<WriteHandler<Subject, Guid, CreateSubjectRequest, UpdateSubjectRequest, SubjectDto>>();

        services.AddScoped<ReadHandler<Enrollment, Guid, EnrollmentDto>>();
        services.AddScoped<WriteHandler<Enrollment, Guid, CreateEnrollmentRequest, UpdateEnrollmentRequest, EnrollmentDto>>();

        services.AddScoped<ReadHandler<Grade, Guid, GradeDto>>();
        services.AddScoped<WriteHandler<Grade, Guid, CreateGradeRequest, UpdateGradeRequest, GradeDto>>();

        services.AddScoped<ReadHandler<WeeklySchedule, Guid, WeeklyScheduleDto>>();
        services.AddScoped<WriteHandler<WeeklySchedule, Guid, CreateWeeklyScheduleRequest, UpdateWeeklyScheduleRequest, WeeklyScheduleDto>>();
        
        return services;
    }
}
