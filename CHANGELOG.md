# Changelog

All notable changes to Knot will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Fixed

-   Collection mapping now properly handles empty collections when element type mappings are not registered
-   Improved error messages for missing collection element type mappings

### Planned

-   Circular reference detection
-   Polymorphic mapping support
-   Value resolvers
-   Before/After map actions
-   Conditional mapping

## [1.0.0] - 2025-11-11

### Added

-   **Core Mapping Engine**: Lightweight object-to-object mapping with automatic property matching
-   **Mapping Configuration**: Fluent API for configuring type mappings
-   **Custom Property Mapping**: `ForMember` method for custom property transformations
-   **Property Ignoring**: `Ignore` method to exclude properties from mapping
-   **Mapping Profiles**: Organize related mappings with `Profile` abstract class
-   **Profile Assembly Scanning**: `AddProfiles` method to scan assemblies for profile implementations
-   **Type Converters**: Extensible `TypeConverter` abstract class for custom type conversions
-   **Extension Methods**: Fluent mapping API via `MappingExtensions`
-   **Collection Mapping**: Support for List, Array, and IEnumerable mappings
-   **Static Mapper**: Global `Mapper` class for convenient application-wide usage
-   **Instance Mapper**: `IMapper` interface and implementation for dependency injection
-   **Expression Compilation**: Optimized performance using compiled LINQ expressions
-   **Expression Caching**: `CompiledExpressionCache` for fast repeated mappings
-   **Reflection Helpers**: Cached reflection metadata to minimize overhead
-   **Exception Handling**: Specific exceptions for mapping scenarios:
    -   `MappingException`: General mapping failures
    -   `TypeConversionException`: Type conversion errors
    -   `MissingPropertyException`: Missing property errors
-   **XML Documentation**: Comprehensive XML comments for IntelliSense
-   **NuGet Package**: Published to NuGet.org as `Knot`
-   **.NET Standard 2.0**: Compatible with .NET Framework 4.6.1+, .NET Core 2.0+, and .NET 5+

### Performance

-   Compiled expressions for property access (10-100x faster than reflection)
-   Expression caching to avoid recompilation
-   Minimal allocations during mapping operations
-   Optimized for millions of mappings per second

### Documentation

-   Comprehensive README with examples
-   Quick start guide
-   API documentation
-   Architecture documentation
-   Contributing guidelines
-   Code of conduct
-   Security policy
-   MIT License
-   **Sample Projects**: 5 complete, runnable examples
    -   BasicMapping: Introduction to core functionality
    -   CustomPropertyMapping: Advanced ForMember configuration
    -   CollectionMapping: List, Array, and IEnumerable mapping
    -   MappingProfiles: Organizing mappings with Profile classes
    -   NestedObjects: Complex object graphs and deep nesting

### Benchmarks

-   `Knot.Benchmarks` project with performance benchmarks
-   Comparison with reflection-based approaches
-   Memory allocation analysis

### CI/CD

-   GitHub Actions workflow for automated builds
-   Code quality analysis with CodeQL
-   Automated NuGet publishing on release

---

## Version History

-   **1.0.0** (2025-11-11): Initial public release

## Upgrade Guides

### From 0.x to 1.0

Version 1.0.0 is the first public release.

1. Install from NuGet: `dotnet add package Knot`
2. Update namespace imports to `using Knot;`
3. Review the configuration API
4. Check the [Migration Guide](docs/guides/migration-guide.md) for detailed instructions (if applicable)

## Release Process

1. **Version Update**: Update version in `Knot.Core.csproj` and `Directory.Build.props`
2. **Changelog**: Add entry to this file under [Unreleased]
3. **Build**: `dotnet build --configuration Release`
4. **Pack**: `dotnet pack --configuration Release`
5. **Tag**: Create Git tag `git tag v1.0.0`
6. **GitHub Release**: Create release on GitHub
7. **Publish**: Push to NuGet.org (automated via GitHub Actions)

## Support

-   **Bug Reports**: [GitHub Issues](https://github.com/dipjyotisikder/Knot/issues)
-   **Documentation**: [docs/](docs/)

---

[Unreleased]: https://github.com/dipjyotisikder/Knot/compare/v1.0.0...HEAD
[1.0.0]: https://github.com/dipjyotisikder/Knot/releases/tag/v1.0.0
