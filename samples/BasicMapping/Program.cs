using System;
using Knot.Configuration;

namespace Knot.Samples.BasicMapping;

internal class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=====================================");
        Console.WriteLine("   KNOT BASIC MAPPING EXAMPLE");
        Console.WriteLine("=====================================\n");

        // Step 1: Define your source and destination models
        // (See Models.cs)

        // Step 2: Configure the mapper
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Person, PersonDto>();
            cfg.CreateMap<Employee, EmployeeDto>();
        });

        // Step 3: Create the mapper instance
        var mapper = config.CreateMapper();

        // Example 1: Simple Person mapping
        Console.WriteLine("EXAMPLE 1: SIMPLE PERSON MAPPING");
        Console.WriteLine("-------------------------------------");

        var person = new Person
        {
            FirstName = "John",
            LastName = "Doe",
            Age = 30,
            Email = "john.doe@example.com"
        };

        var personDto = mapper.Map<PersonDto>(person);

        Console.WriteLine($"Source Object:  {person.FirstName} {person.LastName}, Age: {person.Age}, Email: {person.Email}");
        Console.WriteLine($"Mapped Result:  {personDto.FirstName} {personDto.LastName}, Age: {personDto.Age}, Email: {personDto.Email}");
        Console.WriteLine("Status:         Mapping completed successfully\n");

        // Example 2: Employee mapping (with automatic ignoring)
        Console.WriteLine("EXAMPLE 2: EMPLOYEE MAPPING");
        Console.WriteLine("-------------------------------------");

        var employee = new Employee
        {
            Id = 101,
            FirstName = "Jane",
            LastName = "Smith",
            Email = "jane.smith@company.com",
            Salary = 75000m,
            HireDate = DateTime.Now
        };

        var employeeDto = mapper.Map<EmployeeDto>(employee);

        Console.WriteLine($"Source Object:  ID: {employee.Id}, Name: {employee.FirstName} {employee.LastName}");
        Console.WriteLine($"                Salary: ${employee.Salary:N2}, Email: {employee.Email}");
        Console.WriteLine($"Mapped Result:  ID: {employeeDto.Id}, Name: {employeeDto.FirstName} {employeeDto.LastName}");
        Console.WriteLine($"                Email: {employeeDto.Email}");
        Console.WriteLine("Note:           Salary property not mapped (not present in destination DTO)\n");

        // Example 3: Mapping to existing instance
        Console.WriteLine("EXAMPLE 3: UPDATE EXISTING INSTANCE");
        Console.WriteLine("-------------------------------------");

        var existingPerson = new PersonDto
        {
            FirstName = "Old",
            LastName = "Name",
            Age = 25,
            Email = "old@example.com"
        };

        Console.WriteLine($"Before Update:  {existingPerson.FirstName} {existingPerson.LastName}, Age: {existingPerson.Age}");

        mapper.Map(person, existingPerson);

        Console.WriteLine($"After Update:   {existingPerson.FirstName} {existingPerson.LastName}, Age: {existingPerson.Age}");
        Console.WriteLine("Status:         Existing instance updated successfully\n");

        Console.WriteLine("=====================================");
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}

// Source Models
public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
}

public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public decimal Salary { get; set; }
    public DateTime HireDate { get; set; }
}

// Destination DTOs
public class PersonDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
}

public class EmployeeDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    // Note: Salary and HireDate are intentionally omitted
}
