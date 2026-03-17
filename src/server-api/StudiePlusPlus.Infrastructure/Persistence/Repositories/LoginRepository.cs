using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudiePlusPlus.Application.Abstractions.Persistence;
using StudiePlusPlus.Domain.Auth;

namespace StudiePlusPlus.Infrastructure.Persistence.Repositories;

public sealed class LoginRepository : Repository<Login, Guid>, ILoginRepository
{
    private readonly AppDbContext _db;

    public LoginRepository(AppDbContext db, ILoggerFactory loggerFactory) : base(db, loggerFactory)
    {
        _db = db;
    }

    public async Task<Login> GetByUserIdAsync(Guid userId, CancellationToken ct = default)
    {
        Logger.LogDebug("DB GetByUserId Login userId={UserId}", userId);
        var login = await _db.Logins
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.UserId == userId, ct);

        if (login is null)
            Logger.LogDebug("No Login record found for userId={UserId}", userId);

        return login;
    }
}
