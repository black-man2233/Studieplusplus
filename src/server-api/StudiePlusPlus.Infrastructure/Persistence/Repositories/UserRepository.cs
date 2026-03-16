using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudiePlusPlus.Application.Abstractions.Persistence;
using StudiePlusPlus.Domain.Users;

namespace StudiePlusPlus.Infrastructure.Persistence.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;

    public UserRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<User> GetByEmailAsync(string email, CancellationToken ct = default)
    {
        return await _db.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email.Value == email, ct);
    }
}
