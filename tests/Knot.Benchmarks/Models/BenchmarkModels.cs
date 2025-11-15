namespace Knot.Benchmarks.Models
{
    // Models for basic mapping benchmarks

    public class SimpleSource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public bool IsActive { get; set; }
    }

    public class SimpleDestination
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public bool IsActive { get; set; }
    }

    // Models with nested objects
    public class ComplexSource
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public AddressSource Address { get; set; }
        public List<string> PhoneNumbers { get; set; }
        public ContactInfoSource ContactInfo { get; set; }
    }

    public class ComplexDestination
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public AddressDestination Address { get; set; }
        public List<string> PhoneNumbers { get; set; }
        public ContactInfoDestination ContactInfo { get; set; }
        public string FullName { get; set; }
    }

    public class AddressSource
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
    }

    public class AddressDestination
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
    }

    public class ContactInfoSource
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
    }

    public class ContactInfoDestination
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
    }

    // Deep nesting scenario
    public class Level1Source
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Level2Source Level2 { get; set; }
    }

    public class Level2Source
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Level3Source Level3 { get; set; }
    }

    public class Level3Source
    {
        public int Id { get; set; }
        public string Details { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class Level1Destination
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Level2Destination Level2 { get; set; }
    }

    public class Level2Destination
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Level3Destination Level3 { get; set; }
    }

    public class Level3Destination
    {
        public int Id { get; set; }
        public string Details { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
