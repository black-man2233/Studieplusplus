using System;
using System.Threading;
using System.Threading.Tasks;
using StudiePlusPlus.Domain.Auth;

namespace StudiePlusPlus.Application.Abstractions.Persistence;

public interface ILoginRepository : IRepository<Login, Guid>
{
    Task<Login> GetByUserIdAsync(Guid userId, CancellationToken ct = default);
}
