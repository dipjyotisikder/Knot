namespace Knot
{
    /// <summary>
    /// Defines the contract for mapping objects between different types.
    /// </summary>
    public interface IMapper
    {
        /// <summary>
        /// Maps the source object to a new destination object.
        /// </summary>
        /// <typeparam name="TDestination">The type of the destination object.</typeparam>
        /// <param name="source">The source object to map from.</param>
        /// <returns>A new instance of the destination type with mapped values.</returns>
        TDestination Map<TDestination>(object source);

        /// <summary>
        /// Maps the source object to a new destination object.
        /// </summary>
        /// <typeparam name="TSource">The type of the source object.</typeparam>
        /// <typeparam name="TDestination">The type of the destination object.</typeparam>
        /// <param name="source">The source object to map from.</param>
        /// <returns>A new instance of the destination type with mapped values.</returns>
        TDestination Map<TSource, TDestination>(TSource source);

        /// <summary>
        /// Maps the source object to an existing destination object.
        /// </summary>
        /// <typeparam name="TSource">The type of the source object.</typeparam>
        /// <typeparam name="TDestination">The type of the destination object.</typeparam>
        /// <param name="source">The source object to map from.</param>
        /// <param name="destination">The destination object to map to.</param>
        /// <returns>The destination object with mapped values.</returns>
        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
    }
}
