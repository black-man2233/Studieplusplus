using System;
using StudiePlusPlus.Domain.Common;

namespace StudiePlusPlus.Domain.Academics;

public class ClassGroup : Entity<Guid>
{
    public string Name { get; set; }

    public ClassGroup()
    {
    }

    public ClassGroup(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}