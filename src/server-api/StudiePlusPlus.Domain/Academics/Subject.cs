using System;
using StudiePlusPlus.Domain.Common;

namespace StudiePlusPlus.Domain.Academics;

public class Subject : Entity<Guid>
{
    public string Name { get; set; }

    public Subject()
    {
    }

    public Subject(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}