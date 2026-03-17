using System.Threading;
using System.Threading.Tasks;
using StudiePlusPlus.Domain.Users;

namespace StudiePlusPlus.Application.Abstractions.Persistence;

public interface IUserRepository
{
    Task<User> GetByEmailAsync(string email, CancellationToken ct = default);
}
