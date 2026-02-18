using System;
using StudiePlusPlus.Domain.Users;
using StudiePlusPlus.Domain.ValueObjects;

namespace StudiePlusPlus.Domain.Students;

public sealed class Student : User
{
    private Student() { }

    public Student(Guid id, string firstName, string lastName, Email email, Guid loginId)
        : base(id, firstName, lastName, email, loginId)
    {
    }
}