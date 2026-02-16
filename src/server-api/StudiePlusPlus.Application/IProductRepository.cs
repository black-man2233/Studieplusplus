using StudiePlusPlus.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace StudiePlusPlus.Application;
// Application Layer (Interfaces)
public interface IProductRepository
{
    Task<Product> GetByIdAsync(Guid id);
    Task<IEnumerable<Product>> GetAllAsync();
}