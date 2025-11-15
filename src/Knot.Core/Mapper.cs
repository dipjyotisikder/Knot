using System;
using Knot.Configuration;

namespace Knot
{
    /// <summary>
    /// Global static mapper for application-wide mapping.
    /// </summary>
    public static class Mapper
    {
        private static IMapper _instance = null!;
        private static readonly object _lock = new object();

        /// <summary>
        /// Initializes the global mapper with a configuration action.
        /// </summary>
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
        /// Initializes the global mapper with an existing configuration.
        /// </summary>
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
        /// Maps source to a new destination object.
        /// </summary>
        public static TDestination Map<TDestination>(object source)
        {
            EnsureInitialized();
            return _instance.Map<TDestination>(source);
        }

        /// <summary>
        /// Maps source to a new destination object with explicit types.
        /// </summary>
        public static TDestination Map<TSource, TDestination>(TSource source)
        {
            EnsureInitialized();
            return _instance.Map<TSource, TDestination>(source);
        }

        /// <summary>
        /// Maps source to an existing destination object.
        /// </summary>
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
