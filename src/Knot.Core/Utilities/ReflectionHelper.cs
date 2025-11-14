using System;
using System.Collections.Generic;
using System.Reflection;

namespace Knot.Utilities
{
    /// <summary>
    /// Provides helper methods for reflection operations.
    /// </summary>
    internal static class ReflectionHelper
    {
        /// <summary>
        /// Gets all public readable and writable properties of a type.
        /// </summary>
        /// <param name="type">The type to inspect.</param>
        /// <returns>A collection of PropertyInfo objects.</returns>
        public static IEnumerable<PropertyInfo> GetPublicProperties(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }

        /// <summary>
        /// Gets a property by name, ignoring case.
        /// </summary>
        /// <param name="type">The type to search.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>The PropertyInfo if found; otherwise, null.</returns>
        public static PropertyInfo? GetProperty(Type type, string propertyName)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException("Property name cannot be null or empty.", nameof(propertyName));
            }

            return type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
        }

        /// <summary>
        /// Determines if a type is a primitive or simple type (string, DateTime, etc.).
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns>True if the type is primitive or simple; otherwise, false.</returns>
        public static bool IsSimpleType(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.IsPrimitive ||
                type.IsEnum ||
                type == typeof(string) ||
                type == typeof(decimal) ||
                type == typeof(DateTime) ||
                type == typeof(DateTimeOffset) ||
                type == typeof(TimeSpan) ||
                type == typeof(Guid);
        }

        /// <summary>
        /// Gets the value of a property from an object.
        /// </summary>
        /// <param name="obj">The object instance.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>The property value.</returns>
        public static object? GetPropertyValue(object obj, string propertyName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            var property = GetProperty(obj.GetType(), propertyName);
            if (property == null ||
                !property.CanRead)
            {
                return null;
            }

            return property.GetValue(obj);
        }

        /// <summary>
        /// Sets the value of a property on an object.
        /// </summary>
        /// <param name="obj">The object instance.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="value">The value to set.</param>
        public static void SetPropertyValue(object obj, string propertyName, object value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            var property = GetProperty(obj.GetType(), propertyName);
            if (property == null ||
                !property.CanWrite)
            {
                return;
            }

            property.SetValue(obj, value);
        }

        /// <summary>
        /// Determines if a type has a parameterless constructor.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns>True if the type has a parameterless constructor; otherwise, false.</returns>
        public static bool HasParameterlessConstructor(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.GetConstructor(Type.EmptyTypes) != null;
        }
    }
}
