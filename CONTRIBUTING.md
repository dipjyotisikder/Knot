# Contributing to Knot

First off, thank you for considering contributing to Knot! It's people like you that make Knot such a great tool.

## Table of Contents

-   [Code of Conduct](#code-of-conduct)
-   [How Can I Contribute?](#how-can-i-contribute)
-   [Development Setup](#development-setup)
-   [Pull Request Process](#pull-request-process)
-   [Coding Standards](#coding-standards)
-   [Documentation](#documentation)

## Code of Conduct

This project and everyone participating in it is governed by our [Code of Conduct](CODE_OF_CONDUCT.md). By participating, you are expected to uphold this code.

## How Can I Contribute?

### Reporting Bugs

Before creating bug reports, please check the existing issues to avoid duplicates. When you create a bug report, include as many details as possible:

-   **Use a clear and descriptive title**
-   **Describe the exact steps to reproduce the problem**
-   **Provide specific examples** - Include code samples
-   **Describe the behavior you observed** and what you expected
-   **Include details about your environment**: Knot version, .NET version, OS

### Suggesting Enhancements

Enhancement suggestions are tracked as GitHub issues. When creating an enhancement suggestion:

-   **Use a clear and descriptive title**
-   **Provide a detailed description of the suggested enhancement**
-   **Provide specific examples** to demonstrate the use case
-   **Explain why this enhancement would be useful**

### Your First Code Contribution

Unsure where to begin? Look for issues labeled:

-   `good first issue` - Simple issues perfect for beginners
-   `help wanted` - Issues that need attention

### Pull Requests

1. Fork the repository
2. Create a feature branch from `main`
3. Make your changes
4. Update documentation as needed
5. Submit a pull request

## Development Setup

### Prerequisites

-   .NET 9.0 SDK or later
-   Visual Studio 2022, VS Code, or Rider
-   Git

### Setup Steps

```bash
# Clone your fork
git clone https://github.com/YOUR-USERNAME/Knot.git
cd Knot

# Add upstream remote
git remote add upstream https://github.com/dipjyotisikder/Knot.git

# Restore packages
dotnet restore

# Build the solution
dotnet build
```

### Project Structure

```
Knot/
├── Knot.Core/              # Main library
│   ├── Configuration/      # Mapping configuration
│   ├── Exceptions/         # Custom exceptions
│   ├── Extensions/         # Extension methods
│   ├── Mapping/            # Core mapping logic
│   └── Utilities/          # Helper classes
├── Knot.Benchmarks/        # Performance benchmarks
├── samples/                # Example projects
└── docs/                   # Documentation
```

## Pull Request Process

### Before Submitting

1. **Update your branch** with latest changes from upstream

    ```bash
    git fetch upstream
    git rebase upstream/main
    ```

2. **Run code analysis**

    ```bash
    dotnet build /p:EnforceCodeStyleInBuild=true
    ```

3. **Update documentation** if you've made API changes

### PR Guidelines

-   **Keep PRs focused** - One feature or fix per PR
-   **Write clear commit messages** - Follow conventional commits format
-   **Update CHANGELOG.md** - Add your changes to the unreleased section
-   **Reference issues** - Link related issues in your PR description

### Commit Message Format

We follow the [Conventional Commits](https://www.conventionalcommits.org/) specification:

```
<type>(<scope>): <subject>

<body>

<footer>
```

**Types:**

-   `feat`: New feature
-   `fix`: Bug fix
-   `docs`: Documentation changes
-   `style`: Code style changes (formatting, etc.)
-   `refactor`: Code refactoring
-   `chore`: Build process or tooling changes
-   `perf`: Performance improvements

**Examples:**

```
feat(mapping): add support for polymorphic mapping

fix(config): resolve null reference in profile scanning

docs(readme): update installation instructions
```

## Coding Standards

### C# Style Guide

We follow Microsoft's C# coding conventions with some additions:

#### Naming Conventions

```csharp
// Classes, methods, properties: PascalCase
public class MapperConfiguration
public void CreateMap<TSource, TDestination>()
public string PropertyName { get; set; }

// Interfaces: IPascalCase
public interface IMapper

// Private fields: _camelCase
private readonly IMapper _mapper;

// Local variables, parameters: camelCase
public void Method(string parameterName)
{
    var localVariable = "";
}

// Constants: PascalCase
public const int MaxDepth = 10;
```

#### Code Organization

```csharp
// 1. Using statements
using System;
using System.Linq;

// 2. Namespace
namespace Knot.Configuration
{
    // 3. Class/interface
    public class MapperConfiguration
    {
        // 4. Constants
        private const int DefaultCapacity = 16;

        // 5. Fields
        private readonly MappingRegistry _registry;

        // 6. Constructors
        public MapperConfiguration()
        {
        }

        // 7. Properties
        public bool AllowNullCollections { get; set; }

        // 8. Public methods
        public IMapper CreateMapper()
        {
        }

        // 9. Private methods
        private void ValidateConfiguration()
        {
        }
    }
}
```

#### Code Quality

-   **Keep methods small** - Prefer methods under 20 lines
-   **Single responsibility** - One class/method does one thing
-   **Avoid deep nesting** - Max 3 levels of nesting
-   **Use meaningful names** - Clear, descriptive identifiers
-   **Add XML documentation** - Public APIs must be documented

#### XML Documentation

All public APIs must have XML documentation:

```csharp
/// <summary>
/// Creates a mapping configuration between two types.
/// </summary>
/// <typeparam name="TSource">The source type to map from.</typeparam>
/// <typeparam name="TDestination">The destination type to map to.</typeparam>
/// <returns>A type map configuration for further customization.</returns>
/// <example>
/// <code>
/// cfg.CreateMap&lt;User, UserDto&gt;();
/// </code>
/// </example>
public ITypeMapConfiguration<TSource, TDestination> CreateMap<TSource, TDestination>()
{
    // Implementation
}
```

## Documentation

### Code Documentation

-   **Public APIs**: Must have XML documentation comments
-   **Complex logic**: Add inline comments explaining why, not what
-   **Examples**: Include usage examples in XML docs

### User Documentation

When adding features, update:

1. **README.md** - Add to relevant sections
2. **docs/** - Add detailed guides if applicable
3. **CHANGELOG.md** - Add entry under [Unreleased]
4. **samples/** - Add working examples

### Documentation Style

-   **Be clear and concise**
-   **Provide code examples**
-   **Use proper Markdown formatting**
-   **Include use cases** for new features

## Performance Considerations

-   **Benchmark new features** using Knot.Benchmarks project
-   **Avoid allocations** in hot paths
-   **Cache expensive operations** (reflection, compiled expressions)
-   **Use value types** where appropriate
-   **Profile before optimizing**

### Running Benchmarks

```bash
cd Knot.Benchmarks
dotnet run -c Release
```

## Code Review Process

### What Reviewers Look For

1. **Correctness** - Does the code work as intended?
2. **Performance** - Are there any performance concerns?
3. **Style** - Does it follow our coding standards?
4. **Documentation** - Is it well documented?

### Responding to Feedback

-   **Be open to feedback** - Reviews improve code quality
-   **Ask questions** if feedback is unclear
-   **Make requested changes** or explain why you disagree
-   **Re-request review** after making changes

## Release Process

Maintainers handle releases, but contributors should:

1. **Update CHANGELOG.md** with your changes
2. **Update version** if making breaking changes (discuss with maintainers)
3. **Ensure backwards compatibility** unless explicitly breaking

## Questions?

-   **Open an issue** for general questions
-   **Contact maintainers** for specific concerns or security issues

## Recognition

Contributors will be:

-   Listed in release notes
-   Acknowledged in the project
-   Given proper credit for their work

---

Thank you for contributing to Knot!

Your efforts make this project better for everyone.
