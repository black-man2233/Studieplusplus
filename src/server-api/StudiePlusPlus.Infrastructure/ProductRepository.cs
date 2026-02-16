using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using StudiePlusPlus.Application;
using StudiePlusPlus.Domain;

namespace StudiePlusPlus.Infrastructure;

// Infrastructure Layer (EF Core Implementation)
public class ProductRepository : IProductRepository
{
    private readonly ObservableCollection<Product> _repository;

    public ProductRepository()
    {
        var faker = new Faker<Product>()
            .RuleFor(p => p.Id, f => Guid.NewGuid()) 
            .RuleFor(p => p.Name, f => f.Person.FullName); 

        _repository = new ObservableCollection<Product>(faker.Generate(4));
    }


    public async Task<Product> GetByIdAsync(Guid id)
    {
        var res = _repository.FirstOrDefault(x => x.Id.Equals(id));
        return res;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return _repository;
    }
}