using System;
using System.Collections.Generic;
using Knot.Configuration;
using Knot.Extensions;

namespace Knot.Samples.MappingProfiles;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("================================================");
        Console.WriteLine("   KNOT MAPPING PROFILES EXAMPLE");
        Console.WriteLine("================================================\n");

        // Configure mapper with multiple profiles
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<UserMappingProfile>();
            cfg.AddProfile<ProductMappingProfile>();
            cfg.AddProfile<OrderMappingProfile>();
        });

        var mapper = config.CreateMapper();

        // Example 1: User Mappings
        Console.WriteLine("EXAMPLE 1: USER PROFILE MAPPINGS");
        Console.WriteLine("-------------------------------------");

        var user = new User
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@company.com",
            PasswordHash = "hashed_password_value",
            CreatedDate = DateTime.Now.AddYears(-2),
            IsActive = true
        };

        var userDto = mapper.Map<UserDto>(user);

        Console.WriteLine($"User ID:            {userDto.Id}");
        Console.WriteLine($"Full Name:          {userDto.FullName}");
        Console.WriteLine($"Email:              {userDto.Email}");
        Console.WriteLine($"Account Status:     {userDto.Status}");
        Console.WriteLine($"Password Hash:      {userDto.PasswordHash ?? "(secured - not mapped)"}");
        Console.WriteLine("Profile Applied:    UserMappingProfile\n");

        // Example 2: Product Mappings
        Console.WriteLine("EXAMPLE 2: PRODUCT PROFILE MAPPINGS");
        Console.WriteLine("-------------------------------------");

        var products = new List<Product>
        {
            new Product { Id = 101, Name = "Gaming Laptop", Price = 1499.99m, CategoryId = 1, Stock = 25 },
            new Product { Id = 102, Name = "Wireless Headset", Price = 79.99m, CategoryId = 2, Stock = 150 },
            new Product { Id = 103, Name = "Mechanical Keyboard", Price = 129.99m, CategoryId = 3, Stock = 0 }
        };

        var productDtos = products.MapToList<Product, ProductListDto>(mapper);

        Console.WriteLine($"Products Count:     {productDtos.Count} items");
        Console.WriteLine("Product Catalog:");
        foreach (var dto in productDtos)
        {
            Console.WriteLine($"  [{dto.Id}] {dto.Name,-25} ${dto.Price,8:F2} | {dto.Availability}");
        }
        Console.WriteLine("Profile Applied:    ProductMappingProfile\n");

        // Example 3: Order Mappings with Nested Objects
        Console.WriteLine("EXAMPLE 3: ORDER PROFILE MAPPINGS");
        Console.WriteLine("-------------------------------------");

        var order = new Order
        {
            Id = 5001,
            OrderNumber = "ORD-2025-5001",
            Customer = user,
            OrderDate = DateTime.Now,
            TotalAmount = 1709.97m,
            Status = OrderStatus.Processing
        };

        var orderDto = mapper.Map<OrderSummaryDto>(order);

        Console.WriteLine($"Order Number:       {orderDto.OrderNumber}");
        Console.WriteLine($"Customer:           {orderDto.CustomerName}");
        Console.WriteLine($"Order Date:         {orderDto.OrderDateFormatted}");
        Console.WriteLine($"Total Amount:       ${orderDto.TotalAmount:F2}");
        Console.WriteLine($"Order Status:       {orderDto.Status}");
        Console.WriteLine("Profile Applied:    OrderMappingProfile (with nested User mapping)\n");

        // Example 4: Assembly Scanning (demonstration)
        Console.WriteLine("PROFILE ORGANIZATION BENEFITS");
        Console.WriteLine("-------------------------------------");

        Console.WriteLine("Configured Profiles:");
        Console.WriteLine("  1. UserMappingProfile       - User entity mappings");
        Console.WriteLine("  2. ProductMappingProfile    - Product entity mappings");
        Console.WriteLine("  3. OrderMappingProfile      - Order entity mappings");
        Console.WriteLine();
        Console.WriteLine("Key Benefits:");
        Console.WriteLine("  - Modular organization by domain entity");
        Console.WriteLine("  - Simplified maintenance and updates");
        Console.WriteLine("  - Reusable across multiple applications");
        Console.WriteLine("  - Enhanced team collaboration");
        Console.WriteLine("  - Clear separation of mapping concerns");
        Console.WriteLine("  - Scalable architecture for large projects\n");

        Console.WriteLine("================================================");
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}

// Mapping Profiles
public class UserMappingProfile : Knot.Configuration.Profile
{
    protected override void Configure()
    {
        // User to DTO
        CreateMap<User, UserDto>(map =>
        {
            map.ForMember(dest => dest.FullName,
                src => $"{src.FirstName} {src.LastName}");
            map.ForMember(dest => dest.Status,
                src => src.IsActive ? "Active" : "Inactive");
            map.Ignore(dest => dest.PasswordHash); // Security: never expose password hash
        });
    }
}

public class ProductMappingProfile : Knot.Configuration.Profile
{
    protected override void Configure()
    {
        // Product to List DTO
        CreateMap<Product, ProductListDto>(map =>
        {
            map.ForMember(dest => dest.Availability,
                src => src.Stock > 0 ? $"{src.Stock} in stock" : "Out of stock");
        });

        // Product to Detail DTO
        CreateMap<Product, ProductDetailDto>();
    }
}

public class OrderMappingProfile : Knot.Configuration.Profile
{
    protected override void Configure()
    {
        // Order to Summary DTO
        CreateMap<Order, OrderSummaryDto>(map =>
        {
            map.ForMember(dest => dest.CustomerName,
                src => $"{src.Customer.FirstName} {src.Customer.LastName}");
            map.ForMember(dest => dest.OrderDateFormatted,
                src => src.OrderDate.ToString("MMMM dd, yyyy"));
            map.ForMember(dest => dest.Status,
                src => src.Status.ToString());
        });
    }
}

// Domain Models
public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public bool IsActive { get; set; }
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public int Stock { get; set; }
}

public class Order
{
    public int Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public User Customer { get; set; } = null!;
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
}

public enum OrderStatus
{
    Pending,
    Processing,
    Shipped,
    Delivered,
    Cancelled
}

// DTOs
public class UserDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PasswordHash { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class ProductListDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Availability { get; set; } = string.Empty;
}

public class ProductDetailDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public int Stock { get; set; }
}

public class OrderSummaryDto
{
    public int Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string OrderDateFormatted { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = string.Empty;
}
