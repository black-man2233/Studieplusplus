using System;
using System.Collections;
using System.Collections.Generic;
using StudiePlusPlus.Domain.Common;

namespace StudiePlusPlus.Domain.Teachers;

public class Teacher : Entity<Guid>
{
    public Guid UserId { get; private set; }

    public IEnumerable<string> Specializations { get; set; }
    
    private Teacher() { }

    public Teacher(Guid id, Guid userId, IEnumerable<string> specializations)
    {
        Id = id;
        UserId = userId;
        Specializations = specializations;
    }
}