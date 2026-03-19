using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using Microsoft.Extensions.Logging;
using StudiePlusPlus.Domain.Academics;
using StudiePlusPlus.Domain.Auth;
using StudiePlusPlus.Domain.Common.Enums;
using StudiePlusPlus.Domain.Messaging;
using StudiePlusPlus.Domain.Scheduling;
using StudiePlusPlus.Domain.Students;
using StudiePlusPlus.Domain.Teachers;
using StudiePlusPlus.Domain.Users;
using StudiePlusPlus.Domain.ValueObjects;

namespace StudiePlusPlus.Infrastructure.Persistence.Seeding;

public static class FakeDataSeeder
{
    public static void SeedIfEmpty(AppDbContext context, ILogger logger)
    {
        if (context.Users.Any() || context.Classes.Any() || context.Subjects.Any())
        {
            logger.LogInformation("Skipping fake data seeding because data already exists.");
            return;
        }

        Randomizer.Seed = new Random(20260319);
        var faker = new Faker("en");

        var classes = CreateClasses();
        var subjects = CreateSubjects();
        var teachers = CreateTeachers();
        var students = CreateStudents();

        var enrollments = CreateEnrollments(students, classes, faker);
        var grades = CreateGrades(students, subjects, faker);
        var schedules = CreateWeeklySchedules(classes, teachers, enrollments, faker);
        var messages = CreateMessages(students, teachers, faker);
        var logins = CreateLogins(students, teachers);

        context.Classes.AddRange(classes);
        context.Subjects.AddRange(subjects);
        context.Teachers.AddRange(teachers);
        context.Students.AddRange(students);
        context.Enrollments.AddRange(enrollments);
        context.Grades.AddRange(grades);
        context.WeeklySchedules.AddRange(schedules);
        context.Messages.AddRange(messages);
        context.Logins.AddRange(logins);
        context.SaveChanges();

        logger.LogInformation(
            "Seeded fake data: {Students} students, {Teachers} teachers, {Classes} classes, {Subjects} subjects, {Enrollments} enrollments, {Grades} grades, {Schedules} schedules, {Messages} messages.",
            students.Count,
            teachers.Count,
            classes.Count,
            subjects.Count,
            enrollments.Count,
            grades.Count,
            schedules.Count,
            messages.Count);
    }

    private static List<Class> CreateClasses()
    {
        return new List<Class>
        {
            new(Guid.NewGuid(), "1A"),
            new(Guid.NewGuid(), "1B"),
            new(Guid.NewGuid(), "1C"),
            new(Guid.NewGuid(), "2A"),
            new(Guid.NewGuid(), "2B"),
            new(Guid.NewGuid(), "3A"),
            new(Guid.NewGuid(), "3B"),
            new(Guid.NewGuid(), "3C"),
        };
    }

    private static List<Subject> CreateSubjects()
    {
        return new List<Subject>
        {
            new(Guid.NewGuid(), "Dansk"),
            new(Guid.NewGuid(), "Matematik"),
            new(Guid.NewGuid(), "Engelsk"),
            new(Guid.NewGuid(), "Historie"),
            new(Guid.NewGuid(), "Biologi"),
            new(Guid.NewGuid(), "Fysik"),
            new(Guid.NewGuid(), "Idraet"),
            new(Guid.NewGuid(), "Samfundsfag"),
        };
    }

    private static List<Teacher> CreateTeachers()
    {
        var faker = new Faker("en");
        var specializations = new[]
        {
            "Dansk",
            "Matematik",
            "Engelsk",
            "Historie",
            "Biologi",
            "Fysik",
            "Idraet",
            "Samfundsfag",
        };

        var teachers = new List<Teacher>();
        for (var i = 0; i < 12; i++)
        {
            var firstName = faker.Name.FirstName();
            var lastName = faker.Name.LastName();
            var email = BuildSchoolEmail(firstName, lastName, i, "laerer");
            var teacherSpecializations = faker.PickRandom(specializations, faker.Random.Int(1, 3)).Distinct().ToArray();

            teachers.Add(new Teacher(
                Guid.NewGuid(),
                firstName,
                lastName,
                new Email(email),
                Guid.Empty,
                teacherSpecializations));
        }

        return teachers;
    }

    private static List<Student> CreateStudents()
    {
        var faker = new Faker("en");
        var students = new List<Student>();

        for (var i = 0; i < 96; i++)
        {
            var firstName = faker.Name.FirstName();
            var lastName = faker.Name.LastName();
            var email = BuildSchoolEmail(firstName, lastName, i, "elev");

            students.Add(new Student(
                Guid.NewGuid(),
                firstName,
                lastName,
                new Email(email)));
        }

        return students;
    }

    private static List<Enrollment> CreateEnrollments(IReadOnlyList<Student> students, IReadOnlyList<Class> classes, Faker faker)
    {
        var enrollments = new List<Enrollment>(students.Count);
        var classList = classes.ToList();

        foreach (var student in students)
        {
            var assignedClass = faker.Random.ListItem(classList);
            enrollments.Add(new Enrollment(Guid.NewGuid(), student.Id, assignedClass.Id));
        }

        return enrollments;
    }

    private static List<Grade> CreateGrades(IReadOnlyList<Student> students, IReadOnlyList<Subject> subjects, Faker faker)
    {
        var grades = new List<Grade>();

        foreach (var student in students)
        {
            var subjectCount = faker.Random.Int(4, 7);
            var selectedSubjects = faker.Random.Shuffle(subjects).Take(subjectCount);

            foreach (var subject in selectedSubjects)
            {
                var score = Math.Round(faker.Random.Decimal(2.0m, 12.0m), 2);
                var label = ToGradeLabel(score);

                grades.Add(new Grade(Guid.NewGuid(), student.Id, subject.Id, score, label));
            }
        }

        return grades;
    }

    private static List<WeeklySchedule> CreateWeeklySchedules(
        IReadOnlyList<Class> classes,
        IReadOnlyList<Teacher> teachers,
        IReadOnlyList<Enrollment> enrollments,
        Faker faker)
    {
        var enrollmentsByClass = enrollments
            .GroupBy(x => x.ClassId)
            .ToDictionary(g => g.Key, g => g.Select(x => x.StudentId).ToList());

        var schedules = new List<WeeklySchedule>();
        var teacherList = teachers.ToList();

        foreach (var currentClass in classes)
        {
            if (!enrollmentsByClass.TryGetValue(currentClass.Id, out var classStudentIds) || classStudentIds.Count == 0)
            {
                continue;
            }

            for (var day = DayOfTheWeek.Monday; day <= DayOfTheWeek.Friday; day++)
            {
                var studentId = faker.Random.ListItem(classStudentIds);
                var teacherId = faker.Random.ListItem(teacherList).Id;
                var startHour = faker.Random.Int(8, 14);
                var startTime = DateTime.UtcNow.Date.AddHours(startHour);
                var endTime = startTime.AddMinutes(90);

                var schedule = new WeeklySchedule(studentId, teacherId, day, startTime, endTime)
                {
                    Class = currentClass,
                };

                schedules.Add(schedule);
            }
        }

        return schedules;
    }

    private static List<Message> CreateMessages(
        IReadOnlyList<Student> students,
        IReadOnlyList<Teacher> teachers,
        Faker faker)
    {
        var messages = new List<Message>();
        var users = students.Cast<User>().Concat(teachers).ToList();
        var messageCount = 240;

        for (var i = 0; i < messageCount; i++)
        {
            var sender = faker.Random.ListItem(users);
            var receiver = faker.Random.ListItem(users.Where(x => x.Id != sender.Id).ToList());
            var content = faker.Lorem.Sentence(faker.Random.Int(8, 20));

            messages.Add(new Message(Guid.NewGuid(), sender.Id, receiver.Id, content));
        }

        return messages;
    }

    private static List<Login> CreateLogins(IReadOnlyList<Student> students, IReadOnlyList<Teacher> teachers)
    {
        var logins = new List<Login>(students.Count + teachers.Count);

        foreach (var student in students)
        {
            var loginId = Guid.NewGuid();
            student.Update(student.FirstName, student.LastName, student.Email, loginId);
            logins.Add(new Login(loginId, student.Id, Convert.ToBase64String(Guid.NewGuid().ToByteArray())));
        }

        foreach (var teacher in teachers)
        {
            var loginId = Guid.NewGuid();
            teacher.Update(teacher.FirstName, teacher.LastName, teacher.Email, loginId, teacher.Specializations);
            logins.Add(new Login(loginId, teacher.Id, Convert.ToBase64String(Guid.NewGuid().ToByteArray())));
        }

        return logins;
    }

    private static string ToGradeLabel(decimal score)
    {
        if (score >= 10.0m)
        {
            return "Fremragende";
        }

        if (score >= 7.0m)
        {
            return "God";
        }

        if (score >= 4.0m)
        {
            return "Middel";
        }

        return "Kan forbedres";
    }

    private static string BuildSchoolEmail(string firstName, string lastName, int index, string role)
    {
        var normalizedFirstName = NormalizeForEmail(firstName);
        var normalizedLastName = NormalizeForEmail(lastName);
        return $"{normalizedFirstName}.{normalizedLastName}.{role}{index + 1}@studieplusplus.local";
    }

    private static string NormalizeForEmail(string input)
    {
        return new string(input
            .ToLowerInvariant()
            .Select(c => char.IsLetterOrDigit(c) ? c : '.')
            .ToArray())
            .Trim('.');
    }
}