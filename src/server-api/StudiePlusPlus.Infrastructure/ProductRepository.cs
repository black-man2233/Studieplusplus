using System;
using System.Threading.Tasks;
using StudiePlusPlus.Application;
using StudiePlusPlus.Domain;

namespace StudiePlusPlus.Infrastructure;

// Infrastructure Layer (EF Core Implementation)
public class ProductRepository : IProductRepository
{
    public async Task<Product> GetByIdAsync(Guid id)
    {
        // DB logic here  

        return (new Product());
    }
}