using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Knot.Benchmarks.Models;
using Knot.Configuration;

namespace Knot.Benchmarks
{
    /// <summary>
    /// Benchmarks for simple object mapping scenarios
    /// </summary>
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class SimpleMappingBenchmarks
    {
        private IMapper _mapper;
        private SimpleSource _source;

        [GlobalSetup]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg =>
             {
                 cfg.CreateMap<SimpleSource, SimpleDestination>();
             });

            _mapper = config.CreateMapper();

            _source = new SimpleSource
            {
                Id = 1,
                Name = "John Doe",
                Email = "john.doe@example.com",
                Age = 30,
                IsActive = true
            };
        }

        [Benchmark(Description = "Map single simple object")]
        public SimpleDestination MapSingleObject()
        {
            return _mapper.Map<SimpleDestination>(_source);
        }

        [Benchmark(Description = "Map single object (typed)")]
        public SimpleDestination MapSingleObjectTyped()
        {
            return _mapper.Map<SimpleSource, SimpleDestination>(_source);
        }

        [Benchmark(Description = "Map to existing instance")]
        public SimpleDestination MapToExistingInstance()
        {
            var destination = new SimpleDestination();
            return _mapper.Map(_source, destination);
        }

        [Benchmark(Description = "Manual mapping (baseline)")]
        public SimpleDestination ManualMapping()
        {
            return new SimpleDestination
            {
                Id = _source.Id,
                Name = _source.Name,
                Email = _source.Email,
                Age = _source.Age,
                IsActive = _source.IsActive
            };
        }
    }

    /// <summary>
    /// Benchmarks for collection mapping scenarios
    /// </summary>
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class CollectionMappingBenchmarks
    {
        private IMapper _mapper;
        private List<SimpleSource> _smallCollection;
        private List<SimpleSource> _mediumCollection;
        private List<SimpleSource> _largeCollection;

        [GlobalSetup]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg =>
                   {
                       cfg.CreateMap<SimpleSource, SimpleDestination>();
                   });

            _mapper = config.CreateMapper();

            _smallCollection = CreateCollection(10);
            _mediumCollection = CreateCollection(100);
            _largeCollection = CreateCollection(1000);
        }

        private List<SimpleSource> CreateCollection(int count)
        {
            var list = new List<SimpleSource>(count);
            for (int i = 0; i < count; i++)
            {
                list.Add(new SimpleSource
                {
                    Id = i,
                    Name = $"Person {i}",
                    Email = $"person{i}@example.com",
                    Age = 20 + (i % 50),
                    IsActive = i % 2 == 0
                });
            }
            return list;
        }

        [Benchmark(Description = "Map 10 objects")]
        public List<SimpleDestination> MapSmallCollection()
        {
            var result = new List<SimpleDestination>(_smallCollection.Count);
            foreach (var item in _smallCollection)
            {
                result.Add(_mapper.Map<SimpleDestination>(item));
            }
            return result;
        }

        [Benchmark(Description = "Map 100 objects")]
        public List<SimpleDestination> MapMediumCollection()
        {
            var result = new List<SimpleDestination>(_mediumCollection.Count);
            foreach (var item in _mediumCollection)
            {
                result.Add(_mapper.Map<SimpleDestination>(item));
            }
            return result;
        }

        [Benchmark(Description = "Map 1000 objects")]
        public List<SimpleDestination> MapLargeCollection()
        {
            var result = new List<SimpleDestination>(_largeCollection.Count);
            foreach (var item in _largeCollection)
            {
                result.Add(_mapper.Map<SimpleDestination>(item));
            }
            return result;
        }
    }

    /// <summary>
    /// Benchmarks for complex object mapping with custom resolvers
    /// </summary>
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class ComplexMappingBenchmarks
    {
        private IMapper _mapper;
        private ComplexSource _source;

        [GlobalSetup]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ComplexSource, ComplexDestination>(map =>
                {
                    map.ForMember(
                        dest => dest.FullName,
                        src => $"{src.FirstName} {src.LastName}");
                });
                cfg.CreateMap<AddressSource, AddressDestination>();
                cfg.CreateMap<ContactInfoSource, ContactInfoDestination>();
            });

            _mapper = config.CreateMapper();

            _source = new ComplexSource
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1990, 1, 1),
                Address = new AddressSource
                {
                    Street = "123 Main St",
                    City = "New York",
                    State = "NY",
                    ZipCode = "10001",
                    Country = "USA"
                },
                PhoneNumbers = new List<string> { "555-1234", "555-5678" },
                ContactInfo = new ContactInfoSource
                {
                    Email = "john.doe@example.com",
                    Phone = "555-1234",
                    Fax = "555-9999"
                }
            };
        }

        [Benchmark(Description = "Map complex object with nested properties")]
        public ComplexDestination MapComplexObject()
        {
            return _mapper.Map<ComplexDestination>(_source);
        }

        [Benchmark(Description = "Map complex object with custom resolver")]
        public ComplexDestination MapWithCustomResolver()
        {
            var result = _mapper.Map<ComplexDestination>(_source);
            return result;
        }

        [Benchmark(Description = "Manual complex mapping (baseline)")]
        public ComplexDestination ManualComplexMapping()
        {
            return new ComplexDestination
            {
                Id = _source.Id,
                FirstName = _source.FirstName,
                LastName = _source.LastName,
                DateOfBirth = _source.DateOfBirth,
                FullName = $"{_source.FirstName} {_source.LastName}",
                Address = _source.Address != null ? new AddressDestination
                {
                    Street = _source.Address.Street,
                    City = _source.Address.City,
                    State = _source.Address.State,
                    ZipCode = _source.Address.ZipCode,
                    Country = _source.Address.Country
                } : null,
                PhoneNumbers = _source.PhoneNumbers != null ? new List<string>(_source.PhoneNumbers) : null,
                ContactInfo = _source.ContactInfo != null ? new ContactInfoDestination
                {
                    Email = _source.ContactInfo.Email,
                    Phone = _source.ContactInfo.Phone,
                    Fax = _source.ContactInfo.Fax
                } : null
            };
        }
    }

    /// <summary>
    /// Benchmarks for configuration overhead
    /// </summary>
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class ConfigurationBenchmarks
    {
        [Benchmark(Description = "Create simple configuration")]
        public IMapper CreateSimpleConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SimpleSource, SimpleDestination>();
            });
            return config.CreateMapper();
        }

        [Benchmark(Description = "Create complex configuration")]
        public IMapper CreateComplexConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ComplexSource, ComplexDestination>(map =>
                {
                    map.ForMember(dest => dest.FullName,
                        src => $"{src.FirstName} {src.LastName}");
                });
                cfg.CreateMap<AddressSource, AddressDestination>();
                cfg.CreateMap<ContactInfoSource, ContactInfoDestination>();
            });
            return config.CreateMapper();
        }

        [Benchmark(Description = "Create configuration with 10 mappings")]
        public IMapper CreateMultipleMappings()
        {
            var config = new MapperConfiguration(cfg =>
            {
                for (int i = 0; i < 10; i++)
                {
                    cfg.CreateMap<SimpleSource, SimpleDestination>();
                }
            });
            return config.CreateMapper();
        }
    }

    /// <summary>
    /// Benchmarks for memory allocation patterns
    /// </summary>
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class MemoryAllocationBenchmarks
    {
        private IMapper _mapper;
        private SimpleSource _source;

        [GlobalSetup]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SimpleSource, SimpleDestination>();
            });

            _mapper = config.CreateMapper();

            _source = new SimpleSource
            {
                Id = 1,
                Name = "John Doe",
                Email = "john.doe@example.com",
                Age = 30,
                IsActive = true
            };
        }

        [Benchmark(Description = "Map with new instance creation")]
        public SimpleDestination MapWithNewInstance()
        {
            return _mapper.Map<SimpleDestination>(_source);
        }

        [Benchmark(Description = "Map with pooled instance")]
        public SimpleDestination MapWithPooledInstance()
        {
            var destination = new SimpleDestination();
            return _mapper.Map(_source, destination);
        }

        [Benchmark(Description = "Map 100 times (allocations)", OperationsPerInvoke = 100)]
        public void MapMultipleTimes()
        {
            for (int i = 0; i < 100; i++)
            {
                _ = _mapper.Map<SimpleDestination>(_source);
            }
        }
    }
}
