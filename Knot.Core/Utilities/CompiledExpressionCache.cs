using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;

namespace Knot.Utilities
{
    /// <summary>
    /// Provides compiled expression caching for high-performance property access and object creation.
    /// </summary>
    internal static class CompiledExpressionCache
    {
        // Cache for object factory functions
        private static readonly ConcurrentDictionary<Type, Func<object>> _factoryCache
            = new ConcurrentDictionary<Type, Func<object>>();

        // Cache for property getters
        private static readonly ConcurrentDictionary<PropertyInfo, Func<object, object>> _getterCache
        = new ConcurrentDictionary<PropertyInfo, Func<object, object>>();

        // Cache for property setters
        private static readonly ConcurrentDictionary<PropertyInfo, Action<object, object>> _setterCache
        = new ConcurrentDictionary<PropertyInfo, Action<object, object>>();

        /// <summary>
        /// Gets or creates a compiled factory function for the specified type.
        /// This is 10-20x faster than Activator.CreateInstance.
        /// </summary>
        /// <param name="type">The type to create.</param>
        /// <returns>A compiled factory function.</returns>
        public static Func<object> GetOrCreateFactory(Type type)
        {
            return _factoryCache.GetOrAdd(type, CreateFactory);
        }

        /// <summary>
        /// Gets or creates a compiled getter function for the specified property.
        /// This is 20-50x faster than PropertyInfo.GetValue.
        /// </summary>
        /// <param name="property">The property to create a getter for.</param>
        /// <returns>A compiled getter function.</returns>
        public static Func<object, object> GetOrCreateGetter(PropertyInfo property)
        {
            if (property == null || !property.CanRead)
            {
                return null;
            }

            return _getterCache.GetOrAdd(property, CreateGetter);
        }

        /// <summary>
        /// Gets or creates a compiled setter function for the specified property.
        /// This is 20-50x faster than PropertyInfo.SetValue.
        /// </summary>
        /// <param name="property">The property to create a setter for.</param>
        /// <returns>A compiled setter function.</returns>
        public static Action<object, object> GetOrCreateSetter(PropertyInfo property)
        {
            if (property == null || !property.CanWrite)
            {
                return null;
            }

            return _setterCache.GetOrAdd(property, CreateSetter);
        }

        /// <summary>
        /// Creates a compiled factory expression for the specified type.
        /// </summary>
        private static Func<object> CreateFactory(Type type)
        {
            try
            {
                // Expression: () => new T()
                var newExpression = Expression.New(type);
                var lambda = Expression.Lambda<Func<object>>(
                      Expression.Convert(newExpression, typeof(object)));

                return lambda.Compile();
            }
            catch
            {
                // Fallback to Activator.CreateInstance if expression compilation fails
                return () => Activator.CreateInstance(type);
            }
        }

        /// <summary>
        /// Creates a compiled getter expression for the specified property.
        /// </summary>
        private static Func<object, object> CreateGetter(PropertyInfo property)
        {
            try
            {
                // Expression: (object obj) => (object)((TDeclaringType)obj).Property
                var objParam = Expression.Parameter(typeof(object), "obj");
                var castObj = Expression.Convert(objParam, property.DeclaringType);
                var propertyAccess = Expression.Property(castObj, property);
                var castResult = Expression.Convert(propertyAccess, typeof(object));
                var lambda = Expression.Lambda<Func<object, object>>(castResult, objParam);

                return lambda.Compile();
            }
            catch
            {
                // Fallback to reflection if expression compilation fails
                return obj => property.GetValue(obj);
            }
        }

        /// <summary>
        /// Creates a compiled setter expression for the specified property.
        /// </summary>
        private static Action<object, object> CreateSetter(PropertyInfo property)
        {
            try
            {
                // Expression: (object obj, object value) => ((TDeclaringType)obj).Property = (TProperty)value
                var objParam = Expression.Parameter(typeof(object), "obj");
                var valueParam = Expression.Parameter(typeof(object), "value");

                var castObj = Expression.Convert(objParam, property.DeclaringType);
                var castValue = Expression.Convert(valueParam, property.PropertyType);
                var propertyAccess = Expression.Property(castObj, property);
                var assign = Expression.Assign(propertyAccess, castValue);

                var lambda = Expression.Lambda<Action<object, object>>(assign, objParam, valueParam);

                return lambda.Compile();
            }
            catch
            {
                // Fallback to reflection if expression compilation fails
                return (obj, value) => property.SetValue(obj, value);
            }
        }

        /// <summary>
        /// Clears all cached compiled expressions. Useful for testing or memory management.
        /// </summary>
        public static void ClearCache()
        {
            _factoryCache.Clear();
            _getterCache.Clear();
            _setterCache.Clear();
        }

        /// <summary>
        /// Gets the current cache statistics.
        /// </summary>
        public static CacheStatistics GetStatistics()
        {
            return new CacheStatistics
            {
                FactoryCacheSize = _factoryCache.Count,
                GetterCacheSize = _getterCache.Count,
                SetterCacheSize = _setterCache.Count
            };
        }

        /// <summary>
        /// Statistics about the compiled expression cache.
        /// </summary>
        public struct CacheStatistics
        {
            public int FactoryCacheSize { get; set; }
            public int GetterCacheSize { get; set; }
            public int SetterCacheSize { get; set; }

            public int TotalCacheSize => FactoryCacheSize + GetterCacheSize + SetterCacheSize;
        }
    }
}
