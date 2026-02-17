using System;
using StudiePlusPlus.Domain.Common;
using StudiePlusPlus.Domain.ValueObjects;

namespace StudiePlusPlus.Domain.Users;

public sealed class User : Entity<Guid>
{
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;

    public Email Email { get; private set; } = new Email("zilas@elev.techcollege.dk");

    public Guid LoginId { get; private set; }

    private User() { }

    public User(Guid id, string firstName, string lastName, Email email, Guid loginId)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        LoginId = loginId;
    }
}