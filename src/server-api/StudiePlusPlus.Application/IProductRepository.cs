using System;
using System.Threading.Tasks;
using StudiePlusPlus.Domain;
namespace StudiePlusPlus.Application;
// Application Layer (Interfaces)
public interface IProductRepository  
{
    Task<Product> GetByIdAsync(Guid id);  
}