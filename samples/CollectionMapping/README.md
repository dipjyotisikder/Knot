# Collection Mapping Example

This sample demonstrates mapping collections of objects using Knot's extension methods.

## Features Demonstrated

-   List to List mapping
-   List to Array mapping
-   IEnumerable mapping
-   Integration with LINQ queries
-   Empty collection handling
-   Large collection performance

## Key Extension Methods

### MapToList

```csharp
var dtoList = entities.MapToList<Entity, EntityDto>(mapper);
```

Maps any IEnumerable to a strongly-typed List.

### MapToArray

```csharp
var dtoArray = entities.MapToArray<Entity, EntityDto>(mapper);
```

Maps any IEnumerable to a strongly-typed Array.

### LINQ Integration

```csharp
var filtered = entities
    .Where(e => e.Active)
    .OrderBy(e => e.Name)
    .MapToList<Entity, EntityDto>(mapper);
```

Chain LINQ queries before mapping for efficient data processing.

## Running the Sample

```bash
cd samples/CollectionMapping
dotnet run
```

Or build all samples:

```bash
cd samples
dotnet build Knot.Samples.sln
```

## Performance Notes

Knot's collection mapping is optimized for:

-   **Minimal allocations**: Efficient memory usage
-   **Compiled expressions**: Fast property access
-   **Batch processing**: Handles large collections efficiently

The sample demonstrates mapping 10,000 objects to show real-world performance.

## Use Cases

Collection mapping is essential for:

-   **API Responses**: Convert entity lists to DTO lists
-   **Database Queries**: Transform query results for presentation
-   **Batch Operations**: Process multiple records efficiently
-   **Data Export**: Convert collections for reporting
-   **Integration**: Transform data between systems

## Next Steps

-   [Mapping Profiles](../MappingProfiles/) - Organize configurations
-   [Nested Objects](../NestedObjects/) - Handle complex object graphs
-   [Main Documentation](../../README.md)
