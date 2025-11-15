using System;

namespace Knot.Configuration
{
    /// <summary>
    /// Generic base class for typed converters.
    /// </summary>
    public abstract class TypeConverter<TSource, TDestination> : TypeConverter
    {
        /// <summary>
        /// Source type for this converter.
        /// </summary>
        public override Type SourceType => typeof(TSource);

        /// <summary>
        /// Destination type for this converter.
        /// </summary>
        public override Type DestinationType => typeof(TDestination);

        /// <summary>
        /// Converts a source value to the destination type.
        /// </summary>
        public abstract TDestination Convert(TSource source);

        /// <summary>
        /// Converts a source value to the destination type (untyped).
        /// </summary>
        public override object? Convert(object source)
        {
            if (source == null)
            {
                return default(TDestination)!;
            }

            if (!(source is TSource typedSource))
            {
                throw new ArgumentException($"Source must be of type {typeof(TSource).Name}", nameof(source));
            }

            return Convert(typedSource);
        }
    }

    /// <summary>
    /// Base class for custom type converters.
    /// </summary>
    public abstract class TypeConverter
    {
        /// <summary>
        /// Gets the source type.
        /// </summary>
        public abstract Type SourceType { get; }

        /// <summary>
        /// Gets the destination type.
        /// </summary>
        public abstract Type DestinationType { get; }

        /// <summary>
        /// Converts a source value to the destination type.
        /// </summary>
        public abstract object? Convert(object source);
    }
}
