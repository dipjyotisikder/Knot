using Knot.Exceptions;
using Knot.Utilities;
using System;
using System.Reflection;

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
        public void Execute(object source, object destination)
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
            }
            else
            {
                return;
            }

            try
            {
                // Use compiled setter (20-50x faster than reflection)
                _compiledSetter(destination, value);
            }
            catch (ArgumentException ex)
            {
                throw new MappingException(
                    $"Failed to set property '{DestinationProperty.Name}'. " +
                    $"Type mismatch or conversion error.", ex);
            }
        }
    }
}
