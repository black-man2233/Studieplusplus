using System;
using StudiePlusPlus.Domain.Common;

namespace StudiePlusPlus.Domain.Auth;

public sealed class Login : Entity<Guid>
{
    public Guid   UserId       { get; private set; }
    public string PasswordHash { get; private set; } = string.Empty;

    private Login() { }

    public Login(Guid id, Guid userId, string passwordHash)
    {
        Id           = id;
        UserId       = userId;
        PasswordHash = passwordHash;
    }

    public void UpdatePassword(string newPasswordHash)
    {
        PasswordHash = newPasswordHash;
    }
}
