# Basic Mapping Example

This sample demonstrates the fundamental concepts of object-to-object mapping using Knot.

## What This Sample Covers

-   Simple configuration
-   Mapping objects with matching property names
-   Automatic property matching by convention
-   Mapping to new instances
-   Mapping to existing instances (updates)
-   Automatic exclusion of missing properties

## Running the Sample

```bash
cd samples/BasicMapping
dotnet run
```

## Key Concepts

### 1. Configuration

```csharp
var config = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<Person, PersonDto>();
});
```

This tells Knot how to map between `Person` and `PersonDto`.

### 2. Creating the Mapper

```csharp
var mapper = config.CreateMapper();
```

Creates a reusable mapper instance.

### 3. Performing the Mapping

```csharp
var personDto = mapper.Map<PersonDto>(person);
```

Maps the source object to a new destination instance.

## Property Matching Rules

Knot automatically maps properties when:

1. **Names match exactly** (case-sensitive)
2. **Types are compatible** (or convertible)
3. **Both have public getters/setters**

## Automatic Exclusions

If a property exists in the source but not in the destination, it's automatically ignored. For example:

```csharp
// Employee has: Salary, HireDate
// EmployeeDto doesn't have these
// They are automatically excluded from mapping
```

## Next Steps

After mastering basic mapping, explore:

-   [Custom Property Mapping](../CustomPropertyMapping/) - Map properties with different names
-   [Collection Mapping](../CollectionMapping/) - Map lists and arrays
-   [Mapping Profiles](../MappingProfiles/) - Organize your mappings

## Learn More

-   [Quick Start Guide](../../docs/getting-started/quick-start.md)
-   [Main Documentation](../../README.md)
