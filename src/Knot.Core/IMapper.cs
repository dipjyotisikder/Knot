namespace Knot
{
    /// <summary>
    /// Maps objects from one type to another.
    /// </summary>
    public interface IMapper
    {
        /// <summary>
        /// Maps source to a new destination instance.
        /// </summary>
        TDestination Map<TDestination>(object source);

        /// <summary>
        /// Maps source to a new destination with explicit types.
        /// </summary>
        TDestination Map<TSource, TDestination>(TSource source);

        /// <summary>
        /// Maps source to an existing destination instance.
        /// </summary>
        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
    }
}
