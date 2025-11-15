using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;

namespace Knot.Utilities
{
    /// <summary>
    /// Caches compiled expressions for fast property access and object creation.
    /// </summary>
    internal static class CompiledExpressionCache
    {
        private static readonly ConcurrentDictionary<Type, Func<object>> _factoryCache
            = new ConcurrentDictionary<Type, Func<object>>();

        private static readonly ConcurrentDictionary<PropertyInfo, Func<object, object>> _getterCache
        = new ConcurrentDictionary<PropertyInfo, Func<object, object>>();

        private static readonly ConcurrentDictionary<PropertyInfo, Action<object, object>> _setterCache
        = new ConcurrentDictionary<PropertyInfo, Action<object, object>>();

        /// <summary>
        /// Gets or creates a compiled factory for the given type (10-20x faster than Activator.CreateInstance).
        /// </summary>
        public static Func<object> GetOrCreateFactory(Type type)
        {
            return _factoryCache.GetOrAdd(type, CreateFactory);
        }

        /// <summary>
        /// Gets or creates a compiled getter for the property (20-50x faster than PropertyInfo.GetValue).
        /// </summary>
        public static Func<object, object>? GetOrCreateGetter(PropertyInfo property)
        {
            if (property == null || !property.CanRead)
            {
                return null;
            }

            return _getterCache.GetOrAdd(property, CreateGetter);
        }

        /// <summary>
        /// Gets or creates a compiled setter for the property (20-50x faster than PropertyInfo.SetValue).
        /// </summary>
        public static Action<object, object>? GetOrCreateSetter(PropertyInfo property)
        {
            if (property == null || !property.CanWrite)
            {
                return null;
            }

            return _setterCache.GetOrAdd(property, CreateSetter);
        }

        private static Func<object> CreateFactory(Type type)
        {
            try
            {
                var newExpression = Expression.New(type);
                var lambda = Expression.Lambda<Func<object>>(
                      Expression.Convert(newExpression, typeof(object)));

                return lambda.Compile();
            }
            catch
            {
                return () => Activator.CreateInstance(type);
            }
        }

        private static Func<object, object> CreateGetter(PropertyInfo property)
        {
            try
            {
                var objParam = Expression.Parameter(typeof(object), "obj");
                var castObj = Expression.Convert(objParam, property.DeclaringType);
                var propertyAccess = Expression.Property(castObj, property);
                var castResult = Expression.Convert(propertyAccess, typeof(object));
                var lambda = Expression.Lambda<Func<object, object>>(castResult, objParam);

                return lambda.Compile();
            }
            catch
            {
                return obj => property.GetValue(obj);
            }
        }

        private static Action<object, object> CreateSetter(PropertyInfo property)
        {
            try
            {
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
                return (obj, value) => property.SetValue(obj, value);
            }
        }

        /// <summary>
        /// Clears all cached expressions (useful for testing).
        /// </summary>
        public static void ClearCache()
        {
            _factoryCache.Clear();
            _getterCache.Clear();
            _setterCache.Clear();
        }

        /// <summary>
        /// Gets current cache statistics.
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
        /// Cache statistics.
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
