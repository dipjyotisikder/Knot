using System;
using Knot.Configuration;

namespace Knot.Samples.BasicMapping;

internal class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Knot Basic Mapping Example\n");

        // Define mappers for source and destination models
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Person, PersonDto>();
            cfg.CreateMap<Employee, EmployeeDto>();
        });

        // Create mapper instance
        var mapper = config.CreateMapper();

        // Example 1: Simple Person mapping
        Console.WriteLine("Example 1: Simple person mapping\n");

        var person = new Person
        {
            FirstName = "John",
            LastName = "Doe",
            Age = 30,
            Email = "john.doe@example.com"
        };

        var personDto = mapper.Map<PersonDto>(person);

        Console.WriteLine($"Source object:  {person.FirstName} {person.LastName}, Age: {person.Age}, Email: {person.Email}");
        Console.WriteLine($"Mapped result:  {personDto.FirstName} {personDto.LastName}, Age: {personDto.Age}, Email: {personDto.Email}");
        Console.WriteLine("Status:         Mapping completed successfully\n");

        // Example 2: Employee mapping (automatic field exclusion)
        Console.WriteLine("Example 2: Employee mapping\n");

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

        Console.WriteLine($"Source object:  ID: {employee.Id}, Name: {employee.FirstName} {employee.LastName}");
        Console.WriteLine($"                Salary: ${employee.Salary:N2}, Email: {employee.Email}");
        Console.WriteLine($"Mapped result:  ID: {employeeDto.Id}, Name: {employeeDto.FirstName} {employeeDto.LastName}");
        Console.WriteLine($"                Email: {employeeDto.Email}");
        Console.WriteLine("Note:           Salary property not mapped (not present in destination DTO)\n");

        // Example 3: Mapping to existing instance
        Console.WriteLine("Example 3: Update existing instance\n");

        var existingPerson = new PersonDto
        {
            FirstName = "Old",
            LastName = "Name",
            Age = 25,
            Email = "old@example.com"
        };

        Console.WriteLine($"Before update:  {existingPerson.FirstName} {existingPerson.LastName}, Age: {existingPerson.Age}");

        mapper.Map(person, existingPerson);

        Console.WriteLine($"After update:   {existingPerson.FirstName} {existingPerson.LastName}, Age: {existingPerson.Age}");
        Console.WriteLine("Status:         Existing instance updated successfully\n");

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
