# Knot

A lightweight and efficient object-to-object mapping library for .NET Standard 2.0 and above. Knot simplifies the mapping of properties between different types with minimal configuration.

![NuGet Version](https://img.shields.io/nuget/v/Knot)
![NuGet Downloads](https://img.shields.io/nuget/dt/Knot)
![License](https://img.shields.io/github/license/dipjyotisikder/Knot)

## What is Knot?

Knot is a simple mapping library that helps you transform objects from one type to another. It is perfect for scenarios where you need to convert between entities and DTOs, or any other object transformations in your application.

Think of Knot as a bridge that connects two different object types and transfers data between them automatically.

## Why Choose Knot?

- **Simple to Use**: Get started with just a few lines of code
- **Lightweight**: Minimal overhead and dependencies
- **Fast**: Optimized for performance with expression compilation
- **Flexible**: Supports custom mappings and converters
- **Type-Safe**: Compile-time type checking for your mappings
- **.NET Standard 2.0**: Works with .NET Framework, .NET Core, and modern .NET

## Installation

Install Knot via NuGet Package Manager:

```bash
dotnet add package Knot
```

Or using the Package Manager Console:

```bash
Install-Package Knot
```

## Quick Start

Here is the simplest way to get started with Knot:

```csharp
using Knot;
using Knot.Configuration;

// Define your models
public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
}

public class PersonDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
}

// Configure the mapper
var config = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<Person, PersonDto>();
});

// Create the mapper instance
var mapper = config.CreateMapper();

// Map your objects
var person = new Person
{
    FirstName = "John",
    LastName = "Doe",
    Age = 30
};

var personDto = mapper.Map<PersonDto>(person);
```

## Basic Usage

### Creating a Mapper

There are two ways to create and use a mapper in Knot:

#### Option 1: Instance-Based Mapper

This approach gives you full control over the mapper instance:

```csharp
var config = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<Source, Destination>();
});

var mapper = config.CreateMapper();
var result = mapper.Map<Destination>(source);
```

#### Option 2: Global Static Mapper

Initialize once and use anywhere in your application:

```csharp
// Initialize once at application startup
Mapper.Initialize(cfg =>
{
    cfg.CreateMap<Source, Destination>();
});

// Use anywhere
var result = Mapper.Map<Destination>(source);
```

### Mapping Between Objects

Knot provides several ways to map objects:

```csharp
// Map to a new instance
var destination = mapper.Map<Destination>(source);

// Map with explicit types
var destination = mapper.Map<Source, Destination>(source);

// Map to an existing instance
var existingDestination = new Destination();
mapper.Map(source, existingDestination);
```

### Mapping Collections

You can map collections easily using extension methods:

```csharp
using Knot.Extensions;

var people = new List<Person>
{
    new Person { FirstName = "John", LastName = "Doe", Age = 30 },
    new Person { FirstName = "Jane", LastName = "Smith", Age = 25 }
};

// Map to a list
var dtoList = people.MapToList<Person, PersonDto>(mapper);

// Map to an array
var dtoArray = people.MapToArray<Person, PersonDto>(mapper);

// Map to enumerable
var dtoEnumerable = people.MapTo<Person, PersonDto>(mapper);
```

## Advanced Features

### Custom Property Mapping

Sometimes you need to map properties with different names or apply custom logic:

```csharp
var config = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<Person, PersonDto>(map =>
 {
        // Map a calculated property
        map.ForMember(dest => dest.FullName, 
src => $"{src.FirstName} {src.LastName}");
        
        // Ignore a property
   map.Ignore(dest => dest.SomeProperty);
    });
});
```

### Using Profiles

Profiles help you organize your mapping configurations:

```csharp
public class PersonMappingProfile : Profile
{
    protected internal override void Configure()
    {
        CreateMap<Person, PersonDto>(map =>
        {
            map.ForMember(
                dest => dest.FullName, 
                src => $"{src.FirstName} {src.LastName}");
        });
        
        CreateMap<PersonDto, Person>();
    }
}

// Register the profile
var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<PersonMappingProfile>();
});
```

You can also scan assemblies for profiles:

```csharp
var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfiles(typeof(PersonMappingProfile).Assembly);
});
```

### Type Converters

Create custom converters for complex type transformations:

```csharp
public class StringToIntConverter : TypeConverter
{
    public override bool CanConvert(Type sourceType, Type destinationType)
    {
        return sourceType == typeof(string) && destinationType == typeof(int);
    }

    public override object Convert(object source, Type destinationType)
    {
        if (int.TryParse(source?.ToString(), out var result))
        {
            return result;
        }
        return 0;
    }
}

// Register the converter
var config = new MapperConfiguration(cfg =>
{
    cfg.AddConverter<StringToIntConverter>();
});
```

### Extension Methods

Knot provides convenient extension methods for fluent mapping:

```csharp
using Knot.Extensions;

// Map a single object
var dto = person.MapTo<PersonDto>(mapper);

// Map to an existing object
person.MapTo(existingDto, mapper);

// Map collections
var dtoList = people.MapToList<Person, PersonDto>(mapper);
```

## Real-World Examples

### Example 1: Simple Entity to DTO

```csharp
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedDate { get; set; }
}

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

// Configure
Mapper.Initialize(cfg =>
{
    cfg.CreateMap<Product, ProductDto>();
});

// Use
var product = new Product
{
    Id = 1,
    Name = "Laptop",
    Price = 999.99m,
    CreatedDate = DateTime.Now
};

var productDto = Mapper.Map<ProductDto>(product);
```

### Example 2: Complex Mapping with Custom Logic

```csharp
public class Order
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderItem> Items { get; set; }
}

public class OrderDto
{
    public int Id { get; set; }
    public string OrderDateFormatted { get; set; }
    public int ItemCount { get; set; }
}

var config = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<Order, OrderDto>(map =>
    {
        map.ForMember(dest => dest.Id, src => src.OrderId);
        map.ForMember(
            dest => dest.OrderDateFormatted, 
            src => src.OrderDate.ToString("yyyy-MM-dd"));
        map.ForMember(
            dest => dest.ItemCount, 
            src => src.Items?.Count ?? 0);
    });
});
```

### Example 3: Bidirectional Mapping

```csharp
public class UserProfile : Profile
{
    protected internal override void Configure()
    {
        // Forward mapping
        CreateMap<User, UserDto>(map =>
        {
            map.ForMember(
                dest => dest.DisplayName, 
                src => $"{src.FirstName} {src.LastName}");
            });
        
            // Reverse mapping
        CreateMap<UserDto, User>(map =>
        {
            map.Ignore(dest => dest.Id);
            map.Ignore(dest => dest.CreatedDate);
        });
    }
}
```

## Error Handling

Knot provides specific exceptions for different error scenarios:

```csharp
try
{
    var result = mapper.Map<Destination>(source);
}
catch (MappingException ex)
{
    // Handle mapping errors
    Console.WriteLine($"Mapping failed: {ex.Message}");
}
catch (TypeConversionException ex)
{
    // Handle type conversion errors
    Console.WriteLine($"Type conversion failed: {ex.Message}");
}
catch (MissingPropertyException ex)
{
    // Handle missing property errors
    Console.WriteLine($"Property not found: {ex.Message}");
}
```

## Performance

Knot is designed for performance:

- Uses compiled expressions for fast property access
- Caches reflection metadata to avoid repeated lookups
- Minimal allocations during mapping operations
- Optimized collection mapping

For benchmarking results, check the `Knot.Benchmarks` project included in the repository.

## Best Practices

1. **Initialize Once**: Configure your mappings once at application startup
2. **Use Profiles**: Organize related mappings in Profile classes
3. **Reuse Mapper Instance**: Create the mapper once and reuse it throughout your application
4. **Explicit Configuration**: Define mappings explicitly for better maintainability
5. **Test Your Mappings**: Write unit tests to verify your mapping configurations

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request. For major changes, please open an issue first to discuss what you would like to change.

## License

This project is licensed under the MIT License. See the LICENSE file for details.

## Support

If you encounter any issues or have questions:

- Open an issue on [GitHub](https://github.com/dipjyotisikder/Knot/issues)
- Check existing issues for solutions
- Review the examples in the repository

## Changelog

### Version 1.0.0

- Initial release of Knot
- Type mapping with custom property configuration
- Support for mapping profiles
- Custom type converters
- Extension methods for fluent mapping
- Collection mapping support
- Expression compilation for performance
- Comprehensive exception handling

## Acknowledgments

Thank you to all contributors who have helped make Knot better!

---

Made with care by [Dipjyoti Sikder](https://github.com/dipjyotisikder)
