using System;

namespace Knot.Exceptions
{
    /// <summary>
    /// Thrown when a mapping operation fails.
    /// </summary>
    public class MappingException : Exception
    {
        /// <summary>
        /// Creates a new mapping exception.
        /// </summary>
        public MappingException()
        {
        }

        /// <summary>
        /// Creates a new mapping exception with a message.
        /// </summary>
        public MappingException(string message) : base(message)
        {
        }

        /// <summary>
        /// Creates a new mapping exception with a message and inner exception.
        /// </summary>
        public MappingException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
