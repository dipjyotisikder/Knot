# Custom Property Mapping Example

This sample demonstrates advanced property mapping techniques using Knot's `ForMember` configuration.

## Features Demonstrated

-   Custom property transformations
-   Combining multiple source properties
-   Computed properties and calculations
-   Property ignoring for security
-   Conditional mapping logic
-   String formatting and currency display

## Key Concepts

### ForMember Configuration

```csharp
map.ForMember(dest => dest.FullName,
    src => $"{src.FirstName} {src.LastName}");
```

Map destination properties with custom logic instead of simple property matching.

### Property Ignoring

```csharp
map.Ignore(dest => dest.InternalNotes);
```

Explicitly exclude sensitive or unnecessary properties from mapping.

### Computed Properties

```csharp
map.ForMember(dest => dest.YearsOfService,
    src => DateTime.Now.Year - src.HireDate.Year);
```

Calculate values during mapping based on source data.

## Running the Sample

```bash
cd samples/CustomPropertyMapping
dotnet run
```

Or build all samples:

```bash
cd samples
dotnet build Knot.Samples.sln
```

## Use Cases

This pattern is essential for:

-   **API Responses**: Format data for client consumption
-   **Data Transformation**: Convert between incompatible structures
-   **Security**: Hide sensitive information
-   **Business Logic**: Apply calculations during mapping
-   **Display Formatting**: Prepare data for presentation

## Next Steps

-   [Collection Mapping](../CollectionMapping/) - Map lists and arrays
-   [Mapping Profiles](../MappingProfiles/) - Organize configurations
-   [Main Documentation](../../README.md)
