using System;

namespace Knot.Exceptions
{
    /// <summary>
    /// Exception thrown when a type conversion fails during mapping.
    /// </summary>
    public class TypeConversionException : MappingException
    {
        /// <summary>
        /// Gets the source type.
        /// </summary>
        public Type SourceType { get; }

        /// <summary>
        /// Gets the destination type.
        /// </summary>
        public Type DestinationType { get; }

        /// <summary>
        /// Gets the value that failed to convert.
        /// </summary>
        public object Value { get; }

        /// <summary>
        /// Initializes a new instance of the TypeConversionException class.
        /// </summary>
        public TypeConversionException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the TypeConversionException class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public TypeConversionException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the TypeConversionException class with a specified error message
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public TypeConversionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the TypeConversionException class with type information.
        /// </summary>
        /// <param name="sourceType">The source type.</param>
        /// <param name="destinationType">The destination type.</param>
        /// <param name="value">The value that failed to convert.</param>
        public TypeConversionException(Type sourceType, Type destinationType, object value)
          : base($"Failed to convert value of type '{sourceType?.Name}' to type '{destinationType?.Name}'.")
        {
            SourceType = sourceType;
            DestinationType = destinationType;
            Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the TypeConversionException class with type information and inner exception.
        /// </summary>
        /// <param name="sourceType">The source type.</param>
        /// <param name="destinationType">The destination type.</param>
        /// <param name="value">The value that failed to convert.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public TypeConversionException(Type sourceType, Type destinationType, object value, Exception innerException)
      : base($"Failed to convert value of type '{sourceType?.Name}' to type '{destinationType?.Name}'.", innerException)
        {
            SourceType = sourceType;
            DestinationType = destinationType;
            Value = value;
        }
    }
}
