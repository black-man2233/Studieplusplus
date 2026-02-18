using System;
using StudiePlusPlus.Domain.Common;
using StudiePlusPlus.Domain.ValueObjects;

namespace StudiePlusPlus.Domain.Users;

public abstract class User : Entity<Guid>
{
    public string FirstName { get; protected set; } = string.Empty;
    
    public string LastName { get; protected set; } = string.Empty;

    public Email Email { get; protected set; } = new Email("zilas@elev.techcollege.dk");

    public Guid LoginId { get; protected set; }

    protected User() { }

    protected User(Guid id, string firstName, string lastName, Email email, Guid loginId)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        LoginId = loginId;
    }

    public void Update(string firstName, string lastName, Email email, Guid loginId)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        LoginId = loginId;
    }
}