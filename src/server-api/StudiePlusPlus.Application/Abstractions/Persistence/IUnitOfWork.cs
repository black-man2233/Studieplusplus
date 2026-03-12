using System.Threading;
using System.Threading.Tasks;

namespace StudiePlusPlus.Application.Abstractions.Persistence;


public interface IUnitOfWork
{
    /// <summary>
    /// Persists all changes made through the current DbContext/repositories.
    /// Returns the number of state entries written to the database.
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
