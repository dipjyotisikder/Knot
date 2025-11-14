using System;

namespace Knot.Exceptions
{
    /// <summary>
    /// Exception thrown when a required property is missing during mapping.
    /// </summary>
    public class MissingPropertyException : MappingException
    {
        /// <summary>
        /// Gets the name of the missing property.
        /// </summary>
        public string? PropertyName { get; }

        /// <summary>
        /// Gets the type that is missing the property.
        /// </summary>
        public Type? Type { get; }

        /// <summary>
        /// Initializes a new instance of the MissingPropertyException class.
        /// </summary>
        public MissingPropertyException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the MissingPropertyException class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public MissingPropertyException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the MissingPropertyException class with a specified error message
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public MissingPropertyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the MissingPropertyException class with property information.
        /// </summary>
        /// <param name="propertyName">The name of the missing property.</param>
        /// <param name="type">The type that is missing the property.</param>
        public MissingPropertyException(string propertyName, Type type)
         : base($"Property '{propertyName}' not found on type '{type?.Name}'.")
        {
            PropertyName = propertyName;
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the MissingPropertyException class with property information and inner exception.
        /// </summary>
        /// <param name="propertyName">The name of the missing property.</param>
        /// <param name="type">The type that is missing the property.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public MissingPropertyException(string propertyName, Type type, Exception innerException)
            : base($"Property '{propertyName}' not found on type '{type?.Name}'.", innerException)
        {
            PropertyName = propertyName;
            Type = type;
        }
    }
}
