using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudiePlusPlus.Application.Abstractions.Persistence;
using StudiePlusPlus.Domain.Users;
using StudiePlusPlus.Domain.ValueObjects;

namespace StudiePlusPlus.Infrastructure.Persistence.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;
    private readonly ILogger<UserRepository> _logger;

    public UserRepository(AppDbContext db, ILogger<UserRepository> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<User> GetByEmailAsync(string email, CancellationToken ct = default)
    {
        // Do not log the email value — use a masked hint for security audit trails
        _logger.LogDebug("DB lookup user by email (hash={Hash})", email.GetHashCode());

        var emailVo = new Email(email);
        var user = await _db.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == emailVo, ct);

        if (user is null)
            _logger.LogDebug("No user found for email lookup");
        else
            _logger.LogDebug("User found — userId={UserId}", user.Id);

        return user;
    }
}
