using System;

namespace Knot.Configuration
{
    /// <summary>
    /// Base class for organizing mapping configurations.
    /// </summary>
    public abstract class Profile
    {
        private MapperConfiguration _configuration = null!;

        /// <summary>
        /// Gets the profile name.
        /// </summary>
        public virtual string ProfileName => GetType().Name;

        /// <summary>
        /// Configures the mappings for this profile.
        /// </summary>
        protected internal abstract void Configure();

        /// <summary>
        /// Creates a mapping between two types.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <typeparam name="TDestination">The destination type.</typeparam>
        /// <returns>Fluent configuration for the type mapping.</returns>
        protected TypeMapConfiguration<TSource, TDestination> CreateMap<TSource, TDestination>()
        {
            EnsureConfigurationSet();
            return _configuration.CreateMap<TSource, TDestination>();
        }

        /// <summary>
        /// Creates a mapping between two types with configuration.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <typeparam name="TDestination">The destination type.</typeparam>
        /// <param name="configure">Action to configure the mapping.</param>
        /// <returns>Fluent configuration for the type mapping.</returns>
        protected TypeMapConfiguration<TSource, TDestination> CreateMap<TSource, TDestination>(Action<TypeMapConfiguration<TSource, TDestination>> configure)
        {
            EnsureConfigurationSet();
            return _configuration.CreateMap(configure);
        }

        /// <summary>
        /// Registers a type converter.
        /// </summary>
        /// <typeparam name="TConverter">The type converter type.</typeparam>
        protected void AddConverter<TConverter>() where TConverter : TypeConverter, new()
        {
            EnsureConfigurationSet();
            _configuration.AddConverter<TConverter>();
        }

        /// <summary>
        /// Registers a type converter instance.
        /// </summary>
        /// <param name="converter">The type converter instance.</param>
        protected void AddConverter(TypeConverter converter)
        {
            EnsureConfigurationSet();
            _configuration.AddConverter(converter);
        }

        /// <summary>
        /// Sets the mapper configuration for this profile.
        /// </summary>
        /// <param name="configuration">The mapper configuration.</param>
        internal void SetConfiguration(MapperConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        private void EnsureConfigurationSet()
        {
            if (_configuration == null)
            {
                throw new InvalidOperationException(
                    "Configuration not set. Profiles must be registered with a MapperConfiguration.");
            }
        }
    }
}
