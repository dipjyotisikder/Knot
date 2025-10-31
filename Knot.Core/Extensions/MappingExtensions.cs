using System;
using System.Collections.Generic;
using System.Linq;

namespace Knot.Extensions
{
    /// <summary>
    /// Extension methods for mapping operations.
    /// </summary>
    public static class MappingExtensions
    {
        /// <summary>
        /// Maps the source object to a new destination object.
        /// </summary>
        /// <typeparam name="TDestination">The destination type.</typeparam>
        /// <param name="source">The source object.</param>
        /// <param name="mapper">The mapper instance.</param>
        /// <returns>A new instance of the destination type.</returns>
        public static TDestination MapTo<TDestination>(this object source, IMapper mapper)
        {
            if (mapper == null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            return mapper.Map<TDestination>(source);
        }

        /// <summary>
        /// Maps the source object to an existing destination object.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <typeparam name="TDestination">The destination type.</typeparam>
        /// <param name="source">The source object.</param>
        /// <param name="destination">The destination object.</param>
        /// <param name="mapper">The mapper instance.</param>
        /// <returns>The destination object with mapped values.</returns>
        public static TDestination MapTo<TSource, TDestination>(this TSource source,
                                                                TDestination destination,
                                                                IMapper mapper)
        {
            if (mapper == null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            return mapper.Map(source, destination);
        }

        /// <summary>
        /// Maps a collection of source objects to a collection of destination objects.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <typeparam name="TDestination">The destination type.</typeparam>
        /// <param name="source">The source collection.</param>
        /// <param name="mapper">The mapper instance.</param>
        /// <returns>A collection of mapped destination objects.</returns>
        public static IEnumerable<TDestination> MapTo<TSource, TDestination>(this IEnumerable<TSource> source,
                                                                             IMapper mapper)
        {
            if (source == null)
            {
                return null;
            }

            if (mapper == null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            return source.Select(item => mapper.Map<TSource, TDestination>(item));
        }

        /// <summary>
        /// Maps a collection of source objects to a list of destination objects.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <typeparam name="TDestination">The destination type.</typeparam>
        /// <param name="source">The source collection.</param>
        /// <param name="mapper">The mapper instance.</param>
        /// <returns>A list of mapped destination objects.</returns>
        public static List<TDestination> MapToList<TSource, TDestination>(this IEnumerable<TSource> source,
                                                                          IMapper mapper)
        {
            if (source == null)
            {
                return null;
            }

            if (mapper == null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            return source.Select(item => mapper.Map<TSource, TDestination>(item)).ToList();
        }

        /// <summary>
        /// Maps a collection of source objects to an array of destination objects.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <typeparam name="TDestination">The destination type.</typeparam>
        /// <param name="source">The source collection.</param>
        /// <param name="mapper">The mapper instance.</param>
        /// <returns>An array of mapped destination objects.</returns>
        public static TDestination[] MapToArray<TSource, TDestination>(this IEnumerable<TSource> source,
                                                                       IMapper mapper)
        {
            if (source == null)
            {
                return null;
            }

            if (mapper == null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            return source.Select(item => mapper.Map<TSource, TDestination>(item)).ToArray();
        }
    }
}
