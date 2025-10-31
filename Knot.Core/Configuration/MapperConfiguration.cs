using System;
using System.Collections.Generic;

namespace Knot.Configuration
{
    /// <summary>
    /// Configuration class for setting up mappings.
    /// </summary>
    public class MapperConfiguration
    {
        private readonly MappingRegistry _registry;
        private readonly List<Profile> _profiles;

        /// <summary>
        /// Initializes a new instance of the MapperConfiguration class.
        /// </summary>
        /// <param name="configAction">Action to configure mappings.</param>
        public MapperConfiguration(Action<MapperConfiguration> configAction)
        {
            _registry = new MappingRegistry();
            _profiles = new List<Profile>();
            configAction?.Invoke(this);
        }

        /// <summary>
        /// Creates a mapping between two types.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <typeparam name="TDestination">The destination type.</typeparam>
        /// <returns>Fluent configuration for the type mapping.</returns>
        public TypeMapConfiguration<TSource, TDestination> CreateMap<TSource, TDestination>()
        {
            var typeMap = _registry.CreateMap<TSource, TDestination>();
            return new TypeMapConfiguration<TSource, TDestination>(typeMap);
        }

        /// <summary>
        /// Creates a mapping between two types with configuration.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <typeparam name="TDestination">The destination type.</typeparam>
        /// <param name="configure">Action to configure the mapping.</param>
        /// <returns>Fluent configuration for the type mapping.</returns>
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
        /// <returns>A new IMapper instance.</returns>
        public IMapper CreateMapper()
        {
            return new MapperEngine(_registry);
        }

        /// <summary>
        /// Registers a type converter.
        /// </summary>
        /// <typeparam name="TConverter">The type converter type.</typeparam>
        public void AddConverter<TConverter>() where TConverter : TypeConverter, new()
        {
            var converter = new TConverter();
            _registry.RegisterConverter(converter);
        }

        /// <summary>
        /// Registers a type converter instance.
        /// </summary>
        /// <param name="converter">The type converter instance.</param>
        public void AddConverter(TypeConverter converter)
        {
            _registry.RegisterConverter(converter);
        }

        /// <summary>
        /// Adds a profile to the configuration.
        /// </summary>
        /// <typeparam name="TProfile">The profile type.</typeparam>
        public void AddProfile<TProfile>() where TProfile : Profile, new()
        {
            var profile = new TProfile();
            AddProfile(profile);
        }

        /// <summary>
        /// Adds a profile instance to the configuration.
        /// </summary>
        /// <param name="profile">The profile instance.</param>
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
        /// Adds profiles from the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly to scan for profiles.</param>
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
