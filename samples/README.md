# Sample Projects for Knot

This directory contains practical examples demonstrating various features and usage patterns of the Knot object mapping library.

## Available Samples

### 1. Basic Mapping (`BasicMapping/`)

Simple object-to-object mapping with automatic property matching.

**Demonstrates:**

-   Basic configuration
-   Simple mapping operations
-   Property matching by convention

### 2. Custom Property Mapping (`CustomPropertyMapping/`)

Advanced mapping with custom property transformations.

**Demonstrates:**

-   `ForMember` configuration
-   Property ignoring
-   Computed properties
-   Property name differences

### 3. Collection Mapping (`CollectionMapping/`)

Mapping collections, lists, and arrays.

**Demonstrates:**

-   List mapping
-   Array mapping
-   IEnumerable mapping
-   Extension method usage

### 4. Mapping Profiles (`MappingProfiles/`)

Organizing mappings using Profile classes.

**Demonstrates:**

-   Creating profiles
-   Profile registration
-   Domain-driven organization
-   Multiple profile management
-   Security-conscious mapping

### 5. Nested Objects (`NestedObjects/`)

Mapping complex object graphs with deep nesting.

**Demonstrates:**

-   Deep object hierarchies
-   Nested collections
-   One-to-many relationships
-   Enterprise domain models
-   Complex business structures

## Running the Samples

Each sample is a standalone console application or web project:

```bash
# Navigate to a sample directory
cd samples/BasicMapping

# Run the sample
dotnet run
```

Build all samples at once:

```bash
cd samples
dotnet build Knot.Samples.sln
```

## Learning Path

Recommended order for learning:

1. **BasicMapping** - Start here to understand fundamentals
2. **CustomPropertyMapping** - Learn advanced configuration
3. **CollectionMapping** - Work with collections
4. **MappingProfiles** - Organize your configurations
5. **NestedObjects** - Deal with complex object graphs

## Sample Structure

Each sample follows this structure:

```
SampleName/
├── Models/           # Domain models
├── DTOs/             # Data Transfer Objects
├── Mappings/         # Mapping profiles (if applicable)
├── Program.cs        # Entry point
├── README.md         # Sample-specific documentation
└── *.csproj          # Project file
```

## Contributing Samples

Have a useful example? Contributions are welcome!

1. Create a new directory under `samples/`
2. Add a clear README.md explaining the sample
3. Keep it focused on one feature or concept
4. Include comments in the code
5. Submit a pull request

## Questions?

-   Check the [main documentation](../docs/)
-   Review the [README](../README.md)
-   Open an [issue](https://github.com/dipjyotisikder/Knot/issues)

---

For more information, refer to the main documentation and examples provided in this repository.
