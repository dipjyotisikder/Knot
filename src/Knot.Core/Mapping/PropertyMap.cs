using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Knot.Exceptions;
using Knot.Utilities;

namespace Knot.Mapping
{
    /// <summary>
    /// Represents a mapping configuration between two properties.
    /// </summary>
    internal class PropertyMap
    {
        private Func<object, object>? _compiledGetter;
        private Action<object, object>? _compiledSetter;
        private PropertyInfo? _sourceProperty;

        /// <summary>
        /// Gets the source property information.
        /// </summary>
        public PropertyInfo? SourceProperty => _sourceProperty;

        /// <summary>
        /// Gets the destination property information.
        /// </summary>
        public PropertyInfo DestinationProperty { get; }

        /// <summary>
        /// Gets or sets the custom value resolver function.
        /// </summary>
        public Func<object, object>? ValueResolver { get; set; }

        /// <summary>
        /// Initializes a new instance of the PropertyMap class.
        /// </summary>
        /// <param name="sourceProperty">The source property.</param>
        /// <param name="destinationProperty">The destination property.</param>
        public PropertyMap(PropertyInfo sourceProperty, PropertyInfo destinationProperty)
        {
            _sourceProperty = sourceProperty;
            DestinationProperty = destinationProperty ?? throw new ArgumentNullException(nameof(destinationProperty));

            // Pre-compile property accessors (20-50x faster than reflection)
            CompileSourceGetter();

            if (destinationProperty.CanWrite)
            {
                _compiledSetter = CompiledExpressionCache.GetOrCreateSetter(destinationProperty);
            }
        }

        /// <summary>
        /// Updates the source property and recompiles the getter.
        /// This is used internally for MapFrom configurations.
        /// </summary>
        /// <param name="sourceProperty">The new source property.</param>
        internal void UpdateSourceProperty(PropertyInfo sourceProperty)
        {
            _sourceProperty = sourceProperty;
            CompileSourceGetter();
        }

        private void CompileSourceGetter()
        {
            if (_sourceProperty != null && _sourceProperty.CanRead)
            {
                _compiledGetter = CompiledExpressionCache.GetOrCreateGetter(_sourceProperty);
            }
            else
            {
                _compiledGetter = null;
            }
        }

        /// <summary>
        /// Executes the property mapping from source to destination.
        /// </summary>
        /// <param name="source">The source object.</param>
        /// <param name="destination">The destination object.</param>
        /// <param name="mappingEngine">The mapping engine for nested mappings.</param>
        public void Execute(object source, object destination, IMappingEngine mappingEngine)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            if (_compiledSetter == null)
            {
                return;
            }

            object value;

            if (ValueResolver != null)
            {
                value = ValueResolver(source);
            }
            else if (_compiledGetter != null)
            {
                // Use compiled getter (20-50x faster than reflection)
                value = _compiledGetter(source);

                // Handle nested object mapping
                if (value != null && _sourceProperty != null)
                {
                    value = MapNestedValue(value, _sourceProperty.PropertyType, DestinationProperty.PropertyType, mappingEngine);
                }
            }
            else
            {
                return;
            }

            try
            {
                // Use compiled setter (20-50x faster than reflection)
                _compiledSetter(destination, value!);
            }
            catch (ArgumentException ex)
            {
                throw new MappingException(
                    $"Failed to set property '{DestinationProperty.Name}'. " +
                    $"Type mismatch or conversion error.", ex);
            }
        }

        private object MapNestedValue(object value, Type sourceType, Type destinationType, IMappingEngine mappingEngine)
        {
            // If types are the same or destination type is assignable from source type, no mapping needed
            if (sourceType == destinationType || destinationType.IsAssignableFrom(sourceType))
            {
                return value;
            }

            // Check if it's a simple type that doesn't need nested mapping
            if (IsSimpleType(sourceType) || IsSimpleType(destinationType))
            {
                return value;
            }

            // Handle collections (List, Array, IEnumerable, etc.)
            if (IsCollectionType(sourceType) && IsCollectionType(destinationType))
            {
                return MapCollection(value, sourceType, destinationType, mappingEngine);
            }

            // Handle complex nested objects
            if (sourceType.IsClass && destinationType.IsClass)
            {
                var typeMap = mappingEngine.Registry.GetTypeMap(sourceType, destinationType);
                if (typeMap != null)
                {
                    var context = new MappingContext(value, sourceType, destinationType);
                    return mappingEngine.Execute(context);
                }
            }

            return value;
        }

        private object MapCollection(object sourceCollection, Type sourceType, Type destinationType, IMappingEngine mappingEngine)
        {
            if (sourceCollection == null)
            {
                return null!;
            }

            var sourceEnumerable = sourceCollection as IEnumerable;
            if (sourceEnumerable == null)
            {
                return sourceCollection;
            }

            // Get element types
            var sourceElementType = GetCollectionElementType(sourceType);
            var destElementType = GetCollectionElementType(destinationType);

            if (sourceElementType == null || destElementType == null)
            {
                return sourceCollection;
            }

            // Map each element in the collection (even if empty, we need to create the right type)
            var mappedList = new List<object>();

            // If element types differ, check if we have a mapping and map each item
            if (sourceElementType != destElementType)
            {
                var elementTypeMap = mappingEngine.Registry.GetTypeMap(sourceElementType, destElementType);
                if (elementTypeMap == null)
                {
                    // No mapping available for element types
                    // Check if collection is empty - if so, return empty collection of correct type
                    bool isEmpty = true;
                    foreach (var item in sourceEnumerable)
                    {
                        isEmpty = false;
                        break;
                    }

                    if (isEmpty)
                    {
                        // Return empty collection of the correct destination type
                        return CreateEmptyCollection(destinationType, destElementType);
                    }
                    else
                    {
                        // Collection has items but no mapping - throw exception
                        throw new MappingException(
                            $"Cannot map collection elements from {sourceElementType.Name} to {destElementType.Name}. " +
                            $"No mapping configured for these types. Please add a mapping using CreateMap<{sourceElementType.Name}, {destElementType.Name}>().");
                    }
                }

                foreach (var item in sourceEnumerable)
                {
                    if (item == null)
                    {
                        mappedList.Add(null!);
                    }
                    else
                    {
                        var context = new MappingContext(item, sourceElementType, destElementType);
                        var mappedItem = mappingEngine.Execute(context);
                        mappedList.Add(mappedItem);
                    }
                }
            }
            else
            {
                // Element types are the same, just copy items
                foreach (var item in sourceEnumerable)
                {
                    mappedList.Add(item);
                }
            }

            // Convert to the destination collection type
            if (destinationType.IsArray)
            {
                var array = Array.CreateInstance(destElementType, mappedList.Count);
                for (int i = 0; i < mappedList.Count; i++)
                {
                    array.SetValue(mappedList[i], i);
                }
                return array;
            }
            else if (destinationType.IsGenericType)
            {
                var genericTypeDef = destinationType.GetGenericTypeDefinition();
                if (genericTypeDef == typeof(List<>) || genericTypeDef == typeof(IList<>) ||
                    genericTypeDef == typeof(ICollection<>) || genericTypeDef == typeof(IEnumerable<>))
                {
                    var listType = typeof(List<>).MakeGenericType(destElementType);
                    var list = Activator.CreateInstance(listType) as IList;
                    foreach (var item in mappedList)
                    {
                        list!.Add(item);
                    }
                    return list!;
                }
            }

            return mappedList;
        }

        private bool IsSimpleType(Type type)
        {
            return type.IsPrimitive ||
                   type.IsEnum ||
                   type == typeof(string) ||
                   type == typeof(decimal) ||
                   type == typeof(DateTime) ||
                   type == typeof(DateTimeOffset) ||
                   type == typeof(TimeSpan) ||
                   type == typeof(Guid) ||
                   (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>) &&
                    IsSimpleType(Nullable.GetUnderlyingType(type)!));
        }

        private bool IsCollectionType(Type type)
        {
            if (type == typeof(string))
            {
                return false;
            }

            return type.IsArray ||
                   (type.IsGenericType &&
                    (type.GetGenericTypeDefinition() == typeof(List<>) ||
                     type.GetGenericTypeDefinition() == typeof(IList<>) ||
                     type.GetGenericTypeDefinition() == typeof(ICollection<>) ||
                     type.GetGenericTypeDefinition() == typeof(IEnumerable<>))) ||
                   typeof(IEnumerable).IsAssignableFrom(type);
        }

        private Type? GetCollectionElementType(Type type)
        {
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

            // For non-generic IEnumerable, we can't determine the element type
            if (typeof(IEnumerable).IsAssignableFrom(type))
            {
                // Try to find a generic IEnumerable<T> interface
                var enumerableInterface = type.GetInterfaces()
                    .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>));

                if (enumerableInterface != null)
                {
                    return enumerableInterface.GetGenericArguments()[0];
                }
            }

            return null;
        }

        private object CreateEmptyCollection(Type destinationType, Type elementType)
        {
            // Create empty array
            if (destinationType.IsArray)
            {
                return Array.CreateInstance(elementType, 0);
            }

            // Create empty list/collection
            if (destinationType.IsGenericType)
            {
                var genericTypeDef = destinationType.GetGenericTypeDefinition();
                if (genericTypeDef == typeof(List<>) || genericTypeDef == typeof(IList<>) ||
                    genericTypeDef == typeof(ICollection<>) || genericTypeDef == typeof(IEnumerable<>))
                {
                    var listType = typeof(List<>).MakeGenericType(elementType);
                    return Activator.CreateInstance(listType)!;
                }
            }

            // Default to empty list
            var defaultListType = typeof(List<>).MakeGenericType(elementType);
            return Activator.CreateInstance(defaultListType)!;
        }
    }
}
