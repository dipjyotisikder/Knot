# Knot

A lightweight and efficient object-to-object mapping library for .NET Standard 2.0 and above. Knot simplifies the mapping of properties between different types with minimal configuration.

![NuGet Version](https://img.shields.io/nuget/v/Knot)
![NuGet Downloads](https://img.shields.io/nuget/dt/Knot)
![License](https://img.shields.io/github/license/dipjyotisikder/Knot)

---

## Table of Contents

- [Overview](#overview)
  - [What is Knot?](#what-is-knot)
  - [Why Choose Knot?](#why-choose-knot)
  - [Key Features](#key-features)
- [Getting Started](#getting-started)
  - [Installation](#installation)
  - [Quick Start](#quick-start)
  - [Your First Mapping](#your-first-mapping)
- [Core Concepts](#core-concepts)
  - [Mapper Configuration](#mapper-configuration)
  - [Creating Mappers](#creating-mappers)
  - [Mapping Operations](#mapping-operations)
- [Feature Guide](#feature-guide)
  - [Simple Mapping](#1-simple-mapping)
  - [Custom Property Mapping](#2-custom-property-mapping)
  - [Collection Mapping](#3-collection-mapping)
  - [Mapping Profiles](#4-mapping-profiles)
  - [Type Converters](#5-type-converters)
  - [Extension Methods](#6-extension-methods)
- [Advanced Usage](#advanced-usage)
  - [Nested Object Mapping](#nested-object-mapping)
  - [Conditional Mapping](#conditional-mapping)
  - [Bidirectional Mapping](#bidirectional-mapping)
  - [Assembly Scanning](#assembly-scanning)
- [API Reference](#api-reference)
  - [MapperConfiguration](#mapperconfiguration)
  - [IMapper Interface](#imapper-interface)
  - [Profile Class](#profile-class)
  - [TypeConverter Class](#typeconverter-class)
- [Real-World Examples](#real-world-examples)
- [Error Handling](#error-handling)
- [Performance](#performance)
- [Best Practices](#best-practices)
- [Troubleshooting](#troubleshooting)
- [FAQ](#faq)
- [Contributing](#contributing)
- [Support](#support)
- [Changelog](#changelog)
- [License](#license)

---

## Overview

### What is Knot?

Knot is a simple yet powerful mapping library that helps you transform objects from one type to another. It is perfect for scenarios where you need to convert between entities and DTOs, or any other object transformations in your application.

Think of Knot as a bridge that connects two different object types and transfers data between them automatically, saving you from writing repetitive mapping code.

### Why Choose Knot?

| Feature | Description |
|---------|-------------|
| **Simple to Use** | Get started with just a few lines of code |
| **Lightweight** | Minimal overhead and zero external dependencies |
| **Fast** | Optimized for performance with expression compilation |
| **Flexible** | Supports custom mappings and converters |
| **Type-Safe** | Compile-time type checking for your mappings |
| **.NET Standard 2.0** | Works with .NET Framework, .NET Core, and modern .NET |

### Key Features

- Automatic property mapping by convention (matching property names)
- Custom property mapping with `ForMember` configuration
- Property ignoring with `Ignore` method
- Collection mapping (List, Array, IEnumerable)
- Nested object mapping for complex object graphs
- Mapping profiles for organized configuration
- Custom type converters for special transformations
- Bidirectional mapping support
- Assembly scanning for automatic profile discovery
- Expression compilation for optimal performance
- Fluent extension methods for convenient usage

---

## Getting Started

### Installation

Install Knot via NuGet Package Manager:

**Using .NET CLI:**
```bash
dotnet add package Knot
```

**Using Package Manager Console:**
```powershell
Install-Package Knot
```

**Using Visual Studio:**
1. Right-click on your project
2. Select "Manage NuGet Packages"
3. Search for "Knot"
4. Click "Install"

### Quick Start

Here's the absolute simplest way to get started with Knot in 30 seconds:

```csharp
using Knot;
using Knot.Configuration;

// 1. Define your models
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

// 2. Configure the mapper (one-time setup)
var config = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<Person, PersonDto>();
});

// 3. Create the mapper instance
var mapper = config.CreateMapper();

// 4. Use it!
var person = new Person
{
    FirstName = "John",
    LastName = "Doe",
    Age = 30
};

var personDto = mapper.Map<PersonDto>(person);
// personDto.FirstName == "John"
// personDto.LastName == "Doe"
// personDto.Age == 30
```

### Your First Mapping

Let's walk through a complete example step by step:

**Step 1: Create Source and Destination Classes**

```csharp
// Your database entity
public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public decimal Salary { get; set; }
    public DateTime HireDate { get; set; }
}

// Your API response DTO
public class EmployeeDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    // Notice: Salary is omitted for security
}
```

**Step 2: Configure the Mapping**

```csharp
var config = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<Employee, EmployeeDto>();
    // That's it! Knot automatically maps properties with matching names
});
```

**Step 3: Create and Use the Mapper**

```csharp
var mapper = config.CreateMapper();

var employee = new Employee
{
    Id = 101,
    FirstName = "Jane",
    LastName = "Smith",
    Email = "jane.smith@company.com",
    Salary = 75000m,
    HireDate = DateTime.Now
};

var employeeDto = mapper.Map<EmployeeDto>(employee);
// employeeDto contains: Id, FirstName, LastName, Email
// Salary and HireDate are automatically excluded (not in destination)
```

---

## Core Concepts

### Mapper Configuration

The `MapperConfiguration` class is the heart of Knot. It defines how objects should be mapped.

#### Basic Configuration

```csharp
var config = new MapperConfiguration(cfg =>
{
    // Simple mapping - properties with matching names are mapped automatically
    cfg.CreateMap<Source, Destination>();
    
    // Multiple mappings
    cfg.CreateMap<User, UserDto>();
    cfg.CreateMap<Order, OrderDto>();
    cfg.CreateMap<Product, ProductDto>();
});
```

#### Configuration with Custom Rules

```csharp
var config = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<Person, PersonDto>(map =>
    {
 // Custom property mapping
 map.ForMember(dest => dest.FullName, 
     src => $"{src.FirstName} {src.LastName}");
   
        // Ignore a property
        map.Ignore(dest => dest.InternalId);
    });
});
```

### Creating Mappers

Knot offers two approaches for creating and using mappers:

#### Approach 1: Instance-Based Mapper (Recommended)

This approach gives you **full control** and is ideal for **dependency injection**:

```csharp
// Configure once
var config = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<Source, Destination>();
});

// Create mapper instance
var mapper = config.CreateMapper();

// Use the instance
var result = mapper.Map<Destination>(source);
```

**Benefits:**
- Testable (can inject mock mappers)
- Thread-safe
- Multiple configurations possible
- Perfect for ASP.NET Core DI

**ASP.NET Core Example:**

```csharp
// Startup.cs or Program.cs
public void ConfigureServices(IServiceCollection services)
{
    var config = new MapperConfiguration(cfg =>
    {
        cfg.AddProfiles(typeof(Startup).Assembly);
    });
    
    services.AddSingleton(config.CreateMapper());
}

// Controller
public class UsersController : ControllerBase
{
    private readonly IMapper _mapper;
    
    public UsersController(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    public IActionResult GetUser(int id)
 {
        var user = _userService.GetById(id);
      var userDto = _mapper.Map<UserDto>(user);
        return Ok(userDto);
    }
}
```

#### Approach 2: Global Static Mapper

Initialize once and use **anywhere** in your application:

```csharp
// Initialize once at application startup
Mapper.Initialize(cfg =>
{
    cfg.CreateMap<Source, Destination>();
});

// Use anywhere in your code
var result = Mapper.Map<Destination>(source);
```

**Benefits:**
- Convenient for quick prototypes
- No need to pass mapper instances
- Simple console applications

**Warning:** Static mapper is shared globally. Not recommended for applications with different mapping configurations.

### Mapping Operations

Knot provides multiple ways to perform mapping operations:

#### 1. Map to New Instance

Creates a new destination object:

```csharp
var destination = mapper.Map<Destination>(source);
```

#### 2. Map with Explicit Types

Specify both source and destination types:

```csharp
var destination = mapper.Map<Source, Destination>(source);
```

#### 3. Map to Existing Instance

Update an existing object (useful for partial updates):

```csharp
var existingDestination = new Destination { Id = 1 };
mapper.Map(source, existingDestination);
// existingDestination is now updated with values from source
```

**Complete Example:**

```csharp
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
}

public class ProductUpdateDto
{
    public string Name { get; set; }
    public decimal Price { get; set; }
}

// Configure
Mapper.Initialize(cfg =>
{
    cfg.CreateMap<ProductUpdateDto, Product>();
});

// Usage: Update existing product without changing Id or StockQuantity
var product = _repository.GetById(1); // { Id = 1, StockQuantity = 50 }
var updateDto = new ProductUpdateDto { Name = "New Name", Price = 29.99m };

Mapper.Map(updateDto, product);
// product now has: Id = 1, Name = "New Name", Price = 29.99, StockQuantity = 50
```

---

## Feature Guide

### 1. Simple Mapping

**Convention-based mapping** - Knot automatically maps properties with matching names and compatible types.

```csharp
public class BlogPost
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime PublishedDate { get; set; }
    public string AuthorName { get; set; }
}

public class BlogPostDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string AuthorName { get; set; }
}

// Configuration
var config = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<BlogPost, BlogPostDto>();
    // Id, Title, and AuthorName are automatically mapped
    // Content and PublishedDate are ignored (not in destination)
});

// Usage
var post = new BlogPost
{
    Id = 1,
    Title = "Understanding Knot",
    Content = "Very long content...",
    PublishedDate = DateTime.Now,
    AuthorName = "John Doe"
};

var dto = mapper.Map<BlogPostDto>(post);
// dto.Id == 1
// dto.Title == "Understanding Knot"
// dto.AuthorName == "John Doe"
```

### 2. Custom Property Mapping

Use `ForMember` to map properties with different names or apply transformations:

```csharp
public class Customer
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
  public DateTime DateOfBirth { get; set; }
    public string Email { get; set; }
}

public class CustomerDto
{
    public string FullName { get; set; }
    public int Age { get; set; }
 public string ContactEmail { get; set; }
}

var config = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<Customer, CustomerDto>(map =>
  {
        // Combine properties
        map.ForMember(dest => dest.FullName, 
      src => $"{src.FirstName} {src.LastName}");
        
        // Calculate age
        map.ForMember(dest => dest.Age, 
            src => DateTime.Now.Year - src.DateOfBirth.Year);
        
  // Map from different property name
  map.ForMember(dest => dest.ContactEmail, 
            src => src.Email);
    });
});
```

**Ignore Properties:**

```csharp
public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string InternalToken { get; set; } // Don't map this
}

var config = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<User, UserDto>(map =>
    {
        map.Ignore(dest => dest.InternalToken);
    });
});
```

### 3. Collection Mapping

Map collections effortlessly with built-in extension methods:

```csharp
using Knot.Extensions;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
}

public class BookDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
}

// Configure mapping
Mapper.Initialize(cfg =>
{
    cfg.CreateMap<Book, BookDto>();
});

// Sample data
var books = new List<Book>
{
    new Book { Id = 1, Title = "1984", Author = "George Orwell" },
    new Book { Id = 2, Title = "Brave New World", Author = "Aldous Huxley" },
    new Book { Id = 3, Title = "Fahrenheit 451", Author = "Ray Bradbury" }
};

// Map to List<T>
List<BookDto> bookDtoList = books.MapToList<Book, BookDto>(mapper);

// Map to Array
BookDto[] bookDtoArray = books.MapToArray<Book, BookDto>(mapper);

// Map to IEnumerable<T>
IEnumerable<BookDto> bookDtoEnumerable = books.MapTo<Book, BookDto>(mapper);
```

**Works with any IEnumerable:**

```csharp
// Arrays
Book[] bookArray = GetBooks();
var dtoList = bookArray.MapToList<Book, BookDto>(mapper);

// HashSet
HashSet<Book> bookSet = GetBookSet();
var dtoList = bookSet.MapToList<Book, BookDto>(mapper);

// Custom collections
IEnumerable<Book> bookQuery = GetBooksQuery();
var dtoArray = bookQuery.MapToArray<Book, BookDto>(mapper);
```

### 4. Mapping Profiles

**Profiles** help you organize mapping configurations, especially in large applications.

#### Creating a Profile

```csharp
using Knot.Configuration;

public class UserMappingProfile : Profile
{
    protected internal override void Configure()
    {
        // User to DTO
      CreateMap<User, UserDto>(map =>
        {
  map.ForMember(dest => dest.FullName, 
     src => $"{src.FirstName} {src.LastName}");
        map.Ignore(dest => dest.TempData);
        });
      
  // DTO to User (reverse mapping)
        CreateMap<UserDto, User>(map =>
        {
            map.Ignore(dest => dest.CreatedDate);
      map.Ignore(dest => dest.ModifiedDate);
        });
     
        // UserRole mappings
        CreateMap<UserRole, UserRoleDto>();
      CreateMap<UserRoleDto, UserRole>();
    }
}
```

#### Registering Profiles

**Single Profile:**

```csharp
var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<UserMappingProfile>();
});
```

**Multiple Profiles:**

```csharp
var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<UserMappingProfile>();
    cfg.AddProfile<ProductMappingProfile>();
    cfg.AddProfile<OrderMappingProfile>();
});
```

**Assembly Scanning (Recommended):**

```csharp
var config = new MapperConfiguration(cfg =>
{
    // Automatically finds and registers all Profile classes
    cfg.AddProfiles(typeof(UserMappingProfile).Assembly);
});
```

#### Real-World Profile Structure

```
YourProject/
├── Mappings/
│   ├── UserMappingProfile.cs
│   ├── ProductMappingProfile.cs
│   ├── OrderMappingProfile.cs
│   └── CustomerMappingProfile.cs
└── Startup.cs
```

**Startup.cs:**

```csharp
public void ConfigureServices(IServiceCollection services)
{
    var config = new MapperConfiguration(cfg =>
    {
      cfg.AddProfiles(typeof(Startup).Assembly);
    });
    
    services.AddSingleton(config.CreateMapper());
}
```

### 5. Type Converters

**Type converters** allow you to define custom logic for converting between incompatible types.

#### Creating a Type Converter

```csharp
using Knot.Converters;

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
      return 0; // Default value
    }
}
```

#### Registering a Converter

```csharp
var config = new MapperConfiguration(cfg =>
{
    cfg.AddConverter<StringToIntConverter>();
    cfg.CreateMap<StringModel, IntModel>();
});
```

#### Advanced Converter Examples

**Date Format Converter:**

```csharp
public class DateTimeToStringConverter : TypeConverter
{
    public override bool CanConvert(Type sourceType, Type destinationType)
    {
        return sourceType == typeof(DateTime) && destinationType == typeof(string);
    }

    public override object Convert(object source, Type destinationType)
    {
        if (source is DateTime dateTime)
        {
      return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }
        return string.Empty;
    }
}
```

**Enum to String Converter:**

```csharp
public class EnumToStringConverter : TypeConverter
{
    public override bool CanConvert(Type sourceType, Type destinationType)
    {
        return sourceType.IsEnum && destinationType == typeof(string);
    }

    public override object Convert(object source, Type destinationType)
    {
        return source?.ToString() ?? string.Empty;
    }
}
```

**Complex Object Converter:**

```csharp
public class AddressToStringConverter : TypeConverter
{
    public override bool CanConvert(Type sourceType, Type destinationType)
    {
        return sourceType == typeof(Address) && destinationType == typeof(string);
    }

    public override object Convert(object source, Type destinationType)
    {
        if (source is Address address)
    {
  return $"{address.Street}, {address.City}, {address.State} {address.ZipCode}";
        }
        return string.Empty;
 }
}
```

### 6. Extension Methods

Knot provides **fluent extension methods** for more natural mapping syntax:

```csharp
using Knot.Extensions;
```

#### Single Object Mapping

```csharp
// Map to new instance
var dto = person.MapTo<PersonDto>(mapper);

// Map to existing instance
person.MapTo(existingDto, mapper);
```

#### Collection Mapping

```csharp
var people = GetPeople();

// To List
var dtoList = people.MapToList<Person, PersonDto>(mapper);

// To Array
var dtoArray = people.MapToArray<Person, PersonDto>(mapper);

// To IEnumerable
var dtoEnumerable = people.MapTo<Person, PersonDto>(mapper);
```

#### Chaining with LINQ

```csharp
var activeDtos = users
 .Where(u => u.IsActive)
    .OrderBy(u => u.LastName)
    .MapToList<User, UserDto>(mapper);
```

---

## Advanced Usage

### Nested Object Mapping

Knot automatically handles nested objects when mappings are configured:

```csharp
public class Order
{
  public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public Customer Customer { get; set; }
    public List<OrderItem> Items { get; set; }
}

public class OrderDto
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public CustomerDto Customer { get; set; }
    public List<OrderItemDto> Items { get; set; }
}

// Configure all mappings
var config = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<Order, OrderDto>();
    cfg.CreateMap<Customer, CustomerDto>();
    cfg.CreateMap<OrderItem, OrderItemDto>();
});

// Map the entire object graph
var order = GetOrder();
var orderDto = mapper.Map<OrderDto>(order);
// orderDto.Customer and orderDto.Items are automatically mapped
```

### Conditional Mapping

Apply mapping logic based on conditions:

```csharp
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public bool IsDiscounted { get; set; }
    public decimal DiscountPercentage { get; set; }
}

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal FinalPrice { get; set; }
}

var config = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<Product, ProductDto>(map =>
    {
        map.ForMember(dest => dest.FinalPrice, src =>
        {
      if (src.IsDiscounted)
      {
                return src.Price * (1 - src.DiscountPercentage / 100);
     }
  return src.Price;
        });
    });
});
```

### Bidirectional Mapping

Create mappings in both directions:

```csharp
public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; }
public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime HireDate { get; set; }
    public decimal Salary { get; set; }
}

public class EmployeeDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
  public string Email { get; set; }
}

public class EmployeeMappingProfile : Profile
{
    protected internal override void Configure()
    {
        // Forward: Entity → DTO
 CreateMap<Employee, EmployeeDto>(map =>
        {
      map.ForMember(dest => dest.FullName, 
     src => $"{src.FirstName} {src.LastName}");
        });
    
        // Reverse: DTO → Entity
        CreateMap<EmployeeDto, Employee>(map =>
  {
    map.ForMember(dest => dest.FirstName, src =>
            {
                var parts = src.FullName?.Split(' ');
 return parts?[0] ?? string.Empty;
            });
            
      map.ForMember(dest => dest.LastName, src =>
     {
      var parts = src.FullName?.Split(' ');
        return parts?.Length > 1 ? parts[1] : string.Empty;
  });
    
            // Ignore fields that shouldn't be updated
 map.Ignore(dest => dest.HireDate);
            map.Ignore(dest => dest.Salary);
        });
    }
}
```

### Assembly Scanning

Automatically discover and register all profiles in an assembly:

```csharp
// Scan current assembly
var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfiles(typeof(Program).Assembly);
});

// Scan multiple assemblies
var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfiles(
        typeof(DomainProfile).Assembly,
        typeof(ApiProfile).Assembly,
        typeof(InfrastructureProfile).Assembly
    );
});

// Scan all loaded assemblies
var config = new MapperConfiguration(cfg =>
{
    var assemblies = AppDomain.CurrentDomain.GetAssemblies();
    cfg.AddProfiles(assemblies);
});
```

---

## API Reference

### MapperConfiguration

The main configuration class for defining mappings.

#### Constructor

```csharp
public MapperConfiguration(Action<IMapperConfigurationExpression> configure)
```

#### Methods

| Method | Description | Example |
|--------|-------------|---------|
| `CreateMapper()` | Creates an IMapper instance | `var mapper = config.CreateMapper();` |
| `CreateMap<TSource, TDestination>()` | Defines a mapping between two types | `cfg.CreateMap<User, UserDto>();` |
| `CreateMap<TSource, TDestination>(Action<ITypeMap<TSource, TDestination>>)` | Defines a mapping with custom configuration | `cfg.CreateMap<User, UserDto>(map => { ... });` |
| `AddProfile<TProfile>()` | Registers a mapping profile | `cfg.AddProfile<UserProfile>();` |
| `AddProfiles(params Assembly[])` | Scans assemblies for profiles | `cfg.AddProfiles(assembly);` |
| `AddConverter<TConverter>()` | Registers a type converter | `cfg.AddConverter<StringToIntConverter>();` |

### IMapper Interface

The interface for performing mapping operations.

#### Methods

| Method | Description | Example |
|--------|-------------|---------|
| `TDestination Map<TDestination>(object source)` | Maps source to new destination | `var dto = mapper.Map<UserDto>(user);` |
| `TDestination Map<TSource, TDestination>(TSource source)` | Maps with explicit types | `var dto = mapper.Map<User, UserDto>(user);` |
| `void Map<TSource, TDestination>(TSource source, TDestination destination)` | Maps to existing instance | `mapper.Map(user, existingDto);` |

### Profile Class

Base class for organizing mapping configurations.

#### Methods

```csharp
public abstract class Profile
{
    protected internal abstract void Configure();
    
    protected void CreateMap<TSource, TDestination>();
    
    protected void CreateMap<TSource, TDestination>(
        Action<ITypeMap<TSource, TDestination>> configureMap);
}
```

#### Usage

```csharp
public class MyProfile : Profile
{
    protected internal override void Configure()
    {
        CreateMap<Source, Destination>();
    }
}
```

### TypeConverter Class

Base class for creating custom type converters.

#### Methods

```csharp
public abstract class TypeConverter
{
    public abstract bool CanConvert(Type sourceType, Type destinationType);
  public abstract object Convert(object source, Type destinationType);
}
```

#### Usage

```csharp
public class MyConverter : TypeConverter
{
    public override bool CanConvert(Type sourceType, Type destinationType)
    {
     return sourceType == typeof(string) && destinationType == typeof(int);
    }
 
    public override object Convert(object source, Type destinationType)
    {
        return int.Parse(source.ToString());
    }
}
```

---

## Real-World Examples

### Example 1: ASP.NET Core Web API

**Models:**

```csharp
// Domain/Entities/Product.cs
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsActive { get; set; }
}

// DTOs/ProductDto.cs
public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
 public string CategoryName { get; set; }
}

// DTOs/CreateProductDto.cs
public class CreateProductDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
}
```

**Mapping Profile:**

```csharp
// Mappings/ProductProfile.cs
public class ProductProfile : Profile
{
    protected internal override void Configure()
    {
        CreateMap<Product, ProductDto>(map =>
        {
    map.ForMember(dest => dest.CategoryName, 
                src => src.Category?.Name ?? "Uncategorized");
        });
 
        CreateMap<CreateProductDto, Product>(map =>
        {
            map.Ignore(dest => dest.Id);
        map.Ignore(dest => dest.CreatedDate);
         map.Ignore(dest => dest.ModifiedDate);
            map.ForMember(dest => dest.IsActive, src => true);
        });
}
}
```

**Startup Configuration:**

```csharp
// Program.cs (.NET 6+)
var builder = WebApplication.CreateBuilder(args);

// Configure Knot
var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfiles(typeof(Program).Assembly);
});

builder.Services.AddSingleton(mapperConfig.CreateMapper());

builder.Services.AddControllers();
var app = builder.Build();
```

**Controller:**

```csharp
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;
    
    public ProductsController(IProductService productService, IMapper mapper)
    {
        _productService = productService;
      _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
  var products = await _productService.GetAllAsync();
     var productDtos = products.MapToList<Product, ProductDto>(_mapper);
        return Ok(productDtos);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product == null)
            return NotFound();
    
        var productDto = _mapper.Map<ProductDto>(product);
        return Ok(productDto);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductDto createDto)
    {
        var product = _mapper.Map<Product>(createDto);
        product.CreatedDate = DateTime.UtcNow;
        
    var created = await _productService.CreateAsync(product);
        var productDto = _mapper.Map<ProductDto>(created);
        
      return CreatedAtAction(nameof(GetById), new { id = productDto.Id }, productDto);
    }
}
```

### Example 2: Complex Domain Mapping

**Entities:**

```csharp
public class Order
{
  public int Id { get; set; }
    public string OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public List<OrderItem> Items { get; set; }
    public OrderStatus Status { get; set; }
    public decimal TotalAmount { get; set; }
}

public class OrderItem
{
  public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal LineTotal { get; set; }
}

public class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}

public enum OrderStatus
{
    Pending,
    Processing,
    Shipped,
    Delivered,
    Cancelled
}
```

**DTOs:**

```csharp
public class OrderDto
{
    public int Id { get; set; }
    public string OrderNumber { get; set; }
    public string OrderDateFormatted { get; set; }
    public CustomerSummaryDto Customer { get; set; }
    public List<OrderItemDto> Items { get; set; }
    public string Status { get; set; }
    public decimal TotalAmount { get; set; }
    public int TotalItems { get; set; }
}

public class OrderItemDto
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal LineTotal { get; set; }
}

public class CustomerSummaryDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
}
```

**Mapping Profile:**

```csharp
public class OrderProfile : Profile
{
    protected internal override void Configure()
    {
        CreateMap<Order, OrderDto>(map =>
      {
   map.ForMember(dest => dest.OrderDateFormatted, 
 src => src.OrderDate.ToString("MMM dd, yyyy"));
   map.ForMember(dest => dest.Status, 
       src => src.Status.ToString());
            map.ForMember(dest => dest.TotalItems, 
                src => src.Items?.Sum(i => i.Quantity) ?? 0);
   });
        
        CreateMap<OrderItem, OrderItemDto>(map =>
   {
            map.ForMember(dest => dest.ProductName, 
          src => src.Product?.Name ?? "Unknown");
 });
   
        CreateMap<Customer, CustomerSummaryDto>(map =>
        {
 map.ForMember(dest => dest.FullName, 
     src => $"{src.FirstName} {src.LastName}");
   });
    }
}
```

### Example 3: Repository Pattern with Mapping

```csharp
public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> GetByIdAsync(int id);
    Task<TEntity> AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(int id);
}

public class ProductService : IProductService
{
    private readonly IGenericRepository<Product> _repository;
    private readonly IMapper _mapper;
    
    public ProductService(IGenericRepository<Product> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
      var products = await _repository.GetAllAsync();
      return products.MapToList<Product, ProductDto>(_mapper);
    }
    
    public async Task<ProductDto> GetProductByIdAsync(int id)
    {
        var product = await _repository.GetByIdAsync(id);
        return product?.MapTo<ProductDto>(_mapper);
    }
    
    public async Task<ProductDto> CreateProductAsync(CreateProductDto createDto)
    {
   var product = _mapper.Map<Product>(createDto);
        var created = await _repository.AddAsync(product);
    return _mapper.Map<ProductDto>(created);
    }
    
 public async Task UpdateProductAsync(int id, UpdateProductDto updateDto)
    {
        var product = await _repository.GetByIdAsync(id);
      if (product == null)
throw new NotFoundException($"Product {id} not found");
  
     _mapper.Map(updateDto, product);
  await _repository.UpdateAsync(product);
    }
}
```

---

## Error Handling

Knot provides specific exceptions for different error scenarios:

### Exception Types

| Exception | When It's Thrown | How to Handle |
|-----------|------------------|---------------|
| `MappingException` | General mapping errors | Catch and log, check configuration |
| `TypeConversionException` | Type conversion fails | Add custom converter |
| `MissingPropertyException` | Property not found | Use `Ignore()` or fix mapping |

### Exception Handling Examples

**Basic Error Handling:**

```csharp
try
{
    var result = mapper.Map<Destination>(source);
}
catch (MappingException ex)
{
    _logger.LogError(ex, "Mapping failed for {SourceType} to {DestinationType}", 
        source.GetType(), typeof(Destination));
    throw;
}
```

**Detailed Error Handling:**

```csharp
try
{
    var userDto = mapper.Map<UserDto>(user);
}
catch (TypeConversionException ex)
{
    _logger.LogError(ex, "Type conversion failed: {Message}", ex.Message);
    return BadRequest(new { error = "Invalid data type conversion" });
}
catch (MissingPropertyException ex)
{
    _logger.LogError(ex, "Property mapping failed: {Message}", ex.Message);
    return StatusCode(500, new { error = "Internal mapping configuration error" });
}
catch (MappingException ex)
{
 _logger.LogError(ex, "General mapping error: {Message}", ex.Message);
    return StatusCode(500, new { error = "Mapping operation failed" });
}
```

**Graceful Fallback:**

```csharp
public ProductDto MapProduct(Product product)
{
    try
    {
        return _mapper.Map<ProductDto>(product);
    }
    catch (MappingException ex)
    {
        _logger.LogWarning(ex, "Mapping failed, using manual mapping");
        
  // Fallback to manual mapping
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name ?? "Unknown",
            Price = product.Price
        };
    }
}
```

---

## Performance

Knot is designed for **high performance** with minimal overhead.

### Performance Optimizations

| Feature | Benefit |
|---------|---------|
| **Expression Compilation** | Fast property access (near-native speed) |
| **Reflection Caching** | Metadata cached, no repeated lookups |
| **Minimal Allocations** | Optimized memory usage |
| **Lazy Initialization** | Resources created only when needed |

### Performance Characteristics

```csharp
// Knot compiles expressions once and reuses them
// First mapping: ~5-10ms (compilation + execution)
// Subsequent mappings: ~0.01-0.1ms (execution only)

var config = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<Source, Destination>();
});
var mapper = config.CreateMapper(); // Compilation happens here

// Fast
var result1 = mapper.Map<Destination>(source); // ~5ms (first time)
var result2 = mapper.Map<Destination>(source); // ~0.05ms
var result3 = mapper.Map<Destination>(source); // ~0.05ms
```

### Benchmarking

For detailed benchmarking results, check the `Knot.Benchmarks` project:

```bash
cd Knot.Benchmarks
dotnet run -c Release
```

### Best Practices for Performance

1. **Configure Once**: Create mapper configuration at startup
2. **Reuse Mapper**: Don't create new mapper instances per request
3. **Use Profiles**: Organize mappings efficiently
4. **Avoid Dynamic Mapping**: Configure mappings explicitly
5. **Profile Your Code**: Measure before optimizing

---

## Best Practices

### 1. Configuration Organization

**❌ Don't:**
```csharp
// Scattered configuration in controllers
public class UsersController
{
  public IActionResult Get()
  {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<User, UserDto>();
     });
   var mapper = config.CreateMapper();
     // ...
    }
}
```

**✅ Do:**
```csharp
// Centralized configuration at startup
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfiles(typeof(Startup).Assembly);
        });
        services.AddSingleton(config.CreateMapper());
    }
}
```

### 2. Profile Organization

**Structure:**
```
YourProject/
├── Mappings/
│   ├── UserProfile.cs
│   ├── ProductProfile.cs
│   ├── OrderProfile.cs
│   └── ...
```

**Example:**
```csharp
public class UserProfile : Profile
{
    protected internal override void Configure()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();
        CreateMap<UserRole, UserRoleDto>();
    }
}
```

### 3. Testing Mappings

**Unit Test Example:**

```csharp
public class UserProfileTests
{
    private readonly IMapper _mapper;
    
    public UserProfileTests()
    {
    var config = new MapperConfiguration(cfg =>
   {
 cfg.AddProfile<UserProfile>();
 });
        _mapper = config.CreateMapper();
    }
    
    [Fact]
    public void Should_Map_User_To_UserDto()
    {
        // Arrange
        var user = new User
        {
    Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Email = "john@example.com"
    };
        
        // Act
        var userDto = _mapper.Map<UserDto>(user);
  
        // Assert
        Assert.Equal(user.Id, userDto.Id);
        Assert.Equal("John Doe", userDto.FullName);
     Assert.Equal(user.Email, userDto.Email);
    }
}
```

### 4. Dependency Injection

**ASP.NET Core:**

```csharp
// Program.cs
var builder = WebApplication.CreateBuilder(args);

var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfiles(typeof(Program).Assembly);
});

builder.Services.AddSingleton(mapperConfig.CreateMapper());
```

**Constructor Injection:**

```csharp
public class ProductService
{
    private readonly IMapper _mapper;
    
    public ProductService(IMapper mapper)
    {
        _mapper = mapper;
    }
}
```

### 5. Naming Conventions

- **Entities**: `User`, `Product`, `Order`
- **DTOs**: `UserDto`, `ProductDto`, `OrderDto`
- **Create DTOs**: `CreateUserDto`, `CreateProductDto`
- **Update DTOs**: `UpdateUserDto`, `UpdateProductDto`
- **Profiles**: `UserProfile`, `ProductProfile`

---

## Troubleshooting

### Common Issues and Resolutions

#### Issue 1: Property Name Mismatch

**Problem:**
```csharp
// Source property: FirstName, Destination property: first_name
public class Source { public string FirstName { get; set; } }
public class Dest { public string first_name { get; set; } }
```

**Resolution:**
```csharp
cfg.CreateMap<Source, Dest>(map =>
{
    map.ForMember(dest => dest.first_name, src => src.FirstName);
});
```

#### Issue 2: Null Reference Handling

**Problem:**
```csharp
// Nested object null causes exception
var orderDto = mapper.Map<OrderDto>(order); // order.Customer is null
```

**Resolution:**
```csharp
cfg.CreateMap<Order, OrderDto>(map =>
{
    map.ForMember(dest => dest.CustomerName, 
     src => src.Customer?.Name ?? "Guest");
});
```

#### Issue 3: Incomplete Collection Mapping

**Problem:**
```csharp
// Missing item-level mapping configuration
cfg.CreateMap<Order, OrderDto>();
// OrderItem to OrderItemDto mapping not configured
```

**Resolution:**
```csharp
cfg.CreateMap<Order, OrderDto>();
cfg.CreateMap<OrderItem, OrderItemDto>(); // Required for collection items
```

#### Issue 4: Type Conversion Failure

**Problem:**
```csharp
// Incompatible type conversion
public class Source { public string Age { get; set; } }
public class Dest { public int Age { get; set; } }
```

**Resolution Option 1 - Custom Converter:**
```csharp
cfg.AddConverter<StringToIntConverter>();
```

**Resolution Option 2 - ForMember Configuration:**
```csharp
cfg.CreateMap<Source, Dest>(map =>
{
    map.ForMember(dest => dest.Age, 
      src => int.TryParse(src.Age, out var age) ? age : 0);
});
```

---

## FAQ

### General Questions

**Q: What is the licensing model for Knot?**  
A: Knot is open-source software distributed under the MIT License, permitting free commercial and non-commercial use.

**Q: Which .NET versions are supported?**  
A: Knot targets .NET Standard 2.0, providing compatibility with .NET Framework 4.6.1+, .NET Core 2.0+, and .NET 5+.

**Q: Is Knot thread-safe?**  
A: Yes, mapper instances are fully thread-safe and designed for concurrent usage.

**Q: How does Knot compare to AutoMapper?**  
A: Knot prioritizes simplicity and minimal dependencies, while AutoMapper offers a broader feature set with increased complexity.

### Technical Questions

**Q: Does Knot support private property mapping?**  
A: No, Knot exclusively maps public properties.

**Q: Can Knot handle recursive object structures?**  
A: Yes, provided all types in the object graph have configured mappings.

**Q: Is mapping to interface types supported?**  
A: No, destination types must be concrete instantiable classes.

**Q: How are nullable types handled?**  
A: Knot automatically manages nullable type conversions.

**Q: Is Knot compatible with Entity Framework Core?**  
A: Yes, Knot integrates seamlessly with Entity Framework Core and other ORMs.

---

## Contributing

Contributions to Knot are welcome and encouraged. The following outlines how to contribute effectively:

### Contribution Methods

- **Bug Reports**: Submit detailed issue reports with reproduction steps
- **Feature Requests**: Propose enhancements with clear use cases
- **Documentation**: Improve documentation clarity and completeness
- **Code Contributions**: Submit pull requests for bug fixes or features

### Development Environment Setup

```bash
# Clone the repository
git clone https://github.com/dipjyotisikder/Knot.git

# Navigate to project directory
cd Knot

# Restore NuGet dependencies
dotnet restore

# Build the solution
dotnet build

# Execute test suite
dotnet test
```

### Contribution Guidelines

1. Fork the repository to your GitHub account
2. Create a feature branch (`git checkout -b feature/enhancement-name`)
3. Implement changes with appropriate tests
4. Commit changes with descriptive messages (`git commit -m 'Add feature description'`)
5. Push to your fork (`git push origin feature/enhancement-name`)
6. Submit a pull request with detailed description

For substantial changes, please open an issue first to discuss the proposed modifications.

---

## Support

### Obtaining Assistance

If you encounter issues or require clarification:

- **Documentation**: Consult this comprehensive README
- **Issue Tracker**: [Submit issues on GitHub](https://github.com/dipjyotisikder/Knot/issues)
- **Discussions**: Review existing issues and community discussions
- **Contact**: Reach out to the project maintainer

### Issue Reporting Guidelines

When submitting issue reports, include:

1. Knot library version
2. Target .NET version
3. Minimal reproducible code sample
4. Expected behavior description
5. Actual observed behavior
6. Complete exception stack trace (if applicable)

---

## Changelog

### Version 1.0.0

**Initial Release** - January 2024

#### Core Features
- Expression-based mapping engine with compiled execution
- Convention-based automatic property mapping
- Custom property mapping via `ForMember` configuration
- Selective property exclusion via `Ignore` method
- Organizational mapping profiles
- Custom type converter infrastructure
- Fluent extension methods for enhanced usability
- Collection mapping (List, Array, IEnumerable)
- Nested object graph mapping
- Dual mapper patterns (instance-based and static)
- Assembly scanning for automatic profile discovery
- Comprehensive exception hierarchy
- Complete XML documentation for IntelliSense

#### Performance Enhancements
- Expression compilation for optimized execution
- Reflection metadata caching
- Minimal memory allocation patterns

#### Documentation
- Comprehensive README with usage examples
- Inline XML documentation
- Real-world implementation scenarios

---

## License

This project is licensed under the MIT License.

```
MIT License

Copyright (c) 2024 Dipjyoti Sikder

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```

See the [LICENSE](LICENSE) file for complete details.

---

## Acknowledgments

- Appreciation to all contributors who have enhanced Knot
- Inspired by the .NET object mapping ecosystem
- Built for the .NET developer community

---

## Additional Resources

- **NuGet Package**: [https://www.nuget.org/packages/Knot](https://www.nuget.org/packages/Knot)
- **Source Repository**: [https://github.com/dipjyotisikder/Knot](https://github.com/dipjyotisikder/Knot)
- **Issue Tracker**: [https://github.com/dipjyotisikder/Knot/issues](https://github.com/dipjyotisikder/Knot/issues)
- **Release Notes**: [https://github.com/dipjyotisikder/Knot/releases](https://github.com/dipjyotisikder/Knot/releases)

---

<div align="center">

**Made with care by [Dipjyoti Sikder](https://github.com/dipjyotisikder)**

If you find this library valuable, please consider starring the repository.

[![GitHub stars](https://img.shields.io/github/stars/dipjyotisikder/Knot?style=social)](https://github.com/dipjyotisikder/Knot/stargazers)
[![GitHub forks](https://img.shields.io/github/forks/dipjyotisikder/Knot?style=social)](https://github.com/dipjyotisikder/Knot/network/members)

</div>
