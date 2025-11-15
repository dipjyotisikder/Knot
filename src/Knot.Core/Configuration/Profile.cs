using System;

namespace Knot.Configuration
{
    /// <summary>
    /// Base class for organizing related mappings.
    /// </summary>
    public abstract class Profile
    {
        private MapperConfiguration _configuration = null!;

        /// <summary>
        /// Gets the profile name (defaults to class name).
        /// </summary>
        public virtual string ProfileName => GetType().Name;

        /// <summary>
        /// Override to define mappings for this profile.
        /// </summary>
        protected internal abstract void Configure();

        /// <summary>
        /// Defines a mapping from source to destination type.
        /// </summary>
        protected TypeMapConfiguration<TSource, TDestination> CreateMap<TSource, TDestination>()
        {
            EnsureConfigurationSet();
            return _configuration.CreateMap<TSource, TDestination>();
        }

        /// <summary>
        /// Defines a mapping with custom configuration.
        /// </summary>
        protected TypeMapConfiguration<TSource, TDestination> CreateMap<TSource, TDestination>(Action<TypeMapConfiguration<TSource, TDestination>> configure)
        {
            EnsureConfigurationSet();
            return _configuration.CreateMap(configure);
        }

        /// <summary>
        /// Registers a type converter.
        /// </summary>
        protected void AddConverter<TConverter>() where TConverter : TypeConverter, new()
        {
            EnsureConfigurationSet();
            _configuration.AddConverter<TConverter>();
        }

        /// <summary>
        /// Registers a type converter instance.
        /// </summary>
        protected void AddConverter(TypeConverter converter)
        {
            EnsureConfigurationSet();
            _configuration.AddConverter(converter);
        }

        /// <summary>
        /// Associates this profile with a mapper configuration (internal use).
        /// </summary>
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
