using System;
using StudiePlusPlus.Domain.Common;

namespace StudiePlusPlus.Domain.Academics;

public class Class : Entity<Guid>
{
    public string Name { get; set; }

    public Class()
    {
    }

    public Class(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}