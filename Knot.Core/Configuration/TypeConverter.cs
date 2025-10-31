using System;

namespace Knot.Configuration
{
    /// <summary>
    /// Base class for custom type converters.
    /// </summary>
    /// <typeparam name="TSource">The source type.</typeparam>
    /// <typeparam name="TDestination">The destination type.</typeparam>
    public abstract class TypeConverter<TSource, TDestination> : TypeConverter
    {
        /// <summary>
        /// Gets the source type.
        /// </summary>
        public override Type SourceType => typeof(TSource);

        /// <summary>
        /// Gets the destination type.
        /// </summary>
        public override Type DestinationType => typeof(TDestination);

        /// <summary>
        /// Converts the source value to the destination type.
        /// </summary>
        /// <param name="source">The source value.</param>
        /// <returns>The converted destination value.</returns>
        public abstract TDestination Convert(TSource source);

        /// <summary>
        /// Converts the source value to the destination type.
        /// </summary>
        /// <param name="source">The source value.</param>
        /// <returns>The converted destination value.</returns>
        public override object Convert(object source)
        {
            if (source == null)
            {
                return default(TDestination);
            }

            if (!(source is TSource typedSource))
            {
                throw new ArgumentException($"Source must be of type {typeof(TSource).Name}", nameof(source));
            }

            return Convert(typedSource);
        }
    }

    /// <summary>
    /// Abstract base class for type converters.
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
        /// Converts the source value to the destination type.
        /// </summary>
        /// <param name="source">The source value.</param>
        /// <returns>The converted destination value.</returns>
        public abstract object Convert(object source);
    }
}
