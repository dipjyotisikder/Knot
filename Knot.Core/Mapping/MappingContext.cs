using System;

namespace Knot.Mapping
{
    /// <summary>
    /// Represents the context for a mapping operation.
    /// </summary>
     internal class MappingContext
    {
        /// <summary>
        /// Gets the source value being mapped.
        /// </summary>
        public object SourceValue { get; }

        /// <summary>
        /// Gets the type of the source object.
        /// </summary>
        public Type SourceType { get; }

        /// <summary>
        /// Gets the type of the destination object.
        /// </summary>
        public Type DestinationType { get; }

        /// <summary>
        /// Gets the destination value, if mapping to an existing instance.
        /// </summary>
        public object DestinationValue { get; }

        /// <summary>
        /// Initializes a new instance of the MappingContext class.
        /// </summary>
        /// <param name="sourceValue">The source value.</param>
        /// <param name="sourceType">The source type.</param>
        /// <param name="destinationType">The destination type.</param>
        public MappingContext(object sourceValue, Type sourceType, Type destinationType)
     : this(sourceValue, sourceType, destinationType, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the MappingContext class.
        /// </summary>
        /// <param name="sourceValue">The source value.</param>
        /// <param name="sourceType">The source type.</param>
        /// <param name="destinationType">The destination type.</param>
        /// <param name="destinationValue">The existing destination value.</param>
        public MappingContext(object sourceValue, Type sourceType, Type destinationType, object destinationValue)
        {
            SourceValue = sourceValue;
            SourceType = sourceType ?? throw new ArgumentNullException(nameof(sourceType));
            DestinationType = destinationType ?? throw new ArgumentNullException(nameof(destinationType));
            DestinationValue = destinationValue;
        }
    }
}
