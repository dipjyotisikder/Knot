using System;
using Knot.Configuration;

namespace Knot
{
    /// <summary>
    /// Static mapper class for global configuration and mapping.
    /// </summary>
    public static class Mapper
    {
        private static IMapper _instance = null!;
        private static readonly object _lock = new object();

        /// <summary>
        /// Initializes the global mapper with the specified configuration.
        /// </summary>
        /// <param name="configAction">Action to configure mappings.</param>
        public static void Initialize(Action<MapperConfiguration> configAction)
        {
            if (configAction == null)
            {
                throw new ArgumentNullException(nameof(configAction));
            }

            lock (_lock)
            {
                var config = new MapperConfiguration(configAction);
                _instance = config.CreateMapper();
            }
        }

        /// <summary>
        /// Initializes the global mapper with a mapper configuration.
        /// </summary>
        /// <param name="configuration">The mapper configuration.</param>
        public static void Initialize(MapperConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            lock (_lock)
            {
                _instance = configuration.CreateMapper();
            }
        }

        /// <summary>
        /// Maps the source object to a new destination object.
        /// </summary>
        /// <typeparam name="TDestination">The destination type.</typeparam>
        /// <param name="source">The source object.</param>
        /// <returns>A new instance of the destination type.</returns>
        public static TDestination Map<TDestination>(object source)
        {
            EnsureInitialized();
            return _instance.Map<TDestination>(source);
        }

        /// <summary>
        /// Maps the source object to a new destination object.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <typeparam name="TDestination">The destination type.</typeparam>
        /// <param name="source">The source object.</param>
        /// <returns>A new instance of the destination type.</returns>
        public static TDestination Map<TSource, TDestination>(TSource source)
        {
            EnsureInitialized();
            return _instance.Map<TSource, TDestination>(source);
        }

        /// <summary>
        /// Maps the source object to an existing destination object.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <typeparam name="TDestination">The destination type.</typeparam>
        /// <param name="source">The source object.</param>
        /// <param name="destination">The destination object.</param>
        /// <returns>The destination object with mapped values.</returns>
        public static TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            EnsureInitialized();
            return _instance.Map(source, destination);
        }

        /// <summary>
        /// Resets the global mapper instance.
        /// </summary>
        public static void Reset()
        {
            lock (_lock)
            {
                _instance = null!;
            }
        }

        private static void EnsureInitialized()
        {
            if (_instance == null)
            {
                throw new InvalidOperationException(
                        "Mapper not initialized. Call Mapper.Initialize() before using the mapper.");
            }
        }
    }
}
