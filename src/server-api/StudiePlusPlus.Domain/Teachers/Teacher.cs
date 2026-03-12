using System;
using System.Collections.Generic;
using StudiePlusPlus.Domain.Users;
using StudiePlusPlus.Domain.ValueObjects;

namespace StudiePlusPlus.Domain.Teachers;

public class Teacher : User
{
    public IEnumerable<string> Specializations { get; private set; }
    
    private Teacher() { }

    public Teacher(Guid userId, string firstName, string lastName, Email email, Guid loginId, IEnumerable<string> specializations)
        : base(userId, firstName, lastName, email)
    {
        Specializations = specializations;
    }

    public void Update(string firstName, string lastName, Email email, Guid loginId, IEnumerable<string> specializations)
    {
        base.Update(firstName, lastName, email, loginId);
        Specializations = specializations;
    }
}