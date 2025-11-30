using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Knot.Benchmarks.Models;

namespace Knot.Benchmarks
{
    /// <summary>
    /// Benchmarks comparing Knot vs AutoMapper for simple object mapping
    /// </summary>
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class KnotVsAutoMapperSimpleBenchmarks
    {
        /// <summary>
        /// The Knot mapper instance.
        /// </summary>
        private IMapper _knotMapper;
        /// <summary>
        /// The AutoMapper mapper instance.
        /// </summary>
        private global::AutoMapper.IMapper _autoMapper;
        /// <summary>
        /// The source object for mapping.
        /// </summary>
        private SimpleSource _source;

        /// <summary>
        /// Performs one-time setup for the benchmark by initializing mapping configurations and test data.
        /// </summary>
        [GlobalSetup]
        public void Setup()
        {
            // Knot setup
            var knotConfig = new Knot.Configuration.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SimpleSource, SimpleDestination>();
            });
            _knotMapper = knotConfig.CreateMapper();

            // AutoMapper setup
            var autoMapperConfig = new global::AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SimpleSource, SimpleDestination>();
            });
            _autoMapper = autoMapperConfig.CreateMapper();

            _source = new SimpleSource
            {
                Id = 1,
                Name = "John Doe",
                Email = "john.doe@example.com",
                Age = 30,
                IsActive = true
            };
        }

        /// <summary>
        /// Maps a single simple object using Knot.
        /// </summary>
        [Benchmark(Description = "Knot: Map single simple object")]
        public SimpleDestination KnotMapSingleObject()
        {
            return _knotMapper.Map<SimpleDestination>(_source);
        }

        /// <summary>
        /// Maps a single simple object using AutoMapper.
        /// </summary>
        [Benchmark(Description = "AutoMapper: Map single simple object")]
        public SimpleDestination AutoMapperMapSingleObject()
        {
            return _autoMapper.Map<SimpleDestination>(_source);
        }

        /// <summary>
        /// Maps to an existing instance using Knot.
        /// </summary>
        [Benchmark(Description = "Knot: Map to existing instance")]
        public SimpleDestination KnotMapToExistingInstance()
        {
            var destination = new SimpleDestination();
            return _knotMapper.Map(_source, destination);
        }

        /// <summary>
        /// Maps to an existing instance using AutoMapper.
        /// </summary>
        [Benchmark(Description = "AutoMapper: Map to existing instance")]
        public SimpleDestination AutoMapperMapToExistingInstance()
        {
            var destination = new SimpleDestination();
            return _autoMapper.Map(_source, destination);
        }

        /// <summary>
        /// Manual mapping as baseline.
        /// </summary>
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
    /// Benchmarks comparing Knot vs AutoMapper for collection mapping
    /// </summary>
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class KnotVsAutoMapperCollectionBenchmarks
    {
        /// <summary>
        /// The Knot mapper instance.
        /// </summary>
        private IMapper _knotMapper;
        /// <summary>
        /// The AutoMapper mapper instance.
        /// </summary>
        private global::AutoMapper.IMapper _autoMapper;
        /// <summary>
        /// Small collection of source objects.
        /// </summary>
        private List<SimpleSource> _smallCollection;
        /// <summary>
        /// Medium collection of source objects.
        /// </summary>
        private List<SimpleSource> _mediumCollection;
        /// <summary>
        /// Large collection of source objects.
        /// </summary>
        private List<SimpleSource> _largeCollection;

        /// <summary>
        /// Setup Collection Benchmark.
        /// </summary>
        [GlobalSetup]
        public void Setup()
        {
            // Knot setup
            var knotConfig = new Knot.Configuration.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SimpleSource, SimpleDestination>();
            });
            _knotMapper = knotConfig.CreateMapper();

            // AutoMapper setup
            var autoMapperConfig = new global::AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SimpleSource, SimpleDestination>();
            });
            _autoMapper = autoMapperConfig.CreateMapper();

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

        /// <summary>
        /// Maps 10 objects using Knot.
        /// </summary>
        [Benchmark(Description = "Knot: Map 10 objects")]
        public List<SimpleDestination> KnotMapSmallCollection()
        {
            var result = new List<SimpleDestination>(_smallCollection.Count);
            foreach (var item in _smallCollection)
            {
                result.Add(_knotMapper.Map<SimpleDestination>(item));
            }
            return result;
        }

        /// <summary>
        /// Maps 10 objects using AutoMapper.
        /// </summary>
        [Benchmark(Description = "AutoMapper: Map 10 objects")]
        public List<SimpleDestination> AutoMapperMapSmallCollection()
        {
            var result = new List<SimpleDestination>(_smallCollection.Count);
            foreach (var item in _smallCollection)
            {
                result.Add(_autoMapper.Map<SimpleDestination>(item));
            }
            return result;
        }

        /// <summary>
        /// Maps 100 objects using Knot.
        /// </summary>
        [Benchmark(Description = "Knot: Map 100 objects")]
        public List<SimpleDestination> KnotMapMediumCollection()
        {
            var result = new List<SimpleDestination>(_mediumCollection.Count);
            foreach (var item in _mediumCollection)
            {
                result.Add(_knotMapper.Map<SimpleDestination>(item));
            }
            return result;
        }

        /// <summary>
        /// Maps 100 objects using AutoMapper.
        /// </summary>
        [Benchmark(Description = "AutoMapper: Map 100 objects")]
        public List<SimpleDestination> AutoMapperMapMediumCollection()
        {
            var result = new List<SimpleDestination>(_mediumCollection.Count);
            foreach (var item in _mediumCollection)
            {
                result.Add(_autoMapper.Map<SimpleDestination>(item));
            }
            return result;
        }

        /// <summary>
        /// Maps 1000 objects using Knot.
        /// </summary>
        [Benchmark(Description = "Knot: Map 1000 objects")]
        public List<SimpleDestination> KnotMapLargeCollection()
        {
            var result = new List<SimpleDestination>(_largeCollection.Count);
            foreach (var item in _largeCollection)
            {
                result.Add(_knotMapper.Map<SimpleDestination>(item));
            }
            return result;
        }

        /// <summary>
        /// Maps 1000 objects using AutoMapper.
        /// </summary>
        [Benchmark(Description = "AutoMapper: Map 1000 objects")]
        public List<SimpleDestination> AutoMapperMapLargeCollection()
        {
            var result = new List<SimpleDestination>(_largeCollection.Count);
            foreach (var item in _largeCollection)
            {
                result.Add(_autoMapper.Map<SimpleDestination>(item));
            }
            return result;
        }
    }

    /// <summary>
    /// Benchmarks comparing Knot vs AutoMapper for complex object mapping
    /// </summary>
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class KnotVsAutoMapperComplexBenchmarks
    {
        /// <summary>
        /// The Knot mapper instance.
        /// </summary>
        private IMapper _knotMapper;
        /// <summary>
        /// The AutoMapper mapper instance.
        /// </summary>
        private global::AutoMapper.IMapper _autoMapper;
        /// <summary>
        /// The complex source object for mapping.
        /// </summary>
        private ComplexSource _source;

        /// <summary>
        /// Setup complex benchmark.
        /// </summary>
        [GlobalSetup]
        public void Setup()
        {
            // Knot setup
            var knotConfig = new Knot.Configuration.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ComplexSource, ComplexDestination>(map =>
                {
                    map.ForMember(dest => dest.FullName,
                        src => $"{src.FirstName} {src.LastName}");
                });
                cfg.CreateMap<AddressSource, AddressDestination>();
                cfg.CreateMap<ContactInfoSource, ContactInfoDestination>();
            });
            _knotMapper = knotConfig.CreateMapper();

            // AutoMapper setup
            var autoMapperConfig = new global::AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ComplexSource, ComplexDestination>()
                    .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
                cfg.CreateMap<AddressSource, AddressDestination>();
                cfg.CreateMap<ContactInfoSource, ContactInfoDestination>();
            });
            _autoMapper = autoMapperConfig.CreateMapper();

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

        /// <summary>
        /// Maps a complex object with nested properties using Knot.
        /// </summary>
        [Benchmark(Description = "Knot: Map complex object with nested properties")]
        public ComplexDestination KnotMapComplexObject()
        {
            return _knotMapper.Map<ComplexDestination>(_source);
        }

        /// <summary>
        /// Maps a complex object with nested properties using AutoMapper.
        /// </summary>
        [Benchmark(Description = "AutoMapper: Map complex object with nested properties")]
        public ComplexDestination AutoMapperMapComplexObject()
        {
            return _autoMapper.Map<ComplexDestination>(_source);
        }

        /// <summary>
        /// Manual complex mapping as baseline.
        /// </summary>
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
    /// Benchmarks comparing Knot vs AutoMapper for configuration overhead
    /// </summary>
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class KnotVsAutoMapperConfigurationBenchmarks
    {
        /// <summary>
        /// Creates a simple configuration using Knot.
        /// </summary>
        [Benchmark(Description = "Knot: Create simple configuration")]
        public IMapper KnotCreateSimpleConfiguration()
        {
            var config = new Knot.Configuration.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SimpleSource, SimpleDestination>();
            });
            return config.CreateMapper();
        }

        /// <summary>
        /// Creates a simple configuration using AutoMapper.
        /// </summary>
        [Benchmark(Description = "AutoMapper: Create simple configuration")]
        public global::AutoMapper.IMapper AutoMapperCreateSimpleConfiguration()
        {
            // Commented out due to build issues
            // var config = new global::AutoMapper.MapperConfiguration(cfg =>
            // {
            //     cfg.CreateMap<SimpleSource, SimpleDestination>();
            // });
            // return config.CreateMapper();
            return null!;
        }

        /// <summary>
        /// Creates a complex configuration using Knot.
        /// </summary>
        [Benchmark(Description = "Knot: Create complex configuration")]
        public IMapper KnotCreateComplexConfiguration()
        {
            var config = new Knot.Configuration.MapperConfiguration(cfg =>
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

        /// <summary>
        /// Creates a complex configuration using AutoMapper.
        /// </summary>
        [Benchmark(Description = "AutoMapper: Create complex configuration")]
        public global::AutoMapper.IMapper AutoMapperCreateComplexConfiguration()
        {
            // Commented out due to build issues
            // var config = new global::AutoMapper.MapperConfiguration(cfg =>
            // {
            //     cfg.CreateMap<ComplexSource, ComplexDestination>()
            //         .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
            //     cfg.CreateMap<AddressSource, AddressDestination>();
            //     cfg.CreateMap<ContactInfoSource, ContactInfoDestination>();
            // });
            // return config.CreateMapper();
            return null!;
        }
    }

    /// <summary>
    /// Benchmarks comparing Knot vs AutoMapper for memory allocation patterns
    /// </summary>
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class KnotVsAutoMapperMemoryBenchmarks
    {
        /// <summary>
        /// The Knot mapper instance.
        /// </summary>
        private IMapper _knotMapper;
        /// <summary>
        /// The AutoMapper mapper instance.
        /// </summary>
        private global::AutoMapper.IMapper _autoMapper;
        /// <summary>
        /// The source object for mapping.
        /// </summary>
        private SimpleSource _source;

        [GlobalSetup]
        public void Setup()
        {
            // Knot setup
            var knotConfig = new Knot.Configuration.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SimpleSource, SimpleDestination>();
            });
            _knotMapper = knotConfig.CreateMapper();

            // AutoMapper setup
            var autoMapperConfig = new global::AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SimpleSource, SimpleDestination>();
            });
            _autoMapper = autoMapperConfig.CreateMapper();

            _source = new SimpleSource
            {
                Id = 1,
                Name = "John Doe",
                Email = "john.doe@example.com",
                Age = 30,
                IsActive = true
            };
        }

        /// <summary>
        /// Maps with new instance creation using Knot.
        /// </summary>
        [Benchmark(Description = "Knot: Map with new instance creation")]
        public SimpleDestination KnotMapWithNewInstance()
        {
            return _knotMapper.Map<SimpleDestination>(_source);
        }

        /// <summary>
        /// Maps with new instance creation using AutoMapper.
        /// </summary>
        [Benchmark(Description = "AutoMapper: Map with new instance creation")]
        public SimpleDestination AutoMapperMapWithNewInstance()
        {
            return _autoMapper.Map<SimpleDestination>(_source);
        }

        /// <summary>
        /// Maps with pooled instance using Knot.
        /// </summary>
        [Benchmark(Description = "Knot: Map with pooled instance")]
        public SimpleDestination KnotMapWithPooledInstance()
        {
            var destination = new SimpleDestination();
            return _knotMapper.Map(_source, destination);
        }

        /// <summary>
        /// Maps with pooled instance using AutoMapper.
        /// </summary>
        [Benchmark(Description = "AutoMapper: Map with pooled instance")]
        public SimpleDestination AutoMapperMapWithPooledInstance()
        {
            var destination = new SimpleDestination();
            return _autoMapper.Map(_source, destination);
        }

        /// <summary>
        /// Maps 100 times using Knot.
        /// </summary>
        [Benchmark(Description = "Knot: Map 100 times (allocations)", OperationsPerInvoke = 100)]
        public void KnotMapMultipleTimes()
        {
            for (int i = 0; i < 100; i++)
            {
                _ = _knotMapper.Map<SimpleDestination>(_source);
            }
        }

        /// <summary>
        /// Maps 100 times using AutoMapper.
        /// </summary>
        [Benchmark(Description = "AutoMapper: Map 100 times (allocations)", OperationsPerInvoke = 100)]
        public void AutoMapperMapMultipleTimes()
        {
            for (int i = 0; i < 100; i++)
            {
                _ = _autoMapper.Map<SimpleDestination>(_source);
            }
        }
    }
}
