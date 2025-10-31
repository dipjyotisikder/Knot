using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Knot.Utilities
{
    /// <summary>
    /// Provides helper methods for working with collections.
    /// </summary>
    internal static class CollectionHelper
    {
        /// <summary>
        /// Determines if a type is a collection type.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns>True if the type is a collection; otherwise, false.</returns>
        public static bool IsCollectionType(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (type == typeof(string))
            {
                return false;
            }

            return typeof(IEnumerable).IsAssignableFrom(type);
        }

        /// <summary>
        /// Gets the element type of a collection.
        /// </summary>
        /// <param name="type">The collection type.</param>
        /// <returns>The element type if found; otherwise, typeof(object).</returns>
        public static Type GetElementType(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (type.IsArray)
            {
                return type.GetElementType();
            }

            if (type.IsGenericType)
            {
                var genericArgs = type.GetGenericArguments();
                if (genericArgs.Length == 1)
                {
                    return genericArgs[0];
                }
            }

            var enumerableInterface = type.GetInterfaces()
           .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>));

            if (enumerableInterface != null)
            {
                return enumerableInterface.GetGenericArguments()[0];
            }

            return typeof(object);
        }

        /// <summary>
        /// Creates a list of the specified element type.
        /// </summary>
        /// <param name="elementType">The element type.</param>
        /// <returns>A new list instance.</returns>
        public static object CreateList(Type elementType)
        {
            if (elementType == null)
            {
                throw new ArgumentNullException(nameof(elementType));
            }

            var listType = typeof(List<>).MakeGenericType(elementType);
            return Activator.CreateInstance(listType);
        }

        /// <summary>
        /// Converts a collection to a list of the specified type.
        /// </summary>
        /// <param name="source">The source collection.</param>
        /// <param name="elementType">The element type.</param>
        /// <returns>A list containing the elements.</returns>
        public static object ToList(IEnumerable source, Type elementType)
        {
            if (source == null)
            {
                return null;
            }

            if (elementType == null)
            {
                throw new ArgumentNullException(nameof(elementType));
            }

            var list = CreateList(elementType);
            var addMethod = list.GetType().GetMethod("Add");

            foreach (var item in source)
            {
                addMethod.Invoke(list, new[] { item });
            }

            return list;
        }

        /// <summary>
        /// Converts a collection to an array of the specified type.
        /// </summary>
        /// <param name="source">The source collection.</param>
        /// <param name="elementType">The element type.</param>
        /// <returns>An array containing the elements.</returns>
        public static object ToArray(IEnumerable source, Type elementType)
        {
            if (source == null)
            {
                return null;
            }

            if (elementType == null)
            {
                throw new ArgumentNullException(nameof(elementType));
            }

            var list = new List<object>();
            foreach (var item in source)
            {
                list.Add(item);
            }

            var array = Array.CreateInstance(elementType, list.Count);
            for (int i = 0; i < list.Count; i++)
            {
                array.SetValue(list[i], i);
            }

            return array;
        }
    }
}
