using System;
using System.Collections.Generic;

namespace Knot.Configuration
{
    /// <summary>
    /// Configures object mappings for the mapper.
    /// </summary>
    public class MapperConfiguration
    {
        private readonly MappingRegistry _registry;
        private readonly List<Profile> _profiles;

        /// <summary>
        /// Creates a new mapper configuration with the given setup action.
        /// </summary>
        public MapperConfiguration(Action<MapperConfiguration> configAction)
        {
            _registry = new MappingRegistry();
            _profiles = new List<Profile>();
            configAction?.Invoke(this);
        }

        /// <summary>
        /// Defines a mapping from source to destination type.
        /// </summary>
        public TypeMapConfiguration<TSource, TDestination> CreateMap<TSource, TDestination>()
        {
            var typeMap = _registry.CreateMap<TSource, TDestination>();
            return new TypeMapConfiguration<TSource, TDestination>(typeMap);
        }

        /// <summary>
        /// Defines a mapping with custom configuration.
        /// </summary>
        public TypeMapConfiguration<TSource, TDestination> CreateMap<TSource, TDestination>(
          Action<TypeMapConfiguration<TSource, TDestination>> configure)
        {
            var typeMap = _registry.CreateMap<TSource, TDestination>();
            var config = new TypeMapConfiguration<TSource, TDestination>(typeMap);
            configure?.Invoke(config);
            return config;
        }

        /// <summary>
        /// Creates a mapper instance from this configuration.
        /// </summary>
        public IMapper CreateMapper()
        {
            return new MapperEngine(_registry);
        }

        /// <summary>
        /// Registers a type converter.
        /// </summary>
        public void AddConverter<TConverter>() where TConverter : TypeConverter, new()
        {
            var converter = new TConverter();
            _registry.RegisterConverter(converter);
        }

        /// <summary>
        /// Registers a type converter instance.
        /// </summary>
        public void AddConverter(TypeConverter converter)
        {
            _registry.RegisterConverter(converter);
        }

        /// <summary>
        /// Adds a mapping profile to the configuration.
        /// </summary>
        public void AddProfile<TProfile>() where TProfile : Profile, new()
        {
            var profile = new TProfile();
            AddProfile(profile);
        }

        /// <summary>
        /// Adds a mapping profile instance.
        /// </summary>
        public void AddProfile(Profile profile)
        {
            if (profile == null)
            {
                throw new ArgumentNullException(nameof(profile));
            }

            profile.SetConfiguration(this);
            _profiles.Add(profile);
            profile.Configure();
        }

        /// <summary>
        /// Scans an assembly and registers all Profile classes found.
        /// </summary>
        public void AddProfiles(System.Reflection.Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            var profileType = typeof(Profile);
            var types = assembly.GetTypes();

            foreach (var type in types)
            {
                if (profileType.IsAssignableFrom(type) && !type.IsAbstract && type.GetConstructor(Type.EmptyTypes) != null)
                {
                    var profile = (Profile)Activator.CreateInstance(type);
                    AddProfile(profile);
                }
            }
        }
    }
}
