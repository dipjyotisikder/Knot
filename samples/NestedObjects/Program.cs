using System;
using System.Collections.Generic;
using System.Linq;
using Knot.Configuration;

namespace Knot.Samples.NestedObjects;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("================================================");
        Console.WriteLine("   KNOT NESTED OBJECTS MAPPING EXAMPLE");
        Console.WriteLine("================================================\n");

        // Configure all mappings in the object graph
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Company, CompanyDto>();
            cfg.CreateMap<Department, DepartmentDto>();
            cfg.CreateMap<Employee, EmployeeDto>();
            cfg.CreateMap<Address, AddressDto>();
            cfg.CreateMap<Project, ProjectDto>();
        });

        var mapper = config.CreateMapper();

        // Create complex nested object graph
        var company = new Company
        {
            Id = 1,
            Name = "TechCorp Industries",
            Founded = new DateTime(2010, 1, 15),
            Headquarters = new Address
            {
                Street = "123 Innovation Drive",
                City = "San Francisco",
                State = "CA",
                ZipCode = "94105",
                Country = "USA"
            },
            Departments = new List<Department>
            {
                new Department
                {
                    Id = 101,
                    Name = "Engineering",
                    Budget = 5000000m,
                    Manager = new Employee
                    {
                        Id = 1001,
                        FirstName = "Alice",
                        LastName = "Johnson",
                        Title = "VP of Engineering",
                        Salary = 180000m,
                        HireDate = new DateTime(2015, 3, 1)
                    },
                    Employees = new List<Employee>
                    {
                        new Employee { Id = 1002, FirstName = "Bob", LastName = "Smith", Title = "Senior Engineer", Salary = 140000m, HireDate = new DateTime(2018, 6, 15) },
                        new Employee { Id = 1003, FirstName = "Carol", LastName = "Davis", Title = "Engineer", Salary = 110000m, HireDate = new DateTime(2020, 1, 10) }
                    }
                },
                new Department
                {
                    Id = 102,
                    Name = "Sales",
                    Budget = 3000000m,
                    Manager = new Employee
                    {
                        Id = 2001,
                        FirstName = "David",
                        LastName = "Wilson",
                        Title = "VP of Sales",
                        Salary = 160000m,
                        HireDate = new DateTime(2016, 9, 1)
                    },
                    Employees = new List<Employee>
                    {
                        new Employee { Id = 2002, FirstName = "Eve", LastName = "Martinez", Title = "Sales Rep", Salary = 90000m, HireDate = new DateTime(2019, 4, 20) },
                        new Employee { Id = 2003, FirstName = "Frank", LastName = "Brown", Title = "Sales Rep", Salary = 85000m, HireDate = new DateTime(2021, 7, 1) }
                    }
                }
            },
            ActiveProjects = new List<Project>
            {
                new Project { Id = 301, Name = "Cloud Migration", StartDate = new DateTime(2025, 1, 1), Budget = 500000m, Status = "In Progress" },
                new Project { Id = 302, Name = "Mobile App Redesign", StartDate = new DateTime(2025, 2, 15), Budget = 300000m, Status = "Planning" }
            }
        };

        // Map the entire complex object graph
        Console.WriteLine("MAPPING COMPLEX OBJECT GRAPH");
        Console.WriteLine("================================================\n");

        var companyDto = mapper.Map<CompanyDto>(company);

        // Display mapped results
        Console.WriteLine($"Company Name:       {companyDto.Name}");
        Console.WriteLine($"Founded:            {companyDto.Founded.Year}");
        Console.WriteLine($"Headquarters:       {companyDto.Headquarters.Street}");
        Console.WriteLine($"                    {companyDto.Headquarters.City}, {companyDto.Headquarters.State} {companyDto.Headquarters.ZipCode}");
        Console.WriteLine($"                    {companyDto.Headquarters.Country}");
        Console.WriteLine($"\nDepartments:        {companyDto.Departments.Count} total");

        foreach (var dept in companyDto.Departments)
        {
            Console.WriteLine($"\n  DEPARTMENT: {dept.Name.ToUpper()}");
            Console.WriteLine($"  Budget:           ${dept.Budget:N0}");
            Console.WriteLine($"  Manager:          {dept.Manager.FirstName} {dept.Manager.LastName} ({dept.Manager.Title})");
            Console.WriteLine($"  Team Size:        {dept.Employees.Count} employees");
            Console.WriteLine("  Team Members:");

            foreach (var emp in dept.Employees)
            {
                Console.WriteLine($"    - {emp.FirstName} {emp.LastName,-15} ({emp.Title})");
            }
        }

        Console.WriteLine($"\nActive Projects:    {companyDto.ActiveProjects.Count} total");
        foreach (var proj in companyDto.ActiveProjects)
        {
            Console.WriteLine($"  [{proj.Id}] {proj.Name,-25} Budget: ${proj.Budget,10:N0} | Status: {proj.Status}");
        }

        Console.WriteLine("\n\nNESTED MAPPING STATISTICS");
        Console.WriteLine("================================================");
        Console.WriteLine("Object Type                    Count");
        Console.WriteLine("-------------------------------------");
        Console.WriteLine($"Company                        1");
        Console.WriteLine($"Address                        1");
        Console.WriteLine($"Departments                    {companyDto.Departments.Count}");
        Console.WriteLine($"Employees (including managers) {companyDto.Departments.Count + companyDto.Departments.Sum(d => d.Employees.Count)}");
        Console.WriteLine($"Projects                       {companyDto.ActiveProjects.Count}");

        int totalObjects = 1 + 1 + companyDto.Departments.Count +
                         (companyDto.Departments.Count + companyDto.Departments.Sum(d => d.Employees.Count)) +
                         companyDto.ActiveProjects.Count;
        Console.WriteLine("-------------------------------------");
        Console.WriteLine($"Total Objects Mapped:          {totalObjects}");
        Console.WriteLine("\nStatus:             All nested objects mapped successfully");
        Console.WriteLine("Depth Level:        3 levels deep (Company -> Department -> Employee)");
        Console.WriteLine("Complexity:         High - Multiple one-to-many relationships");

        Console.WriteLine("\n================================================");
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}

// Domain Models
public class Company
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime Founded { get; set; }
    public Address Headquarters { get; set; } = null!;
    public List<Department> Departments { get; set; } = new();
    public List<Project> ActiveProjects { get; set; } = new();
}

public class Department
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Budget { get; set; }
    public Employee Manager { get; set; } = null!;
    public List<Employee> Employees { get; set; } = new();
}

public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public decimal Salary { get; set; }
    public DateTime HireDate { get; set; }
}

public class Address
{
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
}

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public decimal Budget { get; set; }
    public string Status { get; set; } = string.Empty;
}

// DTOs
public class CompanyDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime Founded { get; set; }
    public AddressDto Headquarters { get; set; } = null!;
    public List<DepartmentDto> Departments { get; set; } = new();
    public List<ProjectDto> ActiveProjects { get; set; } = new();
}

public class DepartmentDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Budget { get; set; }
    public EmployeeDto Manager { get; set; } = null!;
    public List<EmployeeDto> Employees { get; set; } = new();
}

public class EmployeeDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
}

public class AddressDto
{
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
}

public class ProjectDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public decimal Budget { get; set; }
    public string Status { get; set; } = string.Empty;
}
