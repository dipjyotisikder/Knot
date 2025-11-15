using System;

namespace Knot.Exceptions
{
    /// <summary>
    /// Thrown when type conversion fails during mapping.
    /// </summary>
    public class TypeConversionException : MappingException
    {
        /// <summary>
        /// The source type that failed to convert.
        /// </summary>
        public Type? SourceType { get; }

        /// <summary>
        /// The destination type that was expected.
        /// </summary>
        public Type? DestinationType { get; }

        /// <summary>
        /// The value that failed conversion.
        /// </summary>
        public object? Value { get; }

        /// <summary>
        /// Creates a new type conversion exception.
        /// </summary>
        public TypeConversionException()
        {
        }

        /// <summary>
        /// Creates a new type conversion exception with a message.
        /// </summary>
        public TypeConversionException(string message) : base(message)
        {
        }

        /// <summary>
        /// Creates a new type conversion exception with a message and inner exception.
        /// </summary>
        public TypeConversionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Creates a new type conversion exception with type details.
        /// </summary>
        public TypeConversionException(Type sourceType, Type destinationType, object value)
          : base($"Failed to convert value of type '{sourceType?.Name}' to type '{destinationType?.Name}'.")
        {
            SourceType = sourceType;
            DestinationType = destinationType;
            Value = value;
        }

        /// <summary>
        /// Creates a new type conversion exception with type details and inner exception.
        /// </summary>
        public TypeConversionException(Type sourceType, Type destinationType, object value, Exception innerException)
      : base($"Failed to convert value of type '{sourceType?.Name}' to type '{destinationType?.Name}'.", innerException)
        {
            SourceType = sourceType;
            DestinationType = destinationType;
            Value = value;
        }
    }
}
