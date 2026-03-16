using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudiePlusPlus.Application.Abstractions.Persistence;
using StudiePlusPlus.Domain.Auth;

namespace StudiePlusPlus.Infrastructure.Persistence.Repositories;

public sealed class LoginRepository : Repository<Login, Guid>, ILoginRepository
{
    private readonly AppDbContext _db;

    public LoginRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task<Login> GetByUserIdAsync(Guid userId, CancellationToken ct = default)
    {
        return await _db.Logins
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.UserId == userId, ct);
    }
}
