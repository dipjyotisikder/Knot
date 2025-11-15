using System;

namespace Knot.Mapping
{
    /// <summary>
    /// Contains information for a mapping operation.
    /// </summary>
    internal class MappingContext
    {
        /// <summary>
        /// The source value being mapped.
        /// </summary>
        public object SourceValue { get; }

        /// <summary>
        /// The type of the source object.
        /// </summary>
        public Type SourceType { get; }

        /// <summary>
        /// The type of the destination object.
        /// </summary>
        public Type DestinationType { get; }

        /// <summary>
        /// The destination value (for mapping to existing instances).
        /// </summary>
        public object? DestinationValue { get; }

        /// <summary>
        /// Creates a mapping context for a new destination instance.
        /// </summary>
        public MappingContext(object sourceValue, Type sourceType, Type destinationType)
     : this(sourceValue, sourceType, destinationType, null!)
        {
        }

        /// <summary>
        /// Creates a mapping context for an existing destination instance.
        /// </summary>
        public MappingContext(object sourceValue, Type sourceType, Type destinationType, object? destinationValue)
        {
            SourceValue = sourceValue;
            SourceType = sourceType ?? throw new ArgumentNullException(nameof(sourceType));
            DestinationType = destinationType ?? throw new ArgumentNullException(nameof(destinationType));
            DestinationValue = destinationValue;
        }
    }
}
