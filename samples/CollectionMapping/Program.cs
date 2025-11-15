using System;
using System.Collections.Generic;
using System.Linq;
using Knot.Configuration;
using Knot.Extensions;

namespace Knot.Samples.CollectionMapping;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("KNOT COLLECTION MAPPING EXAMPLE\n");

        // Configure mappings
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Customer, CustomerDto>();
            cfg.CreateMap<Order, OrderDto>();
            cfg.CreateMap<Product, ProductDto>();
        });

        var mapper = config.CreateMapper();

        // Example 1: List Mapping
        Console.WriteLine("EXAMPLE 1: LIST TO LIST MAPPING\n");

        var customers = new List<Customer>
        {
            new Customer { Id = 1, Name = "Alice Cooper", Email = "alice@example.com", Active = true },
            new Customer { Id = 2, Name = "Bob Smith", Email = "bob@example.com", Active = true },
            new Customer { Id = 3, Name = "Carol White", Email = "carol@example.com", Active = false }
        };

        var customerDtos = customers.MapToList<Customer, CustomerDto>(mapper);

        Console.WriteLine($"Collection Size:    {customerDtos.Count} customers");
        Console.WriteLine("Mapped Results:");
        foreach (var dto in customerDtos)
        {
            Console.WriteLine($"  ID: {dto.Id} | {dto.Name} | {dto.Email} | Active: {dto.Active}");
        }
        Console.WriteLine("Status:             All customers mapped successfully\n");

        // Example 2: Array Mapping
        Console.WriteLine("EXAMPLE 2: LIST TO ARRAY MAPPING\n");

        var orders = new List<Order>
        {
            new Order { Id = 101, OrderNumber = "ORD-2025-001", Amount = 299.99m, Status = "Shipped" },
            new Order { Id = 102, OrderNumber = "ORD-2025-002", Amount = 149.50m, Status = "Processing" },
            new Order { Id = 103, OrderNumber = "ORD-2025-003", Amount = 599.00m, Status = "Delivered" }
        };

        var orderArray = orders.MapToArray<Order, OrderDto>(mapper);

        Console.WriteLine($"Collection Size:    {orderArray.Length} orders");
        Console.WriteLine("Mapped Results:");
        foreach (var dto in orderArray)
        {
            Console.WriteLine($"  {dto.OrderNumber} | Amount: ${dto.Amount:F2} | Status: {dto.Status}");
        }
        Console.WriteLine("Status:             All orders mapped to array successfully\n");

        // Example 3: IEnumerable Mapping with LINQ
        Console.WriteLine("EXAMPLE 3: FILTERED COLLECTION MAPPING\n");

        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Price = 1299.99m, InStock = true },
            new Product { Id = 2, Name = "Mouse", Price = 29.99m, InStock = true },
            new Product { Id = 3, Name = "Keyboard", Price = 89.99m, InStock = false },
            new Product { Id = 4, Name = "Monitor", Price = 399.99m, InStock = true },
            new Product { Id = 5, Name = "Webcam", Price = 79.99m, InStock = false }
        };

        // Filter before mapping
        var inStockProducts = products
            .Where(p => p.InStock)
            .OrderBy(p => p.Price)
            .MapToList<Product, ProductDto>(mapper);

        Console.WriteLine($"Total Products:     {products.Count}");
        Console.WriteLine($"Filtered Results:   {inStockProducts.Count} in-stock products");
        Console.WriteLine("Mapped Results (sorted by price):");
        foreach (var dto in inStockProducts)
        {
            Console.WriteLine($"  {dto.Name,-20} ${dto.Price,8:F2}");
        }
        Console.WriteLine("Status:             Filtered and mapped successfully\n");

        // Example 4: Empty Collection Handling
        Console.WriteLine("EXAMPLE 4: EMPTY COLLECTION HANDLING\n");

        var emptyList = new List<Customer>();
        var emptyResult = emptyList.MapToList<Customer, CustomerDto>(mapper);

        Console.WriteLine($"Input Size:         {emptyList.Count} items");
        Console.WriteLine($"Output Size:        {emptyResult.Count} items");
        Console.WriteLine("Status:             Empty collections handled gracefully without errors\n");

        // Example 5: Large Collection Performance
        Console.WriteLine("EXAMPLE 5: LARGE COLLECTION PERFORMANCE\n");

        var largeCollection = Enumerable.Range(1, 10000)
            .Select(i => new Customer
            {
                Id = i,
                Name = $"Customer {i}",
                Email = $"customer{i}@example.com",
                Active = i % 2 == 0
            })
            .ToList();

        var startTime = DateTime.Now;
        var largeResult = largeCollection.MapToList<Customer, CustomerDto>(mapper);
        var duration = DateTime.Now - startTime;

        Console.WriteLine($"Collection Size:    {largeCollection.Count:N0} items");
        Console.WriteLine($"Execution Time:     {duration.TotalMilliseconds:F2} ms");
        Console.WriteLine($"Average per Item:   {duration.TotalMilliseconds / 10000:F4} ms");
        Console.WriteLine($"Throughput:         {10000 / duration.TotalSeconds:F0} items/second");
        Console.WriteLine("Status:             Large collection mapped efficiently\n");

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}

// Domain Models
public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool Active { get; set; }
}

public class Order
{
    public int Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool InStock { get; set; }
}

// DTOs
public class CustomerDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool Active { get; set; }
}

public class OrderDto
{
    public int Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool InStock { get; set; }
}
