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
public class StudentRepository : IStudentRepository
{
    private readonly ObservableCollection<Student> _repository;

    public StudentRepository()
    {
        var faker = new Faker<Student>()
            .RuleFor(p => p.Id, f => Guid.NewGuid()) 
            .RuleFor(p => p.Name, f => f.Person.FullName); 

        _repository = new ObservableCollection<Student>(faker.Generate(4));
    }


    public async Task<Student> GetByIdAsync(Guid id)
    {
        var res = _repository.FirstOrDefault(x => x.Id.Equals(id));
        return res;
    }

    public async Task<IEnumerable<Student>> GetAllAsync()
    {
        return _repository;
    }
}