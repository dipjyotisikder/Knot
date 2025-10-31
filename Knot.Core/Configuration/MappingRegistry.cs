using Knot.Mapping;
using Knot.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Knot.Configuration
{
    /// <summary>
    /// Registry for managing type mapping configurations.
    /// </summary>
    internal class MappingRegistry
    {
        private readonly Dictionary<TypePair, TypeMap> _typeMaps;
        private readonly List<TypeConverter> _typeConverters;

        /// <summary>
        /// Initializes a new instance of the MappingRegistry class.
        /// </summary>
        public MappingRegistry()
        {
            _typeMaps = new Dictionary<TypePair, TypeMap>();
            _typeConverters = new List<TypeConverter>();
        }

        /// <summary>
        /// Creates a mapping configuration between two types.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <typeparam name="TDestination">The destination type.</typeparam>
        /// <returns>The created TypeMap.</returns>
        public TypeMap CreateMap<TSource, TDestination>()
        {
            return CreateMap(typeof(TSource), typeof(TDestination));
        }

        /// <summary>
        /// Creates a mapping configuration between two types with fluent configuration.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <typeparam name="TDestination">The destination type.</typeparam>
        /// <param name="configure">Optional action to configure the mapping.</param>
        /// <returns>The fluent configuration object.</returns>
        public TypeMapConfiguration<TSource, TDestination> CreateMap<TSource, TDestination>(
            Action<TypeMapConfiguration<TSource, TDestination>> configure)
        {
            var typeMap = CreateMap(typeof(TSource), typeof(TDestination));
            var config = new TypeMapConfiguration<TSource, TDestination>(typeMap);
            configure?.Invoke(config);
            return config;
        }

        /// <summary>
        /// Creates a mapping configuration between two types.
        /// </summary>
        /// <param name="sourceType">The source type.</param>
        /// <param name="destinationType">The destination type.</param>
        /// <returns>The created TypeMap.</returns>
        public TypeMap CreateMap(Type sourceType, Type destinationType)
        {
            var typePair = new TypePair(sourceType, destinationType);

            // Optimized: Use TryGetValue instead of ContainsKey + indexer (2x faster lookup)
            if (_typeMaps.TryGetValue(typePair, out var existingTypeMap))
            {
                return existingTypeMap;
            }

            var typeMap = new TypeMap(sourceType, destinationType);
            _typeMaps[typePair] = typeMap;

            // Auto-configure property mappings
            AutoConfigurePropertyMaps(typeMap);

            return typeMap;
        }

        /// <summary>
        /// Gets a type map for the specified source and destination types.
        /// </summary>
        /// <param name="sourceType">The source type.</param>
        /// <param name="destinationType">The destination type.</param>
        /// <returns>The TypeMap if found; otherwise, null.</returns>
        public TypeMap GetTypeMap(Type sourceType, Type destinationType)
        {
            var typePair = new TypePair(sourceType, destinationType);

            // Optimized: Single dictionary lookup instead of two
            _typeMaps.TryGetValue(typePair, out var typeMap);
            return typeMap;
        }

        /// <summary>
        /// Gets all registered type converters.
        /// </summary>
        public IEnumerable<TypeConverter> GetTypeConverters()
        {
            return _typeConverters;
        }

        /// <summary>
        /// Registers a type converter.
        /// </summary>
        /// <param name="converter">The type converter to register.</param>
        public void RegisterConverter(TypeConverter converter)
        {
            if (converter == null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            _typeConverters.Add(converter);
        }

        private void AutoConfigurePropertyMaps(TypeMap typeMap)
        {
            var sourceProperties = ReflectionHelper.GetPublicProperties(typeMap.SourceType);
            var destinationProperties = ReflectionHelper.GetPublicProperties(typeMap.DestinationType);

            foreach (var destProp in destinationProperties)
            {
                if (!destProp.CanWrite)
                {
                    continue;
                }

                var sourceProp = sourceProperties.FirstOrDefault(sp => sp.Name.Equals(destProp.Name, StringComparison.OrdinalIgnoreCase) && sp.CanRead);

                var propertyMap = new PropertyMap(sourceProp, destProp);
                typeMap.PropertyMaps.Add(propertyMap);
            }
        }

        private struct TypePair : IEquatable<TypePair>
        {
            public Type SourceType { get; }
            public Type DestinationType { get; }

            public TypePair(Type sourceType, Type destinationType)
            {
                SourceType = sourceType;
                DestinationType = destinationType;
            }

            public bool Equals(TypePair other)
            {
                return SourceType == other.SourceType && DestinationType == other.DestinationType;
            }

            public override bool Equals(object obj)
            {
                return obj is TypePair other && Equals(other);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return (SourceType?.GetHashCode() ?? 0) * 397 ^ (DestinationType?.GetHashCode() ?? 0);
                }
            }
        }
    }
}
