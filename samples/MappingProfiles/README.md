# Mapping Profiles Example

This sample demonstrates how to organize mapping configurations using Knot's Profile system for large-scale applications.

## Features Demonstrated

-   Creating mapping profiles
-   Profile registration
-   Multiple profiles in one configuration
-   Domain-driven profile organization
-   Security-conscious mapping
-   Nested object mapping through profiles

## Profile Organization Pattern

```csharp
public class UserMappingProfile : Profile
{
    protected internal override void Configure()
    {
        CreateMap<User, UserDto>(map =>
        {
            map.ForMember(dest => dest.FullName,
                src => $"{src.FirstName} {src.LastName}");
        });
    }
}
```

Profiles encapsulate related mapping configurations for specific domain entities.

## Registration

```csharp
var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<UserMappingProfile>();
    cfg.AddProfile<ProductMappingProfile>();
    cfg.AddProfile<OrderMappingProfile>();
});
```

Register multiple profiles to build complete mapping configurations.

## Running the Sample

```bash
cd samples/MappingProfiles
dotnet run
```

Or build all samples:

```bash
cd samples
dotnet build Knot.Samples.sln
```

## Best Practices

### Organize by Domain

```
Profiles/
├── UserMappingProfile.cs
├── ProductMappingProfile.cs
├── OrderMappingProfile.cs
└── InvoiceMappingProfile.cs
```

Group related mappings together for maintainability.

### Security First

```csharp
map.Ignore(dest => dest.PasswordHash);
map.Ignore(dest => dest.SecurityToken);
```

Always explicitly ignore sensitive properties in profiles.

### Reusability

Profiles can be reused across multiple projects by placing them in shared libraries.

## Use Cases

Profiles are essential for:

-   **Large Applications**: Organize hundreds of mappings
-   **Team Projects**: Clear ownership of mapping logic
-   **Domain-Driven Design**: Align with bounded contexts
-   **Microservices**: Share profiles across services
-   **API Layers**: Separate presentation from domain logic

## Next Steps

-   [Nested Objects](../NestedObjects/) - Handle complex object graphs
-   [Basic Mapping](../BasicMapping/) - Review fundamentals
-   [Main Documentation](../../README.md)
