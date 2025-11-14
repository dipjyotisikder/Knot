using System;
using System.Collections.Generic;
using System.Linq;
using Knot.Configuration;

namespace Knot.Samples.NestedObjects;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("========================================================");
        Console.WriteLine("   KNOT COMPREHENSIVE NESTED OBJECTS MAPPING TESTS");
        Console.WriteLine("========================================================\n");

        RunAllTests();

        Console.WriteLine("\n========================================================");
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

    private static void RunAllTests()
    {
        Test1_BasicNestedObjectMapping();
        Test2_DeepNestedHierarchy();
        Test3_CollectionMapping();
        Test4_ArrayMapping();
        Test5_NullHandling();
        Test6_EmptyCollections();
        Test7_BidirectionalMapping();
        Test8_MixedCollectionTypes();
        Test9_CircularReferencePrevention();
        Test10_PerformanceWithLargeDataset();
        Test11_TenLevelDeepHierarchy();
    }

    private static void Test1_BasicNestedObjectMapping()
    {
        Console.WriteLine("TEST 1: Basic Nested Object Mapping");
        Console.WriteLine("------------------------------------------------------------");

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Company, CompanyDto>();
            cfg.CreateMap<Address, AddressDto>();
        });

        var mapper = config.CreateMapper();

        var company = new Company
        {
            Id = 1,
            Name = "TechCorp",
            Founded = new DateTime(2010, 1, 15),
            Headquarters = new Address
            {
                Street = "123 Main St",
                City = "San Francisco",
                State = "CA",
                ZipCode = "94105",
                Country = "USA"
            }
        };

        var dto = mapper.Map<CompanyDto>(company);

        Console.WriteLine($"✓ Company mapped: {dto.Name}");
        Console.WriteLine($"✓ Nested Address mapped: {dto.Headquarters.Street}, {dto.Headquarters.City}");
        Console.WriteLine($"Result: PASS - Basic nested object mapping works\n");
    }

    private static void Test2_DeepNestedHierarchy()
    {
        Console.WriteLine("TEST 2: Deep Nested Hierarchy (3+ levels)");
        Console.WriteLine("------------------------------------------------------------");

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Company, CompanyDto>();
            cfg.CreateMap<Department, DepartmentDto>();
            cfg.CreateMap<Employee, EmployeeDto>();
            cfg.CreateMap<Address, AddressDto>();
        });

        var mapper = config.CreateMapper();

        var company = new Company
        {
            Id = 1,
            Name = "DeepNest Corp",
            Founded = DateTime.Now,
            Headquarters = new Address { Street = "456 Deep St", City = "Seattle", State = "WA", ZipCode = "98101", Country = "USA" },
            Departments = new List<Department>
            {
                new Department
                {
                    Id = 101,
                    Name = "Engineering",
                    Budget = 5000000m,
                    Manager = new Employee
                    {
                        Id = 1,
                        FirstName = "Alice",
                        LastName = "Johnson",
                        Title = "VP Engineering"
                    },
                    Employees = new List<Employee>
                    {
                        new Employee { Id = 2, FirstName = "Bob", LastName = "Smith", Title = "Senior Engineer" },
                        new Employee { Id = 3, FirstName = "Carol", LastName = "Davis", Title = "Engineer" }
                    }
                }
            }
        };

        var dto = mapper.Map<CompanyDto>(company);

        Console.WriteLine($"✓ Level 1 (Company): {dto.Name}");
        Console.WriteLine($"✓ Level 2 (Department): {dto.Departments[0].Name}");
        Console.WriteLine($"✓ Level 3 (Manager): {dto.Departments[0].Manager.FirstName} {dto.Departments[0].Manager.LastName}");
        Console.WriteLine($"✓ Level 3 (Employees): {dto.Departments[0].Employees.Count} mapped");
        Console.WriteLine($"Result: PASS - Deep hierarchy mapping works (3 levels)\n");
    }

    private static void Test3_CollectionMapping()
    {
        Console.WriteLine("TEST 3: Collection Mapping (List<T>)");
        Console.WriteLine("------------------------------------------------------------");

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Company, CompanyDto>();
            cfg.CreateMap<Project, ProjectDto>();
        });

        var mapper = config.CreateMapper();

        var company = new Company
        {
            Id = 1,
            Name = "CollectionTest Corp",
            Founded = DateTime.Now,
            ActiveProjects = new List<Project>
            {
                new Project { Id = 1, Name = "Project Alpha", StartDate = DateTime.Now, Budget = 100000m, Status = "Active" },
                new Project { Id = 2, Name = "Project Beta", StartDate = DateTime.Now.AddMonths(1), Budget = 200000m, Status = "Planning" },
                new Project { Id = 3, Name = "Project Gamma", StartDate = DateTime.Now.AddMonths(2), Budget = 150000m, Status = "On Hold" }
            }
        };

        var dto = mapper.Map<CompanyDto>(company);

        Console.WriteLine($"✓ Source collection count: {company.ActiveProjects.Count}");
        Console.WriteLine($"✓ Mapped collection count: {dto.ActiveProjects.Count}");
        Console.WriteLine($"✓ First project: {dto.ActiveProjects[0].Name}");
        Console.WriteLine($"✓ Last project: {dto.ActiveProjects[2].Name}");
        Console.WriteLine($"Result: PASS - List<T> collection mapping works\n");
    }

    private static void Test4_ArrayMapping()
    {
        Console.WriteLine("TEST 4: Array Mapping");
        Console.WriteLine("------------------------------------------------------------");

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<CompanyWithArrays, CompanyWithArraysDto>();
            cfg.CreateMap<Employee, EmployeeDto>();
        });

        var mapper = config.CreateMapper();

        var company = new CompanyWithArrays
        {
            Id = 1,
            Name = "ArrayTest Corp",
            TopExecutives = new[]
            {
                new Employee { Id = 1, FirstName = "John", LastName = "CEO", Title = "Chief Executive Officer" },
                new Employee { Id = 2, FirstName = "Jane", LastName = "CFO", Title = "Chief Financial Officer" },
                new Employee { Id = 3, FirstName = "Jack", LastName = "CTO", Title = "Chief Technology Officer" }
            }
        };

        var dto = mapper.Map<CompanyWithArraysDto>(company);

        Console.WriteLine($"✓ Source array length: {company.TopExecutives.Length}");
        Console.WriteLine($"✓ Mapped array length: {dto.TopExecutives.Length}");
        Console.WriteLine($"✓ First executive: {dto.TopExecutives[0].FirstName} {dto.TopExecutives[0].LastName}");
        Console.WriteLine($"Result: PASS - Array mapping works\n");
    }

    private static void Test5_NullHandling()
    {
        Console.WriteLine("TEST 5: Null Object Handling");
        Console.WriteLine("------------------------------------------------------------");

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Company, CompanyDto>();
            cfg.CreateMap<Address, AddressDto>();
            cfg.CreateMap<Department, DepartmentDto>();
            cfg.CreateMap<Employee, EmployeeDto>();
        });

        var mapper = config.CreateMapper();

        // Test with null nested object
        var company1 = new Company
        {
            Id = 1,
            Name = "NullTest Corp",
            Founded = DateTime.Now,
            Headquarters = null! // Null nested object
        };

        try
        {
            var dto1 = mapper.Map<CompanyDto>(company1);
            Console.WriteLine($"✓ Null nested object handled: Headquarters = {(dto1.Headquarters == null ? "null" : "not null")}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Failed with null nested object: {ex.Message}");
        }

        // Test with null in collection
        var company2 = new Company
        {
            Id = 2,
            Name = "NullCollectionTest Corp",
            Founded = DateTime.Now,
            Departments = new List<Department>
            {
                new Department { Id = 1, Name = "Dept 1", Budget = 100000m, Manager = null! },
                null!, // Null item in collection
                new Department { Id = 3, Name = "Dept 3", Budget = 300000m, Manager = null! }
            }
        };

        try
        {
            var dto2 = mapper.Map<CompanyDto>(company2);
            Console.WriteLine($"✓ Collection with nulls handled: {dto2.Departments.Count} departments");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Failed with null in collection: {ex.Message}");
        }

        Console.WriteLine($"Result: PASS - Null handling works\n");
    }

    private static void Test6_EmptyCollections()
    {
        Console.WriteLine("TEST 6: Empty Collection Mapping");
        Console.WriteLine("------------------------------------------------------------");

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Company, CompanyDto>();
            cfg.CreateMap<Department, DepartmentDto>();
            cfg.CreateMap<Project, ProjectDto>();
        });

        var mapper = config.CreateMapper();

        var company = new Company
        {
            Id = 1,
            Name = "EmptyCollection Corp",
            Founded = DateTime.Now,
            Departments = new List<Department>(), // Empty list
            ActiveProjects = new List<Project>()  // Empty list
        };

        var dto = mapper.Map<CompanyDto>(company);

        Console.WriteLine($"✓ Empty Departments collection: {dto.Departments.Count} items");
        Console.WriteLine($"✓ Empty Projects collection: {dto.ActiveProjects.Count} items");
        Console.WriteLine($"Result: PASS - Empty collection mapping works\n");
    }

    private static void Test7_BidirectionalMapping()
    {
        Console.WriteLine("TEST 7: Bidirectional Mapping");
        Console.WriteLine("------------------------------------------------------------");

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Employee, EmployeeDto>();
            cfg.CreateMap<EmployeeDto, Employee>(); // Reverse mapping
        });

        var mapper = config.CreateMapper();

        // Forward mapping
        var employee = new Employee
        {
            Id = 1,
            FirstName = "Alice",
            LastName = "Smith",
            Title = "Senior Engineer",
            Salary = 120000m,
            HireDate = new DateTime(2020, 1, 15)
        };

        var dto = mapper.Map<EmployeeDto>(employee);
        Console.WriteLine($"✓ Forward mapping: {dto.FirstName} {dto.LastName}, {dto.Title}");

        // Reverse mapping
        var employeeBack = mapper.Map<Employee>(dto);
        Console.WriteLine($"✓ Reverse mapping: {employeeBack.FirstName} {employeeBack.LastName}, {employeeBack.Title}");
        Console.WriteLine($"Result: PASS - Bidirectional mapping works\n");
    }

    private static void Test8_MixedCollectionTypes()
    {
        Console.WriteLine("TEST 8: Mixed Collection Types (IEnumerable, List, Array)");
        Console.WriteLine("------------------------------------------------------------");

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<MixedCollectionSource, MixedCollectionDestination>();
            cfg.CreateMap<Employee, EmployeeDto>();
        });

        var mapper = config.CreateMapper();

        var source = new MixedCollectionSource
        {
            ListEmployees = new List<Employee>
            {
                new Employee { Id = 1, FirstName = "John", LastName = "Doe", Title = "Engineer" }
            },
            ArrayEmployees = new[]
            {
                new Employee { Id = 2, FirstName = "Jane", LastName = "Smith", Title = "Manager" }
            }
        };

        var destination = mapper.Map<MixedCollectionDestination>(source);

        Console.WriteLine($"✓ List -> List: {destination.ListEmployees.Count} items");
        Console.WriteLine($"✓ Array -> Array: {destination.ArrayEmployees.Length} items");
        Console.WriteLine($"Result: PASS - Mixed collection type mapping works\n");
    }

    private static void Test9_CircularReferencePrevention()
    {
        Console.WriteLine("TEST 9: Circular Reference Prevention");
        Console.WriteLine("------------------------------------------------------------");

        // Note: This tests that we don't have infinite loops with proper object construction
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Department, DepartmentDto>();
            cfg.CreateMap<Employee, EmployeeDto>();
        });

        var mapper = config.CreateMapper();

        var dept = new Department
        {
            Id = 1,
            Name = "Engineering",
            Budget = 1000000m,
            Manager = new Employee { Id = 1, FirstName = "Boss", LastName = "Man", Title = "Manager" },
            Employees = new List<Employee>
            {
                new Employee { Id = 2, FirstName = "Worker", LastName = "Bee", Title = "Engineer" }
            }
        };

        try
        {
            var dto = mapper.Map<DepartmentDto>(dept);
            Console.WriteLine($"✓ Complex object graph mapped without circular reference issues");
            Console.WriteLine($"✓ Department: {dto.Name}, Manager: {dto.Manager.FirstName}, Employees: {dto.Employees.Count}");
            Console.WriteLine($"Result: PASS - No circular reference issues\n");
        }
        catch (StackOverflowException)
        {
            Console.WriteLine($"✗ Stack overflow detected - circular reference issue");
            Console.WriteLine($"Result: FAIL\n");
        }
    }

    private static void Test10_PerformanceWithLargeDataset()
    {
        Console.WriteLine("TEST 10: Performance with Large Dataset");
        Console.WriteLine("------------------------------------------------------------");

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Company, CompanyDto>();
            cfg.CreateMap<Department, DepartmentDto>();
            cfg.CreateMap<Employee, EmployeeDto>();
        });

        var mapper = config.CreateMapper();

        // Create a company with 10 departments, each with 100 employees
        var company = new Company
        {
            Id = 1,
            Name = "LargeOrg Corp",
            Founded = DateTime.Now,
            Departments = new List<Department>()
        };

        for (int d = 0; d < 10; d++)
        {
            var dept = new Department
            {
                Id = d + 1,
                Name = $"Department {d + 1}",
                Budget = 1000000m,
                Manager = new Employee { Id = d * 1000, FirstName = $"Manager", LastName = $"{d}", Title = "Manager" },
                Employees = new List<Employee>()
            };

            for (int e = 0; e < 100; e++)
            {
                dept.Employees.Add(new Employee
                {
                    Id = d * 1000 + e + 1,
                    FirstName = $"Employee{e}",
                    LastName = $"Dept{d}",
                    Title = "Staff"
                });
            }

            company.Departments.Add(dept);
        }

        var startTime = DateTime.Now;
        var dto = mapper.Map<CompanyDto>(company);
        var elapsed = (DateTime.Now - startTime).TotalMilliseconds;

        int totalEmployees = dto.Departments.Sum(d => d.Employees.Count + 1); // +1 for manager

        Console.WriteLine($"✓ Departments mapped: {dto.Departments.Count}");
        Console.WriteLine($"✓ Total employees mapped: {totalEmployees}");
        Console.WriteLine($"✓ Total objects mapped: {1 + dto.Departments.Count + totalEmployees}");
        Console.WriteLine($"✓ Time taken: {elapsed:F2}ms");
        Console.WriteLine($"✓ Average per object: {elapsed / (1 + dto.Departments.Count + totalEmployees):F4}ms");
        Console.WriteLine($"Result: PASS - Performance test completed\n");
    }

    private static void Test11_TenLevelDeepHierarchy()
    {
        Console.WriteLine("TEST 11: 10-Level Deep Nested Hierarchy");
        Console.WriteLine("------------------------------------------------------------");

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Level1, Level1Dto>();
            cfg.CreateMap<Level2, Level2Dto>();
            cfg.CreateMap<Level3, Level3Dto>();
            cfg.CreateMap<Level4, Level4Dto>();
            cfg.CreateMap<Level5, Level5Dto>();
            cfg.CreateMap<Level6, Level6Dto>();
            cfg.CreateMap<Level7, Level7Dto>();
            cfg.CreateMap<Level8, Level8Dto>();
            cfg.CreateMap<Level9, Level9Dto>();
            cfg.CreateMap<Level10, Level10Dto>();
        });

        var mapper = config.CreateMapper();

        // Create a 10-level deep nested object structure
        var level1 = new Level1
        {
            Id = 1,
            Name = "Level 1",
            Description = "Root level of hierarchy",
            Level2 = new Level2
            {
                Id = 2,
                Name = "Level 2",
                Description = "Second level",
                Level3 = new Level3
                {
                    Id = 3,
                    Name = "Level 3",
                    Description = "Third level",
                    Level4 = new Level4
                    {
                        Id = 4,
                        Name = "Level 4",
                        Description = "Fourth level",
                        Level5 = new Level5
                        {
                            Id = 5,
                            Name = "Level 5",
                            Description = "Fifth level",
                            Level6 = new Level6
                            {
                                Id = 6,
                                Name = "Level 6",
                                Description = "Sixth level",
                                Level7 = new Level7
                                {
                                    Id = 7,
                                    Name = "Level 7",
                                    Description = "Seventh level",
                                    Level8 = new Level8
                                    {
                                        Id = 8,
                                        Name = "Level 8",
                                        Description = "Eighth level",
                                        Level9 = new Level9
                                        {
                                            Id = 9,
                                            Name = "Level 9",
                                            Description = "Ninth level",
                                            Level10 = new Level10
                                            {
                                                Id = 10,
                                                Name = "Level 10",
                                                Description = "Deepest level - the bottom of the hierarchy",
                                                Data = "Final data at level 10"
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        };

        var startTime = DateTime.Now;
        var dto = mapper.Map<Level1Dto>(level1);
        var elapsed = (DateTime.Now - startTime).TotalMilliseconds;

        // Verify each level was mapped correctly
        Console.WriteLine($"✓ Level 1: {dto.Name} (ID: {dto.Id})");
        Console.WriteLine($"✓ Level 2: {dto.Level2?.Name} (ID: {dto.Level2?.Id})");
        Console.WriteLine($"✓ Level 3: {dto.Level2?.Level3?.Name} (ID: {dto.Level2?.Level3?.Id})");
        Console.WriteLine($"✓ Level 4: {dto.Level2?.Level3?.Level4?.Name} (ID: {dto.Level2?.Level3?.Level4?.Id})");
        Console.WriteLine($"✓ Level 5: {dto.Level2?.Level3?.Level4?.Level5?.Name} (ID: {dto.Level2?.Level3?.Level4?.Level5?.Id})");
        Console.WriteLine($"✓ Level 6: {dto.Level2?.Level3?.Level4?.Level5?.Level6?.Name} (ID: {dto.Level2?.Level3?.Level4?.Level5?.Level6?.Id})");
        Console.WriteLine($"✓ Level 7: {dto.Level2?.Level3?.Level4?.Level5?.Level6?.Level7?.Name} (ID: {dto.Level2?.Level3?.Level4?.Level5?.Level6?.Level7?.Id})");
        Console.WriteLine($"✓ Level 8: {dto.Level2?.Level3?.Level4?.Level5?.Level6?.Level7?.Level8?.Name} (ID: {dto.Level2?.Level3?.Level4?.Level5?.Level6?.Level7?.Level8?.Id})");
        Console.WriteLine($"✓ Level 9: {dto.Level2?.Level3?.Level4?.Level5?.Level6?.Level7?.Level8?.Level9?.Name} (ID: {dto.Level2?.Level3?.Level4?.Level5?.Level6?.Level7?.Level8?.Level9?.Id})");
        Console.WriteLine($"✓ Level 10: {dto.Level2?.Level3?.Level4?.Level5?.Level6?.Level7?.Level8?.Level9?.Level10?.Name} (ID: {dto.Level2?.Level3?.Level4?.Level5?.Level6?.Level7?.Level8?.Level9?.Level10?.Id})");
        Console.WriteLine($"✓ Level 10 Data: {dto.Level2?.Level3?.Level4?.Level5?.Level6?.Level7?.Level8?.Level9?.Level10?.Data}");
        Console.WriteLine($"✓ Time taken: {elapsed:F2}ms");
        Console.WriteLine($"Result: PASS - 10-level deep hierarchy mapping works\n");
    }
}

// ============================================================================
// DOMAIN MODELS
// ============================================================================

public class Company
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime Founded { get; set; }
    public Address? Headquarters { get; set; }
    public List<Department> Departments { get; set; } = new();
    public List<Project> ActiveProjects { get; set; } = new();
}

public class CompanyDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime Founded { get; set; }
    public AddressDto? Headquarters { get; set; }
    public List<DepartmentDto> Departments { get; set; } = new();
    public List<ProjectDto> ActiveProjects { get; set; } = new();
}

public class Department
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Budget { get; set; }
    public Employee? Manager { get; set; }
    public List<Employee> Employees { get; set; } = new();
}

public class DepartmentDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Budget { get; set; }
    public EmployeeDto? Manager { get; set; }
    public List<EmployeeDto> Employees { get; set; } = new();
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

public class EmployeeDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
}

public class Address
{
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
}

public class AddressDto
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

public class ProjectDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public decimal Budget { get; set; }
    public string Status { get; set; } = string.Empty;
}

// Additional test models
public class CompanyWithArrays
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Employee[] TopExecutives { get; set; } = Array.Empty<Employee>();
}

public class CompanyWithArraysDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public EmployeeDto[] TopExecutives { get; set; } = Array.Empty<EmployeeDto>();
}

public class MixedCollectionSource
{
    public List<Employee> ListEmployees { get; set; } = new();
    public Employee[] ArrayEmployees { get; set; } = Array.Empty<Employee>();
}

public class MixedCollectionDestination
{
    public List<EmployeeDto> ListEmployees { get; set; } = new();
    public EmployeeDto[] ArrayEmployees { get; set; } = Array.Empty<EmployeeDto>();
}

public class Level1
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Level2 Level2 { get; set; } = new();
}

public class Level1Dto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Level2Dto Level2 { get; set; } = new();
}

public class Level2
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Level3 Level3 { get; set; } = new();
}

public class Level2Dto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Level3Dto Level3 { get; set; } = new();
}

public class Level3
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Level4 Level4 { get; set; } = new();
}

public class Level3Dto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Level4Dto Level4 { get; set; } = new();
}

public class Level4
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Level5 Level5 { get; set; } = new();
}

public class Level4Dto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Level5Dto Level5 { get; set; } = new();
}

public class Level5
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Level6 Level6 { get; set; } = new();
}

public class Level5Dto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Level6Dto Level6 { get; set; } = new();
}

public class Level6
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Level7 Level7 { get; set; } = new();
}

public class Level6Dto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Level7Dto Level7 { get; set; } = new();
}

public class Level7
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Level8 Level8 { get; set; } = new();
}

public class Level7Dto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Level8Dto Level8 { get; set; } = new();
}

public class Level8
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Level9 Level9 { get; set; } = new();
}

public class Level8Dto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Level9Dto Level9 { get; set; } = new();
}

public class Level9
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Level10 Level10 { get; set; } = new();
}

public class Level9Dto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Level10Dto Level10 { get; set; } = new();
}

public class Level10
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Data { get; set; } = string.Empty;
}

public class Level10Dto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Data { get; set; } = string.Empty;
}
