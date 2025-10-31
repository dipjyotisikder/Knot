using Knot.Core.Configuration;
using Knot.Core.Exceptions;
using Knot.Core.Mapping;
using System;

namespace Knot.Core.Knot.Core
{
    /// <summary>
    /// The main mapping engine implementation.
    /// </summary>
    public class MapperEngine : IMapper, IMappingEngine
    {
        private readonly MappingRegistry _registry;

        /// <summary>
        /// Initializes a new instance of the MapperEngine class.
        /// </summary>
        /// <param name="registry">The mapping registry to use.</param>
        public MapperEngine(MappingRegistry registry)
        {
            _registry = registry ?? throw new ArgumentNullException(nameof(registry));
        }

        /// <summary>
        /// Gets the mapping registry containing all registered type mappings.
        /// </summary>
        public MappingRegistry Registry => _registry;

        /// <summary>
        /// Maps the source object to a new destination object.
        /// </summary>
        public TDestination Map<TDestination>(object source)
        {
            if (source == null)
            {
                return default;
            }

            var sourceType = source.GetType();
            var destinationType = typeof(TDestination);

            var context = new MappingContext(source, sourceType, destinationType);
            return (TDestination)Execute(context);
        }

        /// <summary>
        /// Maps the source object to a new destination object.
        /// </summary>
        public TDestination Map<TSource, TDestination>(TSource source)
        {
            if (source == null)
            {
                return default;
            }

            var context = new MappingContext(source, typeof(TSource), typeof(TDestination));
            return (TDestination)Execute(context);
        }

        /// <summary>
        /// Maps the source object to an existing destination object.
        /// </summary>
        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            if (source == null)
            {
                return destination;
            }

            var context = new MappingContext(source, typeof(TSource), typeof(TDestination), destination);
            return (TDestination)Execute(context);
        }

        /// <summary>
        /// Executes a mapping operation using the provided mapping context.
        /// </summary>
        public object Execute(MappingContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var typeMap = _registry.GetTypeMap(context.SourceType, context.DestinationType);
            if (typeMap == null)
            {
                throw new MappingException($"No mapping found from {context.SourceType.Name} to {context.DestinationType.Name}");
            }

            return typeMap.Execute(context);
        }
    }
}
