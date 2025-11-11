# Nested Objects Mapping Example

This sample demonstrates mapping complex object graphs with deep nesting and collections.

## Features Demonstrated

-   Deep object hierarchies
-   Nested collections
-   One-to-many relationships
-   Many-to-many relationships (implicit)
-   Complex business domains
-   Real-world data structures

## Object Graph Structure

```
Company
├── Headquarters (Address)
├── Departments (List)
│   ├── Department
│   │   ├── Manager (Employee)
│   │   └── Employees (List)
│   └── ...
└── ActiveProjects (List)
    └── Project
```

This demonstrates a typical enterprise domain model.

## Configuration Pattern

```csharp
var config = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<Company, CompanyDto>();
    cfg.CreateMap<Department, DepartmentDto>();
    cfg.CreateMap<Employee, EmployeeDto>();
    cfg.CreateMap<Address, AddressDto>();
    cfg.CreateMap<Project, ProjectDto>();
});
```

Configure mappings for each type in the graph. Knot automatically handles the relationships.

## Running the Sample

```bash
cd samples/NestedObjects
dotnet run
```

Or build all samples:

```bash
cd samples
dotnet build Knot.Samples.sln
```

## Key Concepts

### Automatic Traversal

Knot automatically maps nested objects and collections when:

1. All types in the graph have configured mappings
2. Property names match between source and destination
3. Types are compatible or convertible

### Performance Considerations

-   Configure all mappings once at startup
-   Reuse the mapper instance
-   Knot uses compiled expressions for efficient nested mapping
-   No recursive reflection overhead

## Use Cases

Nested mapping is critical for:

-   **API Responses**: Convert rich domain models to DTOs
-   **Domain-Driven Design**: Map aggregates and entities
-   **Database Entities**: Transform ORM objects to view models
-   **Microservices**: Serialize complex structures for transport
-   **Reporting**: Flatten hierarchies for display

## Real-World Scenarios

This pattern handles:

-   E-commerce: Orders with LineItems, Products, Customers
-   CRM: Accounts with Contacts, Opportunities, Activities
-   ERP: Companies with Departments, Employees, Projects
-   Healthcare: Patients with Visits, Prescriptions, Lab Results

## Next Steps

-   [Mapping Profiles](../MappingProfiles/) - Organize complex configurations
-   [Collection Mapping](../CollectionMapping/) - Focus on collections
-   [Main Documentation](../../README.md)
