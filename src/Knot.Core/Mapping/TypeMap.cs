using System;
using System.Collections.Generic;
using Knot.Exceptions;
using Knot.Utilities;

namespace Knot.Mapping
{
    /// <summary>
    /// Represents a mapping configuration between two types.
    /// </summary>
    internal class TypeMap
    {
        private Func<object> _compiledFactory;

        /// <summary>
        /// Gets the source type.
        /// </summary>
        public Type SourceType { get; }

        /// <summary>
        /// Gets the destination type.
        /// </summary>
        public Type DestinationType { get; }

        /// <summary>
        /// Gets the collection of property mappings.
        /// </summary>
        public IList<PropertyMap> PropertyMaps { get; }

        /// <summary>
        /// Initializes a new instance of the TypeMap class.
        /// </summary>
        /// <param name="sourceType">The source type.</param>
        /// <param name="destinationType">The destination type.</param>
        public TypeMap(Type sourceType, Type destinationType)
        {
            SourceType = sourceType ?? throw new ArgumentNullException(nameof(sourceType));
            DestinationType = destinationType ?? throw new ArgumentNullException(nameof(destinationType));
            PropertyMaps = new List<PropertyMap>();

            // Pre-compile the factory for this destination type (10-20x faster than Activator.CreateInstance)
            _compiledFactory = CompiledExpressionCache.GetOrCreateFactory(destinationType);
        }

        /// <summary>
        /// Executes the mapping for the given context.
        /// </summary>
        /// <param name="context">The mapping context.</param>
        /// <param name="mappingEngine">The mapping engine for nested mappings.</param>
        /// <returns>The mapped destination object.</returns>
        public object Execute(MappingContext context, IMappingEngine mappingEngine)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (mappingEngine == null)
            {
                throw new ArgumentNullException(nameof(mappingEngine));
            }

            var destination = context.DestinationValue ?? CreateDestinationInstance();

            foreach (var propertyMap in PropertyMaps)
            {
                try
                {
                    propertyMap.Execute(context.SourceValue, destination, mappingEngine);
                }
                catch (Exception ex)
                {
                    throw new MappingException(
                 $"Error mapping property '{propertyMap.DestinationProperty.Name}' " +
                  $"from {SourceType.Name} to {DestinationType.Name}", ex);
                }
            }

            return destination;
        }

        private object CreateDestinationInstance()
        {
            try
            {
                // Use compiled factory (10-20x faster than Activator.CreateInstance)
                return _compiledFactory();
            }
            catch (Exception ex)
            {
                throw new MappingException(
        $"Failed to create instance of destination type {DestinationType.Name}. " +
   "Ensure the type has a parameterless constructor.", ex);
            }
        }
    }
}
