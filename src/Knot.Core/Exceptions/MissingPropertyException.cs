using System;

namespace Knot.Exceptions
{
    /// <summary>
    /// Thrown when a required property is missing during mapping.
    /// </summary>
    public class MissingPropertyException : MappingException
    {
        /// <summary>
        /// The name of the missing property.
        /// </summary>
        public string? PropertyName { get; }

        /// <summary>
        /// The type that is missing the property.
        /// </summary>
        public Type? Type { get; }

        /// <summary>
        /// Creates a new missing property exception.
        /// </summary>
        public MissingPropertyException()
        {
        }

        /// <summary>
        /// Creates a new missing property exception with a message.
        /// </summary>
        public MissingPropertyException(string message) : base(message)
        {
        }

        /// <summary>
        /// Creates a new missing property exception with a message and inner exception.
        /// </summary>
        public MissingPropertyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Creates a new missing property exception with property details.
        /// </summary>
        public MissingPropertyException(string propertyName, Type type)
         : base($"Property '{propertyName}' not found on type '{type?.Name}'.")
        {
            PropertyName = propertyName;
            Type = type;
        }

        /// <summary>
        /// Creates a new missing property exception with property details and inner exception.
        /// </summary>
        public MissingPropertyException(string propertyName, Type type, Exception innerException)
            : base($"Property '{propertyName}' not found on type '{type?.Name}'.", innerException)
        {
            PropertyName = propertyName;
            Type = type;
        }
    }
}
