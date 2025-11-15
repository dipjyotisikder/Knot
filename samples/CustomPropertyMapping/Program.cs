using System;
using Knot.Configuration;

namespace Knot.Samples.CustomPropertyMapping;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Knot Custom Property Mapping Example\n");

        // Configure mapper with custom property mappings
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Employee, EmployeeDto>(map =>
            {
                // Combine FirstName and LastName into FullName
                map.ForMember(dest => dest.FullName,
                    src => $"{src.FirstName} {src.LastName}");

                // Calculate years of service
                map.ForMember(dest => dest.YearsOfService,
                    src => DateTime.Now.Year - src.HireDate.Year);

                // Format salary with currency
                map.ForMember(dest => dest.SalaryFormatted,
                    src => $"${src.Salary:N2}");

                // Ignore sensitive data
                map.Ignore(dest => dest.InternalNotes);
            });

            cfg.CreateMap<Product, ProductSummaryDto>(map =>
            {
                // Calculate discount price
                map.ForMember(dest => dest.DiscountedPrice,
                    src => src.IsOnSale ? src.Price * 0.9m : src.Price);

                // Format availability status
                map.ForMember(dest => dest.AvailabilityStatus,
                    src => src.StockQuantity > 0 ? "In Stock" : "Out of Stock");
            });
        });

        var mapper = config.CreateMapper();

        // Example 1: Employee with custom mappings
        Console.WriteLine("Example 1: Employee custom mapping\n");

        var employee = new Employee
        {
            Id = 101,
            FirstName = "Sarah",
            LastName = "Johnson",
            Email = "sarah.johnson@company.com",
            Salary = 85000m,
            HireDate = new DateTime(2020, 3, 15),
            Department = "Engineering"
        };

        var employeeDto = mapper.Map<EmployeeDto>(employee);

        Console.WriteLine($"Employee ID:        {employeeDto.Id}");
        Console.WriteLine($"Full Name:          {employeeDto.FullName}");
        Console.WriteLine($"Email:              {employeeDto.Email}");
        Console.WriteLine($"Department:         {employeeDto.Department}");
        Console.WriteLine($"Salary:             {employeeDto.SalaryFormatted}");
        Console.WriteLine($"Years of Service:   {employeeDto.YearsOfService} years");
        Console.WriteLine("Custom Mappings:    FullName (concatenated), YearsOfService (calculated), SalaryFormatted (formatted)\n");

        // Example 2: Product with computed properties
        Console.WriteLine("Example 2: Product custom mapping with discount\n");

        var product = new Product
        {
            Id = 501,
            Name = "Wireless Mouse",
            Price = 29.99m,
            StockQuantity = 150,
            IsOnSale = true
        };

        var productDto = mapper.Map<ProductSummaryDto>(product);

        Console.WriteLine($"Product ID:         {productDto.Id}");
        Console.WriteLine($"Product Name:       {productDto.Name}");
        Console.WriteLine($"Original Price:     ${productDto.Price:F2}");
        Console.WriteLine($"Discounted Price:   ${productDto.DiscountedPrice:F2} (10% off)");
        Console.WriteLine($"Availability:       {productDto.AvailabilityStatus}");
        Console.WriteLine("Custom Mappings:    DiscountedPrice (calculated), AvailabilityStatus (computed)\n");

        // Example 3: Out of stock product
        Console.WriteLine("Example 3: Out of stock product mapping\n");

        var outOfStock = new Product
        {
            Id = 502,
            Name = "Mechanical Keyboard",
            Price = 129.99m,
            StockQuantity = 0,
            IsOnSale = false
        };

        var outOfStockDto = mapper.Map<ProductSummaryDto>(outOfStock);

        Console.WriteLine($"Product ID:         {outOfStockDto.Id}");
        Console.WriteLine($"Product Name:       {outOfStockDto.Name}");
        Console.WriteLine($"Price:              ${outOfStockDto.Price:F2}");
        Console.WriteLine($"Availability:       {outOfStockDto.AvailabilityStatus}");
        Console.WriteLine("Status:             No discount applied (not on sale)\n");

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}

// Source Models
public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public decimal Salary { get; set; }
    public DateTime HireDate { get; set; }
    public string Department { get; set; } = string.Empty;
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public bool IsOnSale { get; set; }
}

// DTOs
public class EmployeeDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string SalaryFormatted { get; set; } = string.Empty;
    public int YearsOfService { get; set; }
    public string Department { get; set; } = string.Empty;
    public string InternalNotes { get; set; } = string.Empty;
}

public class ProductSummaryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal DiscountedPrice { get; set; }
    public string AvailabilityStatus { get; set; } = string.Empty;
}
