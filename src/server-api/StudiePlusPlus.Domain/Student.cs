using System;

namespace StudiePlusPlus.Domain;

// Domain Layer (No dependencies)
public class Student  
{
    public Guid Id { get; set; }  
    public string Name { get; set; }  
}