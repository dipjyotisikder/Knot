using Knot.Mapping;
using Knot.Utilities;
using System;
using System.Linq.Expressions;

namespace Knot.Configuration
{
    /// <summary>
    /// Provides fluent configuration for type mappings.
    /// </summary>
    /// <typeparam name="TSource">The source type.</typeparam>
    /// <typeparam name="TDestination">The destination type.</typeparam>
    public class TypeMapConfiguration<TSource, TDestination>
    {
        private readonly TypeMap _typeMap;

        /// <summary>
        /// Initializes a new instance of the TypeMapConfiguration class.
        /// </summary>
        /// <param name="typeMap">The type map to configure.</param>
        internal TypeMapConfiguration(TypeMap typeMap)
        {
            _typeMap = typeMap ?? throw new ArgumentNullException(nameof(typeMap));
        }

        /// <summary>
        /// Configures a custom mapping for a destination property.
        /// </summary>
        /// <typeparam name="TProperty">The property type.</typeparam>
        /// <param name="destinationMember">Expression to select the destination property.</param>
        /// <param name="valueResolver">Function to resolve the value.</param>
        /// <returns>The configuration object for method chaining.</returns>
        public TypeMapConfiguration<TSource, TDestination> ForMember<TProperty>(Expression<Func<TDestination, TProperty>> destinationMember,
                                                                                Func<TSource, TProperty> valueResolver)
        {
            if (destinationMember == null)
            {
                throw new ArgumentNullException(nameof(destinationMember));
            }

            if (valueResolver == null)
            {
                throw new ArgumentNullException(nameof(valueResolver));
            }

            var destProperty = ExpressionHelper.GetPropertyInfo(destinationMember);

            // Find existing property map or create new one
            PropertyMap propertyMap = null;
            foreach (var pm in _typeMap.PropertyMaps)
            {
                if (pm.DestinationProperty == destProperty)
                {
                    propertyMap = pm;
                    break;
                }
            }

            if (propertyMap == null)
            {
                propertyMap = new PropertyMap(null, destProperty);
                _typeMap.PropertyMaps.Add(propertyMap);
            }

            // Set the custom value resolver
            propertyMap.ValueResolver = source => valueResolver((TSource)source);

            return this;
        }

        /// <summary>
        /// Ignores a destination property during mapping.
        /// </summary>
        /// <typeparam name="TProperty">The property type.</typeparam>
        /// <param name="destinationMember">Expression to select the destination property.</param>
        /// <returns>The configuration object for method chaining.</returns>
        public TypeMapConfiguration<TSource, TDestination> Ignore<TProperty>(
          Expression<Func<TDestination, TProperty>> destinationMember)
        {
            if (destinationMember == null)
            {
                throw new ArgumentNullException(nameof(destinationMember));
            }

            var destProperty = ExpressionHelper.GetPropertyInfo(destinationMember);

            // Remove existing property map if it exists
            for (int i = _typeMap.PropertyMaps.Count - 1; i >= 0; i--)
            {
                if (_typeMap.PropertyMaps[i].DestinationProperty == destProperty)
                {
                    _typeMap.PropertyMaps.RemoveAt(i);
                    break;
                }
            }

            return this;
        }

        /// <summary>
        /// Maps a destination property from a specific source property.
        /// </summary>
        /// <typeparam name="TSourceProperty">The source property type.</typeparam>
        /// <typeparam name="TDestProperty">The destination property type.</typeparam>
        /// <param name="destinationMember">Expression to select the destination property.</param>
        /// <param name="sourceMember">Expression to select the source property.</param>
        /// <returns>The configuration object for method chaining.</returns>
        public TypeMapConfiguration<TSource, TDestination> MapFrom<TSourceProperty, TDestProperty>(Expression<Func<TDestination, TDestProperty>> destinationMember,
        Expression<Func<TSource, TSourceProperty>> sourceMember)
        {
            if (destinationMember == null)
            {
                throw new ArgumentNullException(nameof(destinationMember));
            }

            if (sourceMember == null)
            {
                throw new ArgumentNullException(nameof(sourceMember));
            }

            var destProperty = ExpressionHelper.GetPropertyInfo(destinationMember);
            var sourceProperty = ExpressionHelper.GetPropertyInfo(sourceMember);

            // Find existing property map or create new one
            PropertyMap propertyMap = null;
            foreach (var pm in _typeMap.PropertyMaps)
            {
                if (pm.DestinationProperty == destProperty)
                {
                    propertyMap = pm;
                    break;
                }
            }

            if (propertyMap == null)
            {
                propertyMap = new PropertyMap(sourceProperty, destProperty);
                _typeMap.PropertyMaps.Add(propertyMap);
            }
            else
            {
                // Update the source property using the proper internal method
                propertyMap.UpdateSourceProperty(sourceProperty);
            }

            return this;
        }
    }
}
